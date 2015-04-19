#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
using System.IO;
#endregion

/// Highscore-class by Marvin Karaschewski
/// Represents the Highscore-Screen
/// Makes use of the IGameState-System

namespace MemoryKidz
{
    class Highscore : IGameState
    {
        GraphicsDevice g;
        GraphicsDeviceManager gdm;

        MouseState currentState;
        MouseState lastState;

        List<Rectangle> lines;

        Texture2D lineTexture;
        Texture2D background;
        Texture2D backArrow;
        Texture2D backArrow_select;
        Texture2D retryArrow;
        Texture2D retryArrow_select;
        Texture2D leader_picture;
        Texture2D player_picture_frame;
        Texture2D button_photo_select;
        Texture2D button_photo;

        List<Button> bl;
        //List<Rectangle> imageClickZones;

        List<int> scoreValues;

        public void LoadContent()
        {
            g = new GraphicsDevice();
            lineTexture = new Texture2D(g, 1, 1, false, SurfaceFormat.Color);

            // Loads all used Textures via streaming
            background = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/bg_1080p.png"));
            backArrow = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/BackArrow.png"));
            backArrow_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/BackArrow_select.png"));
            retryArrow = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/RetryArrow.png"));
            retryArrow_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/RetryArrow_select.png"));
            leader_picture = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/placeholder_player.png"));
            player_picture_frame = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Others/player_picture_frame.png"));
            button_photo_select = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo_select.png"));
            button_photo = Texture2D.FromStream(g, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            Int32[] pixel = { 0xFFFFFF }; // White. 0xFF is Red, 0xFF0000 is Blue
            lineTexture.SetData<Int32>(pixel, 0, lineTexture.Width * lineTexture.Height);

            /// Converts a Stream which opens picture to a useable Memorystream for further use
            //Stream s = TitleContainer.OpenStream("Content/Textures/Cardset2/backside.png");
            //MemoryStream ms = new MemoryStream();
            //s.CopyTo(ms);
            //Session.Picture = ms;

            lines = GenerateHighscoreTableLines(ref gdm);

            /// If the Game returns from the detailview the scores are not handled again,
            /// to prevent the values from being written to the database again
            if (GameSpecs.PreviousGamestate != GameState.ImageDetailView)
            {
                Extension.HandleScores(Session.Score);
            }

            // Reads the score-values from the database for the given difficulty
            scoreValues = DatabaseConnection.ReadValuesOnly(GameSpecs.Difficulty);

            // The picture of the Leader of the leaderboard
            leader_picture = Texture2D.FromStream(g, DatabaseConnection.ReadSingleImage(scoreValues[0], GameSpecs.Difficulty));
        }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Highscore(List<Button> btnList, ref GraphicsDeviceManager g)
        {
            gdm = g;
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

            // Hover-Check-Routine for the Button(s)
            if (bl[0].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[0].Texture = backArrow_select;
            }
            else
            {
                bl[0].Texture = backArrow;
            }

            if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[1].Texture = retryArrow_select;
            }
            else
            {
                bl[1].Texture = retryArrow;
            }

            if (bl[2].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[2].Texture = button_photo_select;
            }
            else
            {
                bl[2].Texture = button_photo;
            }

            if (bl[3].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[3].Texture = button_photo_select;
            }
            else
            {
                bl[3].Texture = button_photo;
            }

            if (bl[4].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[4].Texture = button_photo_select;
            }
            else
            {
                bl[4].Texture = button_photo;
            }

            if (bl[5].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[5].Texture = button_photo_select;
            }
            else
            {
                bl[5].Texture = button_photo;
            }

            if (bl[6].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[6].Texture = button_photo_select;
            }
            else
            {
                bl[6].Texture = button_photo;
            }

            if (bl[7].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[7].Texture = button_photo_select;
            }
            else
            {
                bl[7].Texture = button_photo;
            }

            if (bl[8].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[8].Texture = button_photo_select;
            }
            else
            {
                bl[8].Texture = button_photo;
            }

            if (bl[9].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[9].Texture = button_photo_select;
            }
            else
            {
                bl[9].Texture = button_photo;
            }

            if (bl[10].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[10].Texture = button_photo_select;
            }
            else
            {
                bl[10].Texture = button_photo;
            }

            if (bl[11].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
            {
                bl[11].Texture = button_photo_select;
            }
            else
            {
                bl[11].Texture = button_photo;
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
                    GameSpecs.PreviousGamestate = GameState.Highscore;
                    Session.ResetSession();
                    return GameState.MainMenuNoSession;
                }

                // Button to directly re-run for a new game
                if (bl[1].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    Thread.Sleep(200);
                    GameSpecs.PreviousGamestate = GameState.Highscore;
                    Session.ResetSession();
                    return GameState.ChooseDifficulty;
                }
                // Button to go to detailview for extended picture
                if (bl[2].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[0], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[3].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[1], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[4].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[2], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[5].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[3], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[6].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[4], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[7].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[5], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[8].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[6], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[9].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[7], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[10].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[8], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
                // Button to go to detailview for extended picture
                if (bl[11].ClickTangle.Contains(new Point(currentState.X, currentState.Y)))
                {
                    Extension.PlaySoundEffect("menuClick");
                    g.Clear(Color.Black);
                    GameSpecs.DetailPicture = DatabaseConnection.ReadSingleImage(Session.LatestScoreBoard[9], GameSpecs.Difficulty);
                    Thread.Sleep(200);
                    return GameState.ImageDetailView;
                }
            }

            return GameState.Highscore;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, ref Microsoft.Xna.Framework.Graphics.SpriteBatch sp, ref Microsoft.Xna.Framework.GraphicsDeviceManager g, ref Microsoft.Xna.Framework.Graphics.SpriteFont font)
        {
            sp.Begin();

            g.GraphicsDevice.Clear(Color.Black);

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

            // sp.Draw(background, new Rectangle(0, 0, g.PreferredBackBufferWidth, g.PreferredBackBufferHeight), Color.White);

            // sp.DrawString(font, "Highscore", new Vector2(20, 20), Color.Black);

            /// <summary>
            /// Draws all the Lines necessary for the table to be displayed
            /// </summary>
            int index = 0;
            foreach (Rectangle line in lines)
            {
                sp.Draw(lineTexture, lines[index],Color.OrangeRed);
                index++;
            }

            int offsetX = (int)Math.Round((double)g.PreferredBackBufferWidth * 0.020);
            int offsetY = (int)Math.Round((double)g.PreferredBackBufferHeight * 0.020);

            /// <summary>
            /// Draws the linenumbers onto the table
            /// </summary>
            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            sp.DrawString(GameSpecs.scoreFont, (i + 1).ToString(), new Vector2(lines[i].X + offsetX, lines[i].Y - (int)(offsetY * 10)), Color.OrangeRed);
                            break;
                        }
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        {
                            sp.DrawString(font, (i + 1).ToString(), new Vector2(lines[i-1].X + offsetX, lines[i-1].Y + offsetY), Color.OrangeRed);
                            break;
                        }
                }
            }

            ///<summary>
            /// Writes the Highscores into the table
            ///</summary>

            int[] scores = scoreValues.ToArray();

            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {
                    sp.DrawString(GameSpecs.scoreFont, scores[i].ToString(), new Vector2(lines[i].X + offsetX * 20, lines[i].Y + (int)(offsetY - offsetY * 11)), Color.OrangeRed);
                }
                else
                {
                    sp.DrawString(font, scores[i].ToString(), new Vector2(lines[i-1].X + offsetX * 4, lines[i-1].Y + offsetY), Color.OrangeRed);
                }
            }

            // Draw the Button(s)
            foreach (Button btn in bl)
            {
                sp.Draw(btn.Texture, new Rectangle((int)btn.Position.X, (int)btn.Position.Y, btn.SourceRectangle.Width, btn.SourceRectangle.Height), Color.White);
            }

            List<int> dimensions = Extension.CalculateDimensionsInPixel(ref g);

            int pictureWidth = 0;
            int pictureHeight = 0;

            if (g.GraphicsDevice.DisplayMode.Height < 1080)
            {
                pictureHeight = 150;
                pictureWidth = 150;
            }
            else
            {
                pictureHeight = 200;
                pictureWidth = 200;
            }
            // Draws the picture of the leader of the highscore-ladder to the screen
            sp.Draw(player_picture_frame, new Rectangle((dimensions[6] + dimensions[9]) - 15, dimensions[4] - 15, pictureWidth+30, pictureHeight+30), Color.White);
            sp.Draw(leader_picture, new Rectangle(dimensions[6] + dimensions[9], dimensions[4], pictureWidth, pictureHeight), Color.White);

            sp.End();
        }

        public List<Rectangle> GenerateHighscoreTableLines(ref GraphicsDeviceManager graphics)
        {
            // Those Rectangles represent the Lines of the table!
            List<Rectangle> lines = new List<Rectangle>();

            int hZero = graphics.PreferredBackBufferHeight;
            int bZero = graphics.PreferredBackBufferWidth;

            List<int> dimensions = Extension.CalculateDimensionsInPixel(ref graphics);

            // Offsets correct some lack of pixels in certain places
            // Out: Moves the table up a abit 
            // Inline: Adds some Pixels to the horizontal lines to match the grid
            int offsetOut = (int)Math.Round((double)hZero * 0.020);
            int offsetInline = (int)Math.Round((double)hZero * 0.004);

            // Horizontal Lines ([0]-[10])
            lines.Add(new Rectangle(dimensions[6], dimensions[3], dimensions[7] + offsetInline, 4));
            for (int i = 0; i < 10; i++)
            {
                lines.Add(new Rectangle(dimensions[6], dimensions[3] + (dimensions[2] * lines.Count + 1), dimensions[7] + offsetInline, 4));
            }

            // Vertical Lines ([11]-[14])
            lines.Add(new Rectangle(bZero - dimensions[6], dimensions[1] - offsetOut, 4, dimensions[0] + (int)(offsetInline * 2)));
            lines.Add(new Rectangle(dimensions[6] + dimensions[10] + dimensions[8], dimensions[3], 4, dimensions[0] - dimensions[3]));
            lines.Add(new Rectangle(dimensions[6] + dimensions[10], dimensions[3], 4, dimensions[0] - dimensions[3]));
            lines.Add(new Rectangle(dimensions[3], dimensions[1] - offsetOut, 4, dimensions[0] + (int)(offsetInline * 2)));

            return lines;
        }

        public void Unload()
        {
            
        }

        public void Dispose()
        {

        }
    }
}