using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Windows.Media.Imaging;


//KinectClient usings
using Coding4Fun.Kinect.KinectService.Common;
using Coding4Fun.Kinect.KinectService.WpfClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MemoryKidz
{
    /// <summary>
    /// Represents the Endgamescreen which handles the first operations after a gaming-session has ended
    /// </summary>
    public class Endgame : IGameState
    {
        GraphicsDevice g;

        MouseState currentState;
        MouseState lastState;

        Texture2D placeholder;
        Texture2D background;

        Texture2D yes;
        Texture2D yes_select;

        Texture2D no;
        Texture2D no_select;

        List<Button> bl;

        int hZero;
        int bZero;

        public void LoadContent()
        {
            g = new GraphicsDevice();

            placeholder = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/placeholder_kinect.png"));
            background = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/bg_1080p.png"));

            yes = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Yes.png"));
            yes_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons//Yes_select.png"));

            no = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons//No.png"));
            no_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons//No_select.png"));

            hZero = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            bZero = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            if (!GameSpecs.stream.IsConnected)
            {
                GameSpecs.stream.Connect("localhost", 4530);
            }
            GameSpecs.stream.ColorFrameReady += clientColorFrameReady;
            sw.Start();

            // Starts the Countdown for taking a photo
            Countdown.Initialize(10);
            GameSpecs.PhotoTimerUp = false;
        }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Endgame(List<Button> btnList)
        {
            bl = btnList;
            foreach (Button btn in bl)
            {
                // ClickTangle makes a new virtual Rectangle which is not painted, but virtually overlayed to catch clicks provided by the user. 
                btn.ClickTangle = new Rectangle((int)btn.Position.X, (int)btn.Position.Y, btn.SourceRectangle.Width, btn.SourceRectangle.Height);
            }
        }

        public GameState Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            lastState = currentState;
            currentState = Mouse.GetState();

            // Runs the counter-logic if the countdown has not yet reached zero
            if (Countdown.Counter > 0.0)
            {
                Countdown.Counter -= gameTime.ElapsedGameTime.TotalMilliseconds / 600;
            }
            else
            {
                // Sets the flag to true to indicate that it is time to make the photo
                GameSpecs.PhotoTimerUp = true;

                // Sets the PhotoTaken-marker to true
                Session.PhotoTaken = true;
            }

            // Hover-Check-Routines
            if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[0].Texture = yes_select;
            }
            else
            {
                bl[0].Texture = yes;
            }

            if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[1].Texture = no_select;
            }
            else
            {
                bl[1].Texture = no;
            }

            // Check-Routine for catching clicks on the buttons
            if (currentState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && lastState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                // Button to confirm the photo
                if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    if (GameSpecs.PhotoTimerUp)
                    {
                        Extension.PlaySoundEffect("menuClick");
                        Thread.Sleep(200);
                        GameSpecs.PreviousGamestate = GameState.Endgame;
                        return GameState.Highscore;
                    }
                }

                // Button to retry taking the photo
                if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                     if (GameSpecs.PhotoTimerUp)
                    {
                        GameSpecs.PhotoTimerUp = false;
                        Session.PhotoTaken = false;
                        GameSpecs.PhotoSwitch = false;
                        Countdown.Counter = 10;
                        Extension.PlaySoundEffect("menuClick");
                        Thread.Sleep(10);
                    }
                }
            }

            // Insert the value of the highscore to the Database. // Temporary Solution though
            if (Session.ValueInserted == false)
            {
                // Extension.AddValueToDB((int)Timer.Score);
                Thread.Sleep(400);
                Session.ValueInserted = true;
            }

            return GameState.Endgame;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, ref Microsoft.Xna.Framework.Graphics.SpriteBatch sp, ref Microsoft.Xna.Framework.GraphicsDeviceManager graphics, ref Microsoft.Xna.Framework.Graphics.SpriteFont font)
        {
            sp.Begin();
            g.Clear(Color.Black);

            /// Logic for drawing the ActiveBackground
            ActiveBackground.DrawSky();
            ActiveBackground.DrawSunrays();
            ActiveBackground.DrawFlowers();
            ActiveBackground.DrawGrass();
            ActiveBackground.DrawSun();
            ActiveBackground.DrawCloud1();
            ActiveBackground.DrawCloud2();
            ActiveBackground.DrawCloud3();
            ActiveBackground.DrawCloud4();
            ActiveBackground.DrawCloud5();

            // Draws the Background-Image
            // sp.Draw(background, new Rectangle(0, 0, bZero, hZero), Color.White);

            // Top-Left corner caption
            // sp.DrawString(font, "Endgame", new Vector2(20, 20), Color.White);

            // The Scoreboard
            sp.DrawString(GameSpecs.scoreFont, Convert.ToInt32(Timer.Score).ToString(), new Vector2((int)(bZero * 0.38), (int)(hZero * 0.011)), Color.OrangeRed);

            // The Placeholder-graphic for the KinectStream
            sp.Draw(placeholder, new Rectangle((int)(bZero * 0.300), (int)(hZero * 0.200), (int)(bZero * 0.400), (int)(hZero * 0.600)), Color.White);

            // The Countdown for taking the photo
            sp.DrawString(GameSpecs.scoreFont, Math.Round(Countdown.Counter).ToString(), new Vector2((float)(bZero / 2) - 80, (float)(hZero - 160)), Color.Turquoise);

            if (Session.PhotoTaken == true)
            {
                // Draw the Button(s)
                foreach (Button btn in bl)
                {
                    sp.Draw(btn.Texture, btn.Position, btn.SourceRectangle, btn.Color, btn.Rotation, btn.Origin, btn.Scale, btn.Effect, btn.LayerDepth);
                }
            }

            sp.End();
        }

        public void Unload()
        {
            GameSpecs.stream.ColorFrameReady -= clientColorFrameReady;
            GameSpecs.PhotoSwitch = false;
        }

        public void Dispose()
        {
            
        }

        Stopwatch sw = new Stopwatch();
        void clientColorFrameReady(object sender, ColorFrameReadyEventArgs e)
        {
            if (sw.ElapsedMilliseconds >= 40)
            {
                if (!GameSpecs.PhotoTimerUp)
                {
                    try
                    {
                        placeholder = Texture2D.FromStream(g, e.ColorFrame.BitmapImage.StreamSource);
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message;
                    }
                }

                else
                {
                    BitmapImage photo = new BitmapImage();
                    if (GameSpecs.PhotoSwitch == false)
                    {
                        photo = e.ColorFrame.BitmapImage.Clone();
                        GameSpecs.TakenPhoto = photo;
                        GameSpecs.PhotoSwitch = true;
                    }
                    else
                    {
                        photo = GameSpecs.TakenPhoto;
                    }
                    
                    /// Catches the Frame and Converts it to memorystream                    
                    Stream s = photo.StreamSource;
                    MemoryStream ms = new MemoryStream();
                    s.Position = 0;
                    bool go = true;
                    while (go)
                    {
                        int cur = s.ReadByte();
                        byte conv = (byte)cur;

                        if (cur == -1)
                        {
                            go = false;
                        }
                        else
                        {
                            ms.WriteByte(conv);
                        }
                    }
                    ms.Position = 0;
                    
                    Session.Picture = ms;
                    placeholder = Texture2D.FromStream(g, s);
                    ms.Close();
                }
                sw.Restart();
            }
        }
    }
}
