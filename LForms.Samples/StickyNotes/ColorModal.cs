using LealForms.Controls.Buttons;
using LealForms.Controls.Forms;
using LealForms.Controls.Panels;
using LealForms.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LealForms.Samples.StickyNotes;

public class ColorModal(Control owner, Size size, Color selectedColor) : LealModal(size, owner.Location)
{
    public event EventHandler<Color>? ColorChanged;

    public override void LoadComponents()
    {
        var panelColors = new LealPanel()
        {
            Height = 50,
            Dock = DockStyle.Top,
            BackColor = Color.LightGray
        };
        this.Add(panelColors);

        var colors = StickyColors.PastelColors;

        foreach (var color in colors)
        {
            var button = GenerateColorChoiceButton(color, color == selectedColor, (s, e) =>
            {
                ColorChanged?.Invoke(this, color);
                Close();
            });
            panelColors.Add(button);
        }
    }

    private static LealButton GenerateColorChoiceButton(Color color, bool selected, EventHandler onclickHandler) => new(onclickHandler)
    {
        Text = selected ? "✓" : "",
        Width = 50,
        BorderSize = 0,
        BackColor = color,
        ForeColor = StickyColors.TextBackColor,
        MouseHoverColor = color.Darken(0.3),
        MouseDownColor = color,
        Dock = DockStyle.Left,
    };
}