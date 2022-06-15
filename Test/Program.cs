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

            Color compColor = IMikisColorTools.GetComplementaryColor(color);
            Console.WriteLine(string.Format("Complementary: ({0}, {1}, {2}, {3})", compColor.R, compColor.G, compColor.B, compColor.A));
            Console.WriteLine();

            Console.WriteLine(string.Format("CMYK: ({0}, {1}, {2}, {3})", IMikisColorTools.C(color), 
                IMikisColorTools.M(color), IMikisColorTools.Y(color), IMikisColorTools.K(color)));
            Console.WriteLine();

            Color[] triadicColors = IMikisColorTools.GetNEvenlySpacedColorScheme(color, 3);
            foreach (Color c in triadicColors)
            {
                Console.WriteLine(string.Format("Triadic: ({0}, {1}, {2})", c.R, c.G, c.B));
            }
            Console.WriteLine();

            Console.WriteLine("Hex Code: " + IMikisColorTools.GetHexColorCode(color));

            Console.ReadKey();
        }
    }
}
