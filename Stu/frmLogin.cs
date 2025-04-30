using Microsoft.Data.SqlClient;
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
        private void button1_Click_1(object sender, EventArgs e)
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
                    // تست اتصال
                    connection.Open();
                    MessageBox.Show("✅ اتصال موفق به دیتابیس برقرار شد.", "اتصال", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                                nextForm = new Search();
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
                MessageBox.Show($"❌ خطا در اتصال یا اجرای کوئری:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
    }
}
