namespace LForms.Samples
{
    partial class Form1
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
            lealSelectableButton1 = new Controls.Buttons.LealSelectableButton();
            lealSelectableButton2 = new Controls.Buttons.LealSelectableButton();
            lealSelectableButton3 = new Controls.Buttons.LealSelectableButton();
            lealSeparator1 = new Controls.Mischellaneous.LealSeparator();
            lealSeparator2 = new Controls.Mischellaneous.LealSeparator();
            lealSeparator3 = new Controls.Mischellaneous.LealSeparator();
            lealSeparator4 = new Controls.Mischellaneous.LealSeparator();
            SuspendLayout();
            // 
            // lealSelectableButton1
            // 
            lealSelectableButton1.AutoSearch = true;
            lealSelectableButton1.BackColor = Color.Blue;
            lealSelectableButton1.FlatStyle = FlatStyle.Flat;
            lealSelectableButton1.Font = new Font("Arial", 12F);
            lealSelectableButton1.ForeColor = Color.Black;
            lealSelectableButton1.Location = new Point(59, 67);
            lealSelectableButton1.Name = "lealSelectableButton1";
            lealSelectableButton1.Selected = true;
            lealSelectableButton1.SelectedColor = Color.Blue;
            lealSelectableButton1.Size = new Size(250, 62);
            lealSelectableButton1.TabIndex = 0;
            lealSelectableButton1.Text = "lealSelectableButton1";
            lealSelectableButton1.UnSelectedColor = Color.White;
            lealSelectableButton1.UseVisualStyleBackColor = false;
            // 
            // lealSelectableButton2
            // 
            lealSelectableButton2.AutoSearch = true;
            lealSelectableButton2.BackColor = Color.White;
            lealSelectableButton2.FlatStyle = FlatStyle.Flat;
            lealSelectableButton2.Font = new Font("Arial", 12F);
            lealSelectableButton2.ForeColor = Color.Black;
            lealSelectableButton2.Location = new Point(59, 151);
            lealSelectableButton2.Name = "lealSelectableButton2";
            lealSelectableButton2.Selected = false;
            lealSelectableButton2.SelectedColor = Color.Blue;
            lealSelectableButton2.Size = new Size(250, 62);
            lealSelectableButton2.TabIndex = 1;
            lealSelectableButton2.Text = "lealSelectableButton2";
            lealSelectableButton2.UnSelectedColor = Color.White;
            lealSelectableButton2.UseVisualStyleBackColor = false;
            // 
            // lealSelectableButton3
            // 
            lealSelectableButton3.AutoSearch = true;
            lealSelectableButton3.BackColor = Color.White;
            lealSelectableButton3.FlatStyle = FlatStyle.Flat;
            lealSelectableButton3.Font = new Font("Arial", 12F);
            lealSelectableButton3.ForeColor = Color.Black;
            lealSelectableButton3.Location = new Point(59, 235);
            lealSelectableButton3.Name = "lealSelectableButton3";
            lealSelectableButton3.Selected = false;
            lealSelectableButton3.SelectedColor = Color.Blue;
            lealSelectableButton3.Size = new Size(250, 62);
            lealSelectableButton3.TabIndex = 2;
            lealSelectableButton3.Text = "lealSelectableButton3";
            lealSelectableButton3.UnSelectedColor = Color.White;
            lealSelectableButton3.UseVisualStyleBackColor = false;
            // 
            // lealSeparator1
            // 
            lealSeparator1.LineColor = Color.Black;
            lealSeparator1.LineSpacing = 0;
            lealSeparator1.LineThickness = 2;
            lealSeparator1.Location = new Point(59, 135);
            lealSeparator1.MinimumSize = new Size(0, 2);
            lealSeparator1.Name = "lealSeparator1";
            lealSeparator1.Orientation = Orientation.Horizontal;
            lealSeparator1.Size = new Size(250, 10);
            lealSeparator1.TabIndex = 3;
            // 
            // lealSeparator2
            // 
            lealSeparator2.LineColor = Color.Black;
            lealSeparator2.LineSpacing = 5;
            lealSeparator2.LineThickness = 2;
            lealSeparator2.Location = new Point(0, 0);
            lealSeparator2.MinimumSize = new Size(0, 7);
            lealSeparator2.Name = "lealSeparator2";
            lealSeparator2.Orientation = Orientation.Horizontal;
            lealSeparator2.Size = new Size(250, 24);
            lealSeparator2.TabIndex = 3;
            // 
            // lealSeparator3
            // 
            lealSeparator3.LineColor = Color.Black;
            lealSeparator3.LineSpacing = 5;
            lealSeparator3.LineThickness = 2;
            lealSeparator3.Location = new Point(0, 0);
            lealSeparator3.MinimumSize = new Size(0, 7);
            lealSeparator3.Name = "lealSeparator3";
            lealSeparator3.Orientation = Orientation.Horizontal;
            lealSeparator3.Size = new Size(250, 24);
            lealSeparator3.TabIndex = 3;
            // 
            // lealSeparator4
            // 
            lealSeparator4.LineColor = Color.Black;
            lealSeparator4.LineSpacing = 5;
            lealSeparator4.LineThickness = 2;
            lealSeparator4.Location = new Point(59, 219);
            lealSeparator4.MinimumSize = new Size(0, 7);
            lealSeparator4.Name = "lealSeparator4";
            lealSeparator4.Orientation = Orientation.Horizontal;
            lealSeparator4.Size = new Size(250, 10);
            lealSeparator4.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lealSeparator4);
            Controls.Add(lealSeparator1);
            Controls.Add(lealSelectableButton3);
            Controls.Add(lealSelectableButton2);
            Controls.Add(lealSelectableButton1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Controls.Buttons.LealSelectableButton lealSelectableButton1;
        private Controls.Buttons.LealSelectableButton lealSelectableButton2;
        private Controls.Buttons.LealSelectableButton lealSelectableButton3;
        private Controls.Mischellaneous.LealSeparator lealSeparator1;
        private Controls.Mischellaneous.LealSeparator lealSeparator2;
        private Controls.Mischellaneous.LealSeparator lealSeparator3;
        private Controls.Mischellaneous.LealSeparator lealSeparator4;
    }
}