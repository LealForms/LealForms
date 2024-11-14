using LForms.Controls.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Controls.Mischellaneous;

public class LealSwitch : LealPanel
{
    private bool _checked = false;
    private bool _autoSize = true;

    private readonly LealPanel _switchPanel;

    public LealSwitch() : base(false, true)
    {
        _switchPanel = new LealPanel()
        {

        };
    }

    public bool Checked
    {
        get => _checked;
        set
        {
            _checked = value;
            ReDraw();
        }
    }

    public bool AutoSize
    {
        get => _autoSize;
        set
        {
            _autoSize = value;
            ReDraw();
        }
    }

    /// <inheritdoc/>
    protected override void ReDraw()
    {


        if (_autoSize)
            Size = MinimumSize;
    }

    /// <inheritdoc/>
    protected override void LoadComponents()
    {

    }


}