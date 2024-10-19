using LForms.Controls.Forms;
using LForms.Controls.Panels;
using LForms.Controls.TextBoxes;
using LForms.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LForms.Samples;

public class StickyNote : LealForm
{
    // Lets declare our components that will me used in that class
    public LealPanel? _topPanel;
    public LealTextBox? _textBox;

    public string Content { get; set; } = "";

    public override void LoadComponents()
    {
        // Let's remove the default border style
        FormBorderStyle = FormBorderStyle.None;

        // Set the size
        Size = new Size(300, 400);

        TopLevel = true;
        TopMost = true;

        // Set the panel default config
        _topPanel = new LealPanel()
        {
            Height = 38,
            Dock = DockStyle.Top,
            BackColor = Color.White.Darken(0.75), // darken the white color in 75%
        };
        this.Add(_topPanel);

        // Handle mousedown event
        _topPanel.MouseDown += TopPanel_MouseDown;
    }

    private void TopPanel_MouseDown(object? sender, MouseEventArgs e)
    {
        // With this, every time the uses press the mouse down in the panel it's possible to draw the form
        Handle.DragWindowOnMouseDown(e);
    }
}