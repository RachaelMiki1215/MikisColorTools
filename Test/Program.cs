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

            Color compColor = IMikisColorTools.GetComplementaryColor(color);
            Console.WriteLine(string.Format("Complementary: ({0}, {1}, {2}, {3})", compColor.R, compColor.G, compColor.B, compColor.A));

            Console.WriteLine(IMikisColorTools.C(color));
            Console.WriteLine(IMikisColorTools.M(color));
            Console.WriteLine(IMikisColorTools.Y(color));
            Console.WriteLine(IMikisColorTools.K(color));

            Color[] triadicColors = IMikisColorTools.GetNEvenlySpacedColorScheme(Color.LightGoldenrodYellow, 3);
            foreach (Color c in triadicColors)
            {
                Console.WriteLine(string.Format("Triadic: ({0}, {1}, {2})", c.R, c.G, c.B));
            }

            Console.WriteLine(IMikisColorTools.GetHexColorCode(color));

            Console.ReadKey();
        }
    }
}
