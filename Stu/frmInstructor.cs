using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
namespace Stu
{
    public partial class frmInstructor : Form
    {
        public frmInstructor()
        {
            InitializeComponent();
            this.Load += frmInstructor_Load;
        }
        private void frmInstructor_Load(object sender, EventArgs e)
        {
            LoadInstructors();
            LoadSkillGroups();

        }
        private void LoadSkillGroups()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT SkillGroupId, Name FROM SkillGroups";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            comboBox1.DataSource = dt;
                            comboBox1.DisplayMember = "Name";
                            comboBox1.ValueMember = "SkillGroupId";
                            comboBox1.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($":خطا در بارگذاری کارگروه‌ها\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string family = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(family))
            {
                MessageBox.Show("!لطفاً نام و نام خانوادگی مربی را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("!لطفاً یک کارگروه انتخاب کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Instructor (Name, Family) OUTPUT INSERTED.Id VALUES (@Name, @Family)";
                    int instructorId;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Family", family);
                        instructorId = (int)command.ExecuteScalar();
                    }
                    int skillGroupId = Convert.ToInt32(comboBox1.SelectedValue);
                    query = "UPDATE SkillGroups SET InstructorId = @InstructorId WHERE SkillGroupId = @SkillGroupId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InstructorId", instructorId);
                        command.Parameters.AddWithValue("@SkillGroupId", skillGroupId);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("!مربی و کارگروه با موفقیت ثبت شدند", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.SelectedIndex = -1;
                    LoadInstructors();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"خطای SQL:\n{sqlEx.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ثبت مربی:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadInstructors()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                SELECT id,
                    (i.Name + ' ' + i.Family) AS مربی,
                    sg.Name AS کارگروه
                FROM SkillGroups sg
                INNER JOIN Instructor i ON sg.InstructorId = i.Id";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.Visible = dt.Rows.Count > 0;
                        dataGridView1.Columns["Id"].Visible = false;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($":خطا در بارگذاری مربی‌ها\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.Visible = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    textBox1.Text = row.Cells["InstructorFullName"].Value?.ToString().Split(' ')[0]; // نام
                    textBox2.Text = row.Cells["InstructorFullName"].Value?.ToString().Split(' ').Length > 1 ? row.Cells["InstructorFullName"].Value?.ToString().Split(' ')[1] : ""; // نام خانوادگی

                    if (row.Cells["SkillGroupId"].Value != DBNull.Value)
                    {
                        comboBox1.SelectedValue = row.Cells["SkillGroupId"].Value;
                    }
                    else
                    {
                        comboBox1.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($":خطا در انتخاب مربی\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($":خطا در خروج از صفحه جدید\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}