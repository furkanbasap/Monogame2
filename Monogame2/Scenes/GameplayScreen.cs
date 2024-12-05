using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.Scenes
{
    public enum GameDifficulty { NORMAL, HARD}
    public class GameplayScreen : GameScreen
    {
        private GameDifficulty difficulty;
        private Texture2D heroTexture;      // Sprite van de speler
        private Vector2 heroPosition;       // Positie van de speler


        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
            heroPosition = new Vector2(100, 100); // Startpositie van de speler
        }

        public override void LoadContent(ContentManager content) 
        {
            // Laad de sprite van de speler
            heroTexture = content.Load<Texture2D>("Sprites/Player"); // Zorg dat Player.jpg correct is toegevoegd
        }

        public override void Update(GameTime gameTime) {}

        public override void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Begin();

            // Teken de speler
            spriteBatch.Draw(heroTexture, heroPosition, Color.White);

            // Voeg eventueel andere tekenlogica toe, zoals moeilijkheidsgraad weergeven
            spriteBatch.DrawString(
                Game1.ContentManager.Load<SpriteFont>("Fonts/Font"),
                $"Game Mode: {difficulty}",
                new Vector2(10, 10),
                Color.White
            );

            spriteBatch.End();
        }


    }
}
