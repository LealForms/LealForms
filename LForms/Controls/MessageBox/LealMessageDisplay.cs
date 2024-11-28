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

/// <summary>
/// Represents a custom message box display window that extends <see cref="LealForm"/>.
/// Provides a centralized UI for displaying messages with customizable buttons and icons.
/// </summary>
public class LealMessageDisplay : LealForm
{
    private readonly LealPanel _backPanel;
    private readonly LealPanel _messagePanel;
    private readonly LealPanel _buttonsPanel;
    private readonly Label _messageLabel;
    private readonly List<Button> _buttonPanelList = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="LealMessageDisplay"/> class with the specified initial size.
    /// </summary>
    /// <param name="startSize">The initial size of the message display form.</param>
    public LealMessageDisplay(Size startSize) : base(redrawOnResize: true)
    {
        Size = startSize;
        ShowIcon = false;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        StartPosition = FormStartPosition.CenterScreen;

        _messageLabel = new Label()
        {
            Text = Message,
            Font = new("Arial", 8, FontStyle.Regular),
            AutoSize = false,
            TextAlign = ContentAlignment.TopLeft,
        };
        _backPanel = new LealPanel(false, true)
        {
            Dock = DockStyle.Fill
        };
        _messagePanel = new LealPanel(false, true)
        {
            Dock = DockStyle.Fill,
        };
        _buttonsPanel = new LealPanel(false, true)
        {
            Dock = DockStyle.Bottom,
            BackColor = Color.White.Darken(0.15),
        };
    }

    /// <summary>
    /// Gets or sets the font used for the message text.
    /// Overrides the <see cref="Font"/> property of the base class.
    /// </summary>
    public new Font Font 
    { 
        get => _messageLabel.Font;
        set => _messageLabel.Font = value;
    }

    /// <summary>
    /// Gets or sets the message text displayed in the message box.
    /// </summary>
    public required string? Message
    {
        get => _messageLabel?.Text ?? null;
        set => _messageLabel.Text = value;
    }

    /// <summary>
    /// Gets or sets the type of icon displayed in the message box.
    /// </summary>
    public required IconType IconType { get; set; } = IconType.None;

    /// <summary>
    /// Gets or sets the list of buttons to be displayed in the message box.
    /// </summary>
    public required List<LealMessageBoxButton> LealMessageBoxButtons { get; set; }

    /// <inheritdoc/>
    public override void ReDraw()
    {
        _buttonsPanel.Height = 35 + (LealConstants.GAP * 2);
        _buttonPanelList.ForEach(b =>
        {
            b.DockBottomWithPadding(LealConstants.GAP);
            b.BringToFront();
        });
        _buttonsPanel.CentralizeWithSpacingChildrensOfTypeByX<Button>(15); 
        _messageLabel.DockFillWithPadding(LealConstants.GAP, LealConstants.GAP, (LealConstants.GAP * 2) + 35, LealConstants.GAP);
    }

    /// <inheritdoc/>
    public override void LoadComponents()
    {
        this.Add(_backPanel);
        _backPanel.Add(_messagePanel);
        _backPanel.Add(_buttonsPanel);
        _messagePanel.Add(_messageLabel);

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