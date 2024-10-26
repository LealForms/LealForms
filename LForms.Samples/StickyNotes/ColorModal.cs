using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Samples.StickyNotes;

public class ColorModal : UserControl
{
    private readonly int _width;
    private readonly int _height;

    public ColorModal(Control owner, int width, int height)
    {
        Location = owner.Location;
        owner.Move += (s, e) => Location = owner.Location;
        this._width = width;
        this._height = height;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Size = new Size(_width, _height);
        BorderStyle = BorderStyle.None;
        BackColor = Color.White;
        SetTopLevel(true);
    }
}