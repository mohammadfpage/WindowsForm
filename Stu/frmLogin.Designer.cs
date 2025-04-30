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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            panel1 = new Panel();
            panel2 = new Panel();
            label1 = new Label();
            txtUser = new TextBox();
            button1 = new Button();
            label2 = new Label();
            txtPass = new TextBox();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_08_07df415c;
            panel1.Name = "panel1";
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.BackgroundImage = Properties.Resources._2;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txtUser);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(txtPass);
            panel2.Name = "panel2";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.BackColor = Color.SeaShell;
            label1.Name = "label1";
            // 
            // txtUser
            // 
            resources.ApplyResources(txtUser, "txtUser");
            txtUser.BackColor = SystemColors.ControlLight;
            txtUser.Name = "txtUser";
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.BackColor = Color.Gainsboro;
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.BackColor = Color.SeaShell;
            label2.Name = "label2";
            // 
            // txtPass
            // 
            resources.ApplyResources(txtPass, "txtPass");
            txtPass.BackColor = SystemColors.ControlLight;
            txtPass.Name = "txtPass";
            // 
            // frmLogin
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_09_2581277f;
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            HelpButton = true;
            Name = "frmLogin";
            WindowState = FormWindowState.Maximized;
            Load += frmLogin_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private TextBox txtUser;
        private Button button1;
        private Label label2;
        private TextBox txtPass;
    }
}