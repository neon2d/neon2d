using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neon2d.Math
{

    public struct Vector2
    {
        int _x;
        int _y;

        public int X
        {
            get { return _x; }
<<<<<<< HEAD
            set { _x = value;  }
=======
            set { _x = value; }
>>>>>>> origin/new-renderer-#9
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
<<<<<<< HEAD

=======
>>>>>>> origin/new-renderer-#9
    }

    public struct Rectangle
    {
        int _x;
        int _y;
        int _w;
        int _h;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Width
        {
            get { return _w; }
            set { _w = value; }
        }

        public int Height
        {
            get { return _h; }
            set { _h = value; }
        }
    }
<<<<<<< HEAD
    
=======

>>>>>>> origin/new-renderer-#9
}
