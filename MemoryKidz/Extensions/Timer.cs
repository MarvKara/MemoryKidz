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
#endregion

namespace MemoryKidz
{
    public class Timer
    {
        public static double Score { get; set; }
        public static string Zero { get; set; }
        public static bool Running { get; set; }

        public static void Initialize(int amount)
        {
            Running = true;
            Zero = "";
            Score = amount;
        }

        public static void StartTimer()
        {
            Running = true;
        }

        public static void StopTimer()
        {
            Running = false;
        }

        public static void ResetTimer()
        {
            Score = 6000;
        }

        public static int GetScore()
        {
            int s = Int32.Parse(Math.Round(Score).ToString());
            return s;
        }

        public static string ConvertOutput()
        {
            // Placing 0 to keep the score length to 4
            if (Timer.GetScore() > 100 && Timer.GetScore() < 1000)
            {
                return Zero = "0";
            }

            else if (Timer.GetScore() > 10 && Timer.GetScore() < 100)
            {
                return Zero = "00";
            }

            else if (Timer.GetScore() < 10)
            {
                return Zero = "000";
            }

            else
            {
                return Zero = "";
            }
        }
    }
}