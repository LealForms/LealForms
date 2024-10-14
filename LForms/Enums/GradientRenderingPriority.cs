namespace LForms.Enums;

/// <summary>
/// Specifies the rendering priority for the gradient, balancing quality and performance.
/// </summary>
public enum GradientRenderingPriority
{
    /// <summary>
    /// Prioritizes rendering speed over quality by generating a low-resolution gradient and scaling it up.
    /// </summary>
    HighSpeed,

    /// <summary>
    /// Balances rendering speed and quality by generating a medium-resolution gradient.
    /// </summary>
    Balanced,

    /// <summary>
    /// Prioritizes rendering quality over speed by generating a high-resolution gradient.
    /// </summary>
    HighQuality
}