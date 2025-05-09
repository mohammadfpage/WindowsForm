using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Stu
{
    public partial class frmEditInfo : Form
    {
        public frmEditInfo()
        {
            InitializeComponent();
        }

        private void frmEditInfo_Load(object sender, EventArgs e)
        {
            // بارگذاری اطلاعات مدیر و معاون هنگام باز شدن فرم
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    // بارگذاری اطلاعات مدیر (RoleId = 1)
                    string adminQuery = "SELECT Username, Password FROM Users WHERE RoleId = 1";
                    using (SqlCommand command = new SqlCommand(adminQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox3.Text = reader["Username"].ToString();
                                textBox4.Text = reader["Password"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("کاربری با نقش مدیر یافت نشد.", "خطا",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                    // بارگذاری اطلاعات معاون (RoleId = 2)
                    string deputyQuery = "SELECT Username, Password FROM Users WHERE RoleId = 2";
                    using (SqlCommand command = new SqlCommand(deputyQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["Username"].ToString();
                                textBox2.Text = reader["Password"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("کاربری با نقش معاون یافت نشد.", "خطا",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری اطلاعات:\n{ex.Message}", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ویرایش نام کاربری و رمز عبور مدیر
            try
            {
                // دریافت مقادیر ورودی
                string username = textBox3.Text.Trim();
                string password = textBox4.Text.Trim();

                // اعتبارسنجی ورودی‌ها
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("لطفاً نام کاربری و رمز عبور مدیر را وارد کنید.", "خطا",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // به‌روزرسانی اطلاعات مدیر در پایگاه داده
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    // بررسی وجود مدیر
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE RoleId = 1";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("کاربری با نقش مدیر یافت نشد.", "خطا",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // به‌روزرسانی نام کاربری و رمز عبور
                    string updateQuery = "UPDATE Users SET Username = @Username, Password = @Password WHERE RoleId = 1";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("اطلاعات مدیر با موفقیت به‌روزرسانی شد.", "موفقیت",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("به‌روزرسانی اطلاعات مدیر ناموفق بود.", "خطا",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در به‌روزرسانی اطلاعات مدیر:\n{ex.Message}", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ویرایش نام کاربری و رمز عبور معاون
            try
            {
                // دریافت مقادیر ورودی
                string username = textBox1.Text.Trim();
                string password = textBox2.Text.Trim();

                // اعتبارسنجی ورودی‌ها
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("لطفاً نام کاربری و رمز عبور معاون را وارد کنید.", "خطا",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // به‌روزرسانی اطلاعات معاون در پایگاه داده
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    // بررسی وجود معاون
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE RoleId = 2";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        int count = (int)checkCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show("کاربری با نقش معاون یافت نشد.", "خطا",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // به‌روزرسانی نام کاربری و رمز عبور
                    string updateQuery = "UPDATE Users SET Username = @Username, Password = @Password WHERE RoleId = 2";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("اطلاعات معاون با موفقیت به‌روزرسانی شد.", "موفقیت",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("به‌روزرسانی اطلاعات معاون ناموفق بود.", "خطا",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در به‌روزرسانی اطلاعات معاون:\n{ex.Message}", "خطا",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // نام کاربری مدیر - بدون نیاز به منطق خاص
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            // رمز عبور مدیر - بدون نیاز به منطق خاص
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // نام کاربری معاون - بدون نیاز به منطق خاص
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // رمز عبور معاون - بدون نیاز به منطق خاص
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                CreateUser NextForm = new CreateUser();

                NextForm.Show();
                this.Hide();

            }
            catch(Exception ex)
            {
                MessageBox.Show($"خطا در باز کردن فرم جدید:\n{ex.Message}", "خطا",MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}