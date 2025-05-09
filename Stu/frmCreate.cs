using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Stu
{
    public partial class frmCreate : Form
    {
        public frmCreate()
        {
            InitializeComponent();
            this.Load += frmCreate_Load;
        }

        private void frmCreate_Load(object sender, EventArgs e)
        {



            // بارگذاری کارگروه‌ها
            LoadSkillGroups(comboBox2);
            LoadSkillGroups(comboBox3);
            LoadSkillGroups(comboBox4);
        }

        private void LoadSkillGroups(ComboBox comboBox)
        {
            try
            {
                comboBox.DataSource = null;
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT SkillGroupId, Name FROM SkillGroups";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("هیچ کارگروهی در سیستم ثبت نشده است.", "اطلاع", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            comboBox.DataSource = dt;
                            comboBox.DisplayMember = "Name";
                            comboBox.ValueMember = "SkillGroupId";
                            comboBox.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری کارگروه‌ها:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            string entranceYearText = comboBox1.SelectedItem?.ToString();
            string levelStudent = comboBox5.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(entranceYearText) || string.IsNullOrEmpty(levelStudent))
            {
                MessageBox.Show("لطفاً تمام فیلدهای الزامی (نام، نام خانوادگی، سال ورود و سطح دانش‌آموز) را وارد کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? skill1 = comboBox2.SelectedValue as int?;
            int? skill2 = comboBox3.SelectedValue as int?;
            int? skill3 = comboBox4.SelectedValue as int?;

            string description1 = richTextBox1.Text.Trim();
            string description2 = richTextBox2.Text.Trim();
            string description3 = richTextBox3.Text.Trim();

            InsertStudent(firstName, lastName, entranceYearText, levelStudent,
                        skill1, skill2, skill3, description1, description2, description3);
        }

        private void InsertStudent(string firstName, string lastName, string entranceYear,
                                 string levelStudent, int? skill1, int? skill2, int? skill3,
                                 string description1, string description2, string description3)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    // ثبت دانش‌آموز در جدول Student
                    string insertStudentQuery = @"
                        INSERT INTO Student (FirstName, LastName, SchoolYear, LevelStudent, 
                                          Skill1, Skill2, Skill3, Description1, Description2, Description3)
                        VALUES (@FirstName, @LastName, @SchoolYear, @LevelStudent, 
                                @Skill1, @Skill2, @Skill3, @Description1, @Description2, @Description3)";

                    using (SqlCommand command = new SqlCommand(insertStudentQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@SchoolYear", entranceYear);
                        command.Parameters.AddWithValue("@LevelStudent", levelStudent);

                        command.Parameters.AddWithValue("@Skill1", (object?)skill1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Skill2", (object?)skill2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Skill3", (object?)skill3 ?? DBNull.Value);

                        command.Parameters.AddWithValue("@Description1", (object?)description1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description2", (object?)description2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description3", (object?)description3 ?? DBNull.Value);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("اطلاعات دانش آموز با موفقیت ذخیره شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره‌سازی:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // سایر متدهای رویدادی بدون تغییر باقی می‌مانند
        private void frmCreate_Load_1(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void richTextBox2_TextChanged(object sender, EventArgs e) { }
        private void richTextBox3_TextChanged(object sender, EventArgs e) { }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { }


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
                MessageBox.Show($"خطا در بارگذاری فرم جدید:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}