using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MemoryKidz
{
    /// <summary>
    /// ActiveBackground-Class by Gerben Hofman
    /// Supplies the Game with some Moving graphical assets, to make the background more lively
    /// </summary>
    public class ActiveBackground
    {
        public static void DrawSky()
        {
            MainGame.spriteBatch.Draw(MainGame.skyTexture, MainGame.skyPosition, Color.White);
        }

        public static void DrawGrass()
        {
            MainGame.spriteBatch.Draw(MainGame.grassTexture, MainGame.grassPosition, Color.White);
        }

        public static void DrawFlowers()
        {
            MainGame.spriteBatch.Draw(MainGame.flowerTexture, MainGame.flowerPosition, Color.White);
        }

        public static void DrawSun()
        {
            MainGame.spriteBatch.Draw(MainGame.sunTexture, MainGame.sunPosition, Color.White);
        }

       public static void DrawSunrays()
        {
            float circle = MathHelper.Pi * 2;
            MainGame.RotationAngle = MainGame.RotationAngle % circle;
            MainGame.RotationAngle += 0.0003f;

            MainGame.sunraysOrigin = new Vector2(1920, 1920);
            Rectangle sunrayRectangle = new Rectangle(0, 0, 3840, 3840);            
            MainGame.spriteBatch.Draw(MainGame.sunraysTexture, sunrayRectangle, null, Color.White * 0.1f, MainGame.RotationAngle, MainGame.sunraysOrigin, SpriteEffects.None, 0);
        }  

        public static void DrawCloud1()
        {
            MainGame.spriteBatch.Draw(MainGame.cloudTexture, MainGame.cloudPosition1, Color.White);
        }

        public static void DrawCloud2()
        {
            MainGame.spriteBatch.Draw(MainGame.cloudTexture, MainGame.cloudPosition2, Color.White);
        }

        public static void DrawCloud3()
        {
            MainGame.spriteBatch.Draw(MainGame.cloudTexture, MainGame.cloudPosition3, Color.White);
        }

        public static void DrawCloud4()
        {
            MainGame.spriteBatch.Draw(MainGame.cloudTexture, MainGame.cloudPosition4, Color.White);
        }

        public static void DrawCloud5()
        {
            MainGame.spriteBatch.Draw(MainGame.cloudTexture, MainGame.cloudPosition5, Color.White);
        }
    }
}