
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
            studentBindingSource = new BindingSource(components);
            panel1 = new Panel();
            panel2 = new Panel();
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
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // studentBindingSource
            // 
            studentBindingSource.DataSource = typeof(Model.Student);
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_08_07df415c;
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            panel1.Dock = DockStyle.Top;
            panel1.ImeMode = ImeMode.Off;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1091, 226);
            panel1.TabIndex = 45;
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources._2;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
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
            panel2.Location = new Point(0, 226);
            panel2.Name = "panel2";
            panel2.Size = new Size(1091, 399);
            panel2.TabIndex = 46;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(831, 56);
            label7.Name = "label7";
            label7.Size = new Size(124, 24);
            label7.TabIndex = 42;
            label7.Text = "سال تحصیلی";
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Items.AddRange(new object[] { "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم" });
            comboBox5.Location = new Point(799, 89);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(149, 32);
            comboBox5.TabIndex = 41;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(754, 205);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(151, 32);
            comboBox4.TabIndex = 40;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(522, 205);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(166, 32);
            comboBox3.TabIndex = 39;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(290, 205);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 32);
            comboBox2.TabIndex = 38;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 12F);
            label4.Location = new Point(330, 167);
            label4.Name = "label4";
            label4.Size = new Size(85, 24);
            label4.TabIndex = 37;
            label4.Text = "کارگروه 3";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 12F);
            label5.Location = new Point(570, 167);
            label5.Name = "label5";
            label5.Size = new Size(85, 24);
            label5.TabIndex = 36;
            label5.Text = "کارگروه 2";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Tahoma", 12F);
            label6.Location = new Point(799, 167);
            label6.Name = "label6";
            label6.Size = new Size(85, 24);
            label6.TabIndex = 35;
            label6.Text = "کارگروه 1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 12F);
            label3.Location = new Point(635, 56);
            label3.Name = "label3";
            label3.Size = new Size(124, 24);
            label3.TabIndex = 34;
            label3.Text = "سال تحصیلی";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 12F);
            label2.Location = new Point(398, 56);
            label2.Name = "label2";
            label2.Size = new Size(118, 24);
            label2.TabIndex = 33;
            label2.Text = "نام خانوادگی";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 12F);
            label1.Location = new Point(215, 56);
            label1.Name = "label1";
            label1.Size = new Size(34, 24);
            label1.TabIndex = 32;
            label1.Text = "نام";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1400-1401", "1401-1402", "1402-1403", "1403-1404", "1404-1405", "1405-1406", "1406-1407", "1407-1408", "1408-1409", "1409-1410", "1410-1411" });
            comboBox1.Location = new Point(596, 89);
            comboBox1.MinimumSize = new Size(4, 0);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 33);
            comboBox1.TabIndex = 31;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(360, 93);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(176, 32);
            textBox2.TabIndex = 30;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(143, 93);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(161, 32);
            textBox1.TabIndex = 29;
            // 
            // button1
            // 
            button1.Location = new Point(483, 308);
            button1.Name = "button1";
            button1.Size = new Size(205, 34);
            button1.TabIndex = 28;
            button1.Text = "اضافه کردن دانش اموزش";
            button1.UseVisualStyleBackColor = true;
            // 
            // frmCreate
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1091, 625);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "frmCreate";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "اضافه کردن دانش  آموز";
            WindowState = FormWindowState.Maximized;
            Load += frmCreate_Load_1;
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
    }
}