namespace FuzzyRobot
{
    partial class FuzzyForm
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
            this.mapTextBox = new System.Windows.Forms.TextBox();
            this.mapPictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.stepButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mapTextBox
            // 
            this.mapTextBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.mapTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.mapTextBox.Location = new System.Drawing.Point(12, 12);
            this.mapTextBox.Multiline = true;
            this.mapTextBox.Name = "mapTextBox";
            this.mapTextBox.Size = new System.Drawing.Size(177, 134);
            this.mapTextBox.TabIndex = 0;
            this.mapTextBox.Text = "111111111\r\n130000001\r\n101000001\r\n100010001\r\n100000101\r\n100000001\r\n111111111";
            // 
            // mapPictureBox
            // 
            this.mapPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.mapPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapPictureBox.Location = new System.Drawing.Point(196, 12);
            this.mapPictureBox.Name = "mapPictureBox";
            this.mapPictureBox.Size = new System.Drawing.Size(631, 539);
            this.mapPictureBox.TabIndex = 1;
            this.mapPictureBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Draw map";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // stepButton
            // 
            this.stepButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.stepButton.ForeColor = System.Drawing.Color.White;
            this.stepButton.Location = new System.Drawing.Point(13, 181);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(177, 23);
            this.stepButton.TabIndex = 3;
            this.stepButton.Text = "Step";
            this.stepButton.UseVisualStyleBackColor = false;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // FuzzyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(839, 563);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mapPictureBox);
            this.Controls.Add(this.mapTextBox);
            this.Name = "FuzzyForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mapTextBox;
        private System.Windows.Forms.PictureBox mapPictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button stepButton;
    }
}

