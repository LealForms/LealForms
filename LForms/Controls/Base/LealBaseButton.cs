using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Base;

/// <summary>
/// Represents a customizable base button with predefined styles and behaviors.
/// </summary>
public abstract class LealBaseButton : Button
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealBaseButton"/> class with default settings.
    /// </summary>
    protected LealBaseButton()
    {
        Text = "LealButton";
        Cursor = Cursors.Hand;
        BackColor = Color.White;
        ForeColor = Color.Black;
        Size = new Size(200, 50);
        FlatStyle = FlatStyle.Flat;
        Font = new Font("Arial", 12, FontStyle.Regular);
    }
}