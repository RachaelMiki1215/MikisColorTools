using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace MikisColorTools
{
    public static class IMikisColorTools
    {
        private static float[] FloatRGB(Color color)
        {
            return new float[] { (int)color.R / 255f, (int)color.G / 255f, (int)color.B / 255f };
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

        // Based on color conversion formula provided in https://www.rapidtables.com/convert/color/rgb-to-hex.html.
        /// <summary>
        /// Gets the hexadecimal color code string for a System.Drawing.Color struct.
        /// </summary>
        /// <param name="color">The color for which the string would be generated.</param>
        /// <returns>The hexadecimmal color code string.</returns>
        public static string GetHexColorCode(Color color)
        {
            return string.Format("#{0}{1}{2}", Convert.ToString(color.R, 16), 
                Convert.ToString(color.G, 16), Convert.ToString(color.B, 16));
        }

        /// <summary>
        /// Changes hue of System.Drawing.Colorstruct by factor hueDelta and returns product.
        /// </summary>
        /// <param name="color">Base color to be changed.</param>
        /// <param name="hueDelta">The hue difference.</param>
        /// <returns>Color which the hue is changed.</returns>
        public static Color ChangeHue(Color color, float hueDelta)
        {
            if (hueDelta % 360f == 0f)
            {
                return color;
            }

            return GetColorFromAhsl((color.GetHue() + hueDelta) % 360f, 
                color.GetSaturation(), color.GetBrightness(), color.A);
        }

        /// <summary>
        /// Changes saturation of System.Drawing.Color struct by factor saturationDelta and returns product.
        /// </summary>
        /// <param name="color">Base color to be changed.</param>
        /// <param name="saturationDelta">The saturation difference. Number <0 decreases saturation and number >0 increases saturation brightness.</param>
        /// <returns>Color which the saturation is changed.</returns>
        public static Color ChangeSaturation(Color color, float saturationDelta)
        {
            if (saturationDelta == 0f)
            {
                return color;
            }
            else if (color.GetSaturation() + saturationDelta <= 0f)
            {
                return GetColorFromAhsl(color.GetHue(), 0f, color.GetBrightness(), color.A);
            }
            else if (color.GetSaturation() + saturationDelta <= 0f)
            {
                return GetColorFromAhsl(color.GetHue(), 0f, color.GetBrightness(), color.A);
            }

            return GetColorFromAhsl(color.GetHue(), color.GetSaturation() + saturationDelta, 
                color.GetBrightness(), color.A);
        }

        /// <summary>
        /// Changes brightness of System.Drawing.Color struct by factor brighnessDelta and returns product.
        /// </summary>
        /// <param name="color">Base color to be changed.</param>
        /// <param name="brightnessDelta">The brightness difference. Number <0 decreases brightness and number >0 increases brightness.</param>
        /// <returns>Color which the brightness is changed.</returns>
        public static Color ChangeBrightness(Color color, float brightnessDelta)
        {
            if (brightnessDelta == 0f)
            {
                return color;
            }
            else if (color.GetHue() + brightnessDelta <= 0f)
            {
                return Color.Black;
            }
            else if (color.GetHue() + brightnessDelta >= 1f)
            {
                return Color.White;
            }

            return GetColorFromAhsl(color.GetHue(), color.GetSaturation(), 
                color.GetBrightness() + brightnessDelta, color.A);
        }

        // Based on conversion formula provided in https://www.rapidtables.com/convert/color/hsl-to-rgb.html.
        /// <summary>
        /// Gets the System.Drawing.Color structure from the HSL (hue-saturation-brightness) model and A (alpha) value.
        /// </summary>
        /// <param name="hue">The hue of the color from 0.0 to 360.0.</param>
        /// <param name="saturation">The saturation of the color from 0.0 to 1.0.</param>
        /// <param name="brightness">The brightness of the color from 0.0 to 1.0.</param>
        /// <param name="alpha">The transparency of the color from 1 to 255. 255 is opaque and 0 is transparent.</param>
        /// <returns>The Color structure.</returns>
        public static Color GetColorFromAhsl(float hue, float saturation, float brightness, byte alpha = 255)
        {
            float C, X, m;
            float[] rgbPrime = new float[] { 0f, 0f, 0f };
            C = (1f - (float)Math.Abs(Convert.ToDecimal(2f * brightness - 1f))) * saturation;
            X = C * (1f - (float)Math.Abs((hue / 60f) % 2f - 1f));
            m = brightness - (C / 2f);

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

            return Color.FromArgb(alpha, (int)((rgbPrime[0] + m) * 255f),
                (int)((rgbPrime[1] + m) * 255f),
                (int)((rgbPrime[2] + m) * 255f));
        }

        /// <summary>
        /// Gets the complementary System.Drawing.Color structure of a specified Color structure.
        /// </summary>
        /// <param name="color">The Color structure at which the complementary Color structure is calculated.</param>
        /// <returns>The complementary Color structure.</returns>
        public static Color GetComplementaryColor(Color color)
        {
            float hue = (color.GetHue() + 180f) % 360f;

            return GetColorFromAhsl(hue, color.GetSaturation(), color.GetBrightness(), color.A);
        }

        public static Color GetInverseColor(Color color)
        {
            return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
        }

        /// <summary>
        /// Gets an array of n numbers System.Drawing.Color structs which are evenly spaced around the color wheel.
        /// </summary>
        /// <param name="baseColor">The base color which the color scheme should be generated for.</param>
        /// <param name="colorCount">The number (n) of colors to be included in the array, including baseColor.</param>
        /// <returns></returns>
        public static Color[] GetNEvenlySpacedColorScheme(Color baseColor, int colorCount)
        {
            if (colorCount < 1)
            {
                return new Color[] {};
            }
            else if (colorCount == 1)
            {
                return new Color[] { baseColor };
            }
            else if (colorCount == 2)
            {
                return new Color[] { baseColor, GetComplementaryColor(baseColor) };
            }

            List<Color> colors = new();
            float degreeInc = 360f / (float)colorCount;
            float hue = baseColor.GetHue();

            for (int i = 0; i < colorCount; i++)
            {
                if (i == 0)
                {
                    colors.Add(baseColor);
                }
                else
                {
                    colors.Add(GetColorFromAhsl((hue + (degreeInc * (float)i)) % 360f, 
                        baseColor.GetSaturation(), baseColor.GetBrightness(), baseColor.A));
                }
            }

            return colors.ToArray();
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
    }
}
