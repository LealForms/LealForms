using LForms.Enums.MessageBox;
using System.Globalization;
using System.Windows.Forms;

namespace LForms.Models;

/// <summary>
/// Represents a button used in a message box with predefined or custom configurations.
/// </summary>
public sealed class LealMessageBoxButton
{
    /// <summary>
    /// Gets a button configured as "OK".
    /// </summary>
    public static LealMessageBoxButton Ok => new(ButtonType.Ok, "Ok", DialogResult.OK);

    /// <summary>
    /// Gets a button configured as "Cancel".
    /// </summary>
    public static LealMessageBoxButton Cancel => new(ButtonType.Cancel, "Cancel", DialogResult.Cancel);

    /// <summary>
    /// Gets a button configured as "Yes".
    /// </summary>
    public static LealMessageBoxButton Yes => new(ButtonType.Yes, "Yes", DialogResult.Yes);

    /// <summary>
    /// Gets a button configured as "No".
    /// </summary>
    public static LealMessageBoxButton No => new(ButtonType.No, "No", DialogResult.No);

    /// <summary>
    /// Gets a button configured as "Abort".
    /// </summary>
    public static LealMessageBoxButton Abort => new(ButtonType.Abort, "Abort", DialogResult.Abort);

    /// <summary>
    /// Gets a button configured as "Retry".
    /// </summary>
    public static LealMessageBoxButton Retry => new(ButtonType.Retry, "Retry", DialogResult.Retry);

    /// <summary>
    /// Gets a button configured as "Ignore".
    /// </summary>
    public static LealMessageBoxButton Ignore => new(ButtonType.Ignore, "Ignore", DialogResult.Ignore);

    /// <summary>
    /// Gets a button configured as "Try Again".
    /// </summary>
    public static LealMessageBoxButton TryAgain => new(ButtonType.TryAgain, "Try Again", DialogResult.Retry);

    /// <summary>
    /// Gets a button configured as "Continue".
    /// </summary>
    public static LealMessageBoxButton Continue => new(ButtonType.Continue, "Continue", DialogResult.Ignore);

    /// <summary>
    /// Creates a custom message box button with specified text and dialog result.
    /// </summary>
    /// <param name="buttonText">The text displayed on the button.</param>
    /// <param name="dialogResult">The dialog result associated with the button.</param>
    /// <returns>A custom <see cref="LealMessageBoxButton"/> instance.</returns>
    public static LealMessageBoxButton Custom(string buttonText, DialogResult dialogResult)
        => new(ButtonType.Custom, buttonText, dialogResult);

    /// <summary>
    /// Initializes a new instance of the <see cref="LealMessageBoxButton"/> class with the specified type, text, and dialog result.
    /// </summary>
    /// <param name="type">The type of the button.</param>
    /// <param name="buttonText">The text displayed on the button.</param>
    /// <param name="dialogResult">The dialog result associated with the button.</param>
    public LealMessageBoxButton(ButtonType type, string buttonText, DialogResult dialogResult)
    {
        Type = type;
        ButtonText = buttonText;
        DialogResult = dialogResult;

        switch (Culture.DisplayName)
        {
            case "en-US":
                break;
            case "pt-BR":
                switch (type)
                {
                    case ButtonType.Ok:
                        ButtonText = "Ok";
                        break;
                    case ButtonType.Cancel:
                        ButtonText = "Cancelar";
                        break;
                    case ButtonType.Yes:
                        ButtonText = "Sim";
                        break;
                    case ButtonType.No:
                        ButtonText = "Não";
                        break;
                    case ButtonType.Abort:
                        ButtonText = "Abortar";
                        break;
                    case ButtonType.Retry:
                        ButtonText = "Repetir";
                        break;
                    case ButtonType.Ignore:
                        ButtonText = "Ignorar";
                        break;
                    case ButtonType.TryAgain:
                        ButtonText = "Tentar Novamente";
                        break;
                    case ButtonType.Continue:
                        ButtonText = "Continuar";
                        break;
                    case ButtonType.Custom:
                        ButtonText = buttonText;
                        break;
                }
                break;
        }
    }

    /// <summary>
    /// Gets the type of the button.
    /// </summary>
    public ButtonType Type { get; }

    /// <summary>
    /// Gets the text displayed on the button.
    /// </summary>
    public string ButtonText { get; }

    /// <summary>
    /// Gets the dialog result associated with the button.
    /// </summary>
    public DialogResult DialogResult { get; }

    /// <summary>
    /// Gets or sets the culture used to localize button text.
    /// Defaults to the current culture of the environment.
    /// </summary>
    public static CultureInfo Culture { get; set; } = CultureInfo.CurrentCulture;
}