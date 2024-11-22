using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Enums.MessageBox;
using LForms.Extensions;
using LForms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Controls.MessageBox;

public class LealMessageDisplay : LealForm
{
    public LealMessageDisplay(Size startSize, bool tryDarkMode) : base(redrawOnResize: true)
    {
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;

        Size = startSize;

        if (tryDarkMode)
            this.TrySetDarkMode();
    }

    public required string Message { get; set; }
    public new Font Font { get; set; }
    public required IconType IconType { get; set; } = IconType.None;
    public required List<LealMessageBoxButton> LealMessageBoxButtons { get; set; }

    public override void LoadComponents()
    {
        var messageLabel = new Label()
        {
            Text = Message,
            Font = Font,
            AutoSize = false,
            TextAlign = ContentAlignment.TopLeft,
        };
        this.Add(messageLabel);
        messageLabel.DockFillWithPadding(10, 10, 70, 5);

        LealMessageBoxButtons.ForEach(button =>
        {
            var buttonControl = new LealButton()
            {
                Text = button.ButtonText,
                DialogResult = button.DialogResult,
            };
            this.Add(buttonControl);
            buttonControl.SetY(this.Height - buttonControl.Height - 10);
        });
        this.CentralizeWithSpacingChildrensOfTypeByX<LealButton>(15);
    }
}