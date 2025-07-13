using Microsoft.Data.SqlClient;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Stu.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Stu
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
            ConfigureDataGridView();
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.RightToLeft = RightToLeft.Yes;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (!dataGridView1.Columns.Contains("PDF"))
            {
                DataGridViewButtonColumn pdfColumn = new DataGridViewButtonColumn
                {
                    Name = "PDF",
                    HeaderText = "ایجاد گزارش",
                    Text = "PDF",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(pdfColumn);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
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
                    SetGridHeaders();
                    dataGridView1.Visible = true;
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
                MessageBox.Show($"خطا در جستجو: {ex.Message}\n{ex.StackTrace}", "خطا",
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
    eval1.InstructorName AS Skill1Instructor,
    eval2.InstructorName AS Skill2Instructor,
    eval3.InstructorName AS Skill3Instructor,
    eval1.Description AS Skill1Description,
    eval2.Description AS Skill2Description,
    eval3.Description AS Skill3Description
FROM Student s
LEFT JOIN SkillGroups sg1 ON s.Skill1 = sg1.SkillGroupId
LEFT JOIN SkillGroups sg2 ON s.Skill2 = sg2.SkillGroupId
LEFT JOIN SkillGroups sg3 ON s.Skill3 = sg3.SkillGroupId
LEFT JOIN (
    SELECT StudentId, SkillGroupId, InstructorName, Description
    FROM StudentSkillEvaluation e
    WHERE EvaluationDate = (
        SELECT MAX(EvaluationDate)
        FROM StudentSkillEvaluation
        WHERE StudentId = e.StudentId AND SkillGroupId = e.SkillGroupId AND SkillNumber = 1
    ) AND SkillNumber = 1
) eval1 ON s.StudentId = eval1.StudentId AND s.Skill1 = eval1.SkillGroupId
LEFT JOIN (
    SELECT StudentId, SkillGroupId, InstructorName, Description
    FROM StudentSkillEvaluation e
    WHERE EvaluationDate = (
        SELECT MAX(EvaluationDate)
        FROM StudentSkillEvaluation
        WHERE StudentId = e.StudentId AND SkillGroupId = e.SkillGroupId AND SkillNumber = 2
    ) AND SkillNumber = 2
) eval2 ON s.StudentId = eval2.StudentId AND s.Skill2 = eval2.SkillGroupId
LEFT JOIN (
    SELECT StudentId, SkillGroupId, InstructorName, Description
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
            if (dataGridView1.Columns.Contains("StudentId")) dataGridView1.Columns["StudentId"].HeaderText = "کد دانش‌آموز";
            if (dataGridView1.Columns.Contains("FirstName")) dataGridView1.Columns["FirstName"].HeaderText = "نام";
            if (dataGridView1.Columns.Contains("LastName")) dataGridView1.Columns["LastName"].HeaderText = "نام خانوادگی";
            if (dataGridView1.Columns.Contains("SchoolYear")) dataGridView1.Columns["SchoolYear"].HeaderText = "سال ورود";
            if (dataGridView1.Columns.Contains("LevelStudent")) dataGridView1.Columns["LevelStudent"].HeaderText = "پایه تحصیلی";
            if (dataGridView1.Columns.Contains("Skill1Name")) dataGridView1.Columns["Skill1Name"].HeaderText = "کارگروه 1";
            if (dataGridView1.Columns.Contains("Skill2Name")) dataGridView1.Columns["Skill2Name"].HeaderText = "کارگروه 2";
            if (dataGridView1.Columns.Contains("Skill3Name")) dataGridView1.Columns["Skill3Name"].HeaderText = "کارگروه 3";
            if (dataGridView1.Columns.Contains("Skill1Instructor")) dataGridView1.Columns["Skill1Instructor"].Visible = false;
            if (dataGridView1.Columns.Contains("Skill2Instructor")) dataGridView1.Columns["Skill2Instructor"].Visible = false;
            if (dataGridView1.Columns.Contains("Skill3Instructor")) dataGridView1.Columns["Skill3Instructor"].Visible = false;
            if (dataGridView1.Columns.Contains("Skill1Description")) dataGridView1.Columns["Skill1Description"].Visible = false;
            if (dataGridView1.Columns.Contains("Skill2Description")) dataGridView1.Columns["Skill2Description"].Visible = false;
            if (dataGridView1.Columns.Contains("Skill3Description")) dataGridView1.Columns["Skill3Description"].Visible = false;

            var hiddenColumns = new[] { "StudentId", "Skill1", "Skill2", "Skill3" };
            foreach (var colName in hiddenColumns)
            {
                if (dataGridView1.Columns.Contains(colName))
                {
                    dataGridView1.Columns[colName].Visible = false;
                }
            }
        }
        string FixYearOrder(string input)
        {
            var parts = input.Split('-');

            if (parts.Length == 2)
            {
                var from = parts[0].Trim();
                var to = parts[1].Trim();

                if (int.TryParse(from, out int fromYear) && int.TryParse(to, out int toYear))
                {
                    if (fromYear > toYear)
                        return $"{from} - {to}"; 
                }

                return $"{to} - {from}";
            }

            return input;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "PDF")
            {
                GenerateStudentReport(e.RowIndex);
            }
        }

        private void GenerateStudentReport(int rowIndex)
        {
            try
            {
                var row = dataGridView1.Rows[rowIndex];
                if (row.Cells["StudentId"].Value == null)
                {
                    MessageBox.Show("شناسه دانش‌آموز نامعتبر است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int studentId = Convert.ToInt32(row.Cells["StudentId"].Value);
                string firstName = row.Cells["FirstName"].Value?.ToString() ?? "نامشخص";
                string lastName = row.Cells["LastName"].Value?.ToString() ?? "نامشخص";
                string schoolYear = row.Cells["SchoolYear"].Value?.ToString() ?? "نامشخص";
                string levelStudent = row.Cells["LevelStudent"].Value?.ToString() ?? "نامشخص";
                int?[] skillIds = new int?[3];
                string[] skillNames = new string[3];
                string[] skillInstructors = new string[3];
                string[] skillDescriptions = new string[3];
                skillIds[0] = row.Cells["Skill1"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill1"].Value) : null;
                skillIds[1] = row.Cells["Skill2"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill2"].Value) : null;
                skillIds[2] = row.Cells["Skill3"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill3"].Value) : null;
                skillNames[0] = row.Cells["Skill1Name"].Value?.ToString() ?? "نامشخص";
                skillNames[1] = row.Cells["Skill2Name"].Value?.ToString() ?? "نامشخص";
                skillNames[2] = row.Cells["Skill3Name"].Value?.ToString() ?? "نامشخص";
                skillInstructors[0] = row.Cells["Skill1Instructor"].Value?.ToString() ?? "نامشخص";
                skillInstructors[1] = row.Cells["Skill2Instructor"].Value?.ToString() ?? "نامشخص";
                skillInstructors[2] = row.Cells["Skill3Instructor"].Value?.ToString() ?? "نامشخص";
                skillDescriptions[0] = row.Cells["Skill1Description"].Value?.ToString() ?? "بدون توضیحات";
                skillDescriptions[1] = row.Cells["Skill2Description"].Value?.ToString() ?? "بدون توضیحات";
                skillDescriptions[2] = row.Cells["Skill3Description"].Value?.ToString() ?? "بدون توضیحات";

                var skillEvaluations = GetStudentSkillEvaluations(studentId);

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "گزارش تکی");
                Directory.CreateDirectory(folderPath);
                string fileName = $"گزارش_{firstName}_{lastName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(folderPath, fileName);
                QuestPDF.Settings.License = LicenseType.Community;
                Document.Create(container =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!skillIds[i].HasValue) continue;
                        int skillNumber = i + 1;
                        var evaluations = skillEvaluations.FirstOrDefault(e => e.SkillNumber == skillNumber);
                        string[] evaluationValues = evaluations != null
                            ? new[]
                            {
                                evaluations.Aestimatio_peritiae1,
                                evaluations.Aestimatio_peritiae2,
                                evaluations.Aestimatio_peritiae3,
                                evaluations.Aestimatio_peritiae4,
                                evaluations.Aestimatio_peritiae5,
                                evaluations.Aestimatio_peritiae6,
                                evaluations.Aestimatio_peritiae7
                            }
                            : new string[7];
                        string instructorName = skillInstructors[i];
                        string description = skillDescriptions[i];
                        string evaluationDate = evaluations != null
                            ? evaluations.EvaluationDate.ToString("yyyy/MM/dd")
                            : "نامشخص";

                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(2, Unit.Centimetre);
                            page.PageColor(Colors.White);
                            page.ContentFromRightToLeft();
                            page.DefaultTextStyle(x => x.FontSize(14).FontFamily("B Nazanin").Bold().Fallback(x => x.FontFamily("Arial")));

                            page.Background()
                                .Border(1, Unit.Millimetre)
                                .BorderColor(Colors.Blue.Darken2);

                            page.Header()
                                .PaddingBottom(15)
                                .BorderBottom(1, Unit.Millimetre)
                                .BorderColor(Colors.Grey.Darken1)
                                .Background(Colors.Grey.Lighten4)
                                .Padding(15)
                                .Column(col =>
                                {
                                    col.Item().AlignCenter().Text($"{skillNames[i]}")
                                        .FontSize(18).ExtraBold().FontColor(Colors.Blue.Darken2);
                                    col.Item().AlignRight().Text($"نام دانش‌آموز: {firstName} {lastName}")
                                        .FontSize(16).ExtraBold();
                                    col.Item().AlignRight().Text($"پایه تحصیلی: {levelStudent}")
                                        .FontSize(15).ExtraBold();

                                    col.Item().AlignRight().Text($"سال ورود: {FixYearOrder(schoolYear)}")
                                        .FontSize(15).ExtraBold();


                                    col.Item().AlignRight().Text($"کارگروه: {skillNames[i]}")
                                        .FontSize(15).ExtraBold();
                                    col.Item().AlignRight().Text($"مربی: {instructorName}")
                                        .FontSize(15).ExtraBold();
                                });

                            page.Content()
                                //.PaddingVertical(2, Unit.Centimetre)
                                .Border(0.5f, Unit.Millimetre)
                                .BorderColor(Colors.Grey.Darken2)
                                .Padding(10)
                                .Background(Colors.White)
                                .Column(col =>
                                {
                                    col.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(cols =>
                                        {
                                            cols.RelativeColumn(3f);
                                            cols.ConstantColumn(80);
                                            cols.ConstantColumn(70);
                                            cols.ConstantColumn(80);
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().Background(Colors.Blue.Lighten4).Padding(10).AlignCenter().Text("معیار").FontSize(14).Bold();
                                            header.Cell().Background(Colors.Blue.Lighten4).Padding(10).AlignCenter().Text("رو به رشد").FontSize(14).Bold();
                                            header.Cell().Background(Colors.Blue.Lighten4).Padding(10).AlignCenter().Text("مطلوب").FontSize(14).Bold();
                                            header.Cell().Background(Colors.Blue.Lighten4).Padding(10).AlignCenter().Text("نیاز و به رشد").FontSize(14).Bold();
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
                                            table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(10).AlignRight().Text(criteria[j]).FontSize(13);
                                            table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(10).AlignCenter()
                                                .Text(evaluationValues[j] == "رو به رشد" ? "✓" : "").FontSize(13);
                                            table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(10).AlignCenter()
                                                .Text(evaluationValues[j] == "مطلوب" ? "✓" : "").FontSize(13);
                                            table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(10).AlignCenter()
                                                .Text(evaluationValues[j] == "نیاز به رشد" ? "✓" : "").FontSize(13);
                                        }
                                    });

                                    col.Item().AlignRight().Text("توضیحات مربی:")
                                        .FontSize(15).Bold().FontColor(Colors.Black);
                                    col.Item().PaddingTop(2).AlignRight().Text(description)
                                        .FontSize(13).LineHeight(1.3f);
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

                MessageBox.Show($"گزارش با موفقیت در مسیر زیر ذخیره شد:\n{filePath}",
                                "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در تولید گزارش: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateAllStudentsReport()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("دانش‌آموزی برای گزارش‌گیری یافت نشد.", "هشدار",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "گزارش گروهی");
                Directory.CreateDirectory(folderPath);
                string fileName = $"گزارش_گروهی_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
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
                        string[] skillInstructors = new string[3];
                        string[] skillDescriptions = new string[3];
                        skillIds[0] = row.Cells["Skill1"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill1"].Value) : null;
                        skillIds[1] = row.Cells["Skill2"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill2"].Value) : null;
                        skillIds[2] = row.Cells["Skill3"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["Skill3"].Value) : null;
                        skillNames[0] = row.Cells["Skill1Name"].Value?.ToString() ?? "نامشخص";
                        skillNames[1] = row.Cells["Skill2Name"].Value?.ToString() ?? "نامشخص";
                        skillNames[2] = row.Cells["Skill3Name"].Value?.ToString() ?? "نامشخص";
                        skillInstructors[0] = row.Cells["Skill1Instructor"].Value?.ToString() ?? "نامشخص";
                        skillInstructors[1] = row.Cells["Skill2Instructor"].Value?.ToString() ?? "نامشخص";
                        skillInstructors[2] = row.Cells["Skill3Instructor"].Value?.ToString() ?? "نامشخص";
                        skillDescriptions[0] = row.Cells["Skill1Description"].Value?.ToString() ?? "بدون توضیحات";
                        skillDescriptions[1] = row.Cells["Skill2Description"].Value?.ToString() ?? "بدون توضیحات";
                        skillDescriptions[2] = row.Cells["Skill3Description"].Value?.ToString() ?? "بدون توضیحات";

                        var skillEvaluations = GetStudentSkillEvaluations(studentId);

                        for (int i = 0; i < 3; i++)
                        {
                            if (!skillIds[i].HasValue) continue;
                            int skillNumber = i + 1;
                            var evaluations = skillEvaluations.FirstOrDefault(e => e.SkillNumber == skillNumber);
                            string[] evaluationValues = evaluations != null
                                ? new[]
                                {
                                    evaluations.Aestimatio_peritiae1,
                                    evaluations.Aestimatio_peritiae2,
                                    evaluations.Aestimatio_peritiae3,
                                    evaluations.Aestimatio_peritiae4,
                                    evaluations.Aestimatio_peritiae5,
                                    evaluations.Aestimatio_peritiae6,
                                    evaluations.Aestimatio_peritiae7
                                }
                                : new string[7];
                            string instructorName = skillInstructors[i];
                            string description = skillDescriptions[i];
                            string evaluationDate = evaluations != null
                                ? evaluations.EvaluationDate.ToString("yyyy/MM/dd")
                                : "نامشخص";

                            container.Page(page =>
                            {
                                page.Size(PageSizes.A4);
                                page.Margin(2, Unit.Centimetre);
                                page.PageColor(Colors.White);
                                page.ContentFromRightToLeft();
                                page.DefaultTextStyle(x => x.FontSize(14).FontFamily("B Nazanin").Bold().Fallback(x => x.FontFamily("B Nazanin")));

                                page.Background()
                                    .Border(1, Unit.Millimetre)
                                    .BorderColor(Colors.Blue.Darken2);

                                page.Header()
                                    .PaddingBottom(15)
                                    .BorderBottom(1, Unit.Millimetre)
                                    .BorderColor(Colors.Grey.Darken1)
                                    .Background(Colors.Grey.Lighten4)
                                    .Padding(15)
                                    .Column(col =>
                                    {
                                        col.Item().AlignCenter().Text($"{skillNames[i]}")
                                            .FontSize(18).ExtraBold().FontColor(Colors.Blue.Darken2);
                                        col.Item().AlignRight().Text($"نام دانش‌آموز: {firstName} {lastName}")
                                            .FontSize(16).ExtraBold();
                                        col.Item().AlignRight().Text($"پایه تحصیلی: {levelStudent}")
                                            .FontSize(15).ExtraBold();
                                         col.Item().AlignRight().Text($"سال ورود: {FixYearOrder(schoolYear)}")
                                        .FontSize(15).ExtraBold();
                                        col.Item().AlignRight().Text($"کارگروه: {skillNames[i]}")
                                            .FontSize(15).ExtraBold();
                                        col.Item().AlignRight().Text($"مربی: {instructorName}")
                                            .FontSize(15).ExtraBold();
                                    });

                                page.Content()
                                    //.PaddingVertical(2, Unit.Centimetre)
                                    .Border(0.5f, Unit.Millimetre)
                                    .BorderColor(Colors.Grey.Darken2)
                                    .Padding(15)
                                    .Background(Colors.White)
                                    .Column(col =>
                                    {
                                        col.Item().Table(table =>
                                        {
                                            table.ColumnsDefinition(cols =>
                                            {
                                                cols.RelativeColumn(3f);
                                                cols.ConstantColumn(80);
                                                cols.ConstantColumn(70);
                                                cols.ConstantColumn(80);
                                            });

                                            table.Header(header =>
                                            {
                                                header.Cell().Background(Colors.Blue.Lighten4).Padding(8).AlignCenter().Text("معیار").FontSize(14).Bold();
                                                header.Cell().Background(Colors.Blue.Lighten4).Padding(8).AlignCenter().Text("رو به رشد").FontSize(14).Bold();
                                                header.Cell().Background(Colors.Blue.Lighten4).Padding(8).AlignCenter().Text("مطلوب").FontSize(14).Bold();
                                                header.Cell().Background(Colors.Blue.Lighten4).Padding(8).AlignCenter().Text("نیاز به رشد").FontSize(14).Bold();
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
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).AlignRight().Text(criteria[j]).FontSize(13);
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).AlignCenter()
                                                    .Text(evaluationValues[j] == "رو به رشد" ? "✓" : "").FontSize(13);
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).AlignCenter()
                                                    .Text(evaluationValues[j] == "مطلوب" ? "✓" : "").FontSize(13);
                                                table.Cell().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).AlignCenter()
                                                    .Text(evaluationValues[j] == "نیاز به رشد" ? "✓" : "").FontSize(13);
                                            }
                                        });

                                        col.Item().AlignRight().Text("توضیحات مربی:")
                                            .FontSize(15).Bold().FontColor(Colors.Black);
                                        col.Item().PaddingTop(2).AlignRight().Text(description)
                                            .FontSize(13).LineHeight(1.3f);
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

                MessageBox.Show($"گزارش گروهی با موفقیت در مسیر زیر ذخیره شد:\n{filePath}",
                                "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در تولید گزارش گروهی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<SkillEvaluation> GetStudentSkillEvaluations(int studentId)
        {
            List<SkillEvaluation> evaluations = new List<SkillEvaluation>();
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"
SELECT 
    EvaluationDate, 
    Aestimatio_peritiae1, 
    Aestimatio_peritiae2, 
    Aestimatio_peritiae3,
    Aestimatio_peritiae4, 
    Aestimatio_peritiae5, 
    Aestimatio_peritiae6, 
    Aestimatio_peritiae7,
    InstructorName, 
    SkillNumber
FROM StudentSkillEvaluation
WHERE StudentId = @StudentId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            evaluations.Add(new SkillEvaluation
                            {
                                EvaluationDate = reader.GetDateTime(0),
                                Aestimatio_peritiae1 = reader["Aestimatio_peritiae1"]?.ToString() ?? "",
                                Aestimatio_peritiae2 = reader["Aestimatio_peritiae2"]?.ToString() ?? "",
                                Aestimatio_peritiae3 = reader["Aestimatio_peritiae3"]?.ToString() ?? "",
                                Aestimatio_peritiae4 = reader["Aestimatio_peritiae4"]?.ToString() ?? "",
                                Aestimatio_peritiae5 = reader["Aestimatio_peritiae5"]?.ToString() ?? "",
                                Aestimatio_peritiae6 = reader["Aestimatio_peritiae6"]?.ToString() ?? "",
                                Aestimatio_peritiae7 = reader["Aestimatio_peritiae7"]?.ToString() ?? "",
                                InstructorName = reader["InstructorName"] != DBNull.Value ? reader["InstructorName"].ToString() : "نامشخص",
                                SkillNumber = reader.GetInt32(reader.GetOrdinal("SkillNumber"))
                            });
                        }
                    }
                }
            }
            return evaluations;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerateAllStudentsReport();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


}