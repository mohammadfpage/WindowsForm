using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Stu.Data;

namespace Stu
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            // اگر نیاز به مقداردهی اولیه باشد اینجا اضافه کنید
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
                int? entranceYear = null;

                if (!string.IsNullOrEmpty(entranceYearText) && int.TryParse(entranceYearText, out int year))
                {
                    entranceYear = year;
                }

                DataTable result = SearchStudents(firstName, lastName, entranceYear, levelStudent);

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

        private DataTable SearchStudents(string firstName, string lastName, int? entranceYear, string levelStudent)
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
            dataGridView1.Columns["StudentId"].HeaderText = "کد دانش‌آموز";
            dataGridView1.Columns["FirstName"].HeaderText = "نام";
            dataGridView1.Columns["LastName"].HeaderText = "نام خانوادگی";
            dataGridView1.Columns["SchoolYear"].HeaderText = "سال ورود";
            dataGridView1.Columns["LevelStudent"].HeaderText = "پایه تحصیلی";
            dataGridView1.Columns["Skill1"].HeaderText = "مهارت ۱";
            dataGridView1.Columns["Skill2"].HeaderText = "مهارت ۲";
            dataGridView1.Columns["Skill3"].HeaderText = "مهارت ۳";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // نام
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // نام خانوادگی
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // سال ورود
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // پایه تحصیلی
        }
    }
}
