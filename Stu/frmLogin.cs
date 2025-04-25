using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace Stu
{
    public partial class frmLogin : Form
    {
        // رشته اتصال به دیتابیس
        private const string ConnectionString =
            "Data Source=.\\SQL2022;Initial Catalog=School;User ID=sa;Password=MDev2025!!;TrustServerCertificate=True;";

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("نام کاربری و رمز عبور را وارد کنید", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isValidUser = ValidateUser(userName, password);

            if (isValidUser)
            {
                MessageBox.Show("ورود موفقیت‌آمیز بود!", "موفقیت",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                main mainForm = new main();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("نام کاربری یا رمز عبور نادرست است", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateUser(string userName, string password)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @UserName AND Password = @Password";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"خطا در اتصال به دیتابیس:\n{ex.Message}", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}