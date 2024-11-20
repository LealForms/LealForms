using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LForms.Extensions;

/// <summary>
/// Custom extensions functions for <see cref="string"/>s
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Counts the number of lines in the given text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <returns>The number of lines in the text.</returns>
    public static int GetLinesInText(this string text)
    {
        text = text.Replace("\r\n", "\n").Replace("\r", "\n");
        return text.Count(c => c.Equals('\n') || c.Equals('\r')) + 1;
    }

    /// <summary>
    /// Measures and returns the size of the specified text when rendered with the provided font.
    /// </summary>
    /// <param name="text">The string of text to measure.</param>
    /// <param name="font">The <see cref="Font"/> used to render the text.</param>
    /// <returns>A <see cref="Size"/> structure representing the width and height of the rendered text.</returns>
    public static Size GetTextSize(this string text, Font font)
        => TextRenderer.MeasureText(text, font);

    /// <summary>
    /// Replaces all newline characters in the string with the specified replacement value.
    /// </summary>
    /// <param name="text">The string in which newline characters will be replaced.</param>
    /// <param name="value">The value to replace all newline characters with.</param>
    /// <returns>A new string with all newline characters replaced by the specified value.</returns>
    public static string ReplaceNewLineBy(this string text, string value)
        => text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", value);

    /// <summary>
    /// Determines whether a specified string is <c>null</c>, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="text">The string to evaluate.</param>
    /// <returns>
    /// <c>true</c> if the string is <c>null</c>, empty, or consists only of white-space characters; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmptyOrWhiteSpace([NotNullWhen(false)] this string? text)
        => string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
}