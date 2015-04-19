using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

/// MainMenuSession-class by Marvin Karaschewski
/// Represents the Mainmenu when there is a running Session
/// Makes use of the IGameState-System

namespace MemoryKidz
{
    /// <summary>
    /// Represent actions taken while the gamestate is set to MainMenu while A Session is active
    /// </summary>
    public class MainMenuSession : IGameState
    {
        GraphicsDevice g;
        List<Button> bl;

        MouseState currentState;
        MouseState lastState;

        // Declares all used Textures
        Texture2D background;
        Texture2D resume_select;
        Texture2D resume;
        Texture2D quit_select;
        Texture2D quit;

        public void LoadContent()
        {
            g = new GraphicsDevice();

            // Loads all the used Texture-Files through the Content-Pipeline
            background = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/bg_1080p.png"));
            resume_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Resume_select.png"));
            resume = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Resume.png"));
            quit_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Quit_select.png"));
            quit = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Quit.png"));

            Extension.PauseSound();
        }

        public MainMenuSession(List<Button> btnList)
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

            KeyboardState kbState = Keyboard.GetState();

            // Hover-Check-Routines
            if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[0].Texture = resume_select;
            }
            else
            {
                bl[0].Texture = resume;
            }

            if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[1].Texture = quit_select;
            }
            else
            {
                bl[1].Texture = quit;
            }

            // Check-Routine for catching clicks on the buttons
            if (currentState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed)
            {
                // Button to resume game
                if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    g.Clear(Color.Black);
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    GameSpecs.PreviousGamestate = GameState.MainMenuSession;
                    Extension.SetStates(ref currentState, ref lastState);
                    return GameState.Running;
                }

                // Button to end session
                if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Session.ResetSession();
                    g.Clear(Color.Black);
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    GameSpecs.PreviousGamestate = GameState.MainMenuSession;
                    Extension.SetStates(ref currentState, ref lastState);
                    return GameState.MainMenuNoSession;
                }
            }

            if (kbState.IsKeyDown(Keys.Escape))
            {
                g.Clear(Color.Black);
                Thread.Sleep(200);
                return GameState.Running;
            }

            else
            {
                Extension.SetStates(ref currentState, ref lastState);
                return GameState.MainMenuSession;
            }
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, ref SpriteBatch sp, ref GraphicsDeviceManager graphics, ref SpriteFont font)
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
            // sp.Draw(background, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), Color.White);

            // sp.DrawString(font, "Menu", new Vector2(20, 20), Color.White);

            foreach (Button btn in bl)
            {
                sp.Draw(btn.Texture, btn.Position, btn.SourceRectangle, btn.Color, btn.Rotation, btn.Origin, btn.Scale, btn.Effect, btn.LayerDepth);
            }
            
            sp.End();
        }

        public void Unload()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
