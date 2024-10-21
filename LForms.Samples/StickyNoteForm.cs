using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Controls.Panels;
using LForms.Enums;
using LForms.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Samples;

public class StickyNoteForm : LealForm
{
    public override void LoadComponents()
    {
        // Lets create the left panel, with a gradient to get some stylish
        var leftPanel = new LealGradientPanel()
        {
            Width = 200,
            Dock = DockStyle.Left,
            TopLeftGradientColor = Color.Blue,
            TopRightGradientColor = Color.Blue,
            BottomLeftGradientColor = Color.White,
            BottomRightGradientColor = Color.White,
        };
        this.Add(leftPanel); // Simplified method to add controls (no more 'object'.Controls.Add())

        // Lets add some selectable buttons without real functionality
        var button1 = new LealSelectableButton()
        {
            Text = "First button",
            AutoSearch = true, // This enables autoseach, means that if you click it will automatically search for others LealSelectableButtons to change their colors
            Selected = true, // Initialize button1 selected
            BorderSize = 0,
            MouseHoverColor = Color.Red,
            SelectedColor = Color.DarkRed,
            UnSelectedColor = Color.Transparent,
        };
        leftPanel.Add(button1);
        button1.HorizontalCentralize(); // Centralize horizontally the button to it parent
        
        var button2 = new LealSelectableButton()
        {
            Text = "Second button",
            AutoSearch = true,
            BorderSize = 0,
            MouseHoverColor = Color.Red,
            SelectedColor = Color.DarkRed,
            UnSelectedColor = Color.Transparent,
        };
        leftPanel.Add(button2);
        button2.HorizontalCentralize();

        var button3 = new LealSelectableButton()
        {
            Text = "Third button",
            AutoSearch = true,
            BorderSize = 0,
            MouseHoverColor = Color.Red,
            SelectedColor = Color.DarkRed,
            UnSelectedColor = Color.Transparent,
        };
        leftPanel.Add(button3);
        button2.HorizontalCentralize();

        // This creates and waterfall of all the LealSelectableButtons on Y axis, starting at 50, with 10 of padding between each one
        //
        // Button1
        // 
        // Button2
        //
        // Button 3
        leftPanel.WaterFallChildControlsOfTypeByY<LealSelectableButton>(50, 10);

        // Right panel creation
        var rightPanel = new LealPanel()
        {
            Dock = DockStyle.Fill,
        };
        this.Add(rightPanel);
        rightPanel.BringToFront();

        var messageButton = new LealButton()
        {
            Text = "Button Test",
            Rounded = true, // Gives the button rounded edges
        };
        messageButton.Click += (s, e) => ExecuteImportantThing();
        rightPanel.Add(messageButton);
        messageButton.Centralize(); // Centralize vertically and horizontally the button to it parent
    }

    private void ExecuteImportantThing()
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            var dialogResult = this.HandleException(e, ErrorType.Process);

            if (dialogResult == DialogResult.Retry)
                ExecuteImportantThing();
        }
    }
}