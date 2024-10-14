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
            lealSeparator2 = new Controls.Mischellaneous.LealSeparator();
            lealSeparator3 = new Controls.Mischellaneous.LealSeparator();
            lealSeparator5 = new Controls.Mischellaneous.LealSeparator();
            lealTabManager1 = new Controls.Mischellaneous.LealTabManager();
            lealTab1 = new Controls.Mischellaneous.LealTab();
            SuspendLayout();
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
            // lealSeparator5
            // 
            lealSeparator5.LineColor = Color.Black;
            lealSeparator5.LineSpacing = 0;
            lealSeparator5.LineThickness = 2;
            lealSeparator5.Location = new Point(0, 0);
            lealSeparator5.MinimumSize = new Size(0, 2);
            lealSeparator5.Name = "lealSeparator5";
            lealSeparator5.Orientation = Orientation.Horizontal;
            lealSeparator5.Size = new Size(250, 10);
            lealSeparator5.TabIndex = 3;
            // 
            // lealTabManager1
            // 
            lealTabManager1.BackColor = Color.White;
            lealTabManager1.ButtonsAreaSize = 40;
            lealTabManager1.Dock = DockStyle.Fill;
            lealTabManager1.Location = new Point(0, 0);
            lealTabManager1.Name = "lealTabManager1";
            lealTabManager1.SeparatorLineColor = Color.Black;
            lealTabManager1.ShowButtonsBorder = false;
            lealTabManager1.ShowSeparatorLine = true;
            lealTabManager1.Size = new Size(1125, 674);
            lealTabManager1.TabAlignment = TabAlignment.Top;
            lealTabManager1.TabIndex = 0;
            // 
            // lealTab1
            // 
            lealTab1.Dock = DockStyle.Fill;
            lealTab1.Location = new Point(0, 0);
            lealTab1.Name = "lealTab1";
            lealTab1.Size = new Size(200, 100);
            lealTab1.TabIndex = 0;
            lealTab1.TabName = "LealTab";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 674);
            Controls.Add(lealTabManager1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Controls.Mischellaneous.LealSeparator lealSeparator2;
        private Controls.Mischellaneous.LealSeparator lealSeparator3;
        private Controls.Mischellaneous.LealSeparator lealSeparator5;
        private Controls.Mischellaneous.LealTabManager lealTabManager1;
        private Controls.Mischellaneous.LealTab lealTab1;
    }
}