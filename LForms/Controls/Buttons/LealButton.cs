using LForms.Controls.Base;
using System;

namespace LForms.Controls.Buttons;

/// <summary>
/// Represents a basic implementation of the <see cref="LealBaseButton"/> class.
/// </summary>
public class LealButton : LealBaseButton
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealButton"/> class.
    /// </summary>
    public LealButton() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LealButton"/> class with a specified click event handler.
    /// </summary>
    /// <param name="onclickHandler">The event handler that will be called when the button is clicked.</param>
    public LealButton(EventHandler onclickHandler) : base(onclickHandler) { }
}