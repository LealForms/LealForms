using LealForms.Controls.Buttons;
using LealForms.Controls.Forms;
using LealForms.Controls.Miscellaneous;
using LealForms.Controls.Panels;
using LealForms.Enums.Checkbox;
using LealForms.Enums.Switch;
using LealForms.Extensions;
using LealForms.Models;
using LealForms.Samples.StickyNotes;
using System.Drawing;
using System.Windows.Forms;

namespace LealForms.Samples;

public class ExamplesForm : LealForm
{
    private LealForm? _currentApp;

    public override void LoadComponents()
    {
        this.TrySetDarkMode();
        Text = "LealForms | Examples";
        Size = new Size(400, 600);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        var screen = Screen.PrimaryScreen;

        if (screen == null)
            StartPosition = FormStartPosition.CenterScreen;
        else
        {
            StartPosition = FormStartPosition.Manual;
            Location = new Point(screen.WorkingArea.X + 200, screen.WorkingArea.Y + 200);
        }

        var backPanel = new LealGradientPanel(true)
        {
            Dock = DockStyle.Fill,
            TopLeftGradientColor = Color.FromArgb(32, 32, 32),
            TopRightGradientColor = Color.FromArgb(32, 32, 32),
            BottomLeftGradientColor = Color.AliceBlue,
            BottomRightGradientColor = Color.AliceBlue,
        };
        this.Add(backPanel);

        var stickyNoteButton = new LealButton((s, e) => SwapExample(new StickyNotesForm()))
        {
            Text = "Sticky Notes",
            Width = 250
        };
        backPanel.Add(stickyNoteButton);
        stickyNoteButton.HorizontalCentralize();

        var checkbox = new LealCheckbox()
        {
            Text = "Test",
            ForeColor = Color.Black,
            CheckboxStyle = CheckboxStyle.Square,
            CheckboxLabelAlignment = CheckboxLabelAlignment.CheckBoxRightLabelLeft
        };
        backPanel.Add(checkbox);

        var switchButton = new LealSwitch()
        {
            SwitchOrientation = SwitchOrientation.Horizontal,
        };
        backPanel.Add(switchButton);
        switchButton.SetXAfterControl(checkbox, 10);

        var lealCombo = new LealCombo()
        {
            DropdownItemHeight = 50,
            DropdownBackColor = Color.Black,
            DropdownForeColor = Color.White,
        };
        backPanel.Add(lealCombo);
        lealCombo.SetXAfterControl(switchButton, 10);
        lealCombo.AddItem(new LealComboItem("Test 1", 1));
        lealCombo.AddItem(new LealComboItem("Test 2", 2));
        lealCombo.AddItem(new LealComboItem("Test 3", 3));
        lealCombo.AddItem(new LealComboItem("Test 4", 4));
        lealCombo.AddItem(new LealComboItem("Test 5", 5));

        var separator = new LealSeparator()
        {
            Height = 2,
            Orientation = Orientation.Horizontal,
        };
        backPanel.Add(separator);
        separator.DockLeftRightWithPadding(0, 0);
        separator.AddY(backPanel.Height - separator.Height - 30);

        backPanel.CentralizeWithSpacingChildrensOfTypeByY<LealButton>(25);
    }

    private void SwapExample(LealForm form)
    {
        if (_currentApp != null)
        {
            _currentApp.Close();
            _currentApp.Dispose();
            _currentApp = null;
        }

        _currentApp = form;
        _currentApp.Show(this);
    }
}