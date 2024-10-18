using System.Windows.Forms;

namespace LForms.Controls.Panels;

/// <summary>
/// Basic implementation for a panel, enables double buffering
/// </summary>
public class LealPanel : Panel
{
    /// <summary>
    /// Initialize a new instance of <see cref="LealPanel"/>
    /// </summary>
    public LealPanel()
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    /// <summary>
    /// Forces a redraw of the button, optionally implemented by derived classes.
    /// </summary>
    protected virtual void ReDraw() { }
}