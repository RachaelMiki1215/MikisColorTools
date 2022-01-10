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
            Color compColor = IMikisColorTools.GetComplementaryColor(color);

            Console.WriteLine(string.Format("LightCoral: ({0}, {1}, {2}, {3})", color.R, color.G, color.B, color.A));

            Console.WriteLine(string.Format("Complementary: ({0}, {1}, {2}, {3})", compColor.R, compColor.G, compColor.B, compColor.A));

            Console.ReadKey();
        }
    }
}
