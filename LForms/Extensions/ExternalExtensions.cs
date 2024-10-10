using System;
using System.Runtime.InteropServices;

namespace LForms.Extensions;

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
}