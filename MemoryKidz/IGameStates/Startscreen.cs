using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;

/// Startscreen-class by Marvin Karaschewski
/// Represents the Screen which shows up when the game is started
/// Makes use of the Gamestate-System

namespace MemoryKidz
{
    public class Startscreen : IGameState
    {
        GraphicsDevice g;

        Texture2D background;

        int startCounter;

        public void LoadContent()
        {
            g = new GraphicsDevice();

            background = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/LoadingScreenMKv2.jpg"));

            startCounter = 0;
        }

        public GameState Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            if (Startup() < 505)
            {
                return GameState.Startscreen;
            }
            else
            {
                return GameState.MainMenuNoSession;
            }
        }

        public void Draw(GameTime gameTime, ref SpriteBatch sp, ref GraphicsDeviceManager graphics, ref SpriteFont font)
        {
            sp.Begin();

            // Draws Start-Screen
            g.Clear(Color.Black);

            // Draws the Background-Image
            sp.Draw(background, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), Color.White);

            sp.DrawString(font, "Loading Game...", new Vector2(20, 20), Color.White);
            sp.DrawString(font, (startCounter / 5).ToString() + " %", new Vector2(20, 80), Color.Black);

            sp.End();
        }

        // Simulates a loading-animation
        public int Startup()
        {
            Thread.Sleep(1);
            startCounter++;
            return startCounter;
        }

        public void Unload()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
