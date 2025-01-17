using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Controls.Panels;
using LForms.Controls.TextBoxes;
using LForms.Extensions;
using LForms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LForms.Controls.Miscellaneous;

/// <summary>
/// Represents a custom combo box control that combines a text box, a dropdown button, 
/// and a dropdown list for selecting items.
/// </summary>
public class LealCombo : LealPanel
{
    /// <summary>
    /// Occurs when an item is selected from the dropdown list.
    /// </summary>
    public event EventHandler<LealComboItem?>? ItemSelected;

    private bool _isDroppedDown = false;
    private bool _showDropdownButton = true;
    private bool _showFocusRectangle = false;
    private int _maxDropdownHeight = 100;
    private int _minDropdownHeight = 50;
    private int _dropdownItemHeight = 0;
    private bool _sendTextChange = true;

    private readonly LealTextBox _comboText;
    private readonly LealButton _dropdownButton;
    private readonly ListBox _listBox;
    private readonly LealForm _dropdownForm;
    private readonly List<LealComboItem> _items = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="LealCombo"/> class with the specified height.
    /// </summary>
    /// <param name="height">The height of the combo box. Defaults to 30.</param>
    public LealCombo(int height = 30) : base(redrawOnResize: true)
    {
        Height = height;

        _comboText = new LealTextBox()
        {
            Text = "Test",
            TextAlign = HorizontalAlignment.Left
        };
        _dropdownButton = new LealButton()
        {
            Text = "▼",
            BorderSize = 0,
            Font = new Font("Verdana", 8, FontStyle.Regular),
        };
        _listBox = new ListBox()
        {
            BorderStyle = BorderStyle.None,
            DrawMode = DrawMode.OwnerDrawFixed,
            Font = new Font("Rubik", 10, FontStyle.Regular),
        };
        _dropdownForm = new LealForm(true)
        {
            ShowInTaskbar = false,
            TopMost = true,
            FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual,
        };
    }

    /// <summary>
    /// Gets the text box component of the combo box.
    /// </summary>
    public LealTextBox ComboText => _comboText;

    /// <summary>
    /// Gets the dropdown button component of the combo box.
    /// </summary>
    public LealButton DropdownButton => _dropdownButton;
    
    /// <summary>
    /// Gets the currently selected item from the dropdown list.
    /// </summary>
    public LealComboItem? SelectedItem => (LealComboItem?)_listBox.SelectedItem;
    
    /// <summary>
    /// Gets or sets a value indicating whether the dropdown button is visible.
    /// </summary>
    public bool ShowDropdownButton
    {
        get => _showDropdownButton;
        set
        {
            _showDropdownButton = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a focus rectangle is displayed around selected items.
    /// </summary>
    public bool ShowFocusRectangle
    {
        get => _showFocusRectangle;
        set
        {
            _showFocusRectangle = value;
            _listBox.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the maximum height of the dropdown list.
    /// </summary>
    public int MaxDropdownHeight
    {
        get => _maxDropdownHeight;
        set
        {
            _maxDropdownHeight = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the minimum height of the dropdown list.
    /// </summary>
    public int MinDropdownHeight
    {
        get => _minDropdownHeight;
        set
        {
            _minDropdownHeight = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the height of individual items in the dropdown list.
    /// </summary>
    /// <remarks>
    /// If the value is less than or equal to 0, the height will be calculated based on the text size.
    /// </remarks>
    public int DropdownItemHeight
    {
        get => _dropdownItemHeight;
        set
        {
            _dropdownItemHeight = value;

            if (_dropdownItemHeight <= 0)
            {
                var textHeight = "T".GetTextSize(_listBox.Font).Height;
                _dropdownItemHeight = textHeight;
            }

            _listBox.ItemHeight = _dropdownItemHeight;
            _listBox.Invalidate();
        }
    }

    /// <summary>
    /// Gets or sets the background color of the dropdown list.
    /// </summary>
    public Color DropdownBackColor
    {
        get => _dropdownForm.BackColor;
        set
        {
            _dropdownForm.BackColor = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the foreground (text) color of the dropdown list.
    /// </summary>
    public Color DropdownForeColor
    {
        get => _listBox.ForeColor;
        set => _listBox.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets the font used in the dropdown list.
    /// </summary>
    public Font DropdownFont
    {
        get => _listBox.Font;
        set => _listBox.Font = value;
    }

    /// <inheritdoc/>
    protected override void ReDraw()
    {
        _dropdownButton.Width = 30;
        _dropdownButton.Visible = _showDropdownButton;
        _listBox.BackColor = _dropdownForm.BackColor;
        _listBox.DockFillWithPadding(0);
        _dropdownButton.DockTopBottomRightWithPadding(0, 0, 0);
        _comboText.DockFillWithPadding(0, _showDropdownButton ? _dropdownButton.Width : 0, 0, 0);
    }

    /// <inheritdoc/>
    protected override void LoadComponents()
    {
        _comboText.TextChanged += TextComboChanged;
        _comboText.KeyDown += TextComboKeyDown;
        _listBox.Click += ListBox_Click;
        _listBox.DrawItem += ListBox_DrawItem;
        _listBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) ListBox_Click(s, e); };
        _dropdownButton.Click += DropdownButton_Click;

        if (FindForm() is Form form)
            form.Move += (s, e) => _dropdownForm.Location = PointToScreen(new Point(0, Height));

        this.Add(_comboText);
        this.Add(_dropdownButton);
        _dropdownForm.Add(_listBox);

        ReDraw();
    }

    /// <summary>
    /// Displays the dropdown list.
    /// </summary>
    public void ShowDropdown()
    {
        if (_isDroppedDown)
            return;

        var height = 0;

        for (var i = 0; i < _listBox.Items.Count; i++)
            height += _listBox.GetItemHeight(i);

        height = Math.Min(Math.Max(height, _minDropdownHeight), _maxDropdownHeight);

        _dropdownForm.Location = PointToScreen(new Point(0, Height));
        _dropdownForm.Size = new Size(Width, height);
        _dropdownForm.Show();
        _isDroppedDown = true;
    }

    /// <summary>
    /// Hides the dropdown list.
    /// </summary>
    public void HideDropdown()
    {
        if (!_isDroppedDown)
            return;

        _dropdownForm.Hide();
        _isDroppedDown = false;
    }

    /// <summary>
    /// Adds an item to the dropdown list.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(LealComboItem item)
    {
        _items.Add(item);
        _listBox.Items.Add(item);
    }

    /// <summary>
    /// Removes an item from the dropdown list.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public void RemoveItem(LealComboItem item)
    {
        _items.Remove(item);
        _listBox.Items.Remove(item);
    }

    private void TextComboChanged(string text, EventArgs e)
    {
        if (_sendTextChange)
        {
            _sendTextChange = false;
            return;
        }

        ShowDropdown();
        _comboText.Focus();

        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
        {
            HideDropdown();
            _listBox.Items.Clear();
            _listBox.Items.AddRange([.._items]);
            return;
        }

        _listBox.Items.Clear();
        _listBox.Items.AddRange(_items.Where(i => i.Text.Contains(text)).ToArray());
    }

    private void TextComboKeyDown(object? sender, KeyEventArgs e)
    {
        if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter) && _isDroppedDown)
            _listBox.Focus();
    }

    private void DropdownButton_Click(object? sender, EventArgs e)
    {
        if (!_isDroppedDown)
            ShowDropdown();
        else
            HideDropdown();
    }

    private void ListBox_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0 || sender is not ListBox listBox)
            return;

        // Draw background
        e.DrawBackground();

        var textRect = e.Bounds;
        var itemText = listBox.Items[e.Index].ToString() ?? "";
        var textSize = itemText.GetTextSize(e.Font ?? _listBox.Font);

        textRect.Y += (_dropdownItemHeight / 2) - (textSize.Height / 2);

        // Draw the item text
        TextRenderer.DrawText(e.Graphics, itemText, e.Font, textRect, e.ForeColor, TextFormatFlags.Left);

        if (_showFocusRectangle)
            e.DrawFocusRectangle();
    }

    private void ListBox_Click(object? sender, EventArgs e)
    {
        if (_listBox.SelectedItem != null)
        {
            _sendTextChange = true;
            _comboText.Text = _listBox.SelectedItem.ToString();
            ItemSelected?.Invoke(this, (LealComboItem)_listBox.SelectedItem);
            HideDropdown();
        }
    }
}