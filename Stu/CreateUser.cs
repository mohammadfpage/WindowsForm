using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Stu
{
    public partial class CreateUser : Form
    {
        public CreateUser()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            string entranceYearText = comboBox1.SelectedItem?.ToString();
            string levelStudent = comboBox2.SelectedItem?.ToString(); // اضافه کردن انتخاب پایه تحصیلی

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

                if (!string.IsNullOrEmpty(entranceYearText))
                {
                    if (!int.TryParse(entranceYearText, out int year))
                    {
                        entranceYear = year;
                    }
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
                        s.LevelStudent, -- اضافه شده
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
            dataGridView1.Columns["LevelStudent"].HeaderText = "پایه تحصیلی"; // اضافه شده
            dataGridView1.Columns["Skill1"].HeaderText = "مهارت 1";
            dataGridView1.Columns["Skill2"].HeaderText = "مهارت 2";
            dataGridView1.Columns["Skill3"].HeaderText = "مهارت 3";
            dataGridView1.Columns["StudentId"].Visible = false;

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int studentId = (int)dataGridView1.Rows[e.RowIndex].Cells["StudentId"].Value;

                if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index)
                {
                    EditStudent(studentId);
                }
                else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
                {
                    DeleteStudent(studentId);
                }
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
            // اینجا نیازی به کدنویسی اضافی نیست چون مقدارش در button1 گرفته می‌شود
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CreateUser_Load(object sender, EventArgs e)
        {

        }
    }
}
