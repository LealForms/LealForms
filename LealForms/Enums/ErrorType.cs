namespace LealForms.Enums;

/// <summary>
/// Specifies the type of error that can occur in the application.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Represents a non-critical issue that may require attention but does not prevent the application from continuing.
    /// </summary>
    Warning = 1,

    /// <summary>
    /// Represents an error related to a process that may need intervention, but the application may still function.
    /// </summary>
    Process = 2,

    /// <summary>
    /// Represents a critical error that prevents the application from continuing to function correctly.
    /// </summary>
    Critical = 3,
}