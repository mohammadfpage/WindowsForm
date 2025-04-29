
namespace Stu
{
    partial class frmEdit
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
            label8 = new Label();
            comboBox5 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            SuspendLayout();
            // 
            // studentBindingSource
            // 
            studentBindingSource.DataSource = typeof(Model.Student);
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(553, 214);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(151, 28);
            comboBox4.TabIndex = 38;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(314, 214);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(166, 28);
            comboBox3.TabIndex = 37;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(67, 214);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 36;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(109, 175);
            label4.Name = "label4";
            label4.Size = new Size(67, 20);
            label4.TabIndex = 35;
            label4.Text = "کارگروه 3";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(360, 175);
            label5.Name = "label5";
            label5.Size = new Size(67, 20);
            label5.TabIndex = 34;
            label5.Text = "کارگروه 2";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(596, 175);
            label6.Name = "label6";
            label6.Size = new Size(67, 20);
            label6.TabIndex = 33;
            label6.Text = "کارگروه 1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(246, 39);
            label3.Name = "label3";
            label3.Size = new Size(92, 20);
            label3.TabIndex = 32;
            label3.Text = "سال تحصیلی";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(457, 39);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 31;
            label2.Text = "نام خانوادگی";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(677, 39);
            label1.Name = "label1";
            label1.Size = new Size(27, 20);
            label1.TabIndex = 30;
            label1.Text = "نام";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1400", "1401", "1402", "1403", "1404", "1405", "1406", "1407", "1408", "1409", "1410" });
            comboBox1.Location = new Point(223, 80);
            comboBox1.MinimumSize = new Size(4, 0);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 33);
            comboBox1.TabIndex = 29;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(413, 84);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(176, 27);
            textBox2.TabIndex = 28;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(621, 84);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(161, 27);
            textBox1.TabIndex = 27;
            // 
            // button1
            // 
            button1.Location = new Point(295, 322);
            button1.Name = "button1";
            button1.Size = new Size(205, 34);
            button1.TabIndex = 26;
            button1.Text = "ویرایش";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(48, 50);
            label8.Name = "label8";
            label8.Size = new Size(87, 20);
            label8.TabIndex = 42;
            label8.Text = "پایه تحصیلی";
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Items.AddRange(new object[] { "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم" });
            comboBox5.Location = new Point(25, 80);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(151, 28);
            comboBox5.TabIndex = 43;
            // 
            // frmEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBox5);
            Controls.Add(label8);
            Controls.Add(comboBox4);
            Controls.Add(comboBox3);
            Controls.Add(comboBox2);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "frmEdit";
            Text = "EdirForm";
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource studentBindingSource;
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
        private ComboBox comboBox5;
    }
}