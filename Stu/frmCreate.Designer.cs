
namespace Stu
{
    partial class frmCreate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreate));
            studentBindingSource = new BindingSource(components);
            panel1 = new Panel();
            panel2 = new Panel();
            button7 = new Button();
            panel3 = new Panel();
            button2 = new Button();
            label8 = new Label();
            label7 = new Label();
            comboBox5 = new ComboBox();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboBox1 = new ComboBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button1 = new Button();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_08_07df415c;
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            panel1.Dock = DockStyle.Top;
            panel1.ImeMode = ImeMode.Off;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1654, 144);
            panel1.TabIndex = 45;
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_09_2581277f;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(button7);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(comboBox5);
            panel2.Controls.Add(comboBox4);
            panel2.Controls.Add(comboBox3);
            panel2.Controls.Add(comboBox2);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Fill;
            panel2.Font = new Font("Tahoma", 12F);
            panel2.Location = new Point(0, 144);
            panel2.Name = "panel2";
            panel2.Size = new Size(1654, 498);
            panel2.TabIndex = 46;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.None;
            button7.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(906, 272);
            button7.Name = "button7";
            button7.Size = new Size(159, 44);
            button7.TabIndex = 56;
            button7.Text = "پاک سازی مقادیر";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 430);
            panel3.Name = "panel3";
            panel3.Size = new Size(1654, 68);
            panel3.TabIndex = 55;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.Location = new Point(679, 275);
            button2.Name = "button2";
            button2.Size = new Size(159, 41);
            button2.TabIndex = 51;
            button2.Text = "خروج";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("Tahoma", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(578, -49);
            label8.Name = "label8";
            label8.Size = new Size(498, 28);
            label8.TabIndex = 43;
            label8.Text = "نرم افزار کلاس های مهارتی دبستان دخترانه هدایت";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(801, 24);
            label7.Name = "label7";
            label7.Size = new Size(114, 24);
            label7.TabIndex = 42;
            label7.Text = "پایه تحصیلی";
            // 
            // comboBox5
            // 
            comboBox5.Anchor = AnchorStyles.None;
            comboBox5.FormattingEnabled = true;
            comboBox5.Items.AddRange(new object[] { "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم" });
            comboBox5.Location = new Point(784, 66);
            comboBox5.Name = "comboBox5";
            comboBox5.RightToLeft = RightToLeft.Yes;
            comboBox5.Size = new Size(165, 32);
            comboBox5.TabIndex = 41;
            // 
            // comboBox4
            // 
            comboBox4.Anchor = AnchorStyles.None;
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(588, 65);
            comboBox4.Name = "comboBox4";
            comboBox4.RightToLeft = RightToLeft.Yes;
            comboBox4.Size = new Size(156, 32);
            comboBox4.TabIndex = 40;
            // 
            // comboBox3
            // 
            comboBox3.Anchor = AnchorStyles.None;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(390, 65);
            comboBox3.Name = "comboBox3";
            comboBox3.RightToLeft = RightToLeft.Yes;
            comboBox3.Size = new Size(159, 32);
            comboBox3.TabIndex = 39;
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.None;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(190, 65);
            comboBox2.Name = "comboBox2";
            comboBox2.RightToLeft = RightToLeft.Yes;
            comboBox2.Size = new Size(158, 32);
            comboBox2.TabIndex = 38;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 12F);
            label4.Location = new Point(231, 24);
            label4.Name = "label4";
            label4.Size = new Size(85, 24);
            label4.TabIndex = 37;
            label4.Text = "کارگروه 3";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 12F);
            label5.Location = new Point(420, 24);
            label5.Name = "label5";
            label5.Size = new Size(85, 24);
            label5.TabIndex = 36;
            label5.Text = "کارگروه 2";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Tahoma", 12F);
            label6.Location = new Point(618, 24);
            label6.Name = "label6";
            label6.Size = new Size(85, 24);
            label6.TabIndex = 35;
            label6.Text = "کارگروه 1";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 12F);
            label3.Location = new Point(990, 24);
            label3.Name = "label3";
            label3.Size = new Size(124, 24);
            label3.TabIndex = 34;
            label3.Text = "سال تحصیلی";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 12F);
            label2.Location = new Point(1185, 24);
            label2.Name = "label2";
            label2.Size = new Size(118, 24);
            label2.TabIndex = 33;
            label2.Text = "نام خانوادگی";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 12F);
            label1.Location = new Point(1427, 24);
            label1.Name = "label1";
            label1.Size = new Size(34, 24);
            label1.TabIndex = 32;
            label1.Text = "نام";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.None;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1400-1401", "1401-1402", "1402-1403", "1403-1404", "1404-1405", "1405-1406", "1406-1407", "1407-1408", "1408-1409", "1409-1410", "1410-1411" });
            comboBox1.Location = new Point(978, 65);
            comboBox1.MinimumSize = new Size(4, 0);
            comboBox1.Name = "comboBox1";
            comboBox1.RightToLeft = RightToLeft.Yes;
            comboBox1.Size = new Size(156, 33);
            comboBox1.TabIndex = 31;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Location = new Point(1167, 66);
            textBox2.Name = "textBox2";
            textBox2.RightToLeft = RightToLeft.Yes;
            textBox2.Size = new Size(176, 32);
            textBox2.TabIndex = 30;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(1372, 65);
            textBox1.Name = "textBox1";
            textBox1.RightToLeft = RightToLeft.Yes;
            textBox1.Size = new Size(161, 32);
            textBox1.TabIndex = 29;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Location = new Point(784, 173);
            button1.Name = "button1";
            button1.Size = new Size(167, 50);
            button1.TabIndex = 28;
            button1.Text = "اضافه کردن";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // frmCreate
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1654, 642);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmCreate";
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = true;
            Text = "اضافه کردن دانش آموز";
            WindowState = FormWindowState.Maximized;
            Click += frmCreate_Click;
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        private void frmCreate_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private BindingSource studentBindingSource;
        private Panel panel1;
        private Panel panel2;
        private Label label7;
        private ComboBox comboBox5;
        private ComboBox comboBox4;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox comboBox1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button1;
        private Label label8;
        private Button button2;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private Panel panel3;
        private Button button7;
    }
}