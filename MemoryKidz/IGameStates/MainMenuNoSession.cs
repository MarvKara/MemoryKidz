using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using System.Diagnostics;

/// MainMenuNoSession-class by Marvin Karaschewski
/// Represents the normal Mainmenu without a running Session
/// Makes use of the IGameState-System

namespace MemoryKidz
{
    /// <summary>
    /// Represent actions taken while the gamestate is set to MainMenu while no Session is active
    /// </summary>
    public class MainMenuNoSession : IGameState
    {
        GraphicsDevice g;
        List<Button> bl;

        // Declares all used Textures
        Texture2D background;
        Texture2D newGame_select;
        Texture2D newGame;
        Texture2D options_select;
        Texture2D options;
        Texture2D quit_select;
        Texture2D quit;

        MouseState currentState;
        MouseState lastState;

        // Declares all used Sounds
        SoundEffect backgroundMusicFile;
        SoundEffectInstance backgroundMusic;

        public void LoadContent()
        {
            g = new GraphicsDevice();

            // Loads all the used Texture-Files through the Content-Pipeline
            background = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/bg_1080p.png"));
            newGame_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/NewGame_select.png"));
            newGame = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/NewGame.png"));
            options_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Options_select.png"));
            options = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Options.png"));
            quit_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Quit_select.png"));
            quit = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/Quit.png"));

            // Loads all the used Sound-files through the Content-Pipeline
            backgroundMusicFile = SoundEffect.FromStream(TitleContainer.OpenStream("Content/Sounds/MoveForward.wav"));
            backgroundMusic = new SoundEffectInstance(backgroundMusicFile);

            // Stops the current Background-Music in case there is any
            if (Session.Active == true)
            {
                Extension.StopSound();
            }

            if (GameSpecs.PreviousGamestate == GameState.Startscreen || GameSpecs.PreviousGamestate == GameState.MainMenuSession || GameSpecs.PreviousGamestate == GameState.Highscore)
            {
                // Starts the Background-Music suited for the Menu
                Extension.StartSound(ref backgroundMusic);

                if (!GameSpecs.MusicOn)
                {
                    Extension.MuteMusic();
                }
            }

            if (GameSpecs.PreviousGamestate == GameState.OptionMenu)
            {
                // NONE
            }
        }

        public MainMenuNoSession(List<Button> btnList)
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
                bl[0].Texture = newGame_select;
            }
            else
            {
                bl[0].Texture = newGame;
            }

            if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[1].Texture = options_select;
            }
            else
            {
                bl[1].Texture = options;
            }

            if (bl[2].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[2].Texture = quit_select;
            }
            else
            {
                bl[2].Texture = quit;
            }

            // Check-Routine for catching clicks on the buttons
            if (currentState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed)
            {
                // Button to start game

                if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    g.Clear(Color.Black);
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    Extension.SetStates(ref currentState, ref lastState);

                    return GameState.ChooseDifficulty;
                }

                // Button to open optionsmenu
                if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    g.Clear(Color.Black);
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    Extension.SetStates(ref currentState, ref lastState);
                    return GameState.OptionMenu;
                }

                // Button to end game
                if (bl[2].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    g.Clear(Color.Black);
                    Extension.PlaySoundEffect("menuClick");
                    Thread.Sleep(200);
                    Extension.SetStates(ref currentState, ref lastState);

                    Extension.ShutDownKinect();

                    return GameState.None;
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
                return GameState.MainMenuNoSession;
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

            // Top-Left corner caption
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
