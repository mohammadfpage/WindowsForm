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
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboBox1 = new ComboBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(110, 219);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(540, 188);
            dataGridView1.TabIndex = 16;
            dataGridView1.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(130, 43);
            label3.Name = "label3";
            label3.Size = new Size(92, 20);
            label3.TabIndex = 15;
            label3.Text = "سال تحصیلی";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(360, 43);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 14;
            label2.Text = "نام خانوادگی";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(637, 43);
            label1.Name = "label1";
            label1.Size = new Size(27, 20);
            label1.TabIndex = 13;
            label1.Text = "نام";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(74, 82);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(136, 28);
            comboBox1.TabIndex = 12;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(301, 84);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(176, 27);
            textBox2.TabIndex = 11;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(566, 83);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(161, 27);
            textBox1.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new Point(316, 153);
            button1.Name = "button1";
            button1.Size = new Size(134, 34);
            button1.TabIndex = 9;
            button1.Text = "جستجو";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // CreateUser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
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
    }
}