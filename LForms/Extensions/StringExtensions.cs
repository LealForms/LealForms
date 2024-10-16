using System;
using System.Linq;

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
}