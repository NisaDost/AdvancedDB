
namespace AdvancedDB_Form
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            button1 = new Button();
            comboBox1 = new ComboBox();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            numericUpDown2 = new NumericUpDown();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(102, 34);
            label1.Name = "label1";
            label1.Size = new Size(328, 20);
            label1.TabIndex = 0;
            label1.Text = "Select an isolation level and start the simulation.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.Location = new Point(102, 228);
            button1.Name = "button1";
            button1.Size = new Size(179, 54);
            button1.TabIndex = 1;
            button1.Text = "Start simulation";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(102, 57);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(327, 28);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(162, 164);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(71, 27);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(102, 166);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 0;
            label2.Text = "Type A";
            label2.Click += label1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(102, 111);
            label4.Name = "label4";
            label4.Size = new Size(327, 20);
            label4.TabIndex = 0;
            label4.Text = "Select numbers of users to perform transactions.";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Click += label1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(299, 166);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 0;
            label3.Text = "Type B";
            label3.Click += label1_Click;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(358, 164);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(71, 27);
            numericUpDown2.TabIndex = 3;
            numericUpDown2.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // button2
            // 
            button2.Location = new Point(308, 228);
            button2.Name = "button2";
            button2.Size = new Size(121, 54);
            button2.TabIndex = 4;
            button2.Text = "Quit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(537, 315);
            Controls.Add(button2);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Transaction Manager";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        private Label label1;
        private Button button1;
        private ComboBox comboBox1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private Label label4;
        private Label label3;
        private NumericUpDown numericUpDown2;
        private Button button2;
    }
}
