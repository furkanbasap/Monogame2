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

        private Texture2D _backgroundTexture;


        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
        }

        public override void LoadContent(ContentManager content) 
        {
            // Laad de sprite van de speler
            _backgroundTexture = content.Load<Texture2D>("Backgrounds/Background");

        }

        public override void Update(GameTime gameTime) {}

        public override void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Begin();

            // Tekenen
            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

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
