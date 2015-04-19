using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace MemoryKidz
{
    public class ChooseDifficulty : IGameState
    {
        GraphicsDevice g;
        List<Button> bl;

        MouseState currentState;
        MouseState lastState;

        Texture2D background;

        Texture2D easy_select;
        Texture2D easy;
        Texture2D medium_select;
        Texture2D medium;
        Texture2D hard_select;
        Texture2D hard;

        public void LoadContent()
        {
            g = new GraphicsDevice();

            background = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/bg_1080p.png"));
            easy_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Easy_select.png"));
            easy = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Easy.png"));
            medium_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Medium_select.png"));
            medium = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Medium.png"));
            hard_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Hard_select.png"));
            hard = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Hard.png"));
        }

        public ChooseDifficulty(List<Button> btnList)
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

            if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[0].Texture = easy_select;
            }
            else
            {
                bl[0].Texture = easy;
            }

            if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[1].Texture = medium_select;
            }
            else
            {
                bl[1].Texture = medium;
            }

            if (bl[2].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[2].Texture = hard_select;
            }
            else
            {
                bl[2].Texture = hard;
            }

            if (currentState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed)
            {
                // Button to change the Game's difficulty to "Easy" (1)
                if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    g.Clear(Color.Black);
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    GameSpecs.Difficulty = 1;
                    return GameState.Running;
                }

                // Button to change the Game's difficulty to "Medium" (2)
                if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    g.Clear(Color.Black);
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    GameSpecs.Difficulty = 2;
                    return GameState.Running;
                }

                // Button to change the Game's difficulty to "Hard" (3)
                if (bl[2].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    g.Clear(Color.Black);
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    GameSpecs.Difficulty = 3;
                    return GameState.Running;
                }
            }

            return GameState.ChooseDifficulty;
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
            // sp.Draw(background, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), Color.White);

            // sp.DrawString(font, "Choose Difficulty", new Vector2(20, 20), Color.White);

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
