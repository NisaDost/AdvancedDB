
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using System;

namespace AdvancedDB2
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
            this.IsoLvlText = new System.Windows.Forms.Label();
            this.StartSimButton = new System.Windows.Forms.Button();
            this.IsoLvl_Dropdown = new System.Windows.Forms.ComboBox();
            this.TypeADropDown = new System.Windows.Forms.NumericUpDown();
            this.TypeAText = new System.Windows.Forms.Label();
            this.UsersText = new System.Windows.Forms.Label();
            this.TypeBText = new System.Windows.Forms.Label();
            this.TypeBDropDown = new System.Windows.Forms.NumericUpDown();
            this.QuitButton = new System.Windows.Forms.Button();
            this.SelectDB = new System.Windows.Forms.Label();
            this.DBDropdown = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.TypeADropDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeBDropDown)).BeginInit();
            this.SuspendLayout();
            // 
            // IsoLvlText
            // 
            this.IsoLvlText.AutoSize = true;
            this.IsoLvlText.Location = new System.Drawing.Point(100, 114);
            this.IsoLvlText.Name = "IsoLvlText";
            this.IsoLvlText.Size = new System.Drawing.Size(289, 16);
            this.IsoLvlText.TabIndex = 0;
            this.IsoLvlText.Text = "Select an isolation level and start the simulation.";
            this.IsoLvlText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartSimButton
            // 
            this.StartSimButton.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.StartSimButton.Location = new System.Drawing.Point(100, 269);
            this.StartSimButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartSimButton.Name = "StartSimButton";
            this.StartSimButton.Size = new System.Drawing.Size(179, 43);
            this.StartSimButton.TabIndex = 1;
            this.StartSimButton.Text = "Start simulation";
            this.StartSimButton.UseVisualStyleBackColor = true;
            this.StartSimButton.Click += new System.EventHandler(this.StartSim_Click);
            // 
            // IsoLvl_Dropdown
            // 
            this.IsoLvl_Dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IsoLvl_Dropdown.FormattingEnabled = true;
            this.IsoLvl_Dropdown.Location = new System.Drawing.Point(100, 132);
            this.IsoLvl_Dropdown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IsoLvl_Dropdown.Name = "IsoLvl_Dropdown";
            this.IsoLvl_Dropdown.Size = new System.Drawing.Size(327, 24);
            this.IsoLvl_Dropdown.TabIndex = 2;
            // 
            // TypeADropDown
            // 
            this.TypeADropDown.Location = new System.Drawing.Point(160, 218);
            this.TypeADropDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TypeADropDown.Name = "TypeADropDown";
            this.TypeADropDown.Size = new System.Drawing.Size(71, 22);
            this.TypeADropDown.TabIndex = 3;
            // 
            // TypeAText
            // 
            this.TypeAText.AutoSize = true;
            this.TypeAText.Location = new System.Drawing.Point(100, 219);
            this.TypeAText.Name = "TypeAText";
            this.TypeAText.Size = new System.Drawing.Size(51, 16);
            this.TypeAText.TabIndex = 0;
            this.TypeAText.Text = "Type A";
            // 
            // UsersText
            // 
            this.UsersText.AutoSize = true;
            this.UsersText.Location = new System.Drawing.Point(100, 175);
            this.UsersText.Name = "UsersText";
            this.UsersText.Size = new System.Drawing.Size(291, 16);
            this.UsersText.TabIndex = 0;
            this.UsersText.Text = "Select numbers of users to perform transactions.";
            this.UsersText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TypeBText
            // 
            this.TypeBText.AutoSize = true;
            this.TypeBText.Location = new System.Drawing.Point(297, 219);
            this.TypeBText.Name = "TypeBText";
            this.TypeBText.Size = new System.Drawing.Size(51, 16);
            this.TypeBText.TabIndex = 0;
            this.TypeBText.Text = "Type B";
            // 
            // TypeBDropDown
            // 
            this.TypeBDropDown.Location = new System.Drawing.Point(356, 218);
            this.TypeBDropDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TypeBDropDown.Name = "TypeBDropDown";
            this.TypeBDropDown.Size = new System.Drawing.Size(71, 22);
            this.TypeBDropDown.TabIndex = 3;
            // 
            // QuitButton
            // 
            this.QuitButton.Location = new System.Drawing.Point(306, 269);
            this.QuitButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(121, 43);
            this.QuitButton.TabIndex = 4;
            this.QuitButton.Text = "Quit";
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // SelectDB
            // 
            this.SelectDB.AutoSize = true;
            this.SelectDB.Location = new System.Drawing.Point(100, 50);
            this.SelectDB.Name = "SelectDB";
            this.SelectDB.Size = new System.Drawing.Size(293, 16);
            this.SelectDB.TabIndex = 5;
            this.SelectDB.Text = "Select a database to manipulate the data inside.";
            // 
            // DBDropdown
            // 
            this.DBDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DBDropdown.FormattingEnabled = true;
            this.DBDropdown.Location = new System.Drawing.Point(100, 68);
            this.DBDropdown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DBDropdown.Name = "DBDropdown";
            this.DBDropdown.Size = new System.Drawing.Size(327, 24);
            this.DBDropdown.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 350);
            this.Controls.Add(this.DBDropdown);
            this.Controls.Add(this.SelectDB);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.TypeBDropDown);
            this.Controls.Add(this.TypeADropDown);
            this.Controls.Add(this.IsoLvl_Dropdown);
            this.Controls.Add(this.StartSimButton);
            this.Controls.Add(this.TypeBText);
            this.Controls.Add(this.TypeAText);
            this.Controls.Add(this.UsersText);
            this.Controls.Add(this.IsoLvlText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction Manager";
            ((System.ComponentModel.ISupportInitialize)(this.TypeADropDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeBDropDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
