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
            SuspendLayout();
            // 
            // lealSelectableButton1
            // 
            lealSelectableButton1.AutoSearch = true;
            lealSelectableButton1.BackColor = Color.Blue;
            lealSelectableButton1.FlatStyle = FlatStyle.Flat;
            lealSelectableButton1.Font = new Font("Arial", 12F);
            lealSelectableButton1.ForeColor = Color.Black;
            lealSelectableButton1.Location = new Point(327, 67);
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
            lealSelectableButton2.BackColor = Color.Blue;
            lealSelectableButton2.FlatStyle = FlatStyle.Flat;
            lealSelectableButton2.Font = new Font("Arial", 12F);
            lealSelectableButton2.ForeColor = Color.Black;
            lealSelectableButton2.Location = new Point(327, 154);
            lealSelectableButton2.Name = "lealSelectableButton2";
            lealSelectableButton2.Selected = true;
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
            lealSelectableButton3.BackColor = Color.Blue;
            lealSelectableButton3.FlatStyle = FlatStyle.Flat;
            lealSelectableButton3.Font = new Font("Arial", 12F);
            lealSelectableButton3.ForeColor = Color.Black;
            lealSelectableButton3.Location = new Point(327, 245);
            lealSelectableButton3.Name = "lealSelectableButton3";
            lealSelectableButton3.Selected = true;
            lealSelectableButton3.SelectedColor = Color.Blue;
            lealSelectableButton3.Size = new Size(250, 62);
            lealSelectableButton3.TabIndex = 2;
            lealSelectableButton3.Text = "lealSelectableButton3";
            lealSelectableButton3.UnSelectedColor = Color.White;
            lealSelectableButton3.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}