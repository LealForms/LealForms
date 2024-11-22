namespace LForms.Enums.MessageBox;

/// <summary>
/// Specifies the type of buttons to display in a message box.
/// </summary>
public enum ButtonType
{
    /// <summary>
    /// The message box contains an OK button.
    /// </summary>
    OK,

    /// <summary>
    /// The message box contains OK and Cancel buttons.
    /// </summary>
    OKCancel,

    /// <summary>
    /// The message box contains a Close button.
    /// </summary>
    Close,

    /// <summary>
    /// The message box contains Yes and No buttons.
    /// </summary>
    YesNo,

    /// <summary>
    /// The message box contains Yes, No, and Cancel buttons.
    /// </summary>
    YesNoCancel,

    /// <summary>
    /// The message box contains Retry and Cancel buttons.
    /// </summary>
    RetryCancel,

    /// <summary>
    /// The message box contains Abort, Retry, and Ignore buttons.
    /// </summary>
    AbortRetryIgnore,

    /// <summary>
    /// The message box contains Continue button.
    /// </summary>
    Continue,

    /// <summary>
    /// The message box contains Cancel, Try Again, and Continue buttons.
    /// </summary>
    CancelTryAgainContinue,

    /// <summary>
    /// The message box contains a custom button.
    /// </summary>
    Custom,
}