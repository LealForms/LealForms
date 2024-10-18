using LForms.Controls.Panels;
using LForms.Extensions;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.TextBoxes;

/// <summary>
/// Class for a custom text box control
/// </summary>
public class LealTextBox : LealPanel
{
    /// <summary>
    /// Represents the method that will handle the <see cref="KeyPressed"/> event.
    /// </summary>
    /// <param name="text">The current text in the text box.</param>
    /// <param name="e">A <see cref="KeyPressEventArgs"/> that contains the event data.</param>
    public delegate void LealTextBox_KeyPressed(string text, KeyPressEventArgs e);

    /// <summary>
    /// Occurs when a key is pressed while the text box has focus.
    /// </summary>
    public event LealTextBox_KeyPressed? KeyPressed;

    private readonly TextBox _input;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealTextBox"/> class.
    /// </summary>
    public LealTextBox()
    {
        Height = 50;
        BackColor = Color.White;

        _input = new TextBox
        {
            Width = 200,
            WordWrap = false,
            BorderStyle = BorderStyle.None,
            Font = new Font("", 12,  FontStyle.Regular),
        };
        this.Add(_input);

        ReDraw();
        InitializeEventHandlers();
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    public new Font Font
    {
        get => _input.Font;
        set
        {
            _input.Font = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the placeholder text associated with the control.
    /// </summary>
    public string Placeholder
    {
        get => _input.PlaceholderText;
        set => _input.PlaceholderText = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this is a multiline text box control.
    /// </summary>
    public bool Multiline
    {
        get => _input.Multiline;
        set => _input.Multiline = value;
    }

    /// <summary>
    /// Gets or sets how text is aligned in a <see cref="TextBox"/> control.
    /// </summary>
    public HorizontalAlignment TextAlign
    {
        get => _input.TextAlign;
        set => _input.TextAlign = value;
    }

    /// <summary>
    /// Gets or sets the foreground color of the control.
    /// </summary>
    public new Color ForeColor
    {
        get => _input.ForeColor;
        set => _input.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text in the control should appear as the default password character.
    /// </summary>
    public bool UseSystemPasswordChar
    {
        get => _input.UseSystemPasswordChar;
        set => _input.UseSystemPasswordChar = value;
    }

    /// <summary>
    /// Gets or sets the character used to mask characters of a password in a single-line text box control.
    /// </summary>
    public char PasswordChar
    {
        get => _input.PasswordChar;
        set => _input.PasswordChar = value;
    }

    /// <summary>
    /// Initializes the event handlers for the control.
    /// </summary>
    private void InitializeEventHandlers()
    {
        Resize += (s, e) => ReDraw();
        GotFocus += (s, e) => _input.Focus();
        BackColorChanged += (s, e) => _input.BackColor = BackColor;
        _input.KeyPress += (s, e) => KeyPressed?.Invoke(_input.Text, e);
    }

    /// <summary>
    /// Forces a redraw of the control, adjusting size and positioning of internal elements.
    /// </summary>
    protected override void ReDraw()
    {
        MinimumSize = new Size(Width, _input.Height + 2);

        _input.Width = Width - 10;
        _input.BackColor = BackColor;
        _input.Centralize();
    }
}