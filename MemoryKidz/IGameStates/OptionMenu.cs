using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

/// OptionMenu-class by Marvin Karaschewski
/// Represents the OptionsMenu-Screen
/// Makes use of the IGameState-System

namespace MemoryKidz
{
    public class OptionMenu : IGameState
    {
        GraphicsDevice g;
        List<Button> bl;

        MouseState currentState;
        MouseState lastState;


        // Declares all used Textures
        Texture2D background;
        Texture2D back_select;
        Texture2D back;
        Texture2D sound_select;
        Texture2D sound;
        Texture2D sound2_select;
        Texture2D sound2;
        Texture2D noSound_select;
        Texture2D noSound;
        Texture2D noSound2_select;
        Texture2D noSound2;

        public void LoadContent()
        {
            g = new GraphicsDevice();

            // Loads all the used Texture-Files through the Content-Pipeline
            background = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/bg_1080p.png"));
            back_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Back_select.png"));
            back = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Back.png"));
            sound_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Sound_select.png"));
            sound = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Sound.png"));
            sound2_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Sound2_select.png"));
            sound2 = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Sound2.png"));
            noSound_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/NoSound_select.png"));
            noSound = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/NoSound.png"));
            noSound2_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/NoSound2_select.png"));
            noSound2 = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/NoSound2.png"));
        }

        public OptionMenu(List<Button> btnList)
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

            // Hover-Check-Routines
            if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[0].Texture = back_select;
            }
            else
            {
                bl[0].Texture = back;
            }

            if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                if (GameSpecs.SoundOn)
                {
                    bl[1].Texture = sound_select;
                }
                else
                {
                    bl[1].Texture = noSound_select;
                }
            }
            else
            {
                if (GameSpecs.SoundOn)
                {
                    bl[1].Texture = sound;
                }
                else
                {
                    bl[1].Texture = noSound;
                }
            }

            if (bl[2].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                if (GameSpecs.MusicOn)
                {
                    bl[2].Texture = sound2_select;
                }
                else
                {
                    bl[2].Texture = noSound2_select;
                }
            }
            else
            {
                if (GameSpecs.MusicOn)
                {
                    bl[2].Texture = sound2;
                }
                else
                {
                    bl[2].Texture = noSound2;
                }
            }

            // Click-Check-Routine
            if (currentState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed)
            {
                // Button to return to MainMenu-Screen
                if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    Thread.Sleep(200);
                    GameSpecs.PreviousGamestate = GameState.OptionMenu;
                    return GameState.MainMenuNoSession;
                }
                if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    if (GameSpecs.SoundOn)
                    {
                        Extension.MuteUnmuteClickSounds();
                        Thread.Sleep(150);
                    }
                    else
                    {
                        Extension.MuteUnmuteClickSounds();
                    }

                }
                if (bl[2].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    if (GameSpecs.MusicOn)
                    {
                        Extension.MuteMusic();
                        GameSpecs.MusicOn = false;
                        Thread.Sleep(150);
                    }
                    else
                    {
                        Extension.UnmuteMusic();
                        GameSpecs.MusicOn = true;
                        Thread.Sleep(150);
                    }
                }
            }

            return GameState.OptionMenu;
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

            // sp.DrawString(font, "Options", new Vector2(20, 20), Color.White);

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
