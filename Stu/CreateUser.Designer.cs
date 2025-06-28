namespace Stu
{
    partial class CreateUser
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateUser));
            dataGridView1 = new DataGridView();
            edit = new DataGridViewButtonColumn();
            skillbutton1 = new DataGridViewButtonColumn();
            skillbutton2 = new DataGridViewButtonColumn();
            skillbutton3 = new DataGridViewButtonColumn();
            delete = new DataGridViewButtonColumn();
            PDF = new DataGridViewButtonColumn();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboBox1 = new ComboBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            comboBox2 = new ComboBox();
            label4 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            label5 = new Label();
            studentBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { edit, skillbutton1, skillbutton2, skillbutton3, delete, PDF });
            dataGridView1.Location = new Point(23, 273);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RightToLeft = RightToLeft.Yes;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1571, 251);
            dataGridView1.TabIndex = 16;
            dataGridView1.Visible = false;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // edit
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle1.Font = new Font("Tahoma", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            edit.DefaultCellStyle = dataGridViewCellStyle1;
            edit.HeaderText = "ویرایش";
            edit.MinimumWidth = 6;
            edit.Name = "edit";
            edit.ReadOnly = true;
            edit.Text = "ویرایش";
            edit.UseColumnTextForButtonValue = true;
            edit.Width = 125;
            // 
            // skillbutton1
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(224, 224, 224);
            skillbutton1.DefaultCellStyle = dataGridViewCellStyle2;
            skillbutton1.HeaderText = "عملکرد 1";
            skillbutton1.MinimumWidth = 6;
            skillbutton1.Name = "skillbutton1";
            skillbutton1.ReadOnly = true;
            skillbutton1.Resizable = DataGridViewTriState.True;
            skillbutton1.Text = " عملکرد1";
            skillbutton1.UseColumnTextForButtonValue = true;
            skillbutton1.Width = 125;
            // 
            // skillbutton2
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(224, 224, 224);
            skillbutton2.DefaultCellStyle = dataGridViewCellStyle3;
            skillbutton2.HeaderText = "عملکرد 2";
            skillbutton2.MinimumWidth = 6;
            skillbutton2.Name = "skillbutton2";
            skillbutton2.ReadOnly = true;
            skillbutton2.Resizable = DataGridViewTriState.True;
            skillbutton2.Text = "عملکرد2 ";
            skillbutton2.UseColumnTextForButtonValue = true;
            skillbutton2.Width = 125;
            // 
            // skillbutton3
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(224, 224, 224);
            skillbutton3.DefaultCellStyle = dataGridViewCellStyle4;
            skillbutton3.HeaderText = "عملکرد 3";
            skillbutton3.MinimumWidth = 6;
            skillbutton3.Name = "skillbutton3";
            skillbutton3.ReadOnly = true;
            skillbutton3.Resizable = DataGridViewTriState.True;
            skillbutton3.Text = "عملکرد3";
            skillbutton3.UseColumnTextForButtonValue = true;
            skillbutton3.Width = 125;
            // 
            // delete
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle5.Font = new Font("Tahoma", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            delete.DefaultCellStyle = dataGridViewCellStyle5;
            delete.HeaderText = "حذف";
            delete.MinimumWidth = 6;
            delete.Name = "delete";
            delete.ReadOnly = true;
            delete.Text = "حذف";
            delete.UseColumnTextForButtonValue = true;
            delete.Width = 125;
            // 
            // PDF
            // 
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(224, 224, 224);
            PDF.DefaultCellStyle = dataGridViewCellStyle6;
            PDF.HeaderText = "بررسی فعالیت";
            PDF.MinimumWidth = 6;
            PDF.Name = "PDF";
            PDF.ReadOnly = true;
            PDF.Resizable = DataGridViewTriState.True;
            PDF.Text = "گزارش گیری ";
            PDF.ToolTipText = "PDF";
            PDF.UseColumnTextForButtonValue = true;
            PDF.Width = 125;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(788, 117);
            label3.Name = "label3";
            label3.Size = new Size(124, 24);
            label3.TabIndex = 15;
            label3.Text = "سال تحصیلی";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(990, 117);
            label2.Name = "label2";
            label2.Size = new Size(118, 24);
            label2.TabIndex = 14;
            label2.Text = "نام خانوادگی";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(1242, 117);
            label1.Name = "label1";
            label1.Size = new Size(34, 24);
            label1.TabIndex = 13;
            label1.Text = "نام";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.None;
            comboBox1.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1400-1401", "1401-1402", "1402-1403", "1403-1404", "1404-1405", "1405-1406", "1406-1407", "1407-1408", "1408-1409", "1409-1410", "1410-1411" });
            comboBox1.Location = new Point(788, 151);
            comboBox1.Name = "comboBox1";
            comboBox1.RightToLeft = RightToLeft.Yes;
            comboBox1.Size = new Size(136, 30);
            comboBox1.TabIndex = 12;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(961, 152);
            textBox2.Name = "textBox2";
            textBox2.RightToLeft = RightToLeft.No;
            textBox2.Size = new Size(176, 29);
            textBox2.TabIndex = 11;
            textBox2.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(1176, 152);
            textBox1.Name = "textBox1";
            textBox1.RightToLeft = RightToLeft.No;
            textBox1.Size = new Size(169, 29);
            textBox1.TabIndex = 10;
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(392, 143);
            button1.Name = "button1";
            button1.Size = new Size(134, 42);
            button1.TabIndex = 9;
            button1.Text = "جستجو";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(990, 216);
            button2.Name = "button2";
            button2.Size = new Size(223, 37);
            button2.TabIndex = 17;
            button2.Text = "اضافه کردن دانش آموز";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.None;
            comboBox2.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم" });
            comboBox2.Location = new Point(592, 151);
            comboBox2.Name = "comboBox2";
            comboBox2.RightToLeft = RightToLeft.Yes;
            comboBox2.Size = new Size(144, 30);
            comboBox2.TabIndex = 18;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(608, 117);
            label4.Name = "label4";
            label4.Size = new Size(114, 24);
            label4.TabIndex = 19;
            label4.Text = "پایه تحصیلی";
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_08_07df415c;
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            panel1.Dock = DockStyle.Top;
            panel1.ImeMode = ImeMode.Off;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.RightToLeft = RightToLeft.No;
            panel1.Size = new Size(1633, 226);
            panel1.TabIndex = 20;
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_09_2581277f;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(button7);
            panel2.Controls.Add(button6);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(comboBox2);
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(textBox2);
            panel2.Dock = DockStyle.Fill;
            panel2.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            panel2.Location = new Point(0, 226);
            panel2.Name = "panel2";
            panel2.Size = new Size(1633, 536);
            panel2.TabIndex = 21;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.None;
            button7.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(184, 147);
            button7.Name = "button7";
            button7.Size = new Size(159, 39);
            button7.TabIndex = 25;
            button7.Text = "پاک سازی مقادیر";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.None;
            button6.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.Location = new Point(580, 216);
            button6.Name = "button6";
            button6.Size = new Size(156, 37);
            button6.TabIndex = 24;
            button6.Text = "مدیریت مربی";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.None;
            button5.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(298, 218);
            button5.Name = "button5";
            button5.Size = new Size(171, 39);
            button5.TabIndex = 23;
            button5.Text = "خروج";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.None;
            button4.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(788, 216);
            button4.Name = "button4";
            button4.RightToLeft = RightToLeft.No;
            button4.Size = new Size(145, 37);
            button4.TabIndex = 22;
            button4.Text = "مدیریت ورود";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.None;
            button3.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(1268, 218);
            button3.Name = "button3";
            button3.Size = new Size(221, 39);
            button3.TabIndex = 21;
            button3.Text = "گزارش گیری کل نتایج";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(592, 36);
            label5.Name = "label5";
            label5.Size = new Size(498, 28);
            label5.TabIndex = 20;
            label5.Text = "نرم افزار کلاس های مهارتی دبستان دخترانه هدایت";
            // 
            // CreateUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1633, 762);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CreateUser";
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = true;
            Text = "صفحه مدیریت دانش آموزان";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox comboBox1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private ComboBox comboBox2;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private BindingSource studentBindingSource;
        private Label label5;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private DataGridViewButtonColumn edit;
        private DataGridViewButtonColumn skillbutton1;
        private DataGridViewButtonColumn skillbutton2;
        private DataGridViewButtonColumn skillbutton3;
        private DataGridViewButtonColumn delete;
        private DataGridViewButtonColumn PDF;
        private Button button7;
    }
}