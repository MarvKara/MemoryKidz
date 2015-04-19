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

/// Class by Michel Pater
/// v1.0
/// Slight changes by Marvin Karaschewski

namespace MemoryKidz
{
    /// <summary>
    /// Represents a clickable Button
    /// </summary>
    public class Button : DrawableGameComponent
    {
        //Boolean for checking if the button is selected
        bool _isSelected = false;
        //Event to simulate a click (ENTER event)
        public event Action<Button> Click;

        public Button(Game game): base(game)            
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);
            
            // Variables that are needed to draw the button on the stage
            Position = Vector2.Zero;
            SourceRectangle = new Rectangle();
            Color = Color.White;
            Scale = Vector2.One;
        }

        public Button(Game game, Vector2 pos, Rectangle outlines): base(game)
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            // Variables that are needed to draw the button on the stage
            Position = pos;
            SourceRectangle = outlines;
            Color = Color.White;
            Scale = Vector2.One;
        }

        /// <summary>
        /// Override for the update method for checking if the button is selected
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //Gettin the keyboardstate
            KeyboardState kbs = Keyboard.GetState();

            //Checkin if the button is selected and if the ENTER button has been pressed
            if (kbs.IsKeyDown(Keys.Enter) && IsSelected)
            {
                if (Click != null)
                {
                    Click(this);
                }
            }
        }

        #region Variable Definitions
        //Declaring all the listed variables, with get and set methods
        public SpriteBatch SpriteBatch { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin{ get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects Effect { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public float LayerDepth { get; set; }
        public Rectangle ClickTangle { get; set; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set { 
                _isSelected = value; 
                //Changing the texture rectangle when the button is selected (from x = 0 to x = 350 in the buttontexture)
                if (_isSelected)
                {
                    SourceRectangle = new Rectangle(350, 0, 350, Texture.Height);
                }
                else
                {
                    SourceRectangle = new Rectangle(0, 0, 350, Texture.Height);
                }
                
            }
        }
        #endregion

        /// <summary>
        /// Overriding the Draw method for button texture drawing
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effect, LayerDepth);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
