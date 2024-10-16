using System.Windows.Forms;

namespace LForms.Controls.Base;

/// <summary>
/// Basic implementation for a panel, enables double buffering
/// </summary>
public abstract class LealBasePanel : Panel
{
    /// <summary>
    /// Initialize a new instance of <see cref="LealBasePanel"/>
    /// </summary>
    protected LealBasePanel()
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    /// <summary>
    /// Redraws the component, organizing it according to the controls
    /// </summary>
    protected abstract void ReDraw();
}