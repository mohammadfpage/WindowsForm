using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.IO;
using QuestPDF.Helpers;

namespace Stu
{
    public partial class CreateUser : Form
    {
        public CreateUser()
        {
            InitializeComponent();
            dataGridView1.RightToLeft = RightToLeft.Yes;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            string entranceYearText = comboBox1.SelectedItem?.ToString();
            string levelStudent = comboBox2.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) &&
                string.IsNullOrEmpty(entranceYearText) && string.IsNullOrEmpty(levelStudent))
            {
                MessageBox.Show("لطفاً حداقل یکی از فیلدها را وارد یا انتخاب کنید.", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable result = SearchStudents(firstName, lastName, entranceYearText, levelStudent);

                if (result.Rows.Count > 0)
                {
                    dataGridView1.DataSource = result;
                    dataGridView1.Visible = true;
                    SetGridHeaders();
                }
                else
                {
                    dataGridView1.Visible = false;
                    MessageBox.Show("دانش‌آموزی با مشخصات وارد شده یافت نشد.", "نتیجه جستجو",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"یک خطا رخ داد:\n{ex.Message}", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable SearchStudents(string firstName, string lastName, string entranceYear, string levelStudent)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT 
                        s.StudentId, 
                        s.FirstName, 
                        s.LastName, 
                        s.SchoolYear,
                        s.LevelStudent,
                        sg1.Name AS Skill1, 
                        sg2.Name AS Skill2, 
                        sg3.Name AS Skill3,
                        s.Description1,
                        s.Description2,
                        s.Description3
                    FROM 
                        Student s
                    LEFT JOIN 
                        SkillGroups sg1 ON s.Skill1 = sg1.SkillGroupId
                    LEFT JOIN 
                        SkillGroups sg2 ON s.Skill2 = sg2.SkillGroupId
                    LEFT JOIN 
                        SkillGroups sg3 ON s.Skill3 = sg3.SkillGroupId
                    WHERE 
                        (ISNULL(@FirstName, '') = '' OR s.FirstName LIKE '%' + @FirstName + '%')
                        AND (ISNULL(@LastName, '') = '' OR s.LastName LIKE '%' + @LastName + '%')
                        AND (ISNULL(@EntranceYear, '') = '' OR s.SchoolYear = @EntranceYear)
                        AND (ISNULL(@LevelStudent, '') = '' OR s.LevelStudent = @LevelStudent)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(firstName) ? DBNull.Value : (object)firstName);
                    command.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(lastName) ? DBNull.Value : (object)lastName);
                    command.Parameters.AddWithValue("@EntranceYear", string.IsNullOrEmpty(entranceYear) ? DBNull.Value : (object)entranceYear);
                    command.Parameters.AddWithValue("@LevelStudent", string.IsNullOrEmpty(levelStudent) ? DBNull.Value : (object)levelStudent);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        private void SetGridHeaders()
        {
            dataGridView1.Columns["StudentId"].HeaderText = "کد دانش‌آموز";
            dataGridView1.Columns["FirstName"].HeaderText = "نام";
            dataGridView1.Columns["LastName"].HeaderText = "نام خانوادگی";
            dataGridView1.Columns["SchoolYear"].HeaderText = "سال ورود";
            dataGridView1.Columns["LevelStudent"].HeaderText = "پایه تحصیلی";
            dataGridView1.Columns["Skill1"].HeaderText = "مهارت 1";
            dataGridView1.Columns["Skill2"].HeaderText = "مهارت 2";
            dataGridView1.Columns["Skill3"].HeaderText = "مهارت 3";

            dataGridView1.Columns["Description1"].Visible = false;
            dataGridView1.Columns["Description2"].Visible = false;
            dataGridView1.Columns["Description3"].Visible = false;
            dataGridView1.Columns["StudentId"].Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int studentId = (int)dataGridView1.Rows[e.RowIndex].Cells["StudentId"].Value;

                if (e.ColumnIndex == dataGridView1.Columns["Edit"]?.Index)
                {
                    EditStudent(studentId);
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Delete"]?.Index)
                {
                    DeleteStudent(studentId);
                }
                else if (e.ColumnIndex == dataGridView1.Columns["PDF"]?.Index)
                {
                    GenerateStudentPDF(e.RowIndex);
                }
            }
        }

        private void GenerateStudentPDF(int rowIndex)
        {
            try
            {
                // دریافت اطلاعات دانش‌آموز از ردیف انتخاب‌شده
                var row = dataGridView1.Rows[rowIndex];
                string firstName = row.Cells["FirstName"].Value?.ToString() ?? "نامشخص";
                string lastName = row.Cells["LastName"].Value?.ToString() ?? "نامشخص";
                string schoolYear = row.Cells["SchoolYear"].Value?.ToString() ?? "نامشخص";
                string levelStudent = row.Cells["LevelStudent"].Value?.ToString() ?? "نامشخص";
                string skill1 = row.Cells["Skill1"].Value?.ToString() ?? "نامشخص";
                string skill2 = row.Cells["Skill2"].Value?.ToString() ?? "نامشخص";
                string skill3 = row.Cells["Skill3"].Value?.ToString() ?? "نامشخص";
                string description1 = row.Cells["Description1"].Value?.ToString() ?? "نامشخص";
                string description2 = row.Cells["Description2"].Value?.ToString() ?? "نامشخص";
                string description3 = row.Cells["Description3"].Value?.ToString() ?? "نامشخص";

                // تنظیم مسیر پوشه و نام فایل PDF
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "گزارش دانش‌آموزان");
                Directory.CreateDirectory(folderPath); // ایجاد پوشه اگر وجود نداشته باشد
                string fileName = $"{firstName}_{lastName}.pdf";
                string filePath = Path.Combine(folderPath, fileName);

                // تنظیمات QuestPDF
                QuestPDF.Settings.License = LicenseType.Community; // برای استفاده رایگان

                // ایجاد سند PDF
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.ContentFromRightToLeft(); // تنظیم content direction به RTL
                        page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial")); // فونت Arial برای فارسی

                        // سربرگ
                        page.Header()
                            .PaddingBottom(10)
                            .Text("گزارش اطلاعات دانش‌آموز")
                            .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium)
                            .AlignCenter();

                        // محتوای اصلی (جدول)
                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(150); // ستون عنوان
                                    columns.RelativeColumn();   // ستون مقدار
                                });

                                // تنظیمات جدول
                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("عنوان").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("مقدار").Bold();
                                });

                                // اضافه کردن ردیف‌ها
                                void AddRow(string label, string value)
                                {
                                    table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(5).Text(label);
                                    table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(5).Text(value);
                                }

                                AddRow("نام", firstName);
                                AddRow("نام خانوادگی", lastName);
                                AddRow("سال ورود", schoolYear);
                                AddRow("پایه تحصیلی", levelStudent);
                                AddRow("مهارت ۱", skill1);
                                AddRow("مهارت ۲", skill2);
                                AddRow("مهارت ۳", skill3);
                                AddRow("توضیحات ۱", description1);
                                AddRow("توضیحات ۲", description2);
                                AddRow("توضیحات ۳", description3);
                            });

                        // پاورقی
                        page.Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.Span("صفحه ");
                                x.CurrentPageNumber();
                                x.Span(" از ");
                                x.TotalPages();
                            });
                    });
                }).GeneratePdf(filePath);

                MessageBox.Show($"فایل PDF با موفقیت در {filePath} ذخیره شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ایجاد فایل PDF:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditStudent(int studentId)
        {
            frmEdit editForm = new frmEdit(studentId);
            editForm.ShowDialog();
            button1_Click(null, null);
        }

        private void DeleteStudent(int studentId)
        {
            var result = MessageBox.Show("آیا از حذف این دانش‌آموز مطمئن هستید؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM Student WHERE StudentId = @StudentId";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@StudentId", studentId);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("دانش‌آموز با موفقیت حذف شد.", "حذف موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطا در حذف دانش‌آموز:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCreate createForm = new frmCreate();
            createForm.ShowDialog();
            button1_Click(null, null);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.DataSource == null || dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("جدول خالی است. لطفاً ابتدا داده‌ها را بارگذاری کنید.", "خطا",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "گزارش‌گیری کلی");
                Directory.CreateDirectory(folderPath);
                string fileName = $"گزارش_کلی_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(folderPath, fileName);


                QuestPDF.Settings.License = LicenseType.Community;


                Document.Create(container =>
                {

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;


                        string firstName = row.Cells["FirstName"].Value?.ToString() ?? "نامشخص";
                        string lastName = row.Cells["LastName"].Value?.ToString() ?? "نامشخص";
                        string schoolYear = row.Cells["SchoolYear"].Value?.ToString() ?? "نامشخص";
                        string levelStudent = row.Cells["LevelStudent"].Value?.ToString() ?? "نامشخص";
                        string skill1 = row.Cells["Skill1"].Value?.ToString() ?? "نامشخص";
                        string skill2 = row.Cells["Skill2"].Value?.ToString() ?? "نامشخص";
                        string skill3 = row.Cells["Skill3"].Value?.ToString() ?? "نامشخص";
                        string description1 = row.Cells["Description1"].Value?.ToString() ?? "نامشخص";
                        string description2 = row.Cells["Description2"].Value?.ToString() ?? "نامشخص";
                        string description3 = row.Cells["Description3"].Value?.ToString() ?? "نامشخص";


                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(2, Unit.Centimetre);
                            page.PageColor(Colors.White);
                            page.ContentFromRightToLeft();
                            page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                            page.Header()
                                .PaddingBottom(10)
                                .Text("اطلاعات و توصیف عملکرد دانش آموز در کارگروه")
                                .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium)
                                .AlignCenter();

                            page.Content()
                                .PaddingVertical(1, Unit.Centimetre)
                                .Column(column =>
                                {
                                    // نمایش نام و نام خانوادگی بالای جدول
                                    column.Item()
                                        .PaddingVertical(10)
                                        .Text($"دانش‌آموز: {firstName} {lastName}")
                                        .FontSize(14).Bold();

                                    // ایجاد جدول برای اطلاعات دانش‌آموز
                                    column.Item()
                                        .PaddingBottom(10)
                                        .Table(table =>
                                        {
                                            table.ColumnsDefinition(columns =>
                                            {
                                                columns.ConstantColumn(150); // ستون عنوان
                                                columns.RelativeColumn();   // ستون مقدار
                                            });

                                            // سرستون‌های جدول
                                            table.Header(header =>
                                            {
                                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("عنوان").Bold();
                                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("مقدار").Bold();
                                            });

                                            // تابع کمکی برای اضافه کردن ردیف به جدول
                                            void AddRow(string label, string value)
                                            {
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(5).Text(label);
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(5).Text(value);
                                            }

                                            // اضافه کردن ردیف‌های اطلاعات )
                                            AddRow("نام", firstName);
                                            AddRow("نام خانوادگی", lastName);
                                            AddRow("سال ورود", schoolYear);
                                            AddRow("پایه تحصیلی", levelStudent);
                                            AddRow("کارگروه 1", skill1);
                                            AddRow("کارگروه 2", skill2);
                                            AddRow("کارگروه 3", skill3);
                                            AddRow("توصیف عملکرد کارگروه 1", description1);
                                            AddRow("توصیف عملکرد کارگروه 2", description2);
                                            AddRow("توصیف عملکرد کارگروه 3", description3);
                                        });
                                });

                            // پاورقی
                            page.Footer()
                                .AlignCenter()
                                .Text(x =>
                                {
                                    x.Span("صفحه ");
                                    x.CurrentPageNumber();
                                    x.Span(" از ");
                                    x.TotalPages();
                                });
                        });
                    }
                }).GeneratePdf(filePath); // ذخیره فایل PDF در مسیر مشخص‌شده

                MessageBox.Show($"فایل PDF با موفقیت در {filePath} ذخیره شد.", "موفقیت",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ایجاد فایل PDF:\n{ex.Message}", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                frmEditInfo NextForm = new frmEditInfo();
                NextForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری فرم جدید \n {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بستن فرم \n {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}