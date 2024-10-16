using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Collections.Generic;

namespace LForms.Extensions;

/// <summary>
/// Custom extensions functions for everything related to <see cref="System.Drawing"/>
/// </summary>
public static class DrawingExtensions
{
    /// <summary>
    /// Adjusts the brightness of the specified color by darkening it based on a given factor.
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to be darkened.</param>
    /// <param name="darkeningFactor">
    /// A value between 0 and 1 that represents the degree of darkening. 
    /// A value of 0 returns the original color, while a value of 1 returns black.
    /// </param>
    /// <returns>
    /// A new <see cref="Color"/> that is a darker version of the original, with the same alpha component.
    /// </returns>
    public static Color Darken(this Color color, double darkeningFactor)
    {
        darkeningFactor = Math.Clamp(darkeningFactor, 0, 1);

        var r = (int)(color.R * (1 - darkeningFactor));
        var g = (int)(color.G * (1 - darkeningFactor));
        var b = (int)(color.B * (1 - darkeningFactor));

        r = Math.Clamp(r, 0, 255);
        g = Math.Clamp(g, 0, 255);
        b = Math.Clamp(b, 0, 255);

        return Color.FromArgb(color.A, r, g, b);
    }

    /// <summary>
    /// Resizes the given image to the specified dimensions, optionally using high-quality rendering settings.
    /// </summary>
    /// <param name="image">The source <see cref="Image"/> to resize.</param>
    /// <param name="newWidth">The desired width of the resized image.</param>
    /// <param name="newHeight">The desired height of the resized image.</param>
    /// <param name="highQuality">
    /// A boolean value that indicates whether to use high-quality rendering settings. 
    /// If <c>true</c>, the method applies high-quality settings; otherwise, it uses faster, lower-quality settings.
    /// </param>
    /// <returns>A resized <see cref="Bitmap"/> with the specified dimensions.</returns>
    /// <remarks>
    /// The method maintains the original image's resolution and uses different graphics settings based on 
    /// the <paramref name="highQuality"/> parameter to either prioritize quality or performance.
    /// </remarks>
    public static Bitmap ResizeImage(this Image image, int newWidth, int newHeight, bool highQuality = true)
    {
        var destRect = new Rectangle(0, 0, newWidth, newHeight);
        var destImage = new Bitmap(newWidth, newHeight);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using var graphics = Graphics.FromImage(destImage);

        graphics.CompositingMode = CompositingMode.SourceCopy;

        if (highQuality)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }
        else
        {
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.PixelOffsetMode = PixelOffsetMode.None;
        }

        using var wrapMode = new ImageAttributes();
        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

        return destImage;
    }

    /// <summary>
    /// Generates a 2D gradient bitmap for a given rectangle by blending four corner colors using a PathGradientBrush.
    /// </summary>
    /// <param name="rect">The rectangle area to fill with the gradient.</param>
    /// <param name="colorTopLeft">Color at the top-left corner.</param>
    /// <param name="colorTopRight">Color at the top-right corner.</param>
    /// <param name="colorBottomRight">Color at the bottom-right corner.</param>
    /// <param name="colorBottomLeft">Color at the bottom-left corner.</param>
    /// <returns>A Bitmap containing the gradient.</returns>
    public static Bitmap Gradient2D(this Rectangle rect, Color colorTopLeft, Color colorTopRight, Color colorBottomRight, Color colorBottomLeft)
    {
        if (rect.Width == 0)
            rect.Width = 1;

        if (rect.Height == 0)
            rect.Height = 1;

        var colors = new List<Color> { colorTopLeft, colorTopRight, colorBottomRight, colorBottomLeft };
        var bmp = new Bitmap(rect.Width, rect.Height);

        using var g = Graphics.FromImage(bmp);
        using var pgb = new PathGradientBrush(GetCorners(rect).ToArray())
        {
            CenterColor = CalculateCentralColor(colors),
            SurroundColors = [.. colors]
        };

        g.FillRectangle(pgb, rect);

        return bmp;
    }

    /// <summary>
    /// Blends two colors together based on a specified blend ratio.
    /// </summary>
    /// <param name="firstColor">The starting color.</param>
    /// <param name="secondColor">The ending color.</param>
    /// <param name="blendRatio">A float between 0 and 1 representing the blend weight.</param>
    /// <returns>The blended Color.</returns>
    public static Color BlendColors(this Color firstColor, Color secondColor, float blendRatio)
    {
        blendRatio = Math.Clamp(blendRatio, 0f, 1f);
        byte a = (byte)(firstColor.A + (secondColor.A - firstColor.A) * blendRatio);
        byte r = (byte)(firstColor.R + (secondColor.R - firstColor.R) * blendRatio);
        byte g = (byte)(firstColor.G + (secondColor.G - firstColor.G) * blendRatio);
        byte b = (byte)(firstColor.B + (secondColor.B - firstColor.B) * blendRatio);

        return Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    /// Retrieves a list of the four corner points of a rectangle in a specific order.
    /// </summary>
    /// <param name="rect">The rectangle from which to get the corners.</param>
    /// <returns>A list of PointF representing the corners of the rectangle.</returns>
    public static List<PointF> GetCorners(this RectangleF rect) =>
    [
        rect.Location,
        new PointF(rect.Right, rect.Top),
        new PointF(rect.Right, rect.Bottom),
        new PointF(rect.Left, rect.Bottom)
    ];

    /// <summary>
    /// Calculates the average (central) color from a list of colors by averaging their ARGB components.
    /// </summary>
    /// <param name="colors">A list of Colors to average.</param>
    /// <returns>The averaged Color.</returns>
    public static Color CalculateCentralColor(this List<Color> colors)
    {
        if (colors.Count != 4)
            throw new ArgumentException("Exactly four colors are required.");

        var avgColor1 = BlendColors(colors[0], colors[2], 0.5f); // Top-left and bottom-right
        var avgColor2 = BlendColors(colors[1], colors[3], 0.5f); // Top-right and bottom-left

        // Blend the two averages to get the central color
        return BlendColors(avgColor1, avgColor2, 0.5f);
    }
}