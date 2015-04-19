using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MemoryKidz
{
    class ImageDetailView : IGameState
    {
        GraphicsDevice g;

        // Declares all used Textures
        // Texture2D background;
        Texture2D player_picture;

        MouseState currentState;
        MouseState lastState;

        Rectangle detailPictureOutlines;

        int hZero;
        int bZero;

        public void LoadContent()
        {
            g = new GraphicsDevice();

            hZero = g.DisplayMode.Height;
            bZero = g.DisplayMode.Width;

            // Loads all the used Texture-Files through the Content-Pipeline
            // background = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/bg_1080p.png"));

            // The picture which should be shown on the screen in detail
            player_picture = Texture2D.FromStream(g, GameSpecs.DetailPicture);

            detailPictureOutlines = new Rectangle((int)(bZero * 0.25), (int)(hZero * 0.20), 800, 600);
        }

        public GameState Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            lastState = currentState;
            currentState = Mouse.GetState();

            if (currentState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed)
            {
                if(!detailPictureOutlines.Contains(new Point(currentState.X, currentState.Y)))
                {
                    GameSpecs.PreviousGamestate = GameState.ImageDetailView;
                    return GameState.Highscore;
                }
                Extension.SetStates(ref currentState, ref lastState);
            }

            return GameState.ImageDetailView;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, ref Microsoft.Xna.Framework.Graphics.SpriteBatch sp, ref Microsoft.Xna.Framework.GraphicsDeviceManager g, ref Microsoft.Xna.Framework.Graphics.SpriteFont font)
        {
            sp.Begin();

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

            // Draws the player-picture in question to detailview
            // sp.Draw(player_picture, detailPictureOutlines, Color.White);

            sp.Draw(player_picture, new Rectangle((int)(bZero * 0.300), (int)(hZero * 0.200), (int)(bZero * 0.400), (int)(hZero * 0.600)), Color.White);

            // Draws the caption in the Topleft-Corner
            // sp.DrawString(font, "Detailview - Click anywhere to return", new Vector2(20, 20), Color.Black);

            // Draws the caption that tells the player how to return to the highscore-tablescreen
            sp.DrawString(font, "Click on the Background to return", new Vector2((int)(bZero * 0.300), 40), Color.White);
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
