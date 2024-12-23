using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using Monogame2.GameObjects;
using Monogame2.Animation;
using Monogame2.Input;

namespace Monogame2.Scenes
{
    public enum GameDifficulty { NORMAL, HARD}
    public class GameplayScreen : GameScreen
    {
        private GameDifficulty difficulty;

        private Texture2D _backgroundTexture;

        private int playerLives = 3;

        //coin
        private Texture2D _coinTexture;

        private Vector2 _position = new Vector2(200, 200);

        private Animations _anim;
        //coin

        //player
        private Texture2D _playerTexture;
        //player

        //private Coin coin = new Coin(new Vector2(200,200));

        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
        }

        public override void LoadContent(ContentManager content) 
        {
            // Laad de sprite van de speler
            _backgroundTexture = content.Load<Texture2D>("Backgrounds/Background");
            _coinTexture = content.Load<Texture2D>("Objects/coin");
            _playerTexture = content.Load<Texture2D>("Actors/Hero");
            _anim = new(_coinTexture, 16, 1, 0.1f);
           

        }

        public override void Update(GameTime gameTime) 
        {
            //coin.Update();
            _anim.Update();
            InputManager.Update();
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Begin();

            // Tekenen
            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_coinTexture, new Vector2(300, 300), Color.White);
            spriteBatch.Draw(_playerTexture, new Vector2(500, 500), Color.White);

            spriteBatch.DrawString(Game1.ContentManager.Load<SpriteFont>("Fonts/Font"),$"Game Mode: {difficulty}",new Vector2(10, 10),Color.White);
            spriteBatch.DrawString(Game1.ContentManager.Load<SpriteFont>("Fonts/Font"), $"Lives: {playerLives}", new Vector2(10, 40), Color.White);

            _anim.Draw(_position);

            spriteBatch.End();
        }


    }
}
