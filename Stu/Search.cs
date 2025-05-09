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
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
            // تنظیمات اولیه DataGridView
            dataGridView1.RightToLeft = RightToLeft.Yes;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void Search_Load(object sender, EventArgs e)
        {
            // مقداردهی اولیه در صورت نیاز
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // دریافت مقادیر ورودی از کاربر
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            string entranceYearText = comboBox1.SelectedItem?.ToString();
            string levelStudent = comboBox2.SelectedItem?.ToString();

            // بررسی خالی نبودن حداقل یکی از فیلدها
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) &&
                string.IsNullOrEmpty(entranceYearText) && string.IsNullOrEmpty(levelStudent))
            {
                MessageBox.Show("لطفاً حداقل یکی از فیلدها را وارد یا انتخاب کنید.", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int? entranceYear = null;
                if (!string.IsNullOrEmpty(entranceYearText) && int.TryParse(entranceYearText, out int year))
                {
                    entranceYear = year;
                }

                // جستجوی دانش‌آموزان
                DataTable result = SearchStudents(firstName, lastName, entranceYear, levelStudent);

                if (result.Rows.Count > 0)
                {
                    dataGridView1.DataSource = result;
                    dataGridView1.Visible = true;
                    SetGridHeaders();
                    // افزودن ستون دکمه PDF
                    AddPdfButtonColumn();
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

        private DataTable SearchStudents(string firstName, string lastName, int? entranceYear, string levelStudent)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                // پرس‌وجوی جستجو
                string query = @"
                    SELECT 
                        s.StudentId, 
                        s.FirstName, 
                        s.LastName, 
                        s.SchoolYear,
                        s.LevelStudent,
                        sg1.Name AS Skill1, 
                        sg2.Name AS Skill2, 
                        sg3.Name AS Skill3
                    FROM 
                        Student s
                    LEFT JOIN 
                        SkillGroups sg1 ON s.Skill1 = sg1.SkillGroupId
                    LEFT JOIN 
                        SkillGroups sg2 ON s.Skill2 = sg2.SkillGroupId
                    LEFT JOIN 
                        SkillGroups sg3 ON s.Skill3 = sg3.SkillGroupId
                    WHERE 
                        (@FirstName = '' OR s.FirstName LIKE '%' + @FirstName + '%')
                        AND (@LastName = '' OR s.LastName LIKE '%' + @LastName + '%')
                        AND (@EntranceYear IS NULL OR s.SchoolYear = @EntranceYear)
                        AND (@LevelStudent = '' OR s.LevelStudent = @LevelStudent)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(firstName) ? "" : firstName);
                    command.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(lastName) ? "" : lastName);
                    command.Parameters.AddWithValue("@EntranceYear", entranceYear.HasValue ? (object)entranceYear.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@LevelStudent", string.IsNullOrEmpty(levelStudent) ? "" : levelStudent);

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
            // تنظیم عنوان ستون‌ها به فارسی
            dataGridView1.Columns["StudentId"].HeaderText = "کد دانش‌آموز";
            dataGridView1.Columns["FirstName"].HeaderText = "نام";
            dataGridView1.Columns["LastName"].HeaderText = "نام خانوادگی";
            dataGridView1.Columns["SchoolYear"].HeaderText = "سال ورود";
            dataGridView1.Columns["LevelStudent"].HeaderText = "پایه تحصیلی";
            dataGridView1.Columns["Skill1"].HeaderText = "مهارت ۱";
            dataGridView1.Columns["Skill2"].HeaderText = "مهارت ۲";
            dataGridView1.Columns["Skill3"].HeaderText = "مهارت ۳";
            dataGridView1.Columns["StudentId"].Visible = false;
        }

        private void AddPdfButtonColumn()
        {
            // افزودن ستون دکمه PDF اگر وجود نداشته باشد
            if (!dataGridView1.Columns.Contains("PDF"))
            {
                DataGridViewButtonColumn pdfColumn = new DataGridViewButtonColumn
                {
                    Name = "PDF",
                    HeaderText = "ایجاد PDF",
                    Text = "PDF",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(pdfColumn);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // تولید گزارش کلی برای تمام دانش‌آموزان
            try
            {
                // بررسی وجود داده در DataGridView
                if (dataGridView1.DataSource == null || dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("جدول خالی است. لطفاً ابتدا داده‌ها را بارگذاری کنید.", "خطا",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // تنظیم مسیر ذخیره‌سازی
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "گزارش‌گیری کلی");
                Directory.CreateDirectory(folderPath);
                string fileName = $"گزارش_کلی_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(folderPath, fileName);

                // تنظیمات QuestPDF
                QuestPDF.Settings.License = LicenseType.Community;

                // ایجاد سند PDF
                Document.Create(container =>
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        // دریافت اطلاعات دانش‌آموز
                        string firstName = row.Cells["FirstName"].Value?.ToString() ?? "نامشخص";
                        string lastName = row.Cells["LastName"].Value?.ToString() ?? "نامشخص";
                        string schoolYear = row.Cells["SchoolYear"].Value?.ToString() ?? "نامشخص";
                        string levelStudent = row.Cells["LevelStudent"].Value?.ToString() ?? "نامشخص";
                        string skill1 = row.Cells["Skill1"].Value?.ToString() ?? "نامشخص";
                        string skill2 = row.Cells["Skill2"].Value?.ToString() ?? "نامشخص";
                        string skill3 = row.Cells["Skill3"].Value?.ToString() ?? "نامشخص";

                        // ایجاد صفحه جدید برای هر دانش‌آموز
                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(2, Unit.Centimetre);
                            page.PageColor(Colors.White);
                            page.ContentFromRightToLeft();
                            page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                            // سربرگ
                            page.Header()
                                .PaddingBottom(10)
                                .Text("گزارش کلی دانش‌آموزان")
                                .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium)
                                .AlignCenter();

                            // محتوای صفحه
                            page.Content()
                                .PaddingVertical(1, Unit.Centimetre)
                                .Column(column =>
                                {
                                    // نمایش نام و نام خانوادگی بالای جدول
                                    column.Item()
                                        .PaddingVertical(10)
                                        .Text($"دانش‌آموز: {firstName} {lastName}")
                                        .FontSize(14).Bold();

                                    // ایجاد جدول
                                    column.Item()
                                        .PaddingBottom(10)
                                        .Table(table =>
                                        {
                                            table.ColumnsDefinition(columns =>
                                            {
                                                columns.ConstantColumn(150);
                                                columns.RelativeColumn();
                                            });

                                            table.Header(header =>
                                            {
                                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("عنوان").Bold();
                                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("مقدار").Bold();
                                            });

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
                }).GeneratePdf(filePath);

                MessageBox.Show($"فایل PDF با موفقیت در {filePath} ذخیره شد.", "موفقیت",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ایجاد فایل PDF:\n{ex.Message}", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // بررسی کلیک روی ستون PDF
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["PDF"]?.Index)
            {
                try
                {
                    // دریافت اطلاعات دانش‌آموز از ردیف انتخاب‌شده
                    var row = dataGridView1.Rows[e.RowIndex];
                    string firstName = row.Cells["FirstName"].Value?.ToString() ?? "نامشخص";
                    string lastName = row.Cells["LastName"].Value?.ToString() ?? "نامشخص";
                    string schoolYear = row.Cells["SchoolYear"].Value?.ToString() ?? "نامشخص";
                    string levelStudent = row.Cells["LevelStudent"].Value?.ToString() ?? "نامشخص";
                    string skill1 = row.Cells["Skill1"].Value?.ToString() ?? "نامشخص";
                    string skill2 = row.Cells["Skill2"].Value?.ToString() ?? "نامشخص";
                    string skill3 = row.Cells["Skill3"].Value?.ToString() ?? "نامشخص";

                    // تنظیم مسیر ذخیره‌سازی
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string folderPath = Path.Combine(desktopPath, "گزارش دانش‌آموزان");
                    Directory.CreateDirectory(folderPath);
                    string fileName = $"{firstName}_{lastName}.pdf";
                    string filePath = Path.Combine(folderPath, fileName);

                    // تنظیمات QuestPDF
                    QuestPDF.Settings.License = LicenseType.Community;

                    // ایجاد سند PDF
                    Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(2, Unit.Centimetre);
                            page.PageColor(Colors.White);
                            page.ContentFromRightToLeft();
                            page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                            // سربرگ
                            page.Header()
                                .PaddingBottom(10)
                                .Text("اطلاعات و توصیف عملکرد دانش آموز")
                                .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium)
                                .AlignCenter();

                            // محتوای صفحه
                            page.Content()
                                .PaddingVertical(1, Unit.Centimetre)
                                .Column(column =>
                                {
                                    // نمایش نام و نام خانوادگی بالای جدول
                                    column.Item()
                                        .PaddingVertical(10)
                                        .Text($"دانش‌آموز: {firstName} {lastName}")
                                        .FontSize(14).Bold();

                                    // ایجاد جدول
                                    column.Item()
                                        .PaddingBottom(10)
                                        .Table(table =>
                                        {
                                            table.ColumnsDefinition(columns =>
                                            {
                                                columns.ConstantColumn(150);
                                                columns.RelativeColumn();
                                            });

                                            table.Header(header =>
                                            {
                                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("عنوان").Bold();
                                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("مقدار").Bold();
                                            });

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
                    }).GeneratePdf(filePath);

                    MessageBox.Show($"فایل PDF با موفقیت در {filePath} ذخیره شد.", "موفقیت",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطا در ایجاد فایل PDF:\n{ex.Message}", "خطا",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       
    }
}