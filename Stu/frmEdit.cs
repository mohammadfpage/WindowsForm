using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Stu
{
    public partial class frmEdit : Form
    {
        private int _studentId;

        public frmEdit(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;
            LoadSkillGroups();
            LoadStudentData();
        }

        private void LoadSkillGroups()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT SkillGroupId, Name FROM SkillGroups";

                    DataTable skillGroups = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.Fill(skillGroups);
                    }

                    comboBox4.DataSource = skillGroups.Copy();
                    comboBox4.DisplayMember = "Name";
                    comboBox4.ValueMember = "SkillGroupId";

                    comboBox3.DataSource = skillGroups.Copy();
                    comboBox3.DisplayMember = "Name";
                    comboBox3.ValueMember = "SkillGroupId";

                    comboBox2.DataSource = skillGroups.Copy();
                    comboBox2.DisplayMember = "Name";
                    comboBox2.ValueMember = "SkillGroupId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری گروه‌های مهارتی: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudentData()
        {
            try
            {
                // غیرفعال کردن رویدادها برای جلوگیری از خطا در مقداردهی اولیه
                textBox1.TextChanged -= textBox1_TextChanged;
                textBox2.TextChanged -= textBox2_TextChanged;
                richTextBox1.TextChanged -= richTextBox1_TextChanged;
                richTextBox2.TextChanged -= richTextBox2_TextChanged;
                richTextBox3.TextChanged -= richTextBox3_TextChanged;

                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT FirstName, LastName, SchoolYear, LevelStudent, 
                               Skill1, Skill2, Skill3, Description1, Description2, Description3
                        FROM Student
                        WHERE StudentId = @StudentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", _studentId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // اطلاعات پایه
                                textBox1.Text = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                                textBox2.Text = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";

                                // سال تحصیلی
                                if (reader["SchoolYear"] != DBNull.Value)
                                {
                                    string year = reader["SchoolYear"].ToString();
                                    if (comboBox1.Items.Contains(year))
                                        comboBox1.SelectedItem = year;
                                    else
                                    {
                                        comboBox1.SelectedIndex = 0;
                                        MessageBox.Show($"مقدار SchoolYear ({year}) در لیست مجاز نیست.", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    comboBox1.SelectedIndex = 0;
                                }

                                // سطح دانش‌آموز
                                if (reader["LevelStudent"] != DBNull.Value)
                                {
                                    string level = reader["LevelStudent"].ToString();
                                    if (comboBox5.Items.Contains(level))
                                        comboBox5.SelectedItem = level;
                                    else
                                    {
                                        comboBox5.SelectedIndex = 0;
                                        MessageBox.Show($"مقدار LevelStudent ({level}) در لیست مجاز نیست.", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    comboBox5.SelectedIndex = 0;
                                }

                                // مهارت‌ها
                                SetSelectedValueSafe(comboBox4, reader["Skill1"]);
                                SetSelectedValueSafe(comboBox3, reader["Skill2"]);
                                SetSelectedValueSafe(comboBox2, reader["Skill3"]);

                                // توضیحات
                                richTextBox1.Text = reader["Description1"] != DBNull.Value ? reader["Description1"].ToString() : "";
                                richTextBox2.Text = reader["Description2"] != DBNull.Value ? reader["Description2"].ToString() : "";
                                richTextBox3.Text = reader["Description3"] != DBNull.Value ? reader["Description3"].ToString() : "";
                            }
                            else
                            {
                                MessageBox.Show("دانش‌آموز یافت نشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری اطلاعات دانش‌آموز: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // فعال کردن مجدد رویدادها
                textBox1.TextChanged += textBox1_TextChanged;
                textBox2.TextChanged += textBox2_TextChanged;
                richTextBox1.TextChanged += richTextBox1_TextChanged;
                richTextBox2.TextChanged += richTextBox2_TextChanged;
                richTextBox3.TextChanged += richTextBox3_TextChanged;
            }
        }

        private void SetSelectedValueSafe(ComboBox comboBox, object value)
        {
            if (value != DBNull.Value)
            {
                int intValue = Convert.ToInt32(value);
                foreach (DataRowView item in comboBox.Items)
                {
                    if ((int)item[comboBox.ValueMember] == intValue)
                    {
                        comboBox.SelectedValue = intValue;
                        return;
                    }
                }
            }
            comboBox.SelectedIndex = -1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string firstName = textBox1.Text.Trim();
                string lastName = textBox2.Text.Trim();
                string schoolYear = comboBox1.SelectedItem?.ToString();
                string levelStudent = comboBox5.SelectedItem?.ToString();
                int? skill1 = comboBox4.SelectedValue as int?;
                int? skill2 = comboBox3.SelectedValue as int?;
                int? skill3 = comboBox2.SelectedValue as int?;
                string description1 = richTextBox1.Text.Trim();
                string description2 = richTextBox2.Text.Trim();
                string description3 = richTextBox3.Text.Trim();

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    MessageBox.Show("نام و نام خانوادگی نباید خالی باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string updateQuery = @"
                        UPDATE Student
                        SET FirstName = @FirstName,
                            LastName = @LastName,
                            SchoolYear = @SchoolYear,
                            LevelStudent = @LevelStudent,
                            Skill1 = @Skill1,
                            Skill2 = @Skill2,
                            Skill3 = @Skill3,
                            Description1 = @Description1,
                            Description2 = @Description2,
                            Description3 = @Description3
                        WHERE StudentId = @StudentId";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@SchoolYear", string.IsNullOrEmpty(schoolYear) ? DBNull.Value : schoolYear);
                        command.Parameters.AddWithValue("@LevelStudent", string.IsNullOrEmpty(levelStudent) ? DBNull.Value : levelStudent);
                        command.Parameters.AddWithValue("@Skill1", skill1 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Skill2", skill2 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Skill3", skill3 ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Description1", string.IsNullOrEmpty(description1) ? DBNull.Value : description1);
                        command.Parameters.AddWithValue("@Description2", string.IsNullOrEmpty(description2) ? DBNull.Value : description2);
                        command.Parameters.AddWithValue("@Description3", string.IsNullOrEmpty(description3) ? DBNull.Value : description3);
                        command.Parameters.AddWithValue("@StudentId", _studentId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("اطلاعات با موفقیت ویرایش شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("هیچ رکوردی ویرایش نشد.", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ویرایش اطلاعات: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // اعتبارسنجی نام
            if (textBox1.Text.Length > 50)
            {
                MessageBox.Show("نام نمی‌تواند بیش از 50 کاراکتر باشد.", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Text = textBox1.Text.Substring(0, 50);
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // اعتبارسنجی نام خانوادگی
            if (textBox2.Text.Length > 50)
            {
                MessageBox.Show("نام خانوادگی نمی‌تواند بیش از 50 کاراکتر باشد.", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Text = textBox2.Text.Substring(0, 50);
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // توضیح عملکرد 1
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            // توضیح عملکرد 2
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            // توضیح عملکرد 3
        }

        private void frmEdit_Load(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                CreateUser NextForm = new CreateUser();
                NextForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بازکردن فرم جدید:\n{ex.Message}","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}