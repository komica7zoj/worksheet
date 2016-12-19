namespace worksheet
{
    partial class AddUserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.staffnametextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.applybutton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.staffnumbertextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.unitcomboBox = new System.Windows.Forms.ComboBox();
            this.CCcomboBox = new System.Windows.Forms.ComboBox();
            this.gradecomboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Staff Name";
            // 
            // staffnametextBox
            // 
            this.staffnametextBox.Location = new System.Drawing.Point(163, 60);
            this.staffnametextBox.Name = "staffnametextBox";
            this.staffnametextBox.Size = new System.Drawing.Size(213, 22);
            this.staffnametextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grade";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "CC";
            // 
            // applybutton
            // 
            this.applybutton.Location = new System.Drawing.Point(708, 204);
            this.applybutton.Name = "applybutton";
            this.applybutton.Size = new System.Drawing.Size(104, 30);
            this.applybutton.TabIndex = 6;
            this.applybutton.Text = "button1";
            this.applybutton.UseVisualStyleBackColor = true;
            this.applybutton.Click += new System.EventHandler(this.applybutton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(428, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Unit";
            // 
            // staffnumbertextBox
            // 
            this.staffnumbertextBox.Location = new System.Drawing.Point(497, 60);
            this.staffnumbertextBox.Name = "staffnumbertextBox";
            this.staffnumbertextBox.Size = new System.Drawing.Size(213, 22);
            this.staffnumbertextBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(428, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "Staff Number";
            // 
            // unitcomboBox
            // 
            this.unitcomboBox.FormattingEnabled = true;
            this.unitcomboBox.Items.AddRange(new object[] {
            "Comms",
            "Ops",
            "Project",
            "Nav",
            "Radar",
            "Trn",
            "Sof",
            "Sys"});
            this.unitcomboBox.Location = new System.Drawing.Point(497, 103);
            this.unitcomboBox.Name = "unitcomboBox";
            this.unitcomboBox.Size = new System.Drawing.Size(121, 20);
            this.unitcomboBox.TabIndex = 10;
            // 
            // CCcomboBox
            // 
            this.CCcomboBox.FormattingEnabled = true;
            this.CCcomboBox.Items.AddRange(new object[] {
            "CKL9",
            "CKL6"});
            this.CCcomboBox.Location = new System.Drawing.Point(163, 143);
            this.CCcomboBox.Name = "CCcomboBox";
            this.CCcomboBox.Size = new System.Drawing.Size(121, 20);
            this.CCcomboBox.TabIndex = 11;
            // 
            // gradecomboBox
            // 
            this.gradecomboBox.FormattingEnabled = true;
            this.gradecomboBox.Items.AddRange(new object[] {
            "E02",
            "E03",
            "E04",
            "E05",
            "E06",
            "E07",
            "E08",
            "I03",
            "I04",
            "I05",
            "I06",
            "I07",
            "I08",
            "I09"});
            this.gradecomboBox.Location = new System.Drawing.Point(163, 103);
            this.gradecomboBox.Name = "gradecomboBox";
            this.gradecomboBox.Size = new System.Drawing.Size(121, 20);
            this.gradecomboBox.TabIndex = 12;
            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 262);
            this.Controls.Add(this.gradecomboBox);
            this.Controls.Add(this.CCcomboBox);
            this.Controls.Add(this.unitcomboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.staffnumbertextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.applybutton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.staffnametextBox);
            this.Controls.Add(this.label1);
            this.Name = "AddUserForm";
            this.Text = "AddUserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox staffnametextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button applybutton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox staffnumbertextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox unitcomboBox;
        private System.Windows.Forms.ComboBox CCcomboBox;
        private System.Windows.Forms.ComboBox gradecomboBox;
    }
}