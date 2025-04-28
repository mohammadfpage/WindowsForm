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
            dataGridView1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            string entranceYearText = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(entranceYearText))
            {
                MessageBox.Show("لطفاً حداقل یکی از فیلدها را وارد یا انتخاب کنید.", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int? entranceYear = null;

                // تبدیل سال ورود به int اگر انتخاب شده باشد
                if (!string.IsNullOrEmpty(entranceYearText))
                {
                    if (int.TryParse(entranceYearText, out int year))
                    {
                        entranceYear = year;
                    }
                    else
                    {
                        MessageBox.Show("سال ورود باید عددی صحیح باشد.", "خطای داده‌ای",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                DataTable result = SearchStudents(firstName, lastName, entranceYear);

                if (result.Rows.Count > 0)
                {
                    dataGridView1.DataSource = result;
                    dataGridView1.Visible = true;
                    SetGridHeaders();
                }
                else
                {
                    dataGridView1.Visible = false;
                    MessageBox.Show("دانشجویی با مشخصات وارد شده یافت نشد.", "نتیجه جستجو",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"یک خطا رخ داد:\n{ex.Message}", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable SearchStudents(string firstName, string lastName, int? entranceYear)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                // Query to join Student table with SkillGroups for Skill1, Skill2, Skill3
                string query = @"
            SELECT 
                s.StudentId, 
                s.FirstName, 
                s.LastName, 
                s.SchoolYear,
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
                AND (@EntranceYear IS NULL OR s.SchoolYear = @EntranceYear)"; // Correcting entrance year condition

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(firstName) ? "" : firstName);
                    command.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(lastName) ? "" : lastName);
                    command.Parameters.AddWithValue("@EntranceYear", entranceYear.HasValue ? entranceYear.Value.ToString() : DBNull.Value); // Fix for entranceYear

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
            dataGridView1.Columns["StudentId"].HeaderText = "کد دانشجو";
            dataGridView1.Columns["FirstName"].HeaderText = "نام";
            dataGridView1.Columns["LastName"].HeaderText = "نام خانوادگی";
            dataGridView1.Columns["SchoolYear"].HeaderText = "سال ورود";
            dataGridView1.Columns["Skill1"].HeaderText = "مهارت 1";
            dataGridView1.Columns["Skill2"].HeaderText = "مهارت 2";
            dataGridView1.Columns["Skill3"].HeaderText = "مهارت 3";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // برای عملیات روی کلیک سلول
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }
    }
}
