using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using neon2d;
using neon2d.Physics;
using neon2d.Math;
using neon2d.Networking;

namespace n2d
{
    class Program : BasicGame
    {
        private bool movingup = false;
        private int imagey = 10;

        private Vector2i vector = new Vector2i(10, 20);

        private ParticleSystem particles;

        private Prop particleProp = new Prop(new Bitmap(Environment.CurrentDirectory + @"\..\..\res\particle.png"), 25, 25);
        private Prop particleProp2 = new Prop(new Bitmap(Environment.CurrentDirectory + @"\..\..\res\particle2.png"), 25, 25);

        public Program()
            : base(800, 600, "Neon2D Demo!")
        {
        }

        public override void onStart()
        {
            // Vector2f's
            Console.WriteLine(new Vector2f(10, 20.2f) + new Vector2f(20, 10.3f));

            // Vecto2i's
            Console.WriteLine(new Vector2i(10, 20) - new Vector2i(5, 10));

            Vector2f vector = new Vector2f(10, 10);
            Console.WriteLine(vector);

            Vector2f.normalize(vector);

            Console.WriteLine(vector);

            //particles
            particles = new ParticleSystem(7, 0, 7, 0, 10);
            particles.setFluctuation(2);

            for(int i = 0; i <= 10; i++)
            {
                particles.addParticle(particleProp);
            }

            Colour test = new Colour("FF00FF00");
            Console.WriteLine(test);
        }

        public override void onUpdate()
        {
            checkInput();

            if (movingup)
            {
                imagey--;
            }

            string imagepath = Environment.CurrentDirectory + @"\..\..\res\demoimage.png"; //this is /bin/Debug/ btw
            Prop demoimage = new Prop(new Bitmap(imagepath), 150, 150);
            scene.render(demoimage, 0, imagey);

            Rect rect1 = new Rect(0, 0, 60, 60);
            Rect rect2 = new Rect(10, 10, 40, 40);

            if (rect1.intersects(rect2))
            {
                //Console.WriteLine("intersecting!");
            }

            Shape.Triangle tri = new Shape.Triangle(50, 50, false);
            Shape.Rectangle rect = new Shape.Rectangle(45, 80, true);
            Shape.Ellipse ell = new Shape.Ellipse(30, 50);
            Shape.Line li = new Shape.Line(600, 400, 650, 350);

            scene.render(tri, 100, 100, 2, Brushes.Blue);
            scene.render(rect, 300, 300, 1, Brushes.Green);
            scene.render(ell, 50, 50, 3, Brushes.Red);
            scene.render(li, 4, Brushes.Yellow);

            int mx = scene.getMouseX();
            int my = scene.getMouseY();

            scene.render(mx.ToString(), 0, 0);
            scene.render(my.ToString(), 0, 10);
            scene.render("Vector result: " + vector.ToString(), 200, 100);

            //THIS CODE DEMOS BACKGROUNDS
            //BUT ALSO MAKES THE WINDOW MORE CLUTTERED (BAD FOR THE DEMO)
            //scene.renderBackground(demoimage, true);

            particles.addParticle(particleProp);
            particles.addParticle(particleProp2);
            
            particles.step();
            scene.render(particles, 400, 300);
        }

        public void checkInput()
        {
            if(scene.readKeyDown(Keys.Up))
            {
                movingup = true;
            }

            if(scene.keyUp())
            {
                movingup = false;
            }
        }

        public static void Main(string [] args)
        {
            Program demo = new Program();
        }
    }
}
