using LForms.Controls.Mischellaneous;
using LForms.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Samples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _ = this.TrySetDarkMode();
            this.Handle.UseImmersiveDarkMode(true);
            CustomInitialization();
        }

        private void CustomInitialization()
        {
            var tab1 = new LealTab()
            {
                TabName = "Tab1",
                BackColor = Color.AntiqueWhite,
            };
            var labelTab1 = new Label()
            {
                Text = "tab1",
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            tab1.Add(labelTab1);

            lealTabManager1.Add(tab1);

            var tab2 = new LealTab()
            {
                TabName = "Tab2",
                BackColor = Color.Bisque,
            };
            var labelTab2 = new Label()
            {
                Text = "tab2",
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            tab2.Add(labelTab2);
            lealTabManager1.Add(tab2);
        }
    }
}
