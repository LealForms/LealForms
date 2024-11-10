using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Controls.Panels;
using LForms.Extensions;
using LForms.Samples.StickyNotes;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace LForms.Samples;

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
            Width = 50
        };
        backPanel.Add(stickyNoteButton);
        stickyNoteButton.HorizontalCentralize();

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