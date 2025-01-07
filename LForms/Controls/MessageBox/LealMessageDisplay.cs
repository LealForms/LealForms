using LForms.Controls.Forms;
using LForms.Controls.Mischellaneous;
using LForms.Controls.Panels;
using LForms.Enums.MessageBox;
using LForms.Extensions;
using LForms.Models;
using System;
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
    private readonly bool _autoSize = false;
    private readonly List<Button> _buttonPanelList = [];
    private FlatStyle _buttonFlatStyle = FlatStyle.Flat;

    private int _spacing = 15;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealMessageDisplay"/> class with the specified initial size.
    /// </summary>
    /// <param name="startSize">The initial size of the message display form.</param>
    /// <param name="startPosition">The initial position of the message display form.</param>
    public LealMessageDisplay(Size? startSize, FormStartPosition startPosition) : base(redrawOnResize: true)
    {
        if (startSize != null)
            Size = startSize.Value;
        else
            _autoSize = true;

        ShowIcon = false;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;

        StartPosition = startPosition;
        FormBorderStyle = FormBorderStyle.FixedSingle;

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
    /// Gets or sets the flat style applied to all buttons in the panel.
    /// </summary>
    /// <value>
    /// A <see cref="FlatStyle"/> value that determines the appearance of the buttons.
    /// </value>
    /// <remarks>
    /// When the value is set, all buttons in the <c>_buttonPanelList</c> will be updated to use the specified flat style.
    /// </remarks>
    public FlatStyle ButtonFlatStyle
    {
        get => _buttonFlatStyle;
        set
        {
            _buttonFlatStyle = value;
            _buttonPanelList.ForEach(b => b.FlatStyle = value);
        }
    }

    /// <summary>
    /// Gets or sets the spacing between buttons in the panel.
    /// </summary>
    public int Spacing
    {
        get => _spacing;
        set
        {
            _spacing = value;
            ReDraw();
        }
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
        _buttonsPanel.CentralizeWithSpacingChildrensOfTypeByX<Button>(_spacing); 
        _messageLabel.DockFillWithPadding(LealConstants.GAP, LealConstants.GAP, (LealConstants.GAP * 2) + 35, LealConstants.GAP);

        if (_autoSize)
        {
            var messageSize = TextRenderer.MeasureText(_messageLabel.Text, Font);
            var buttonsSize = LealMessageBoxButtons.Count * 100;
            var gapsSize = LealMessageBoxButtons.Count * _spacing + (_spacing * 5);
            var messageWidth = messageSize.Width + (LealConstants.GAP * 2);

            if (messageWidth > gapsSize + buttonsSize)
                Width = messageWidth;
            else
                Width = gapsSize + buttonsSize;

            var calculatedHeight = messageSize.Height + (LealConstants.GAP * 4) + _buttonsPanel.Height + 100;
            Height = Math.Max(250, calculatedHeight);
        }
    }

    /// <inheritdoc/>
    public override void LoadComponents()
    {
        this.Add(_backPanel);
        _backPanel.Add(_messagePanel);
        _backPanel.Add(_buttonsPanel);
        _messagePanel.Add(_messageLabel);

        var separator = new LealSeparator()
        {
            Height = 1,
            Dock = DockStyle.Top,
            BackColor = Color.White.Darken(0.15),
        };
        this.Add(separator);

        LealMessageBoxButtons.ForEach(button =>
        {
            var btn = new Button()
            {
                Height = 35,
                Width = 100,
                BackColor = Color.White,
                Text = button.ButtonText,
                FlatStyle = FlatStyle.Flat,
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