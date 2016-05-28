using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neon2d
{
    public class Shape
    {

        public class Line
        {

            public int lX1 = 0;
            public int lY1 = 0;
            public int lX2 = 0;
            public int lY2 = 0;

            public Line(int x1, int y1, int x2, int y2)
            {
                lX1 = x1;
                lY1 = y1;
                lX2 = x2;
                lY2 = y2;
            }
            public Line(Math.Vector2i point1, Math.Vector2i point2)
            {
                lX1 = point1.x;
                lY1 = point1.y;
                lX2 = point2.x;
                lY2 = point2.y;
            }

        }

        /// <summary>
        /// A rectangular 2D shape
        /// </summary>
        public class Rectangle
        {

            public int rectWidth = 0;
            public int rectHeight = 0;
            public bool filled = false;

            /// <param name="width">The width of the Rectangle</param>
            /// <param name="height">The height of the Rectangle</param>
            /// <param name="filled">Determines if the Rectangle is filled-in or not</param>
            public Rectangle(int width, int height, bool filled = false)
            {
                rectWidth = width;
                rectHeight = height;
                this.filled = filled;
            }

            /// <param name="dimensions">A Vector2 representing the width and height of the Rectangle</param>
            /// <param name="filled">Determines if the Rectangle is filled-in or not</param>
            public Rectangle(Math.Vector2i dimensions, bool filled = false)
            {
                rectWidth = (int)dimensions.x;
                rectHeight = (int)dimensions.y;
                this.filled = filled;
            }
        
        }

        /// <summary>
        /// A circular 2D shape
        /// </summary>
        public class Ellipse
        {
            public int ellipsWidth = 0;
            public int ellipsHeight = 0;
            public bool filled;
            
            /// <param name="width">The width of the Ellipse</param>
            /// <param name="height">The height of the Ellipse</param>
            /// <param name="filled">Determines if the Ellipse is filled-in or not</param>
            public Ellipse(int width, int height, bool filled = false)
            {
                ellipsWidth = width;
                ellipsHeight = height;
                this.filled = filled;
            }
            
            /// <param name="dimensions">A Vector2 representing the width and height of the Ellipse</param>
            /// <param name="filled">Determines if the Ellipse is filled-in or not</param>
            public Ellipse(Math.Vector2i dimensions, bool filled = false)
            {
                ellipsWidth = (int)dimensions.x;
                ellipsHeight = (int)dimensions.y;
                this.filled = filled;
            }

        }

        /// <summary>
        /// A triangular 2D shape
        /// </summary>
        public class Triangle
        {
            
            public int triWidth = 0;
            public int triHeight = 0;

            /// <param name="width">The width of the Triangle's base</param>
            /// <param name="height">The height of the Triangle's peak</param>
            public Triangle(int width, int height)
            {
                triWidth = width;
                triHeight = height;
            }
            
            /// <param name="dimensions">A Vector2 representing the width of the Triangle's base and the height of the Triangle's peak</param>
            public Triangle(Math.Vector2i dimensions)
            {
                triWidth = (int)dimensions.x;
                triHeight = (int)dimensions.y;
            }

        }

    }
}
