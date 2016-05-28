using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;
using System.Threading;
using System.Reflection;

namespace neon2d
{

    public class Game
    {

        public Window window;
        public Scene scene;
        public Window defaultwindow = new Window(800, 600, "neon2d");
        public int fps = 30;
        public int tick = 0;

        public System.Timers.Timer refreshtimer;

        public Action update;

        public bool windowOpened = false;
        public bool threadRunning = false;
        public bool shouldRender = false;

        public Game(Window mainwindow, Scene mainscene, Action onUpdate, int framerate = 30)
        {
            window = mainwindow;
            scene = mainscene;
            fps = framerate;
            
            window.gamewindow.Focus();
            window.gamewindow.Paint += new System.Windows.Forms.PaintEventHandler(this.window_Paint);
            window.gamewindow.Shown += Gamewindow_Shown;

            window.gamewindow.FormClosed += Gamewindow_FormClosed;

            refreshtimer = new System.Timers.Timer(1000 / framerate);
            refreshtimer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            refreshtimer.Enabled = true;

            update = onUpdate;

        }

        private void Gamewindow_Shown(object sender, EventArgs e)
        {
            window.gamewindow.Activate();
        }

        private void Gamewindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        public void window_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for(int i = 0; i <= scene.renderlist.Count - 1; i++)
            {
                if (scene.renderlist[i].GetType() == typeof(Scene.SpriteStruct))
                {
                    //its a sprite
                    Scene.SpriteStruct placeholder = (Scene.SpriteStruct)scene.renderlist[i];

                    g.DrawImage(placeholder.s.currentFrame, new Rectangle(placeholder.x, placeholder.y, placeholder.s.spriteWidth, placeholder.s.spriteHeight));
                }
                else if (scene.renderlist[i].GetType() == typeof(Scene.PropStruct))
                {
                    //its a prop
                    Scene.PropStruct placeholder = (Scene.PropStruct)scene.renderlist[i];

                    g.DrawImage(placeholder.p.propSource, new Rectangle(placeholder.x, placeholder.y, placeholder.p.propWidth, placeholder.p.propHeight));
                }
                else if (scene.renderlist[i].GetType() == typeof(Scene.TextStruct))
                {
                    //its text
                    Scene.TextStruct placeholder = (Scene.TextStruct)scene.renderlist[i];

                    g.DrawString(placeholder.stringtext, placeholder.stringfont, placeholder.stringcolor, placeholder._x, placeholder._y);
                }
                else if (scene.renderlist[i].GetType() == typeof(Scene.LineStruct))
                {
                    //its a line
                    Scene.LineStruct placeholder = (Scene.LineStruct)scene.renderlist[i];

                    g.DrawLine(placeholder.p, new Point(placeholder.line.lX1, placeholder.line.lY1), new Point(placeholder.line.lX2, placeholder.line.lY2));

                }
                else if (scene.renderlist[i].GetType() == typeof(Scene.RectStruct))
                {
                    //its a rect
                    Scene.RectStruct placeholder = (Scene.RectStruct)scene.renderlist[i];

                    if (!placeholder.rect.filled)
                    {
                        g.DrawRectangle(placeholder.p, placeholder.x, placeholder.y, placeholder.rect.rectWidth, placeholder.rect.rectHeight);
                    }
                    else
                    {
                        //fill it
                        g.FillRectangle(placeholder.p.Brush, placeholder.x, placeholder.y, placeholder.rect.rectWidth, placeholder.rect.rectHeight);
                    }
                }
                else if (scene.renderlist[i].GetType() == typeof(Scene.EllipsStruct))
                {
                    //its an ellipse
                    Scene.EllipsStruct placeholder = (Scene.EllipsStruct)scene.renderlist[i];

                    if (!placeholder.ell.filled)
                    {
                        g.DrawEllipse(placeholder.p, placeholder.x, placeholder.y, placeholder.ell.ellipsWidth, placeholder.ell.ellipsHeight);
                    }
                    else
                    {
                        //fill it
                        g.FillEllipse(placeholder.p.Brush, placeholder.x, placeholder.y, placeholder.ell.ellipsHeight, placeholder.ell.ellipsHeight);
                    }
                }
                else if (scene.renderlist[i].GetType() == typeof(Scene.TriStruct))
                {
                    //its a triangle
                    Scene.TriStruct placeholder = (Scene.TriStruct)scene.renderlist[i];

                    g.DrawLine(placeholder.p, new Point(placeholder.x, placeholder.y + placeholder.tri.triHeight), new Point(placeholder.x + (int)(placeholder.tri.triWidth / 2), placeholder.y));
                    g.DrawLine(placeholder.p, new Point(placeholder.x + (int)(placeholder.tri.triWidth / 2), placeholder.y), new Point(placeholder.x + placeholder.tri.triWidth, placeholder.y + placeholder.tri.triHeight));
                    g.DrawLine(placeholder.p, new Point(placeholder.x, placeholder.y + placeholder.tri.triHeight), new Point(placeholder.x + placeholder.tri.triWidth, placeholder.y + placeholder.tri.triHeight));
                }
                else if (scene.renderlist[i].GetType() == typeof(Scene.ParticleRenderStruct))
                {
                    //its a particle system
                    Scene.ParticleRenderStruct renderholder = (Scene.ParticleRenderStruct)scene.renderlist[i];
                    ParticleSystem placeholder = (ParticleSystem)renderholder.ps;

                    for (int j = 0; j <= placeholder.particles.Count - 1; j++)
                    {

                        if (placeholder.particles[j] != null)
                        {
                            //safe to render
                            ParticleSystem.ParticleStruct particle = (ParticleSystem.ParticleStruct)placeholder.particles[j];
                            if (particle.particleSource.GetType() == typeof(Prop))
                            {
                                Prop particleprop = (Prop)particle.particleSource;
                                g.DrawImage(particleprop.propSource, renderholder.x + particle.x, renderholder.y + particle.y);
                            }
                            if (particle.particleSource.GetType() == typeof(Sprite))
                            {
                                Sprite particlesprite = (Sprite)particle.particleSource;
                                g.DrawImage(particlesprite.currentFrame, renderholder.x + particle.x, renderholder.y + particle.y);
                            }
                        }
                        else
                        {
                            //its null, break
                            break;
                        }
                    }
                }
                    
            }
        }

        public void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //clear draw buffer
            scene.cleanRenderBuffer();
            //refill draw buffer
            
            update();
            //render draw buffer
            shouldRender = true;
            
        }

        public void runGame()
        {
            Thread windowthread = new Thread(windowRender);
            threadRunning = true;
            windowthread.Start();
        }

        public void windowRender()
        {
            if(!windowOpened)
            {
                window.gamewindow.Show();
                window.gamewindow.Refresh();
                window.gamewindow.SuspendLayout();
                window.gamewindow.ResumeLayout();
            }
            while (threadRunning)
            {
                Application.DoEvents();
                if(shouldRender)
                {
                    window.gamewindow.Refresh();
                    shouldRender = false;
                }
            }
        }

    }
}
