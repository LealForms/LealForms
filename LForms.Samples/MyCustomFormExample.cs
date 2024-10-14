using LForms.Controls.Base;
using LForms.Controls.Mischellaneous;
using LForms.Extensions;

namespace LForms.Samples;

internal sealed class MyCustomFormExample : LealBaseForm
{
    public override void LoadComponents()
    {
        var lealTabManager = new LealTabManager()
        {
            Dock = DockStyle.Fill,
        };
        this.Add(lealTabManager);

        var tab1 = new LealTab()
        {
            TabName = "Tab1",
            BackColor = Color.AntiqueWhite,
        };
        var labelTab1 = new Label()
        {
            Text = "tab1",
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
        };
        tab1.Add(labelTab1);

        lealTabManager.Add(tab1);

        var tab2 = new LealTab()
        {
            TabName = "Tab2",
            BackColor = Color.Bisque,
        };
        var labelTab2 = new Label()
        {
            Text = "tab2",
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
        };
        tab2.Add(labelTab2);
        lealTabManager.Add(tab2);
    }
}