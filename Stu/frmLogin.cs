using Microsoft.Data.SqlClient;
using Stu.Data;
using System;
using System.Windows.Forms;

namespace Stu
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("لطفاً نام کاربری و رمز عبور را وارد کنید!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"SELECT RoleId FROM Users WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        var result = command.ExecuteScalar();

                        if (result != null)
                        {
                            int roleId = Convert.ToInt32(result);

                            MessageBox.Show("ورود موفق!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Form nextForm = null;

                            if (roleId == 1)
                                nextForm = new CreateUser();
                            else if (roleId == 2)
                                nextForm = new main();
                            else
                            {
                                MessageBox.Show("نقش کاربری تعریف نشده است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            nextForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("نام کاربری یا رمز عبور اشتباه است!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در اتصال به دیتابیس: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
