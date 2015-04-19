using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

/// Extension by Marvin Karaschewski
/// Makes commonly used functions available for all classes
/// Supports the framework with Randoms, Sounds and integrates the database-classes to the framework

namespace MemoryKidz
{
    static class Extension
    {
        // The SoundEffectInstance representing the currently playing Background-Music-Track
        private static SoundEffectInstance currentBackgroundSound;

        private static SoundEffect menuClickSound;

        /// <summary>
        /// A method to shuffle generic lists
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Starts playing a certain sound
        /// </summary>
        public static void StartSound(ref SoundEffectInstance instance)
        {
            currentBackgroundSound = instance;
            currentBackgroundSound.Volume = 0.1f;
            currentBackgroundSound.IsLooped = true;
            currentBackgroundSound.Play();
            if (!GameSpecs.MusicOn)
            {
                currentBackgroundSound.Volume = 0;
            }
        }

        /// <summary>
        /// Plays a short sound effect
        /// </summary>
        public static void PlaySoundEffect(string sound)
        {
            if (GameSpecs.SoundOn)
            {
                if (sound == "menuClick")
                {
                    menuClickSound = SoundEffect.FromStream(TitleContainer.OpenStream("Content/Sounds/click3.wav"));
                    SoundEffectInstance click = new SoundEffectInstance(menuClickSound);
                    click.Volume = 1.0f;
                    click.Play();
                }

                if (sound == "cardFlip")
                {
                    menuClickSound = SoundEffect.FromStream(TitleContainer.OpenStream("Content/Sounds/CardFlip.wav"));
                    SoundEffectInstance click = new SoundEffectInstance(menuClickSound);
                    click.Volume = 1.0f;
                    click.Play();
                }
            }
        }

        /// <summary>
        /// Mutes the current soundtrack
        /// </summary>
        public static void MuteMusic()
        {
            currentBackgroundSound.Volume = 0;
        }

        /// <summary>
        /// Un-Mutes the current soundtrack
        /// </summary>
        public static void UnmuteMusic()
        {
            currentBackgroundSound.Volume = 0.1f;
        }

        /// <summary>
        /// Stops playing the current sound
        /// </summary>
        public static void StopSound()
        {
            currentBackgroundSound.Stop();
        }

        /// <summary>
        /// Pauses The current sound
        /// </summary>
        public static void PauseSound()
        {
            // currentBackgroundSound.Pause();
            currentBackgroundSound.Volume = 0;
        }

        /// <summary>
        /// Resume the current sound
        /// </summary>
        public static void ResumeSound()
        {
            currentBackgroundSound.Volume = 0.2f;
            if (!GameSpecs.MusicOn)
            {
                currentBackgroundSound.Volume = 0;
            }
        }

        /// <summary>
        /// Mutes all Clicksounds
        /// </summary>
        public static void MuteUnmuteClickSounds()
        {
            if (GameSpecs.SoundOn)
            {
                GameSpecs.SoundOn = false;
            }
            else
            {
                GameSpecs.SoundOn = true;
            }
        }

        /// <summary>
        /// Generator for the position of the detailview buttons
        /// </summary>
        /// <param name="gdm"></param>
        /// <returns></returns>
        public static List<Rectangle> GenerateViewButtonPositions(ref GraphicsDeviceManager gdm)
        {
            List<Rectangle> positions = new List<Rectangle>();
            List<int> dimensions = Extension.CalculateDimensionsInPixel(ref gdm);

            int hZero = gdm.GraphicsDevice.DisplayMode.Height;
            int bZero = gdm.GraphicsDevice.DisplayMode.Width;

            int pictureWidth = 0;
            int pictureHeight = 0;

            if (hZero < 1080)
            {
                pictureHeight = 150;
                pictureWidth = 150;
            }
            else
            {
                pictureHeight = 200;
                pictureWidth = 200;
            }

            int yStart = dimensions[3] + 4;

            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {
                    positions.Add(new Rectangle((dimensions[6] + dimensions[9]), dimensions[4], pictureWidth, pictureHeight));
                }
                if (i == 1)
                {
                    positions.Add(new Rectangle(dimensions[6] + dimensions[8] + dimensions[10] + 8, yStart + (4 + dimensions[2] * 1), dimensions[8], dimensions[2]));
                }
                else
                {
                    positions.Add(new Rectangle(dimensions[6] + dimensions[8] + dimensions[10] + 8, yStart + (4 + dimensions[2] * i), dimensions[8], dimensions[2]));
                }
            }

            return positions;
        }

        /// <summary>
        /// Calculates the dimensions of Highscore-table according to the screenresolution
        /// </summary>
        /// <returns>List of integer values: ha[0], hb[1], hc[2], hd[3], he[4], hf[5], ba[6], bb[7], bc[8], bd[9], be[10], bf[11]</returns>
        public static List<int> CalculateDimensionsInPixel(ref GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int hZero = graphics.PreferredBackBufferHeight;
            int bZero = graphics.PreferredBackBufferWidth;

            List<int> dimensions = new List<int>();
            int ha, hb, hc, hd, he, hf, ba, bb, bc, bd, be, bf;

            ha = (int)Math.Round((double)hZero * 0.9861);
            hb = (int)Math.Round((double)hZero * 0.0138);
            hc = (int)Math.Round((double)hZero * 0.0787);
            hd = (int)Math.Round((double)hZero * 0.2778);
            he = (int)Math.Round((double)hZero * 0.0463);
            hf = (int)Math.Round((double)hZero * 0.1851);
            ba = (int)Math.Round((double)bZero * 0.1563);
            bb = (int)Math.Round((double)bZero * 0.6875);
            bc = (int)Math.Round((double)bZero * 0.3125);
            bd = (int)Math.Round((double)bZero * 0.2116);
            be = (int)Math.Round((double)bZero * 0.0625);
            bf = (int)Math.Round((double)bZero * 0.1041);

            dimensions = FillDimensionList(dimensions, ha, hb, hc, hd, he, hf, ba, bb, bc, bd, be, bf);

            return dimensions;
        }

        /// <summary>
        /// Shorthand-Method for filling the List of Dimensions. Just to save Code!
        /// </summary>
        public static List<int> FillDimensionList(List<int> dim, int ha, int hb, int hc, int hd, int he, int hf, int ba, int bb, int bc, int bd, int be, int bf)
        {
            dim.Add(ha);
            dim.Add(hb);
            dim.Add(hc);
            dim.Add(hd);
            dim.Add(he);
            dim.Add(hf);
            dim.Add(ba);
            dim.Add(bb);
            dim.Add(bc);
            dim.Add(bd);
            dim.Add(be);
            dim.Add(bf);

            return dim;
        }

        /// <summary>
        /// Trys to insert value to dictionary. Prohibits doublettes, returns false if failed, is run again after failing.
        /// </summary>
        private static bool TryInsert(ref Dictionary<int, byte[]> dict, KeyValuePair<int, byte[]> vp)
        {
            try
            {
                dict.Add(vp.Key, vp.Value);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
            return true;
        }

        public static void SetStates(ref MouseState currentState, ref MouseState lastState)
        {
            lastState = currentState;
            currentState = Mouse.GetState();
        }

        /// <summary>
        /// Shuts down the Childprocress, which handles all Kinect-based activities
        /// </summary>
        public static void ShutDownKinect()
        {
            Process[] processes = Process.GetProcessesByName("KinectCursorController");
            if (processes.Length != 0)
            {
                processes[0].Kill();
            }
        }

        /// <summary>
        /// Handles the scores. 
        /// Checks if there is a new Highscore and handles the input via the database class
        /// </summary>
        public static void HandleScores(int currentScore)
        {
            int[] highscores = DatabaseConnection.ReadValuesOnly(GameSpecs.Difficulty).ToArray();
            bool isNewHighscore = false;

            List<byte[]> images = new List<byte[]>();

            // Gets the images for each score and writes them to a List
            foreach (int score in highscores)
            {
                MemoryStream s = DatabaseConnection.ReadSingleImage(score, GameSpecs.Difficulty);
                images.Add(s.ToArray());
            }

            Dictionary<int, byte[]> valuePairs = new Dictionary<int, byte[]>();

            // Assembles the value Dictionary with the integers and images
            for (int i = 0; i < 10; i++)
            {
                valuePairs.Add(highscores[i], images.ToArray()[i]);
            }

            // Checks if a new highscore is reached
            foreach (int score in highscores)
            {
                if (currentScore > score)
                {
                    isNewHighscore = true;
                }
            }

            // If a new highscore is reached the new values are placed into the list and the list is sorted descending
            if (isNewHighscore)
            {
                int insertScore = Timer.GetScore();

                while (!TryInsert(ref valuePairs, new KeyValuePair<int, byte[]>(insertScore, Session.Picture.ToArray())))
                {
                    insertScore++;
                }

                // Orders the dictionary to sort in the new highscore which the player achieved
                valuePairs = valuePairs.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                /// Removes the lowest value from the collection which makes it ready for being replacement for the values in the database
                List<int> keys = new List<int>();
                foreach (KeyValuePair<int, byte[]> kvp in valuePairs)
                {
                    keys.AddRange(valuePairs.Keys);
                }

                int lowestValue = keys[10];
                valuePairs.Remove(lowestValue);

                DatabaseConnection.ReplaceHighscores(valuePairs);
            }
        }

        public static class ThreadSafeRandom
        {
            [ThreadStatic]
            private static Random Local;

            public static Random ThisThreadsRandom
            {
                get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
            }
        }
    }
}