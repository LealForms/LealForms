using LealForms.Enums;
using System;
using System.Windows.Forms;

namespace LealForms.Extensions;

/// <summary>
/// Custom extensions functions for <see cref="Exception"/>s
/// </summary>
public static class ExceptionsExtensions
{
    /// <summary>
    /// Displays an error dialog with a message based on the specified <see cref="ErrorType"/> and the given exception details.
    /// Provides options to retry or cancel the current operation, or simply acknowledge depending on the error type.
    /// </summary>
    /// <param name="form">The current <see cref="Form"/> displaying the dialog.</param>
    /// <param name="exception">The exception that was thrown.</param>
    /// <param name="errorType">The type of error to categorize the exception.</param>
    /// <param name="additionalMessage">Extra message information.</param>
    /// <returns>A <see cref="DialogResult"/> indicating the user's choice.</returns>
    public static DialogResult HandleException(this Form form, Exception exception, ErrorType errorType, string additionalMessage = "")
    {
        var message = errorType.GetMessageByError();

        return MessageBox.Show(
            form,
            $"{message}\n\nDetails: {exception.Message}",
            $"{errorType} Error",
            errorType == ErrorType.Critical ? MessageBoxButtons.OK : MessageBoxButtons.RetryCancel,
            errorType == ErrorType.Warning ? MessageBoxIcon.Warning : MessageBoxIcon.Error,
            MessageBoxDefaultButton.Button1
        );
    }

    /// <summary>
    /// Displays an error dialog with a message based on the specified <see cref="ErrorType"/>.
    /// Provides options to retry or cancel the current operation.
    /// </summary>
    /// <param name="exception">The exception that was thrown.</param>
    /// <param name="errorType">The type of error to categorize the exception.</param>
    /// <param name="additionalMessage">Extra message information.</param>
    /// <returns>A <see cref="DialogResult"/> indicating the user's choice.</returns>
    public static DialogResult HandleException(this Exception exception, ErrorType errorType, string additionalMessage = "")
    {
        var message = errorType.GetMessageByError();

        return MessageBox.Show(
            $"{message}\n{additionalMessage}\n\nDetails: {exception.Message}",
            $"{errorType} Error",
            errorType == ErrorType.Critical ? MessageBoxButtons.OK : MessageBoxButtons.RetryCancel,
            errorType == ErrorType.Warning ? MessageBoxIcon.Warning : MessageBoxIcon.Error,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.ServiceNotification
        );
    }

    /// <summary>
    /// Retrieves the appropriate error message based on the specified <see cref="ErrorType"/>.
    /// </summary>
    /// <param name="errorType">The type of error.</param>
    /// <returns>A string representing the message associated with the error type.</returns>
    public static string GetMessageByError(this ErrorType errorType) => errorType switch
    {
        ErrorType.Warning => $"{errorType}: A non-critical issue occurred.",
        ErrorType.Process => $"{errorType} error occurred. You may retry or cancel the current operation.",
        ErrorType.Critical => $"{errorType} error occurred. The application may not function correctly.",
        _ => "An unexpected error occurred. The application may not function correctly.",
    };
}