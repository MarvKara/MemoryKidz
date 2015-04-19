using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

/// Session-Class by Marvin Karaschewski
/// Static class used to save current Session data and carry it over to other parts of the program
/// class is integral part of the MemoryKidz-Framework

namespace MemoryKidz
{
    /// <summary>
    /// Saves information about the currently running Session
    /// </summary>
    public static class Session
    {
        public static int Id { get; set; }
        public static int Score { get; set; }
        public static int Difficulty { get; set; }
        public static List<Card> CardList { get; set; }
        public static bool Active { get; set; }
        public static List<Card> TurnedCards { get; set; }
        public static int PairsUntilWin { get; set; }
        public static bool Gameover { get; set; }
        public static bool PhotoTaken { get; set; }
        public static bool ValueInserted { get; set; }
        public static MemoryStream Picture{ get; set; }
        public static bool ScoresHandled { get; set; }
        public static int[] LatestScoreBoard { get; set; }
        

        public static void NewSession(int difficulty, List<Card> cardList)
        {
            Id = 1;
            Difficulty = difficulty;
            Score = Timer.GetScore();
            CardList = cardList;
            TurnedCards = new List<Card>();
            PairsUntilWin = cardList.Count / 2;
            Gameover = false;
            PhotoTaken = false;
            ValueInserted = false;
        }

        public static void ResetTurnedCards()
        {
            TurnedCards.Clear();
        }

        public static void ResetSession()
        {
            Timer.ResetTimer();
            Extension.StopSound();
            Active = false;
        }
    }
}