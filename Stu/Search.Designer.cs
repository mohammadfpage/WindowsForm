namespace Stu
{
    partial class Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
            panel1 = new Panel();
            studentBindingSource = new BindingSource(components);
            panel2 = new Panel();
            button7 = new Button();
            button2 = new Button();
            label5 = new Label();
            comboBox2 = new ComboBox();
            dataGridView1 = new DataGridView();
            PDF = new DataGridViewButtonColumn();
            label4 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            panel1.Size = new Size(1454, 226);
            panel1.TabIndex = 22;
            // 
            // studentBindingSource
            // 
            studentBindingSource.DataSource = typeof(Model.Student);
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.WhatsApp_Image_2025_04_28_at_02_09_09_2581277f;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(button7);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(comboBox2);
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(textBox2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 226);
            panel2.Name = "panel2";
            panel2.Size = new Size(1454, 526);
            panel2.TabIndex = 23;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.None;
            button7.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(254, 143);
            button7.Name = "button7";
            button7.Size = new Size(159, 39);
            button7.TabIndex = 32;
            button7.Text = "پاک سازی مقادیر";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(41, 143);
            button2.Name = "button2";
            button2.Size = new Size(195, 41);
            button2.TabIndex = 31;
            button2.Text = "گزارش گیری کلی";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(470, 23);
            label5.Name = "label5";
            label5.Size = new Size(498, 28);
            label5.TabIndex = 30;
            label5.Text = "نرم افزار کلاس های مهارتی دبستان دخترانه هدایت";
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.None;
            comboBox2.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم" });
            comboBox2.Location = new Point(626, 148);
            comboBox2.Name = "comboBox2";
            comboBox2.RightToLeft = RightToLeft.Yes;
            comboBox2.Size = new Size(144, 32);
            comboBox2.TabIndex = 28;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { PDF });
            dataGridView1.Location = new Point(97, 207);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RightToLeft = RightToLeft.Yes;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1275, 251);
            dataGridView1.TabIndex = 27;
            dataGridView1.Visible = false;
            // 
            // PDF
            // 
            PDF.HeaderText = "بررسی فعالیت";
            PDF.MinimumWidth = 6;
            PDF.Name = "PDF";
            PDF.ReadOnly = true;
            PDF.Resizable = DataGridViewTriState.True;
            PDF.SortMode = DataGridViewColumnSortMode.Automatic;
            PDF.Text = "گزارش";
            PDF.UseColumnTextForButtonValue = true;
            PDF.Width = 125;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 12F);
            label4.Location = new Point(639, 109);
            label4.Name = "label4";
            label4.Size = new Size(114, 24);
            label4.TabIndex = 29;
            label4.Text = "پایه تحصیلی";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.None;
            comboBox1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1400-1401", "1401-1402", "1402-1403", "1403-1404", "1404-1405", "1405-1406", "1406-1407", "1407-1408", "1408-1409", "1409-1410", "1410-1411" });
            comboBox1.Location = new Point(810, 148);
            comboBox1.Name = "comboBox1";
            comboBox1.RightToLeft = RightToLeft.Yes;
            comboBox1.Size = new Size(158, 32);
            comboBox1.TabIndex = 23;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(442, 143);
            button1.Name = "button1";
            button1.Size = new Size(134, 41);
            button1.TabIndex = 20;
            button1.Text = "جستجو";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 12F);
            label1.Location = new Point(1283, 109);
            label1.Name = "label1";
            label1.Size = new Size(34, 24);
            label1.TabIndex = 24;
            label1.Text = "نام";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 12F);
            label2.Location = new Point(1028, 109);
            label2.Name = "label2";
            label2.Size = new Size(118, 24);
            label2.TabIndex = 25;
            label2.Text = "نام خانوادگی";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(1217, 152);
            textBox1.Name = "textBox1";
            textBox1.RightToLeft = RightToLeft.No;
            textBox1.Size = new Size(169, 32);
            textBox1.TabIndex = 21;
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 12F);
            label3.Location = new Point(822, 109);
            label3.Name = "label3";
            label3.Size = new Size(124, 24);
            label3.TabIndex = 26;
            label3.Text = "سال تحصیلی";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(1004, 152);
            textBox2.Name = "textBox2";
            textBox2.RightToLeft = RightToLeft.No;
            textBox2.Size = new Size(183, 32);
            textBox2.TabIndex = 22;
            textBox2.TextAlign = HorizontalAlignment.Right;
            // 
            // Search
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1454, 752);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Search";
            RightToLeft = RightToLeft.No;
            Text = "صفحه جستجوی دانش آموزان";
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private BindingSource studentBindingSource;
        private Panel panel2;
        private ComboBox comboBox2;
        private DataGridView dataGridView1;
        private Label label4;
        private ComboBox comboBox1;
        private Button button1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
        private Label label5;
        private Button button2;
        private DataGridViewButtonColumn PDF;
        private Button button7;
    }
}