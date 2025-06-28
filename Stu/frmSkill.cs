using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Stu
{
    public partial class frmSkill : Form
    {
        private int studentId;

        private int skillGroupId;

        private int skillNumber;

        private int? currentEvaluationId = null;

        private readonly Dictionary<string, ComboBox> comboBoxMapping = new Dictionary<string, ComboBox>
        {
            { "Aestimatio_peritiae1", null },
            { "Aestimatio_peritiae2", null },
            { "Aestimatio_peritiae3", null },
            { "Aestimatio_peritiae4", null },
            { "Aestimatio_peritiae5", null },
            { "Aestimatio_peritiae6", null },
            { "Aestimatio_peritiae7", null } 
        };

        private readonly string[] criterionStatuses = { "نیاز به رشد", "مطلوب", "رو به رشد" };

        public frmSkill(int studentId, int skillGroupId, int skillNumber)
        {
            if (studentId <= 0 || skillGroupId <= 0 || skillNumber <= 0)
            {
                MessageBox.Show("شناسه دانش‌آموز گروه مهارتی یا شماره مهارت نامعتبر است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            InitializeComponent();
            this.studentId = studentId;
            this.skillGroupId = skillGroupId;
            this.skillNumber = skillNumber;
            InitializeComboBoxMapping();
            LoadSkillGroupName();
            LoadPreviousEvaluation();
        }

        private void InitializeComboBoxMapping()
        {
            try
            {
                if (comboBox1 == null || comboBox2 == null || comboBox3 == null ||
                    comboBox4 == null || comboBox5 == null || comboBox6 == null || comboBox7 == null ||
                    label4 == null || label2 == null || label3 == null ||
                    label5 == null || label6 == null || label8 == null || label9 == null ||
                    richTextBox1 == null)
                {
                    throw new Exception(".یکی از کنترل‌های فرم به‌درستی تعریف نشده است");
                }

                label4.Text = "مسئولیت‌پذیری";
                label2.Text = "استفاده بهینه از ابزار";
                label3.Text = "مشارکت در کار گروهی";
                label5.Text = "درک مهارتی از فعالیت";
                label6.Text = "ثبات و تاب‌آوری در انجام فعالیت";
                label8.Text = "پویایی و نشاط در انجام فعالیت";
                label9.Text = "میزان شرکت در کارگروه";

                comboBoxMapping["Aestimatio_peritiae1"] = comboBox1;
                comboBoxMapping["Aestimatio_peritiae2"] = comboBox2;
                comboBoxMapping["Aestimatio_peritiae3"] = comboBox3;
                comboBoxMapping["Aestimatio_peritiae4"] = comboBox5;
                comboBoxMapping["Aestimatio_peritiae5"] = comboBox6;
                comboBoxMapping["Aestimatio_peritiae6"] = comboBox4;
                comboBoxMapping["Aestimatio_peritiae7"] = comboBox7;

                foreach (var combo in comboBoxMapping.Values)
                {
                    foreach (var status in criterionStatuses)
                        combo.Items.Add(status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در تنظیم کامبوباکس‌ها: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadSkillGroupName()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Name FROM SkillGroups WHERE SkillGroupId = @SkillGroupId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SkillGroupId", skillGroupId);
                        string skillGroupName = cmd.ExecuteScalar()?.ToString() ?? "نامشخص";
                        this.Text = $"ارزیابی کارگروه {skillNumber}: {skillGroupName}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری نام گروه مهارت: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadPreviousEvaluation()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"
                SELECT TOP 1 Id, EvaluationDate, Description,
                       Aestimatio_peritiae1, Aestimatio_peritiae2, Aestimatio_peritiae3,
                       Aestimatio_peritiae4, Aestimatio_peritiae5, Aestimatio_peritiae6,
                       Aestimatio_peritiae7, InstructorName
                FROM StudentSkillEvaluation
                WHERE StudentId = @StudentId AND SkillGroupId = @SkillGroupId AND SkillNumber = @SkillNumber
                ORDER BY EvaluationDate DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@SkillGroupId", skillGroupId);
                        cmd.Parameters.AddWithValue("@SkillNumber", skillNumber);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentEvaluationId = Convert.ToInt32(reader["Id"]);
                                richTextBox1.Text = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty;

                                foreach (var mapping in comboBoxMapping)
                                {
                                    if (reader[mapping.Key] != DBNull.Value)
                                    {
                                        mapping.Value.SelectedItem = reader[mapping.Key].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری ارزیابی قبلی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxMapping.Any(c => c.Value.SelectedItem == null))
                {
                    MessageBox.Show("لطفاً تمام معیارها را انتخاب کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string instructorName = null;
                            string queryInstructor = @"
                        SELECT i.Name + ' ' + i.Family AS InstructorName
                        FROM SkillGroups sg
                        INNER JOIN Instructor i ON sg.InstructorId = i.Id
                        WHERE sg.SkillGroupId = @SkillGroupId";
                            using (SqlCommand cmd = new SqlCommand(queryInstructor, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SkillGroupId", skillGroupId);
                                var result = cmd.ExecuteScalar();
                                if (result != null)
                                {
                                    instructorName = result.ToString();
                                }
                            }

                            if (currentEvaluationId.HasValue)
                            {
                                string updateQuery = @"
                            UPDATE StudentSkillEvaluation 
                            SET EvaluationDate = GETDATE(),
                                Aestimatio_peritiae1 = @Aestimatio_peritiae1,
                                Aestimatio_peritiae2 = @Aestimatio_peritiae2,
                                Aestimatio_peritiae3 = @Aestimatio_peritiae3,
                                Aestimatio_peritiae4 = @Aestimatio_peritiae4,
                                Aestimatio_peritiae5 = @Aestimatio_peritiae5,
                                Aestimatio_peritiae6 = @Aestimatio_peritiae6,
                                Aestimatio_peritiae7 = @Aestimatio_peritiae7,
                                Description = @Description,
                                InstructorName = @InstructorName,
                                SkillNumber = @SkillNumber
                            WHERE Id = @EvaluationId";

                                using (SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@EvaluationId", currentEvaluationId.Value);
                                    foreach (var mapping in comboBoxMapping)
                                    {
                                        cmd.Parameters.AddWithValue($"@{mapping.Key}", mapping.Value.SelectedItem.ToString());
                                    }
                                    cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(richTextBox1.Text) ? DBNull.Value : (object)richTextBox1.Text);
                                    cmd.Parameters.AddWithValue("@InstructorName", (object)instructorName ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@SkillNumber", skillNumber);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery = @"
                            INSERT INTO StudentSkillEvaluation (
                                StudentId, SkillGroupId, EvaluationDate,
                                Aestimatio_peritiae1, Aestimatio_peritiae2, Aestimatio_peritiae3,
                                Aestimatio_peritiae4, Aestimatio_peritiae5, Aestimatio_peritiae6,
                                Aestimatio_peritiae7, Description, InstructorName, SkillNumber
                            )
                            VALUES (
                                @StudentId, @SkillGroupId, GETDATE(),
                                @Aestimatio_peritiae1, @Aestimatio_peritiae2, @Aestimatio_peritiae3,
                                @Aestimatio_peritiae4, @Aestimatio_peritiae5, @Aestimatio_peritiae6,
                                @Aestimatio_peritiae7, @Description, @InstructorName, @SkillNumber
                            );
                            SELECT SCOPE_IDENTITY();";

                                using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                                    cmd.Parameters.AddWithValue("@SkillGroupId", skillGroupId);
                                    foreach (var mapping in comboBoxMapping)
                                    {
                                        cmd.Parameters.AddWithValue($"@{mapping.Key}", mapping.Value.SelectedItem.ToString());
                                    }
                                    cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(richTextBox1.Text) ? DBNull.Value : (object)richTextBox1.Text);
                                    cmd.Parameters.AddWithValue("@InstructorName", (object)instructorName ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@SkillNumber", skillNumber);
                                    currentEvaluationId = Convert.ToInt32(cmd.ExecuteScalar());
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("ارزیابی با موفقیت ثبت شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"خطا در ثبت ارزیابی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ثبت ارزیابی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CreateUser newfrm = new CreateUser();
                newfrm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در باز کردن فرم جدید: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بستن فرم: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
