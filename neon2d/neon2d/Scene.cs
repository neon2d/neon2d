using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Media;
using System.Drawing;

using neon2d.UI;

namespace neon2d
{
    public class Scene
    {

        //VARIABLES
        public ArrayList renderlist = new ArrayList();
        public int renderct = 0;

        public ArrayList controllist = new ArrayList();
        public int controlct = 0;

        public Form ownerwindow;

        public Keys downkey;
        public bool keytrue;
        public bool keyuptrue;

        public bool clicktrue;
        public bool doubleclicktrue;
        public bool heldtrue;

        public int mousex = 0;
        public int mousey = 0;

        /// <param name="owner">The Window to render on</param>
        public Scene(Window owner)
        {
            ownerwindow = owner.gamewindow;
            ownerwindow.KeyDown += Gamewindow_KeyDown;
            ownerwindow.KeyUp += Ownerwindow_KeyUp;
            ownerwindow.MouseMove += Ownerwindow_MouseMove;
            ownerwindow.Click += Ownerwindow_Click;
            ownerwindow.MouseDown += Ownerwindow_MouseDown;
            ownerwindow.DoubleClick += Ownerwindow_DoubleClick;
            ownerwindow.MouseUp += Ownerwindow_MouseUp;
        }

        //EVENTS
        private void Ownerwindow_MouseDown(object sender, MouseEventArgs e)
        {
            heldtrue = true;
        }

        private void Ownerwindow_MouseUp(object sender, MouseEventArgs e)
        {
            heldtrue = false;
            clicktrue = false;
            doubleclicktrue = false;
        }

        //mouse input stuff
        private void Ownerwindow_DoubleClick(object sender, EventArgs e)
        {
            doubleclicktrue = true;
        }
        
        private void Ownerwindow_Click(object sender, EventArgs e)
        {
            clicktrue = true;
        }

        private void Ownerwindow_MouseMove(object sender, MouseEventArgs e)
        {
            mousex = e.X;
            mousey = e.Y;
        }

        private void Ownerwindow_KeyUp(object sender, KeyEventArgs e)
        {
            keyuptrue = true;
        }

        private void Gamewindow_KeyDown(object sender, KeyEventArgs e)
        {
            keyuptrue = false;
            downkey = e.KeyCode;
        }

        /// <summary>
        /// A structure used to store info given in render()
        /// </summary>
        public struct SpriteStruct
        {
            public Sprite s;
            public int x;
            public int y;

            public SpriteStruct(Sprite s, int x, int y)
            {
                this.s = s;
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// A structure used to store info given in render()
        /// </summary>
        public struct PropStruct
        {
            public Prop p;
            public int x;
            public int y;

            public PropStruct(Prop p, int x, int y)
            {
                this.p = p;
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// Draw on the owner Window
        /// </summary>
        /// <param name="render">The Sprite to render (current frame is rendered)</param>
        /// <param name="x">The x-coordinate to render at</param>
        /// <param name="y">The y-coordinate to render at</param>
        public void render(Sprite render, int x, int y)
        {
            renderlist.Add(new SpriteStruct(render, x, y));
            renderct++;
        }

        /// <summary>
        /// Draw on the owner Window
        /// </summary>
        /// <param name="render">The Prop to render</param>
        /// <param name="x">The x-coordinate to render at</param>
        /// <param name="y">The y-coordinate to render at</param>
        public void render(Prop render, int x, int y)
        {
            renderlist.Add(new PropStruct(render, x, y));
            renderct++;
        }


        //differs from ParticleSystem.ParticleStruct!!
        /// <summary>
        /// A structure used to store info given in render()
        /// </summary>
        public struct ParticleRenderStruct
        {
            public int x;
            public int y;
            public ParticleSystem ps;

            public ParticleRenderStruct(int x, int y, ParticleSystem ps)
            {
                this.x = x;
                this.y = y;
                this.ps = ps;
            }

        }

        /// <summary>
        /// Draw on the owner Window
        /// </summary>
        /// <param name="render">The ParticleSystem to render</param>
        /// <param name="x">The x-coordinate to render at</param>
        /// <param name="y">The y-coordinate to render at</param>
        public void render(ParticleSystem ps, int x, int y)
        {
            renderlist.Add(new ParticleRenderStruct(x, y, ps));
            renderct++;
        }

        /// <summary>
        /// A structure used to store info given in render()
        /// </summary>
        public struct TextStruct
        {
            public string stringtext;
            public int _x;
            public int _y;
            public Brush stringcolor;
            public Font stringfont;

            public TextStruct(string text, int x, int y, Brush textcolor, Font textfont)
            {
                stringtext = text;
                _x = x;
                _y = y;
                stringcolor = textcolor;
                stringfont = textfont;
            }

        }

        /// <summary>
        /// Draw on the owner Window
        /// </summary>
        /// <param name="text">The Text to render</param>
        /// <param name="x">The x-coordinate to render at</param>
        /// <param name="y">The y-coordinate to render at</param>
        /// <param name="textcolor">The color of the Text</param>
        /// <param name="textfont">The font of the Text</param>
        public void render(string text, int x, int y, Brush textcolor = null, Font textfont = null)
        {
            Brush tempcolor;
            Font tempfont;
            if (textcolor == null)
            {
                tempcolor = Brushes.White;
            }
            else
            {
                tempcolor = textcolor;
            }

            if (textfont == null)
            {
                tempfont = SystemFonts.DialogFont;
            }
            else
            {
                tempfont = textfont;
            }
            renderlist.Add(new TextStruct(text, x, y, tempcolor, tempfont));
            renderct++;
        }

        //shape stuff goes here
        //starting with some special structs to hold them in the arrays
        //DEVS DON'T NEED TO USE THESE STRUCTS! THEY ARE MERELY USED AS THE DATATYPE FOR THE ARRAYS
        //SO WE CAN HOLD LINES, BRUSHES, AND INT WIDTHS IN THE SAME ARRAY.

        /// <summary>
        /// A structure used to store info given in render()
        /// </summary>
        public struct LineStruct
        {

            public Shape.Line line;
            public Pen p;

            public LineStruct(Shape.Line l, Pen p)
            {
                line = l;
                this.p = p;
            }
        }

        /// <summary>
        /// A structure used to store info given in render()
        /// </summary>
        public struct RectStruct
        {

            public Shape.Rectangle rect;
            public int x;
            public int y;
            public Pen p;

            public RectStruct(Shape.Rectangle r, int x, int y, Pen p)
            {
                rect = r;
                this.x = x;
                this.y = y;
                this.p = p;
            }
        }

        /// <summary>
        /// A structure used to store info given in render()
        /// </summary>
        public struct EllipsStruct
        {

            public Shape.Ellipse ell;
            public int x;
            public int y;
            public Pen p;

            public EllipsStruct(Shape.Ellipse e, int x, int y, Pen p)
            {
                ell = e;
                this.x = x;
                this.y = y;
                this.p = p;
            }
        }

        /// <summary>
        /// A structure used to store info given in render()
        /// </summary>
        public struct TriStruct
        {

            public Shape.Triangle tri;
            public int x;
            public int y;
            public Pen p;

            public TriStruct(Shape.Triangle t, int x, int y, Pen p)
            {
                tri = t;
                this.x = x;
                this.y = y;
                this.p = p;
            }
        }

        //THE ACTUAL SHAPE RENDERING IS HERE

        /// <summary>
        /// Draw on the owner Window
        /// </summary>
        /// <param name="line">The Line to render</param>
        /// <param name="thickness">The thickness of the Line</param>
        /// <param name="color">The color of the Line</param>
        public void render(Shape.Line line, int thickness = 1, Brush color = null)
        {
            Pen pcolor;
            if(color == null)
            {
                pcolor = new Pen(Brushes.Black, thickness);
            }
            else
            {
                pcolor = new Pen(color, thickness);
            }
            LineStruct ls = new LineStruct(line, pcolor);
            renderlist.Add(ls);
            renderct++;
        }

        /// <summary>
        /// Draw on the owner Window
        /// </summary>
        /// <param name="rect">The Rectangle to render</param>
        /// <param name="x">The x-coordinate to render at</param>
        /// <param name="y">The y-coordinate to render at</param>
        /// <param name="thickness">The border thickness of the Rectangle (if not filled-in)</param>
        /// <param name="color">The color of the Rectangle</param>
        public void render(Shape.Rectangle rect, int x, int y, int thickness = 1, Brush color = null)
        {
            Pen pcolor;
            if (color == null)
            {
                pcolor = new Pen(Brushes.Black, thickness);
            }
            else
            {
                pcolor = new Pen(color, thickness);
            }
            RectStruct rs = new RectStruct(rect, x, y, pcolor);
            renderlist.Add(rs);
            renderct++;
        }

        /// <summary>
        /// Draw on the owner Window
        /// </summary>
        /// <param name="ellipse">The Ellipse to render</param>
        /// <param name="x">The x-coordinate to render at</param>
        /// <param name="y">The y-coordinate to render at</param>
        /// <param name="thickness">The border thickness of the Ellipse (if not filled-in)</param>
        /// <param name="color">The color of the Ellipse</param>
        public void render(Shape.Ellipse ellipse, int x, int y, int thickness = 1, Brush color = null)
        {
            Pen pcolor;
            if (color == null)
            {
                pcolor = new Pen(Brushes.Black, thickness);
            }
            else
            {
                pcolor = new Pen(color, thickness);
            }
            EllipsStruct es = new EllipsStruct(ellipse, x, y, pcolor);
            renderlist.Add(es);
            renderct++;
        }

        /// <summary>
        /// Draw on the owner Window
        /// </summary>
        /// <param name="triangle">The Triangle to render</param>
        /// <param name="x">The x-coordinate to render at</param>
        /// <param name="y">The y-coordinate to render at</param>
        /// <param name="thickness">The border thickness of the Triangle</param>
        /// <param name="color">The color of the Triangle</param>
        public void render(Shape.Triangle triangle, int x, int y, int thickness = 1, Brush color = null)
        {
            Pen pcolor;
            if (color == null)
            {
                pcolor = new Pen(Brushes.Black, thickness);
            }
            else
            {
                pcolor = new Pen(color, thickness);
            }
            TriStruct ts = new TriStruct(triangle, x, y, pcolor);
            renderlist.Add(ts);
            renderct++;
        }

        //SOUND STUFF
        /// <summary>
        /// Play a Sound through the speakers
        /// </summary>
        /// <param name="gamesound">The Sound played</param>
        /// <param name="loop">Determines whether to loop the Sound or not</param>
        public void playSound(Sound gamesound, bool loop = false)
        {
            SoundPlayer psound = new SoundPlayer(gamesound.soundsource);
            if(loop)
            {
                psound.PlayLooping();
            }
            else
            {
                psound.Play();
            }
        }

        //INPUT STUFF
        /// <summary>
        /// Get the state of a keyboard key
        /// </summary>
        /// <param name="keyToDetect">The keyboard key to detect</param>
        /// <returns>If that key has been pressed</returns>
        public bool readKeyDown(Keys keyToDetect)
        {
            if (downkey == keyToDetect)
            {
                keytrue = true;
            }
            else
            {
                keytrue = false;
            }
            return keytrue;
        }

        /// <summary>
        /// Check for any key's release
        /// </summary>
        /// <returns>If a key has been released</returns>
        public bool keyUp()
        {
            return keyuptrue;
        }

        /// <summary>
        /// Detects if the mouse has been clicked
        /// </summary>
        /// <returns>The mouse's click state</returns>
        public bool mouseClick()
        {
            return clicktrue;
        }


        /// <summary>
        /// Detects if the mouse has been double-clicked
        /// </summary>
        /// <returns>The mouse's double-click state</returns>
        public bool mouseDoubleClick()
        {
            return doubleclicktrue;
        }

        /// <summary>
        /// Detects if the mouse is held down
        /// </summary>
        /// <returns>The mouse's held-down state</returns>
        public bool mouseHeld()
        {
            return heldtrue;
        }

        /// <summary>
        /// Gets the mouse's x-coordinate
        /// </summary>
        /// <returns>The mouse's x-coordinate</returns>
        public int getMouseX()
        {
            return mousex;
        }

        /// <summary>
        /// Gets the mouse's y-coordinate
        /// </summary>
        /// <returns>The mouse's y-coordinate</returns>
        public int getMouseY()
        {
            return mousey;
        }

        //UI STUFF
        //WindowsUI
        /// <summary>
        /// Add a WindowsControl to the Scene. Controls can only be modified before the game launches
        /// </summary>
        /// <param name="control">The WindowsControl to add</param>
        public void addUIElement(WindowsControl control)
        {
            controllist.Add(control);
            controlct++;
        }
        //more coming soon...

        //MISC. STUFF
        /// <summary>
        /// Clear the render list (and therefor the screen)
        /// </summary>
        public void cleanRenderBuffer()
        {
            renderlist.Clear();
            renderct = 0;
        }
    }
}
