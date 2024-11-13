using LForms.Controls.Panels;
using LForms.Enums;
using LForms.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Controls.Mischellaneous;

public class LealCheckbox : LealPanel
{
    private int _gap = 5;
    private bool _rounded = false;
    private bool _checked = false;
    private CheckboxLabelAlignment _checkboxLabelAlignment = CheckboxLabelAlignment.CheckBoxLeftLabelRight;

    private readonly Label _label;

    public LealCheckbox(int width = 100, int height = 50) : base(false, true)
    {
        Size = new Size(width, height);
        _label = new Label() 
        {
            AutoSize = false,
            Text = "LealCheckbox",
            Font = new Font("Rubik", 10, FontStyle.Regular),
        };
    }

    public new string Text
    {
        get => _label.Text;
        set
        {
            _label.Text = value;
        }
    }

    public Label Label => _label;

    public bool Checked
    {
        get => _checked;
        set
        {
            _checked = value;
            ReDraw();
        }
    }

    public bool Rounded
    {
        get => _rounded;
        set
        {
            _rounded = value;
            ReDraw();
        }
    }

    public int Gap
    {
        get => _gap;
        set
        {
            _gap = value;
            ReDraw();
        }
    }

    public CheckboxLabelAlignment CheckboxLabelAlignment
    {
        get => _checkboxLabelAlignment;
        set
        {
            _checkboxLabelAlignment = value;
            ReDraw();
        }
    }

    protected override void ReDraw()
    {


        if (_rounded)
            this.GenerateRoundRegion();
        else
            Region = null;
    }
}