using System;
using System.Drawing;
using MikisColorTools;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Color color = Color.LightCoral;
            Console.WriteLine(string.Format("LightCoral: ({0}, {1}, {2}, {3})", color.R, color.G, color.B, color.A));
            Console.WriteLine();

            Color compColor = MikisColorTools.MikisColorTools.GetComplementaryColor(color);
            Console.WriteLine(string.Format("Complementary: ({0}, {1}, {2}, {3})", compColor.R, compColor.G, compColor.B, compColor.A));
            Console.WriteLine();

            Console.WriteLine(string.Format("CMYK: ({0}, {1}, {2}, {3})", MikisColorTools.MikisColorTools.C(color),
                MikisColorTools.MikisColorTools.M(color), MikisColorTools.MikisColorTools.Y(color), MikisColorTools.MikisColorTools.K(color)));
            Console.WriteLine();

            Color[] triadicColors = MikisColorTools.MikisColorTools.GetNEvenlySpacedColorScheme(color, 3);
            foreach (Color c in triadicColors)
            {
                Console.WriteLine(string.Format("Triadic: ({0}, {1}, {2})", c.R, c.G, c.B));
            }
            Console.WriteLine();

            Console.WriteLine("Hex Code: " + MikisColorTools.MikisColorTools.GetHexColorCode(color));

            Console.ReadKey();
        }
    }
}
