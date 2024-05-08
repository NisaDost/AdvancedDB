
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
            IsoLvlText = new Label();
            StartSimButton = new Button();
            IsoLvl_Dropdown = new ComboBox();
            TypeADropDown = new NumericUpDown();
            TypeAText = new Label();
            UsersText = new Label();
            TypeBText = new Label();
            TypeBDropDown = new NumericUpDown();
            QuitButton = new Button();
            SelectDB = new Label();
            DBDropdown = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)TypeADropDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TypeBDropDown).BeginInit();
            SuspendLayout();
            // 
            // IsoLvlText
            // 
            IsoLvlText.AutoSize = true;
            IsoLvlText.Location = new Point(100, 142);
            IsoLvlText.Name = "IsoLvlText";
            IsoLvlText.Size = new Size(328, 20);
            IsoLvlText.TabIndex = 0;
            IsoLvlText.Text = "Select an isolation level and start the simulation.";
            IsoLvlText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StartSimButton
            // 
            StartSimButton.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            StartSimButton.Location = new Point(100, 336);
            StartSimButton.Name = "StartSimButton";
            StartSimButton.Size = new Size(179, 54);
            StartSimButton.TabIndex = 1;
            StartSimButton.Text = "Start simulation";
            StartSimButton.UseVisualStyleBackColor = true;
            StartSimButton.Click += StartSim_Click;
            // 
            // IsoLvl_Dropdown
            // 
            IsoLvl_Dropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            IsoLvl_Dropdown.FormattingEnabled = true;
            IsoLvl_Dropdown.Location = new Point(100, 165);
            IsoLvl_Dropdown.Name = "IsoLvl_Dropdown";
            IsoLvl_Dropdown.Size = new Size(327, 28);
            IsoLvl_Dropdown.TabIndex = 2;
            IsoLvl_Dropdown.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // TypeADropDown
            // 
            TypeADropDown.Location = new Point(160, 272);
            TypeADropDown.Name = "TypeADropDown";
            TypeADropDown.Size = new Size(71, 27);
            TypeADropDown.TabIndex = 3;
            // 
            // TypeAText
            // 
            TypeAText.AutoSize = true;
            TypeAText.Location = new Point(100, 274);
            TypeAText.Name = "TypeAText";
            TypeAText.Size = new Size(54, 20);
            TypeAText.TabIndex = 0;
            TypeAText.Text = "Type A";
            // 
            // UsersText
            // 
            UsersText.AutoSize = true;
            UsersText.Location = new Point(100, 219);
            UsersText.Name = "UsersText";
            UsersText.Size = new Size(327, 20);
            UsersText.TabIndex = 0;
            UsersText.Text = "Select numbers of users to perform transactions.";
            UsersText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TypeBText
            // 
            TypeBText.AutoSize = true;
            TypeBText.Location = new Point(297, 274);
            TypeBText.Name = "TypeBText";
            TypeBText.Size = new Size(53, 20);
            TypeBText.TabIndex = 0;
            TypeBText.Text = "Type B";
            // 
            // TypeBDropDown
            // 
            TypeBDropDown.Location = new Point(356, 272);
            TypeBDropDown.Name = "TypeBDropDown";
            TypeBDropDown.Size = new Size(71, 27);
            TypeBDropDown.TabIndex = 3;
            // 
            // QuitButton
            // 
            QuitButton.Location = new Point(306, 336);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(121, 54);
            QuitButton.TabIndex = 4;
            QuitButton.Text = "Quit";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // SelectDB
            // 
            SelectDB.AutoSize = true;
            SelectDB.Location = new Point(100, 62);
            SelectDB.Name = "SelectDB";
            SelectDB.Size = new Size(328, 20);
            SelectDB.TabIndex = 5;
            SelectDB.Text = "Select a database to manipulate the data inside.";
            // 
            // DBDropdown
            // 
            DBDropdown.FormattingEnabled = true;
            DBDropdown.Location = new Point(100, 85);
            DBDropdown.Name = "DBDropdown";
            DBDropdown.Size = new Size(327, 28);
            DBDropdown.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 437);
            Controls.Add(DBDropdown);
            Controls.Add(SelectDB);
            Controls.Add(QuitButton);
            Controls.Add(TypeBDropDown);
            Controls.Add(TypeADropDown);
            Controls.Add(IsoLvl_Dropdown);
            Controls.Add(StartSimButton);
            Controls.Add(TypeBText);
            Controls.Add(TypeAText);
            Controls.Add(UsersText);
            Controls.Add(IsoLvlText);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Transaction Manager";
            ((System.ComponentModel.ISupportInitialize)TypeADropDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)TypeBDropDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        private Label IsoLvlText;
        private Button StartSimButton;
        private ComboBox IsoLvl_Dropdown;
        private NumericUpDown TypeADropDown;
        private Label TypeAText;
        private Label UsersText;
        private Label TypeBText;
        private NumericUpDown TypeBDropDown;
        private Button QuitButton;
        private Label SelectDB;
        private ComboBox DBDropdown;
    }
}
