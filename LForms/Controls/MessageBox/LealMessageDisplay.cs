using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Controls.Panels;
using LForms.Enums.MessageBox;
using LForms.Extensions;
using LForms.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.MessageBox;

internal class LealMessageDisplay : LealForm
{
    private readonly LealPanel _backPanel;
    private readonly LealPanel _messagePanel;
    private readonly LealPanel _buttonsPanel;
    private readonly List<Button> _buttonPanelList = [];

    public LealMessageDisplay(Size startSize) : base(redrawOnResize: true)
    {
        Size = startSize;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;

        _backPanel = new LealPanel(false, true)
        {
            Dock = DockStyle.Fill
        };
        _messagePanel = new LealPanel(false, true)
        {
            Dock = DockStyle.Top,
        };
        _buttonsPanel = new LealPanel(false, true)
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White.Darken(0.15),
        };
    }

    public new Font Font { get; set; }
    public required string? Message { get; set; }
    public required IconType IconType { get; set; } = IconType.None;
    public required List<LealMessageBoxButton> LealMessageBoxButtons { get; set; }

    /// <inheritdoc/>
    public override void ReDraw()
    {
        _buttonPanelList.ForEach(b =>
        {
            b.DockBottomWithPadding(LealConstants.GAP);
            b.BringToFront();
        });
        _backPanel.CentralizeWithSpacingChildrensOfTypeByX<Button>(15);
    }

    /// <inheritdoc/>
    public override void LoadComponents()
    {
        this.Add(_backPanel);
        _backPanel.Add(_messagePanel);
        _backPanel.Add(_buttonsPanel);

        var msgLbl = new Label()
        {
            Text = Message,
            Font = Font,
            AutoSize = false,
            TextAlign = ContentAlignment.TopLeft,
        };
        _messagePanel.Add(msgLbl);

        msgLbl.DockFillWithPadding(LealConstants.GAP, LealConstants.GAP, (LealConstants.GAP * 2) + 35, LealConstants.GAP);

        LealMessageBoxButtons.ForEach(button =>
        {
            var btn = new Button()
            {
                Height = 35,
                Width = 100,
                BackColor = Color.White,
                Text = button.ButtonText,
                DialogResult = button.DialogResult,
            };
            _buttonsPanel.Add(btn);
            _buttonPanelList.Add(btn);

            if (button.Type == ButtonType.Custom)
                btn.SetAutoWidth();
        });
        ReDraw();
    }
}