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

/// IGameState by Marvin Karaschewski 
/// Part of the MemoryKidz-Framework
/// Interface is used as a scheme for all Gamestates

namespace MemoryKidz
{
    public interface IGameState : IDisposable
    {
        void LoadContent();
        GameState Update(GameTime gameTime);
        void Draw(GameTime gameTime, ref SpriteBatch spriteBatch, ref GraphicsDeviceManager graphics, ref SpriteFont font);
        void Unload();
    }
}
