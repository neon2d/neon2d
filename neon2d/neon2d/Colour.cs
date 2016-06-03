using System.Drawing;

namespace neon2d
{
    /// <summary>
    /// Represents a colour with Alpha, Red, Green and Blue components.
    /// </summary>
    public class Colour
    {
        /// <summary>
        /// Red colour. A = 255, R = 255, G = 0, B = 0
        /// </summary>
        public static readonly Colour RED = new Colour(255, 255, 0, 0);

        /// <summary>
        /// Green colour. A = 255, R = 0, G = 255, B = 0
        /// </summary>
        public static readonly Colour GREEN = new Colour(255, 0, 255, 0);

        /// <summary>
        /// Blue Colour. A = 255, R = 0, G = 0, B = 255
        /// </summary>
        public static readonly Colour BLUE = new Colour(255, 0, 0, 255);

        /// <summary>
        /// White colour. A = 255, R = 255, G = 255, B = 255
        /// </summary>
        public static readonly Colour WHITE = new Colour(255, 255, 255, 255);

        /// <summary>
        /// Black colour. A = 255, R = 0, G = 0, B = 0
        /// </summary>
        public static readonly Colour BLACK = new Colour(255, 0, 0, 0);

        /// <summary>
        /// Yellow colour. A = 255, R = 255, G = 216, B = 0
        /// </summary>
        public static readonly Colour YELLOW = new Colour(255, 255, 216, 0);

        /// <summary>
        /// The alpha component of this colour.
        /// Integer (32 Bit). Should be between 0 and 255
        /// </summary>
        public int a;


        /// <summary>
        /// The red component of this colour
        /// Integer (32 Bit). Should be between 0 and 255
        /// </summary>
        public int r;

        /// <summary>
        /// The green component of this colour
        /// Integer (32 Bit). Should be between 0 and 255
        /// </summary>
        public int g;

        /// <summary>
        /// The blue componenet of this colour
        /// Integer (32 Bit). Should be between 0 and 255
        /// </summary>
        public int b;

        /// <summary>
        /// Creates a new ARGB colour.
        /// </summary>
        /// <param name="a">Alpha Compnent. Integer (32 bit). Between 0 and 225.</param>
        /// <param name="r">Red Compnent. Integer (32 bit). Between 0 and 225.</param>
        /// <param name="g">Green Compnent. Integer (32 bit). Between 0 and 225.</param>
        /// <param name="b">Blue Compnent. Integer (32 bit). Between 0 and 225.</param>
        public Colour(int a, int r, int g, int b)
        {
            this.a = a;
            this.r = r;
            this.r = g;
            this.r = b;
        }

        public Colour(string argb)
        {
            if(!argb.StartsWith("#"))
            {
                neon2d.Message.neonError("Hex colour value should start with #.");
                return;
            }

            System.Windows.Media.Color sysCol = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(argb);

            this.a = sysCol.A;
            this.r = sysCol.R;
            this.g = sysCol.G;
            this.b = sysCol.B;
        }

        /// <summary>
        /// Returns a string representation of this colour
        /// In the format: "Alpha: [A] Red: [R] Green [G] Blue [B] [Newline]"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string rgb = "Alpha: " + (int)this.a + " Red: " + this.r + " Green: " + this.g + " Blue: " + this.b;
            return rgb + "\n";
        }

        public Color toSysColor()
        {
            return Color.FromArgb(a, r, g, b);
        }

        public Brush toSysBrush()
        {
            return new SolidBrush(toSysColor());
        }
    }

    public static class ColourUtils
    {
        public static Colour toNeonColour(this Color sysColor)
        {
            return new Colour(sysColor.A, sysColor.R, sysColor.G, sysColor.B);
        }
    }

}
