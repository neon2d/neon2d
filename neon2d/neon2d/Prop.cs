using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace neon2d
{
    public class Prop
    {

        public Bitmap propSource;
        public int propWidth;
        public int propHeight;

        /// <param name="image">The Bitmap image displayed</param>
        /// <param name="width">The width of this Prop</param>
        /// <param name="height">The height of this Prop</param>
        public Prop(Bitmap image, int width, int height)
        {
            propSource = image;
            propWidth = width;
            propHeight = height;
        }

        /// <param name="image">The Bitmap image displayed at original scale</param>
        public Prop(Bitmap image)
        {
            propSource = image;
            propWidth = image.Width;
            propHeight = image.Height;
        }

    }
}
