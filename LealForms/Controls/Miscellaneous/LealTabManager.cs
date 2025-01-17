using LealForms.Controls.Base;
using LealForms.Controls.Buttons;
using LealForms.Controls.Panels;
using LealForms.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LealForms.Controls.Miscellaneous;

/// <summary>
/// Manages a collection of tabs and their associated buttons within a container.
/// Provides functionality to handle tab alignment, button area size, separator line color, 
/// and visibility of button borders. The tabs can be displayed at the top, bottom, left, or right of the panel.
/// </summary>
public class LealTabManager : LealPanel
{
    private readonly Panel _buttonsPanel = new();
    private readonly LealSeparator _separator = new()
    {
        Dock = DockStyle.Top,
        LineSpacing = 0,
    };
    private readonly List<LealBaseTab> _tabs = [];

    private bool _loadingNewTab = false;
    private bool _realocatingTabRemotion = false;
    private int _buttonAreaSize = 32;
    private bool _showSeparatorLine = true;
    private bool _showButtonsBorder = false;
    private LealBaseTab? _selectedTab = null;
    private Color _separatorLineColor = Color.Black;
    private TabAlignment _alignment = TabAlignment.Top;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealTabManager"/> class.
    /// </summary>
    public LealTabManager()
    {
        this.Add(_separator);
        this.Add(_buttonsPanel);

        Resize += LealTabManager_Resize;
        ControlAdded += LealTabManager_ControlAdded;
        ControlRemoved += LealTabManager_ControlRemoved;
    }

    /// <summary>
    /// Gets or sets the size of the area where the tab buttons are displayed.
    /// </summary>
    public int ButtonsAreaSize
    {
        get => _buttonAreaSize;
        set
        {
            _buttonAreaSize = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the alignment of the tabs (Top, Bottom, Left, or Right).
    /// </summary>
    public TabAlignment TabAlignment
    {
        get => _alignment;
        set
        {
            _alignment = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the color of the separator line between the buttons and the content area.
    /// </summary>
    public Color SeparatorLineColor
    {
        get => _separatorLineColor;
        set
        {
            _separatorLineColor = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets if the separator line between the buttons and the content area will be displayed.
    /// </summary>
    public bool ShowSeparatorLine
    {
        get => _showSeparatorLine;
        set
        {
            _showSeparatorLine = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a border should be shown around the tab buttons.
    /// </summary>
    public bool ShowButtonsBorder
    {
        get => _showButtonsBorder;
        set
        {
            _showButtonsBorder = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Redraws the layout of the tab manager, updating the alignment, button docking, and separator settings.
    /// </summary>
    protected override void ReDraw()
    {
        var alignmentTopBottom = _alignment == TabAlignment.Top || TabAlignment == TabAlignment.Bottom;
        var selectableButtons = _buttonsPanel.GetChildrenOfType<LealSelectableButton>().ToList();

        foreach (var button in selectableButtons)
        {
            button.Dock = alignmentTopBottom ? DockStyle.Left : DockStyle.Top;
            button.FlatAppearance.BorderSize = ShowButtonsBorder ? 1 : 0;
        }

        _buttonsPanel.Dock = AlignmentToDock();
        _separator.Dock = AlignmentToDock();
        _separator.Size = _separator.MinimumSize;
        _separator.LineColor = _separatorLineColor;
        _separator.Orientation = alignmentTopBottom ? Orientation.Horizontal : Orientation.Vertical;
        _separator.Visible = _showSeparatorLine;

        if (alignmentTopBottom)
        {
            _buttonsPanel.Height = _buttonAreaSize;
            _buttonsPanel.MinimumSize = new Size(0, _buttonAreaSize);
        }
        else
        {
            _buttonsPanel.Width = _buttonAreaSize;
            _buttonsPanel.MinimumSize = new Size(_buttonAreaSize, 0);
        }

        var selected = selectableButtons.Where(p => p.Selected);

        if (!selected.Any() && selectableButtons.Count > 0)
            selectableButtons[0].Selected = true;

        Invalidate();
    }

    /// <summary>
    /// Converts the current tab alignment to the corresponding dock style.
    /// </summary>
    /// <returns>The <see cref="DockStyle"/> corresponding to the tab alignment.</returns>
    private DockStyle AlignmentToDock() => _alignment switch
    {
        TabAlignment.Top => DockStyle.Top,
        TabAlignment.Bottom => DockStyle.Bottom,
        TabAlignment.Left => DockStyle.Left,
        TabAlignment.Right => DockStyle.Right,
        _ => throw new InvalidOperationException("Invalid alignment")
    };

    /// <summary>
    /// Handles the Resize event, triggering a redraw of the tab layout.
    /// </summary>
    private void LealTabManager_Resize(object? sender, EventArgs e) => ReDraw();
    
    /// <summary>
    /// Adds a new tab to the collection and creates a corresponding button
    /// with specific alignment and docking settings. The button is added
    /// to the buttons panel and made visible in the appropriate order.
    /// </summary>
    /// <param name="tab">The <see cref="LealBaseTab"/> instance representing the tab to add.</param>
    private void AddTab(LealBaseTab tab)
    {
        var alignmentTopBottom = _alignment == TabAlignment.Top || _alignment == TabAlignment.Bottom;
        var dock = alignmentTopBottom ? DockStyle.Left : DockStyle.Top;

        var newButton = new LealSelectableButton(_tabs.Count)
        {
            AutoSearch = true,
            Text = tab.TabName,
            Dock = dock,
        };
        newButton.SetAutoWidth();
        newButton.OnSelectButton += TabSelected;

        _tabs.Add(tab);
        _buttonsPanel.Add(newButton);
        newButton.BringToFront();

        var firstButton = _buttonsPanel.GetChildrenOfType<LealSelectableButton>().Last();
        TabSelected(firstButton, null, firstButton.ObjectRef);
        ReDraw();
    }

    /// <summary>
    /// Handles the event when a control is added to the tab manager. If the control is a tab, 
    /// it is added to the tab list, and a corresponding button is created.
    /// </summary>
    private void LealTabManager_ControlAdded(object? sender, ControlEventArgs e)
    {
        if (_loadingNewTab)
        {
            _loadingNewTab = false;
            return;
        }

        if (e.Control == null)
            return;

        _realocatingTabRemotion = true;
        this.Remove(e.Control);

        if (e.Control is LealBaseTab tab)
            AddTab(tab);
    }

    /// <summary>
    /// Handles the event when a control is removed of the tab manager. If the control is a tab, 
    /// it is removed from the tab list, and a corresponding button is also removed.
    /// </summary>
    private void LealTabManager_ControlRemoved(object? sender, ControlEventArgs e)
    {
        if (e.Control is not LealBaseTab lbt)
            return;

        if (_realocatingTabRemotion)
        {
            _realocatingTabRemotion = false;
            return;
        }

        var tabIndex = _tabs.FindIndex(tab => tab.GetHashCode() == lbt.GetHashCode());
        var buttonIndex = _buttonsPanel.GetChildrenOfType<LealSelectableButton>().ToList().FindIndex(btn => btn.ObjectRef is int ob && ob == tabIndex);

        if (tabIndex == -1 || buttonIndex == -1)
            throw new InvalidOperationException($"<{typeof(LealSelectableButton)}> has no reference to a <{typeof(LealBaseTab)}>. Unexpected Error!");

        _tabs.RemoveAt(tabIndex);
        _buttonsPanel.Controls.RemoveAt(buttonIndex);
    }

    /// <summary>
    /// Handles the event when a tab button is selected, displaying the corresponding tab content.
    /// </summary>
    private void TabSelected(LealSelectableButton button, MouseEventArgs? eventArgs, object? control)
    {
        if (control is not int index)
            return;

        if (_selectedTab != null)
        {
            _realocatingTabRemotion = true;
            this.Remove(_selectedTab);
        }

        _selectedTab = _tabs[index];
        _loadingNewTab = true;
        this.Add(_selectedTab);
    }
}