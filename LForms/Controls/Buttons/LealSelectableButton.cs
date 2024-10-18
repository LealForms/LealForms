using LForms.Controls.Base;
using LForms.Extensions;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Buttons;

/// <summary>
/// Represents a selectable button that can change its appearance based on its selected state and trigger an event when selected.
/// Inherits from <see cref="LealBaseButton"/>.
/// </summary>
public class LealSelectableButton : LealBaseButton
{
    /// <summary>
    /// Delegate for handling the <see cref="OnSelectButton"/> event when the button is selected.
    /// </summary>
    /// <param name="button">The <see cref="LealSelectableButton"/> instance that was selected.</param>
    /// <param name="eventArgs">The <see cref="MouseEventArgs"/> associated with the selection.</param>
    /// /// <param name="objectRef">The optional value associated with the selection.</param>
    public delegate void OnSelected(LealSelectableButton button, MouseEventArgs? eventArgs, object? objectRef);

    /// <summary>
    /// Occurs when the button is selected.
    /// </summary>
    public event OnSelected? OnSelectButton;

    private bool _selected = false;
    private Color _selectedColor = Color.Blue;
    private Color _unselectedColor = Color.White;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealSelectableButton"/> class and subscribes to the MouseClick event.
    /// </summary>
    public LealSelectableButton()
    {
        MouseClick += LealSelectableButton_MouseClick;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LealSelectableButton"/> class and subscribes to the MouseClick event.
    /// Also assign a object reference passed into the <see cref="OnSelectButton"/> event.
    /// </summary>
    public LealSelectableButton(object? objectReference)
    {
        ObjectRef = objectReference;
        MouseClick += LealSelectableButton_MouseClick;
    }

    /// <summary>
    /// Gets a object reference that will be passed into the <see cref="OnSelectButton"/> event.
    /// </summary>
    public object? ObjectRef { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the button should automatically deselect other <see cref="LealSelectableButton"/> controls in its parent.
    /// </summary>
    [Category("Behavior")]
    [Description("Indicates whether the button automatically deselects other LealSelectableButton controls in its parent container.")]
    public bool AutoSearch { get; set; } = false;

    /// <summary>
    /// Gets or sets the color of the button when it is not selected.
    /// Changing this value triggers a redraw of the button.
    /// </summary>
    [Category("Appearance")]
    [Description("The background color of the button when it is not selected.")]
    public Color UnSelectedColor
    {
        get => _unselectedColor;
        set
        {
            _unselectedColor = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the color of the button when it is selected.
    /// Changing this value triggers a redraw of the button.
    /// </summary>
    [Category("Appearance")]
    [Description("The background color of the button when it is selected.")]
    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            _selectedColor = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the button is selected.
    /// Changing this value updates the button's appearance and triggers a redraw.
    /// </summary>
    [Category("Behavior")]
    [Description("Indicates whether the button is selected. When selected, the button's background color changes to the SelectedColor.")]
    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Redraws the button based on its selected state, updating the background color and invalidating the control.
    /// </summary>
    protected override void ReDraw()
    {
        BackColor = Selected ? _selectedColor : _unselectedColor;
        Invalidate();
    }

    /// <summary>
    /// Handles the MouseClick event. Selects the button and triggers the <see cref="OnSelectButton"/> event.
    /// If <see cref="AutoSearch"/> is enabled, deselects all other <see cref="LealSelectableButton"/> controls in the parent container.
    /// </summary>
    /// <param name="sender">The source of the event, typically the button itself.</param>
    /// <param name="e">The <see cref="MouseEventArgs"/> that contains the event data.</param>
    private void LealSelectableButton_MouseClick(object? sender, MouseEventArgs e)
    {
        if (AutoSearch)
        {
            var selectableButtons = Parent?.GetChildsOfType<LealSelectableButton>()!;

            foreach (var selectableBtn in selectableButtons)
            {
                if (selectableBtn.AutoSearch)
                    selectableBtn.Selected = false;
            }
        }

        Selected = true;
        OnSelectButton?.Invoke(this, e, ObjectRef);
    }
}