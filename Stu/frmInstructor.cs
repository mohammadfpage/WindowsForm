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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string family = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(family))
            {
                MessageBox.Show("لطفاً نام و نام خانوادگی مربی را وارد کنید!", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = "INSERT INTO Instructor (Name, Family) VALUES (@Name, @Family)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Family", family);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("مربی با موفقیت ثبت شد!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                LoadInstructors();
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

                    string query = "SELECT Id, Name, Family FROM Instructor";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.Visible = dt.Rows.Count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری مربی‌ها:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    textBox1.Text = row.Cells["Name"].Value?.ToString();
                    textBox2.Text = row.Cells["Family"].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در انتخاب مربی:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CreateUser nextForm = new CreateUser();
                nextForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در باز کردن فرم جدید:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
