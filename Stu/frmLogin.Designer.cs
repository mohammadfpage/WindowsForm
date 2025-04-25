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
            GetLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            txtUserName = new TextBox();
            txtPassword = new TextBox();
            SuspendLayout();
            // 
            // GetLogin
            // 
            GetLogin.Location = new Point(121, 282);
            GetLogin.Name = "GetLogin";
            GetLogin.Size = new Size(75, 23);
            GetLogin.TabIndex = 0;
            GetLogin.Text = "ورود";
            GetLogin.UseVisualStyleBackColor = true;
            GetLogin.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(141, 95);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 1;
            label1.Text = "نام کاربری";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(148, 194);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 2;
            label2.Text = "رمز عبور";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(88, 124);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(156, 23);
            txtUserName.TabIndex = 3;
            txtUserName.TextChanged += textBox1_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(88, 224);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(156, 23);
            txtPassword.TabIndex = 4;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 450);
            Controls.Add(txtPassword);
            Controls.Add(txtUserName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(GetLogin);
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button GetLogin;
        private Label label1;
        private Label label2;
        private TextBox txtUserName;
        private TextBox txtPassword;
    }
}