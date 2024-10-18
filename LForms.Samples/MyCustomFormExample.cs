using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Controls.Mischellaneous;
using LForms.Controls.Panels;
using LForms.Controls.TextBoxes;
using LForms.Extensions;

namespace LForms.Samples;

internal sealed class MyCustomFormExample : LealForm
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
        var gradientPanel = new LealGradientPanel()
        {
            Dock = DockStyle.Fill,
            TopLeftGradientColor = Color.AliceBlue,
            TopRightGradientColor = Color.AliceBlue,
            BottomLeftGradientColor = Color.Black,
            BottomRightGradientColor = Color.Black,
        };

        tab1.Add(gradientPanel);

        var tab2 = new LealTab()
        {
            TabName = "Tab2",
            BackColor = Color.Bisque,
        };
        var panel2 = new Panel()
        {
            AutoSize = false,
            Dock = DockStyle.Fill,
        };
        tab2.Add(panel2);
        var btn1 = new LealButton() { Text = "btn1" };
        var btn2 = new LealButton() { Text = "btn2" };
        var textBox = new LealTextBox();
        panel2.Add(btn1);
        panel2.Add(btn2);
        panel2.Add(textBox);

        lealTabManager.Add(tab1);
        lealTabManager.Add(tab2);
        btn1.HorizontalCentralize(panel2);
        btn2.HorizontalCentralize(panel2);
        textBox.HorizontalCentralize(panel2);
        btn1.BorderSize = 0;
        btn2.BorderSize = 0;
        btn1.GenerateCustomRoundRegion(20, true, true, false, false);
        btn2.GenerateCustomRoundRegion(20, false, false, true, true);
        textBox.GenerateRoundRegion();
        panel2.Controls.WaterFallControlsOfType<Control>(200, 20);
    }
}