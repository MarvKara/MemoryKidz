using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using System.Windows.Media.Imaging;
using Coding4Fun.Kinect.KinectService.WpfClient;

/// GameSpecs-class by Marvin Karaschewski
/// Provides important information in places which are hard to get to normally
/// class is integral part of the MemoryKidz-Framework

namespace MemoryKidz
{
    /// <summary>
    /// Some properties which need to be globally available
    /// </summary>
    class GameSpecs
    {
        public static int Difficulty { get; set; }
        public static int CurrentCard { get; set; }
        public static SpriteFont scoreFont { get; set; }
        public static GameState PreviousGamestate { get; set; }
        public static bool MusicOn { get; set; }
        public static bool SoundOn { get; set; }
        public static Stream DetailPicture { get; set; }
        public static bool PhotoTimerUp { get; set; }
        public static bool PhotoSwitch { get; set; }
        public static BitmapImage TakenPhoto {get; set;}
        public static ColorClient stream = new ColorClient();
    }
}