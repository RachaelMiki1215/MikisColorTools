using System;
using System.Linq;
using System.Drawing;

namespace MikisColorTools
{
    public interface IMikisColorTools
    {
        private static float[] FloatRGB(Color color)
        {
            return new float[] { color.R / 255, color.G / 255, color.B / 255 };
        }

        // TODO: Determine luminance calculation methods which could be used.
        //public enum LuminanceCalcMethod
        //{

        //}

        //public float GetLuminance(LuminanceCalcMethod method, sysDraw.Color color)
        //{
        //    return (float)0.1;
        //}

        //public float GetContrastRatio(LuminanceCalcMethod method, sysDraw.Color color1, sysDraw.Color color2)
        //{
        //    return (float)0.1;
        //}

        /// <summary>
        /// Gets the System.Drawing.Color structure from the HSL (hue-saturation-brightness) model and A (alpha) value.
        /// </summary>
        /// <param name="hue">The hue of the color from 0.0 to 360.0.</param>
        /// <param name="saturation">The saturation of the color from 0.0 to 1.0.</param>
        /// <param name="brightness">The brightness of the color from 0.0 to 1.0.</param>
        /// <param name="alpha">The transparency of the color from 1 to 255. 255 is opaque and 0 is transparent.</param>
        /// <returns>The Color structure.</returns>
        // Based on conversion formula provided in https://www.rapidtables.com/convert/color/hsl-to-rgb.html.
        public static Color ColorFromAhsl(float hue, float saturation, float brightness, byte alpha = 255)
        {
            float C, X, m;
            float[] rgbPrime = new float[] { 0f, 0f, 0f };
            C = (1 - (float)Math.Abs(Convert.ToDecimal(2 * brightness - 1))) * saturation;
            X = C * (1 - (float)Math.Abs((hue / 60) % 2 - 1));
            m = brightness - (C / 2);

            if (hue >= 0f && hue < 60f)
            {
                rgbPrime = new float[] { C, X, 0f };
            }
            else if (hue >= 60f && hue < 120f)
            {
                rgbPrime = new float[] { X, C, 0f };
            }
            else if (hue >= 120f && hue < 180f)
            {
                rgbPrime = new float[] { 0f, C, X };
            }
            else if (hue >= 180f && hue < 240f)
            {
                rgbPrime = new float[] { 0f, X, C };
            }
            else if (hue >= 240f && hue < 300f)
            {
                rgbPrime = new float[] { X, 0f, C };
            }
            else if (hue >= 300f && hue < 360f)
            {
                rgbPrime = new float[] { C, 0f, X };
            }

            return Color.FromArgb(alpha, (int)((rgbPrime[0] + m) * 255),
                (int)((rgbPrime[1] + m) * 255),
                (int)((rgbPrime[2] + m) * 255));
        }

        /// <summary>
        /// Gets the complementary System.Drawing.Color structure of a specified Color structure.
        /// </summary>
        /// <param name="color">The Color structure at which the complementary Color structure is calculated.</param>
        /// <returns>The complementary Color structure.</returns>
        public static Color GetComplementaryColor_SystemDrawingColor(Color color)
        {
            float hue = (color.GetHue() + 180f) / 360f;

            return ColorFromAhsl(hue, color.GetSaturation(), color.GetBrightness(), color.A);
        }

        // TODO: Add ChangeBrightness method for sysDraw.Color.

        // TODO: Add ChangeBrightness method for sysMedia.Color as well.

        // Based on conversion formula provided in https://www.rapidtables.com/convert/color/rgb-to-cmyk.html.
        /// <summary>
        /// Gets the K (key) component value of this System.Drawing.Color structure in the CMYK color model.
        /// </summary>
        /// <param name="color">The System.Drawing.Color structure which the K (key) component should be extracted from.</param>
        /// <returns>The K (key) component value of this color.</returns>
        public static float K(Color color)
        {
            float[] floatRGB = FloatRGB(color);

            return (float)(1 - floatRGB.Max());
        }

        // Based on conversion formula provided in https://www.rapidtables.com/convert/color/rgb-to-cmyk.html.
        /// <summary>
        /// Gets the C (cyan) component value of this System.Drawing.Color structure in the CMYK color model.
        /// </summary>
        /// <param name="color">The System.Drawing.Color structure which the C (cyan) component should be extracted from.</param>
        /// <returns>The C (cyan) component value of this color.</returns>
        public static float C(Color color)
        {
            float[] floatRGB = FloatRGB(color);

            return (1 - floatRGB[0] - K(color)) / (1 - K(color));
        }

        // Based on conversion formula provided in https://www.rapidtables.com/convert/color/rgb-to-cmyk.html.
        /// <summary>
        /// Gets the M (magenta) component value of this System.Drawing.Color structure in the CMYK color model.
        /// </summary>
        /// <param name="color">The System.Drawing.Color structure which the M (magenta) component should be extracted from.</param>
        /// <returns>The M (magenta) component value of this color.</returns>
        public static float M(Color color)
        {
            float[] floatRGB = FloatRGB(color);

            return (1 - floatRGB[1] - K(color)) / (1 - K(color));
        }

        // Based on conversion formula provided in https://www.rapidtables.com/convert/color/rgb-to-cmyk.html.
        /// <summary>
        /// Gets the Y (yellow) component value of this System.Drawing.Color structure in the CMYK color model.
        /// </summary>
        /// <param name="color">The System.Drawing.Color structure which the Y (yellow) component should be extracted from.</param>
        /// <returns>The Y (yellow) component value of this color.</returns>
        public static float Y(Color color)
        {
            float[] floatRGB = FloatRGB(color);

            return (1 - floatRGB[2] - K(color)) / (1 - K(color));
        }

    }
}
