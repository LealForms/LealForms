using LForms.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Extensions;

public static class ExceptionsExtensions
{
    /// <summary>
    /// Displays an error dialog with a message based on the specified <see cref="ErrorType"/>.
    /// Provides options to retry or cancel the current operation.
    /// </summary>
    /// <param name="exception">The exception that was thrown.</param>
    /// <param name="errorType">The type of error to categorize the exception.</param>
    /// <returns>A <see cref="DialogResult"/> indicating the user's choice.</returns>
    public static DialogResult HandleException(this Exception exception, ErrorType errorType)
    {
        var message = errorType switch
        {
            ErrorType.Warning => $"{errorType}: A non-critical issue occurred.",
            ErrorType.Process => $"{errorType} error occurred. You may retry or cancel the current operation.",
            ErrorType.Critical => $"{errorType} error occurred. The application may not function correctly.",
            _ => "An unexpected error occurred. The application may not function correctly.",
        };

        return MessageBox.Show(
            $"{message}\n\nDetails: {exception.Message}",
            $"{errorType} Error",
            errorType == ErrorType.Critical ? MessageBoxButtons.OK : MessageBoxButtons.RetryCancel,
            errorType == ErrorType.Warning ? MessageBoxIcon.Warning : MessageBoxIcon.Error,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.ServiceNotification
        );
    }
}