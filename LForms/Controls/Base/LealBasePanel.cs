using System.Windows.Forms;

namespace LForms.Controls.Base;

public abstract class LealBasePanel : Panel
{
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