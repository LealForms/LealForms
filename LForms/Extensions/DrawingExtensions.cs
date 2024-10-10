using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace LForms.Extensions;

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
    /// Resizes an image to the specified width and height while maintaining high quality.
    /// </summary>
    /// <param name="image">The source <see cref="Image"/> to resize.</param>
    /// <param name="newWidth">The target width of the resized image.</param>
    /// <param name="newHeight">The target height of the resized image.</param>
    /// <returns>A new <see cref="Bitmap"/> instance of the resized image.</returns>
    /// <remarks>
    /// This method applies high-quality settings for smoothing, pixel offset, and interpolation to ensure
    /// the resized image maintains visual quality. The <see cref="WrapMode.TileFlipXY"/> wrap mode is used to
    /// prevent the appearance of seams when tiles are flipped both horizontally and vertically.
    /// </remarks>
    public static Bitmap ResizeImage(this Image image, int newWidth, int newHeight)
    {
        var destRect = new Rectangle(0, 0, newWidth, newHeight);
        var targetImage = new Bitmap(newWidth, newHeight);

        targetImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (var g = Graphics.FromImage(targetImage))
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            using var wrapMode = new ImageAttributes();

            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            g.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        }

        return targetImage;
    }
}