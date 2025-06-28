using Microsoft.Data.SqlClient;
using QuestPDF.Infrastructure;
using Stu.Model;
using System.Data;
using System.IO;
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.IO;
using QuestPDF.Helpers;
using Stu.Model;
using System.Security.AccessControl;
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
            InitializeDataGridView();
        }
        private void InitializeDataGridView()
        {

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
                MessageBox.Show("لطفاً حداقل یکی از فیلدها را وارد یا انتخاب کنید", "خطا",
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
    s.Skill1,
    s.Skill2,
    s.Skill3,
    sg1.Name AS Skill1Name, 
    sg2.Name AS Skill2Name, 
    sg3.Name AS Skill3Name,
    eval1.Description AS Skill1Description,
    eval2.Description AS Skill2Description,
    eval3.Description AS Skill3Description
FROM 
    Student s
LEFT JOIN 
    SkillGroups sg1 ON s.Skill1 = sg1.SkillGroupId
LEFT JOIN 
    SkillGroups sg2 ON s.Skill2 = sg2.SkillGroupId
LEFT JOIN 
    SkillGroups sg3 ON s.Skill3 = sg3.SkillGroupId
LEFT JOIN (
    SELECT StudentId, SkillGroupId, Description
    FROM StudentSkillEvaluation e
    WHERE EvaluationDate = (
        SELECT MAX(EvaluationDate)
        FROM StudentSkillEvaluation
        WHERE StudentId = e.StudentId AND SkillGroupId = e.SkillGroupId AND SkillNumber = 1
    ) AND SkillNumber = 1
) eval1 ON s.StudentId = eval1.StudentId AND s.Skill1 = eval1.SkillGroupId
LEFT JOIN (
    SELECT StudentId, SkillGroupId, Description
    FROM StudentSkillEvaluation e
    WHERE EvaluationDate = (
        SELECT MAX(EvaluationDate)
        FROM StudentSkillEvaluation
        WHERE StudentId = e.StudentId AND SkillGroupId = e.SkillGroupId AND SkillNumber = 2
    ) AND SkillNumber = 2
) eval2 ON s.StudentId = eval2.StudentId AND s.Skill2 = eval2.SkillGroupId
LEFT JOIN (
    SELECT StudentId, SkillGroupId, Description
    FROM StudentSkillEvaluation e
    WHERE EvaluationDate = (
        SELECT MAX(EvaluationDate)
        FROM StudentSkillEvaluation
        WHERE StudentId = e.StudentId AND SkillGroupId = e.SkillGroupId AND SkillNumber = 3
    ) AND SkillNumber = 3
) eval3 ON s.StudentId = eval3.StudentId AND s.Skill3 = eval3.SkillGroupId
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
            dataGridView1.Columns["Skill1Name"].HeaderText = "کارگروه 1";
            dataGridView1.Columns["Skill2Name"].HeaderText = "کارگروه 2";
            dataGridView1.Columns["Skill3Name"].HeaderText = "کارگروه 3";
            if (dataGridView1.Columns.Contains("Skill1Description"))
                dataGridView1.Columns["Skill1Description"].Visible = false;
            if (dataGridView1.Columns.Contains("Skill2Description"))
                dataGridView1.Columns["Skill2Description"].Visible = false;
            if (dataGridView1.Columns.Contains("Skill3Description"))
                dataGridView1.Columns["Skill3Description"].Visible = false;
            dataGridView1.Columns["StudentId"].Visible = false;
            dataGridView1.Columns["Skill1"].Visible = false;
            dataGridView1.Columns["Skill2"].Visible = false;
            dataGridView1.Columns["Skill3"].Visible = false;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int studentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["StudentId"].Value);
                int? skillGroupId = null;
                int skillNumber = 0;

                if (e.ColumnIndex == dataGridView1.Columns["skillbutton1"]?.Index)
                {
                    skillGroupId = dataGridView1.Rows[e.RowIndex].Cells["Skill1"].Value != DBNull.Value
                        ? Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Skill1"].Value)
                        : (int?)null;
                    skillNumber = 1;
                }
                else if (e.ColumnIndex == dataGridView1.Columns["skillbutton2"]?.Index)
                {
                    skillGroupId = dataGridView1.Rows[e.RowIndex].Cells["Skill2"].Value != DBNull.Value
                        ? Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Skill2"].Value)
                        : (int?)null;
                    skillNumber = 2;
                }
                else if (e.ColumnIndex == dataGridView1.Columns["skillbutton3"]?.Index)
                {
                    skillGroupId = dataGridView1.Rows[e.RowIndex].Cells["Skill3"].Value != DBNull.Value
                        ? Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Skill3"].Value)
                        : (int?)null;
                    skillNumber = 3;
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Edit"]?.Index)
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
                if (skillGroupId.HasValue)
                {
                    try
                    {
                        frmSkill skillForm = new frmSkill(studentId, skillGroupId.Value, skillNumber);
                        skillForm.ShowDialog();
                        button1_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"خطا در باز کردن فرم ارزیابی:\n{ex.Message}", "خطا",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private List<SkillEvaluation> GetSkillEvaluations(int studentId, int skillGroupId, int skillNumber)
        {
            List<SkillEvaluation> evaluations = new List<SkillEvaluation>();
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"
            SELECT EvaluationDate, Aestimatio_peritiae1, Aestimatio_peritiae2, Aestimatio_peritiae3,
                   Aestimatio_peritiae4, Aestimatio_peritiae5, Aestimatio_peritiae6, 
                   Aestimatio_peritiae7, InstructorName
            FROM StudentSkillEvaluation
            WHERE StudentId = @StudentId AND SkillGroupId = @SkillGroupId AND SkillNumber = @SkillNumber";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    command.Parameters.AddWithValue("@SkillGroupId", skillGroupId);
                    command.Parameters.AddWithValue("@SkillNumber", skillNumber);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            evaluations.Add(new SkillEvaluation
                            {
                                EvaluationDate = reader.GetDateTime(0),
                                Aestimatio_peritiae1 = reader["Aestimatio_peritiae1"]?.ToString(),
                                Aestimatio_peritiae2 = reader["Aestimatio_peritiae2"]?.ToString(),
                                Aestimatio_peritiae3 = reader["Aestimatio_peritiae3"]?.ToString(),
                                Aestimatio_peritiae4 = reader["Aestimatio_peritiae4"]?.ToString(),
                                Aestimatio_peritiae5 = reader["Aestimatio_peritiae5"]?.ToString(),
                                Aestimatio_peritiae6 = reader["Aestimatio_peritiae6"]?.ToString(),
                                Aestimatio_peritiae7 = reader["Aestimatio_peritiae7"]?.ToString(),
                                InstructorName = reader["InstructorName"] != DBNull.Value ? reader["InstructorName"].ToString() : "نامشخص"
                            });
                        }
                    }
                }
            }
            return evaluations;
        }
        private void GenerateStudentPDF(int rowIndex)
        {
            try
            {
                var row = dataGridView1.Rows[rowIndex];
                int studentId = Convert.ToInt32(row.Cells["StudentId"].Value);
                string firstName = row.Cells["FirstName"].Value?.ToString() ?? "نامشخص";
                string lastName = row.Cells["LastName"].Value?.ToString() ?? "نامشخص";
                string schoolYear = row.Cells["SchoolYear"].Value?.ToString() ?? "نامشخص";
                string levelStudent = row.Cells["LevelStudent"].Value?.ToString() ?? "نامشخص";
                int?[] skillIds = new int?[3];
                string[] skillNames = new string[3];
                string[] skillDescriptions = new string[3];
                skillIds[0] = row.Cells["Skill1"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill1"].Value) : null;
                skillIds[1] = row.Cells["Skill2"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill2"].Value) : null;
                skillIds[2] = row.Cells["Skill3"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill3"].Value) : null;
                skillNames[0] = row.Cells["Skill1Name"].Value?.ToString() ?? "نامشخص";
                skillNames[1] = row.Cells["Skill2Name"].Value?.ToString() ?? "نامشخص";
                skillNames[2] = row.Cells["Skill3Name"].Value?.ToString() ?? "نامشخص";
                skillDescriptions[0] = row.Cells["Skill1Description"].Value?.ToString() ?? "بدون توضیحات";
                skillDescriptions[1] = row.Cells["Skill2Description"].Value?.ToString() ?? "بدون توضیحات";
                skillDescriptions[2] = row.Cells["Skill3Description"].Value?.ToString() ?? "بدون توضیحات";
                var skillInstructors = GetStudentSkillInstructors(studentId);

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "گزارش دانش‌آموزان");
                Directory.CreateDirectory(folderPath);
                string fileName = $"گزارش_تکی_{firstName}_{lastName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(folderPath, fileName);

                QuestPDF.Settings.License = LicenseType.Community;
                Document.Create(container =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!skillIds[i].HasValue) continue;
                        int skillNumber = i + 1;
                        var evaluations = GetSkillEvaluations(studentId, skillIds[i].Value, skillNumber);
                        var latestEvaluation = evaluations.OrderByDescending(e => e.EvaluationDate).FirstOrDefault();
                        string[] evaluationValues = latestEvaluation != null
                            ? new[] {
                        latestEvaluation.Aestimatio_peritiae1,
                        latestEvaluation.Aestimatio_peritiae2,
                        latestEvaluation.Aestimatio_peritiae3,
                        latestEvaluation.Aestimatio_peritiae4,
                        latestEvaluation.Aestimatio_peritiae5,
                        latestEvaluation.Aestimatio_peritiae6,
                        latestEvaluation.Aestimatio_peritiae7
                            }
                            : new string[7];
                        string instructorName = latestEvaluation?.InstructorName ?? "نامشخص";
                        string description = skillDescriptions[i];

                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(2, Unit.Centimetre);
                            page.PageColor(Colors.White);
                            page.ContentFromRightToLeft();
                            page.DefaultTextStyle(x => x.FontSize(14).FontFamily("B Nazanin").Bold());

                            page.Background()
                                .Border(1, Unit.Millimetre)
                                .BorderColor(Colors.Blue.Darken2);

                            page.Header()
                                .PaddingBottom(15)
                                .BorderBottom(1, Unit.Millimetre)
                                .BorderColor(Colors.Grey.Darken1)
                                .Background(Colors.Grey.Lighten4)
                                .Padding(10)
                                .Column(column =>
                                {
                                    column.Item().Text($"کارگروه: {skillNames[i]}")
                                        .FontSize(18).Bold().FontColor(Colors.Blue.Darken2).AlignCenter();
                                    column.Item().PaddingTop(5).Text($"نام دانش‌آموز: {firstName} {lastName}")
                                        .FontSize(16).ExtraBold();
                                    column.Item().Text($"پایه تحصیلی: {levelStudent}")
                                        .FontSize(15).ExtraBold();
                                    column.Item().Text($"سال ورود: {schoolYear}")
                                        .FontSize(15).ExtraBold();
                                    column.Item().Text($"نام مربی: {instructorName}")
                                        .FontSize(15).ExtraBold();
                                });

                            page.Content()
                                .PaddingVertical(1.5f, Unit.Centimetre)
                                .Border(0.5f, Unit.Millimetre)
                                .BorderColor(Colors.Grey.Darken2)
                                .Padding(10)
                                .Background(Colors.White)
                                .Column(column =>
                                {
                                    column.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(2.5f);
                                            columns.ConstantColumn(70);
                                            columns.ConstantColumn(60);
                                            columns.ConstantColumn(70);
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().Background(Colors.Blue.Lighten4).Padding(8).Text("معیار").FontSize(14).Bold();
                                            header.Cell().Background(Colors.Blue.Lighten4).Padding(8).Text("نیاز به رشد").FontSize(14).Bold();
                                            header.Cell().Background(Colors.Blue.Lighten4).Padding(8).Text("مطلوب").FontSize(14).Bold();
                                            header.Cell().Background(Colors.Blue.Lighten4).Padding(8).Text("رو به رشد").FontSize(14).Bold();
                                        });

                                        string[] criteria = new[]
                                        {
                                    "مسئولیت‌پذیری",
                                    "استفاده بهینه از ابزار",
                                    "مشارکت در کار گروهی",
                                    "درک مهارتی از فعالیت",
                                    "ثبات و تاب‌آوری در انجام فعالیت",
                                    "پویایی و نشاط در انجام فعالیت",
                                    "میزان شرکت در کارگروه"
                                        };

                                        for (int j = 0; j < criteria.Length; j++)
                                        {
                                            table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Text(criteria[j]).FontSize(13);
                                            table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Text(evaluationValues[j] == "نیاز به رشد" ? "✓" : "").FontSize(13).AlignCenter();
                                            table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Text(evaluationValues[j] == "مطلوب" ? "✓" : "").FontSize(13).AlignCenter();
                                            table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Text(evaluationValues[j] == "رو به رشد" ? "✓" : "").FontSize(13).AlignCenter();
                                        }
                                    });

                                    column.Item().PaddingTop(15).Text("توضیح عملکرد دانش‌آموز در کارگروه مربوطه:")
                                        .FontSize(15).Bold().FontColor(Colors.Black);
                                    column.Item().PaddingTop(8).Text(description)
                                        .FontSize(13).LineHeight(1.2f);
                                });

                            page.Footer()
                                .AlignCenter()
                                .Text(x =>
                                {
                                    x.Span("صفحه ").FontSize(12).Bold();
                                    x.CurrentPageNumber().FontSize(12).Bold();
                                    x.Span(" از ").FontSize(12).Bold();
                                    x.TotalPages().FontSize(12).Bold();
                                });
                        });
                    }
                }).GeneratePdf(filePath);

                if (File.Exists(filePath))
                {
                    MessageBox.Show($"فایل PDF با موفقیت در {filePath} ذخیره شد برای مشاهده روی فایل کلیک کنید", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"فایل PDF در {filePath} ذخیره شد اما قابل دسترسی نیست لطفاً مسیر را بررسی کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("دسترسی به مسیر ذخیره‌سازی وجود ندارد لطفاً دسترسی‌ها را بررسی کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"خطا در ذخیره فایل: فایل ممکن است در حال استفاده باشد یا مسیر نامعتبر است\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        string query = "DELETE FROM StudentSkillEvaluation WHERE StudentId = @StudentId";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@StudentId", studentId);
                            command.ExecuteNonQuery();
                        }
                    }
                    using (SqlConnection connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();
                        string query2 = "DELETE FROM Student WHERE StudentId = @StudentId";
                        using (SqlCommand command = new SqlCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@StudentId", studentId);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("دانش‌آموز با موفقیت حذف شد", "حذف موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطا در حذف دانش‌آموز:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public class SkillInstructor
        {
            public string SkillName { get; set; }
            public string InstructorName { get; set; }
        }
        public List<SkillInstructor> GetStudentSkillInstructors(int studentId)
        {
            var result = new List<SkillInstructor>();
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"
            SELECT 
                s.Skill1, s.Skill2, s.Skill3,
                sg1.Name AS Skill1Name, 
                sg2.Name AS Skill2Name, 
                sg3.Name AS Skill3Name,
                eval1.InstructorName AS Instructor1,
                eval2.InstructorName AS Instructor2,
                eval3.InstructorName AS Instructor3
            FROM Student s
            LEFT JOIN SkillGroups sg1 ON s.Skill1 = sg1.SkillGroupId
            LEFT JOIN SkillGroups sg2 ON s.Skill2 = sg2.SkillGroupId
            LEFT JOIN SkillGroups sg3 ON s.Skill3 = sg3.SkillGroupId
            LEFT JOIN (
                SELECT StudentId, SkillGroupId, InstructorName
                FROM StudentSkillEvaluation e
                WHERE EvaluationDate = (
                    SELECT MAX(EvaluationDate)
                    FROM StudentSkillEvaluation
                    WHERE StudentId = e.StudentId AND SkillGroupId = e.SkillGroupId AND SkillNumber = 1
                ) AND SkillNumber = 1
            ) eval1 ON s.StudentId = eval1.StudentId AND s.Skill1 = eval1.SkillGroupId
            LEFT JOIN (
                SELECT StudentId, SkillGroupId, InstructorName
                FROM StudentSkillEvaluation e
                WHERE EvaluationDate = (
                    SELECT MAX(EvaluationDate)
                    FROM StudentSkillEvaluation
                    WHERE StudentId = e.StudentId AND SkillGroupId = e.SkillGroupId AND SkillNumber = 2
                ) AND SkillNumber = 2
            ) eval2 ON s.StudentId = eval2.StudentId AND s.Skill2 = eval2.SkillGroupId
            LEFT JOIN (
                SELECT StudentId, SkillGroupId, InstructorName
                FROM StudentSkillEvaluation e
                WHERE EvaluationDate = (
                    SELECT MAX(EvaluationDate)
                    FROM StudentSkillEvaluation
                    WHERE StudentId = e.StudentId AND SkillGroupId = e.SkillGroupId AND SkillNumber = 3
                ) AND SkillNumber = 3
            ) eval3 ON s.StudentId = eval3.StudentId AND s.Skill3 = eval3.SkillGroupId
            WHERE s.StudentId = @StudentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.Add(new SkillInstructor
                            {
                                SkillName = reader["Skill1Name"]?.ToString() ?? "نامشخص",
                                InstructorName = reader["Instructor1"] != DBNull.Value ? reader["Instructor1"].ToString() : "نامشخص"
                            });
                            result.Add(new SkillInstructor
                            {
                                SkillName = reader["Skill2Name"]?.ToString() ?? "نامشخص",
                                InstructorName = reader["Instructor2"] != DBNull.Value ? reader["Instructor2"].ToString() : "نامشخص"
                            });
                            result.Add(new SkillInstructor
                            {
                                SkillName = reader["Skill3Name"]?.ToString() ?? "نامشخص",
                                InstructorName = reader["Instructor3"] != DBNull.Value ? reader["Instructor3"].ToString() : "نامشخص"
                            });
                        }
                    }
                }
            }
            return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            frmCreate createForm = new frmCreate();
            createForm.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.DataSource == null || dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("جدول خالی است لطفاً ابتدا داده‌ها را بارگذاری کنید", "خطا",
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
                        int studentId = Convert.ToInt32(row.Cells["StudentId"].Value);
                        string firstName = row.Cells["FirstName"].Value?.ToString() ?? "نامشخص";
                        string lastName = row.Cells["LastName"].Value?.ToString() ?? "نامشخص";
                        string schoolYear = row.Cells["SchoolYear"].Value?.ToString() ?? "نامشخص";
                        string levelStudent = row.Cells["LevelStudent"].Value?.ToString() ?? "نامشخص";
                        int?[] skillIds = new int?[3];
                        string[] skillNames = new string[3];
                        string[] skillDescriptions = new string[3];
                        skillIds[0] = row.Cells["Skill1"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill1"].Value) : null;
                        skillIds[1] = row.Cells["Skill2"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill2"].Value) : null;
                        skillIds[2] = row.Cells["Skill3"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill3"].Value) : null;
                        skillNames[0] = row.Cells["Skill1Name"].Value?.ToString() ?? "نامشخص";
                        skillNames[1] = row.Cells["Skill2Name"].Value?.ToString() ?? "نامشخص";
                        skillNames[2] = row.Cells["Skill3Name"].Value?.ToString() ?? "نامشخص";
                        skillDescriptions[0] = row.Cells["Skill1Description"].Value?.ToString() ?? "بدون توضیحات";
                        skillDescriptions[1] = row.Cells["Skill2Description"].Value?.ToString() ?? "بدون توضیحات";
                        skillDescriptions[2] = row.Cells["Skill3Description"].Value?.ToString() ?? "بدون توضیحات";
                        var skillInstructors = GetStudentSkillInstructors(studentId);

                        for (int i = 0; i < 3; i++)
                        {
                            if (!skillIds[i].HasValue) continue;
                            int skillNumber = i + 1;
                            var evaluations = GetSkillEvaluations(studentId, skillIds[i].Value, skillNumber);
                            var latestEvaluation = evaluations.OrderByDescending(e => e.EvaluationDate).FirstOrDefault();
                            string[] evaluationValues = latestEvaluation != null
                                ? new[] {
                            latestEvaluation.Aestimatio_peritiae1,
                            latestEvaluation.Aestimatio_peritiae2,
                            latestEvaluation.Aestimatio_peritiae3,
                            latestEvaluation.Aestimatio_peritiae4,
                            latestEvaluation.Aestimatio_peritiae5,
                            latestEvaluation.Aestimatio_peritiae6,
                            latestEvaluation.Aestimatio_peritiae7
                                }
                                : new string[7];
                            string instructorName = latestEvaluation?.InstructorName ?? "نامشخص";
                            string description = skillDescriptions[i];

                            container.Page(page =>
                            {
                                page.Size(PageSizes.A4);
                                page.Margin(2, Unit.Centimetre);
                                page.PageColor(Colors.White);
                                page.ContentFromRightToLeft();
                                page.DefaultTextStyle(x => x.FontSize(14).FontFamily("B Nazanin").Bold());
                                page.Background()
                                    .Border(1, Unit.Millimetre)
                                    .BorderColor(Colors.Blue.Darken2);

                                page.Header()
                                    .PaddingBottom(15)
                                    .BorderBottom(1, Unit.Millimetre)
                                    .BorderColor(Colors.Grey.Darken1)
                                    .Background(Colors.Grey.Lighten4)
                                    .Padding(10)
                                    .Column(column =>
                                    {
                                        column.Item().Text($"کارگروه: {skillNames[i]}")
                                            .FontSize(18).Bold().FontColor(Colors.Blue.Darken2).AlignCenter();
                                        column.Item().PaddingTop(5).Text($"نام دانش‌آموز: {firstName} {lastName}")
                                            .FontSize(16).ExtraBold();
                                        column.Item().Text($"پایه تحصیلی: {levelStudent}")
                                            .FontSize(15).ExtraBold();
                                        column.Item().Text($"سال ورود: {schoolYear}")
                                            .FontSize(15).ExtraBold();
                                        column.Item().Text($"نام مربی: {instructorName}")
                                            .FontSize(15).ExtraBold();
                                    });

                                page.Content()
                                    .PaddingVertical(1.5f, Unit.Centimetre)
                                    .Border(0.5f, Unit.Millimetre)
                                    .BorderColor(Colors.Grey.Darken2)
                                    .Padding(10)
                                    .Background(Colors.White)
                                    .Column(column =>
                                    {
                                        column.Item().Table(table =>
                                        {
                                            table.ColumnsDefinition(columns =>
                                            {
                                                columns.RelativeColumn(2.5f);
                                                columns.ConstantColumn(70);
                                                columns.ConstantColumn(60);
                                                columns.ConstantColumn(70);
                                            });

                                            table.Header(header =>
                                            {
                                                header.Cell().Background(Colors.Blue.Lighten4).Padding(8).Text("معیار").FontSize(14).Bold();
                                                header.Cell().Background(Colors.Blue.Lighten4).Padding(8).Text("نیاز به رشد").FontSize(14).Bold();
                                                header.Cell().Background(Colors.Blue.Lighten4).Padding(8).Text("مطلوب").FontSize(14).Bold();
                                                header.Cell().Background(Colors.Blue.Lighten4).Padding(8).Text("رو به رشد").FontSize(14).Bold();
                                            });

                                            string[] criteria = new[]
                                            {
                                        "مسئولیت‌پذیری",
                                        "استفاده بهینه از ابزار",
                                        "مشارکت در کار گروهی",
                                        "درک مهارتی از فعالیت",
                                        "ثبات و تاب‌آوری در انجام فعالیت",
                                        "پویایی و نشاط در انجام فعالیت",
                                        "میزان شرکت در کارگروه"
                                            };

                                            for (int j = 0; j < criteria.Length; j++)
                                            {
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Text(criteria[j]).FontSize(13);
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Text(evaluationValues[j] == "نیاز به رشد" ? "✓" : "").FontSize(13).AlignCenter();
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Text(evaluationValues[j] == "مطلوب" ? "✓" : "").FontSize(13).AlignCenter();
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Text(evaluationValues[j] == "رو به رشد" ? "✓" : "").FontSize(13).AlignCenter();
                                            }
                                        });

                                        column.Item().PaddingTop(15).Text("توضیح عملکرد دانش‌آموز در کارگروه مربوطه:")
                                            .FontSize(15).Bold().FontColor(Colors.Black);
                                        column.Item().PaddingTop(8).Text(description)
                                            .FontSize(13).LineHeight(1.2f);
                                    });

                                page.Footer()
                                    .AlignCenter()
                                    .Text(x =>
                                    {
                                        x.Span("صفحه ").FontSize(12).Bold();
                                        x.CurrentPageNumber().FontSize(12).Bold();
                                        x.Span(" از ").FontSize(12).Bold();
                                        x.TotalPages().FontSize(12).Bold();
                                    });
                            });
                        }
                    }
                }).GeneratePdf(filePath);

                MessageBox.Show($"فایل PDF با موفقیت در {filePath} ذخیره شد", "موفقیت",
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                frmInstructor newfrm = new frmInstructor();
                newfrm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بستن فرم \n {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }
    }
}