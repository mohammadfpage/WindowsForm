using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Stu.Data; 

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
            // پر کردن سال تحصیلی
            comboBox1.Items.AddRange(new object[] { "1400-1401", "1401-1402", "1402-1403", "1403-1404" });

            // بارگذاری کارگروه‌ها
            LoadSkillGroups(comboBox2);
            LoadSkillGroups(comboBox3);
            LoadSkillGroups(comboBox4);
        }

        private void LoadSkillGroups(ComboBox comboBox)
        {
            try
            {
                comboBox.DataSource = null; // پاک کردن هر چیزی که قبلاً توش بوده
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
                            comboBox.DisplayMember = "Name";           // چیزی که کاربر می‌بینه
                            comboBox.ValueMember = "SkillGroupId";      // چیزی که ذخیره میشه
                            comboBox.SelectedIndex = -1;                // هیچی انتخاب نشده باشه
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در بارگذاری کارگروه‌ها:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text.Trim();
            string lastName = textBox2.Text.Trim();
            string entranceYearText = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(entranceYearText))
            {
                MessageBox.Show("لطفاً نام، نام خانوادگی و سال ورود را وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(entranceYearText, out int entranceYear))
            {
                MessageBox.Show("سال ورود باید عددی صحیح باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? skill1 = comboBox2.SelectedValue as int?;
            int? skill2 = comboBox3.SelectedValue as int?;
            int? skill3 = comboBox4.SelectedValue as int?;

            InsertStudent(firstName, lastName, entranceYear, skill1, skill2, skill3);
        }

        private void InsertStudent(string firstName, string lastName, int entranceYear, int? skill1, int? skill2, int? skill3)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Student (FirstName, LastName, SchoolYear, Skill1, Skill2, Skill3)
                        VALUES (@FirstName, @LastName, @SchoolYear, @Skill1, @Skill2, @Skill3)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@SchoolYear", entranceYear);
                        command.Parameters.AddWithValue("@Skill1", (object?)skill1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Skill2", (object?)skill2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Skill3", (object?)skill3 ?? DBNull.Value);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("دانشجو با موفقیت ذخیره شد.", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در ذخیره سازی:\n{ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmCreate_Load_1(object sender, EventArgs e)
        {

        }
    }
}
