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
            dataGridView1 = new DataGridView();
            edit = new DataGridViewButtonColumn();
            delete = new DataGridViewButtonColumn();
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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { edit, delete });
            dataGridView1.Location = new Point(12, 267);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(980, 251);
            dataGridView1.TabIndex = 16;
            dataGridView1.Visible = false;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // edit
            // 
            edit.HeaderText = "ویرایش";
            edit.MinimumWidth = 6;
            edit.Name = "edit";
            edit.Text = "ویرایش";
            edit.ToolTipText = "ویرایش";
            edit.UseColumnTextForButtonValue = true;
            edit.Width = 125;
            // 
            // delete
            // 
            delete.HeaderText = "حذف";
            delete.MinimumWidth = 6;
            delete.Name = "delete";
            delete.ReadOnly = true;
            delete.Text = "حذف";
            delete.ToolTipText = "حذف";
            delete.UseColumnTextForButtonValue = true;
            delete.Width = 125;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(243, 27);
            label3.Name = "label3";
            label3.Size = new Size(92, 20);
            label3.TabIndex = 15;
            label3.Text = "سال تحصیلی";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(433, 27);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 14;
            label2.Text = "نام خانوادگی";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(688, 27);
            label1.Name = "label1";
            label1.Size = new Size(27, 20);
            label1.TabIndex = 13;
            label1.Text = "نام";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1400", "1401", "1402", "1403", "1404", "1405", "1406", "1407", "1408", "1409", "1410" });
            comboBox1.Location = new Point(217, 60);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(136, 28);
            comboBox1.TabIndex = 12;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(400, 61);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(176, 27);
            textBox2.TabIndex = 11;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(622, 62);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(161, 27);
            textBox1.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new Point(478, 141);
            button1.Name = "button1";
            button1.Size = new Size(134, 34);
            button1.TabIndex = 9;
            button1.Text = "جستجو";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(148, 146);
            button2.Name = "button2";
            button2.Size = new Size(123, 29);
            button2.TabIndex = 17;
            button2.Text = "اضافه کردن دانش اموزش";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم" });
            comboBox2.Location = new Point(26, 60);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(144, 28);
            comboBox2.TabIndex = 18;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(46, 27);
            label4.Name = "label4";
            label4.Size = new Size(87, 20);
            label4.TabIndex = 19;
            label4.Text = "پایه تحصیلی";
            // 
            // CreateUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1004, 657);
            Controls.Add(label4);
            Controls.Add(comboBox2);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "CreateUser";
            Text = "CreateUser";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private DataGridViewButtonColumn edit;
        private DataGridViewButtonColumn delete;
        private ComboBox comboBox2;
        private Label label4;
    }
}