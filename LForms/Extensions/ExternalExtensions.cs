using System;
using System.Runtime.InteropServices;

namespace LealForms.Extensions;

/// <summary>
/// Custom extensions functions for extern DllImport callers
/// </summary>
public static class ExternalExtensions
{
    /// <summary>
    /// Releases the current mouse capture.
    /// </summary>
    /// <returns>Returns true if the operation was successful.</returns>
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    /// <summary>
    /// Sends a message to the specified window or windows.
    /// </summary>
    /// <param name="hWnd">Handle to the window.</param>
    /// <param name="Msg">The message to send.</param>
    /// <param name="wParam">Additional message-specific information.</param>
    /// <param name="lParam">Additional message-specific information.</param>
    /// <returns>Result of the message processing.</returns>
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    /// <summary>
    /// Creates a region with rounded corners.
    /// </summary>
    /// <param name="nLeftRect">Left coordinate of the rectangle.</param>
    /// <param name="nTopRect">Top coordinate of the rectangle.</param>
    /// <param name="nRightRect">Right coordinate of the rectangle.</param>
    /// <param name="nBottomRect">Bottom coordinate of the rectangle.</param>
    /// <param name="nWidthEllipse">Width of the ellipse used for rounding.</param>
    /// <param name="nHeightEllipse">Height of the ellipse used for rounding.</param>
    /// <returns>Handle to the created region.</returns>
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    
    /// <summary>
    /// Sets the value of a specified attribute for a window using the Desktop Window Manager (DWM) API.
    /// </summary>
    /// <param name="hwnd">A handle to the window for which the attribute will be set.</param>
    /// <param name="attr">The attribute to set. This is specified as an integer and corresponds to a DWM attribute.</param>
    /// <param name="attrValue">A reference to the value of the attribute to set.</param>
    /// <param name="attrSize">The size, in bytes, of the <paramref name="attrValue"/>.</param>
    /// <returns>Returns an integer indicating the success or failure of the function. Zero indicates success, while a non-zero value indicates an error.</returns>
    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    /// <summary>
    /// Checks if the system's theme is set to dark mode.
    /// </summary>
    /// <returns>True if the system is using dark mode; otherwise, false.</returns>
    [DllImport("UXTheme.dll", SetLastError = true, EntryPoint = "#138")]
    public static extern bool ShouldSystemUseDarkMode();
}