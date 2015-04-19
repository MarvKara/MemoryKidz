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

/// Card-Class by Marvin Karaschewski
/// Represents the individual Card-Objects
/// Object is used by the Running-Class

namespace MemoryKidz
{
    /// <summary>
    /// An instance represents a memory-card-object displayed during runtime
    /// </summary>
    public class Card
    {
        public int Id { get; set; }
        public string Motive { get; set; }
        public int MotiveID { get; set; }
        public Rectangle Zone { get; set; }
        public Texture2D CurrentTexture { get; set; }

        public Card(int id, int motive, Texture2D texture)
        {
            this.Id = id;
            this.Motive = "motive" + motive.ToString();
            this.MotiveID = motive;
            this.CurrentTexture = texture;
        }
    }
}
