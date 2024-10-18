using System.Drawing;

namespace LForms;

/// <summary>
/// This class defines constants used in the Extensions classes for window messages, parameters etc.
/// </summary>
public static class LealConstants
{
    /// <summary>
    /// Default Form Width
    /// </summary>
    public static readonly int DEFAULT_WIDTH = 1600;

    /// <summary>
    /// Default Form Height
    /// </summary>
    public static readonly int DEFAULT_HEIGHT = 900;

    /// <summary>
    /// Default gap space between controls
    /// </summary>
    public static readonly int GAP = 20;

    /// <summary>
    /// The default curve radius (in pixels) used for creating rounded regions in a <see cref="Region"/>.
    /// This value determines the smoothness of the corner rounding.
    /// </summary>
    public static readonly int ELIPSE_CURVE = 25;

    /// <summary>
    /// Message indicating that the left mouse button was pressed in the non-client area of a window.
    /// </summary>
    public static readonly int WM_NCLBUTTONDOWN = 0xA1;

    /// <summary>
    /// Message used to determine what part of a window corresponds to a specific point, used for hit testing.
    /// </summary>
    public static readonly int WM_NCHITTEST = 0x84;

    /// <summary>
    /// Constant representing the DWM window attribute for dark mode before Windows 10 version 20H1.
    /// </summary>
    public static readonly int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;

    /// <summary>
    /// Constant representing the DWM window attribute for dark mode after Windows 10 version 20H1.
    /// </summary>
    public static readonly int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

    /// <summary>
    /// Hit-test value indicating the title bar or caption area of a window.
    /// </summary>
    public static readonly int HT_CAPTION = 0x2;

    /// <summary>
    /// Hit-test value for the bottom border of a window.
    /// </summary>
    public static readonly int HTBOTTOM = 15;

    /// <summary>
    /// Hit-test value for the right border of a window.
    /// </summary>
    public static readonly int HTRIGHT = 11;

    /// <summary>
    /// Hit-test value for the bottom-right corner of a window.
    /// </summary>
    public static readonly int HTBOTTOMRIGHT = 17;

    /// <summary>
    /// Hit-test value for the top border of a window.
    /// </summary>
    public static readonly int HTTOP = 12;

    /// <summary>
    /// Hit-test value for the top-right corner of a window.
    /// </summary>
    public static readonly int HTTOPRIGHT = 14;

    /// <summary>
    /// Hit-test value for the top-left corner of a window.
    /// </summary>
    public static readonly int HTTOPLEFT = 13;

    /// <summary>
    /// Hit-test value for the left border of a window.
    /// </summary>
    public static readonly int HTLEFT = 10;

    /// <summary>
    /// Hit-test value for the bottom-left corner of a window.
    /// </summary>
    public static readonly int HTBOTTOMLEFT = 16;
}