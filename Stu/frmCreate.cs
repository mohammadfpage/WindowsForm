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
            InsertStudent(firstName, lastName, entranceYearText, levelStudent,
                        skill1, skill2, skill3);
        }
        private void InsertStudent(string firstName, string lastName, string entranceYear, string levelStudent, int? skill1, int? skill2, int? skill3)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string insertStudentQuery = @"
                        INSERT INTO Student (FirstName, LastName, SchoolYear, LevelStudent, Skill1, Skill2, Skill3)
                        VALUES (@FirstName, @LastName, @SchoolYear, @LevelStudent,@Skill1, @Skill2, @Skill3)";
                    using (SqlCommand command = new SqlCommand(insertStudentQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@SchoolYear", entranceYear);
                        command.Parameters.AddWithValue("@LevelStudent", levelStudent);
                        command.Parameters.AddWithValue("@Skill1", (object?)skill1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Skill2", (object?)skill2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Skill3", (object?)skill3 ?? DBNull.Value);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("اطلاعات دانش آموز با موفقیت ذخیره شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره‌سازی:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری فرم جدید:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری فرم جدید:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            
        }
    }
}