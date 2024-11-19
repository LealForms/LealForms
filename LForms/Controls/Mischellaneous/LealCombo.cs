using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Controls.Panels;
using LForms.Controls.TextBoxes;
using LForms.Extensions;
using LForms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LForms.Controls.Mischellaneous;

public class LealCombo : LealPanel
{
    private bool _isDroppedDown = false;
    private bool _showDropdownButton = true;
    private int _dropdownHeight = 100;

    private readonly LealTextBox _comboText;
    private readonly LealButton _dropdownButton;
    private readonly ListBox _listBox;
    private readonly LealForm _dropdownForm;

    private readonly List<LealComboItem> _items = [];

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
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.None
        };
        _dropdownForm = new LealForm(true)
        {
            ShowInTaskbar = false,
            TopMost = true,
            FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual,
        };
    }

    public LealTextBox ComboText => _comboText;

    public LealButton DropdownButton => _dropdownButton;

    public LealComboItem? SelectedItem => (LealComboItem?)_listBox.SelectedItem;

    public bool ShowDropdownButton
    {
        get => _showDropdownButton;
        set
        {
            _showDropdownButton = value;
            ReDraw();
        }
    }

    public int DropdownHeight
    {
        get => _dropdownHeight;
        set
        {
            _dropdownHeight = value;
            ReDraw();
        }
    }

    /// <inheritdoc/>
    protected override void ReDraw()
    {
        this.Add(_comboText);
        this.Add(_dropdownButton);
        _dropdownForm.Add(_listBox);

        _dropdownButton.Width = 30;
        _dropdownButton.Visible = _showDropdownButton;
        _dropdownButton.DockTopBottomRightWithPadding(0, 0, 0);
        _comboText.DockFillWithPadding(0, _showDropdownButton ? _dropdownButton.Width : 0, 0, 0);
    }

    /// <inheritdoc/>
    protected override void LoadComponents()
    {
        _comboText.TextChanged += TextComboChanged;
        _comboText.KeyDown += TextComboKeyDown;

        _listBox.Click += ListBox_Click;
        _dropdownButton.Click += DropdownButton_Click;
        ReDraw();
    }

    public void ShowDropdown()
    {
        if (_isDroppedDown)
            return;

        _dropdownForm.Location = PointToScreen(new Point(0, Height));
        _dropdownForm.Size = new Size(Width, _dropdownHeight);
        _dropdownForm.Show();
        _isDroppedDown = true;
    }

    public void HideDropdown()
    {
        if (!_isDroppedDown)
            return;

        _dropdownForm.Hide();
        _isDroppedDown = false;
    }

    public void AddItem(LealComboItem item)
    {
        _items.Add(item);
        _listBox.Items.Add(item);
    }

    public void RemoveItem(LealComboItem item)
    {
        _items.Remove(item);
        _listBox.Items.Remove(item);
    }

    private void TextComboChanged(string text, EventArgs e)
    {
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

    private void ListBox_Click(object? sender, EventArgs e)
    {
        if (_listBox.SelectedItem != null)
        {
            _comboText.Text = _listBox.SelectedItem.ToString();
            HideDropdown();
        }
    }
}