namespace Stu
{
    partial class frmLogin
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
            panel1 = new Panel();
            panel2 = new Panel();
            label5 = new Label();
            button1 = new Button();
            textPass = new TextBox();
            textUser = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_08_07df415c;
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            panel1.ImeMode = ImeMode.Off;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(802, 226);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_09_2581277f;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(textPass);
            panel2.Controls.Add(textUser);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            panel2.Location = new Point(0, 226);
            panel2.Name = "panel2";
            panel2.RightToLeft = RightToLeft.Yes;
            panel2.Size = new Size(802, 481);
            panel2.TabIndex = 7;
            panel2.Paint += panel2_Paint;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 13.8F);
            label5.ImeMode = ImeMode.NoControl;
            label5.Location = new Point(149, 41);
            label5.Name = "label5";
            label5.Size = new Size(498, 28);
            label5.TabIndex = 22;
            label5.Text = "نرم افزار کلاس های مهارتی دبستان دخترانه هدایت";
            // 
            // button1
            // 
            button1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(371, 350);
            button1.Name = "button1";
            button1.Size = new Size(94, 47);
            button1.TabIndex = 4;
            button1.Text = "ورود";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textPass
            // 
            textPass.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textPass.Location = new Point(311, 273);
            textPass.Name = "textPass";
            textPass.Size = new Size(207, 32);
            textPass.TabIndex = 3;
            textPass.TextAlign = HorizontalAlignment.Right;
            textPass.TextChanged += textBox2_TextChanged;
            // 
            // textUser
            // 
            textUser.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textUser.Location = new Point(311, 148);
            textUser.Name = "textUser";
            textUser.Size = new Size(207, 32);
            textUser.TabIndex = 2;
            textUser.TextAlign = HorizontalAlignment.Right;
            textUser.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.SeaShell;
            label2.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(371, 229);
            label2.Name = "label2";
            label2.Size = new Size(76, 24);
            label2.TabIndex = 1;
            label2.Text = "رمز عبور";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SeaShell;
            label1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(353, 104);
            label1.Name = "label1";
            label1.Size = new Size(94, 24);
            label1.TabIndex = 0;
            label1.Text = "نام کاربری";
            label1.Click += label1_Click;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.BackgroundImage = Properties.Resources.photo_2025_05_01_10_01_30;
            panel3.Location = new Point(12, 148);
            panel3.Name = "panel3";
            panel3.Size = new Size(270, 287);
            panel3.TabIndex = 23;
            // 
            // panel4
            // 
            panel4.BackgroundImage = Properties.Resources.photo_2025_05_01_10_01_42;
            panel4.Location = new Point(541, 148);
            panel4.Name = "panel4";
            panel4.Size = new Size(239, 265);
            panel4.TabIndex = 24;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 707);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "frmLogin";
            RightToLeftLayout = true;
            Text = "frmLoginn";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private Button button1;
        private TextBox textPass;
        private TextBox textUser;
        private Label label5;
        private Panel panel4;
        private Panel panel3;
    }
}