using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Monogame2.GameObjects;
using Monogame2.Global;
using Microsoft.Xna.Framework.Input;
using Monogame2.Utils;
using System.Diagnostics.Metrics;

namespace Monogame2.Scenes
{
    public enum GameDifficulty { NORMAL, HARD}
    public class GameplayScreen : GameScreen
    {
        private GameDifficulty difficulty;

        private Texture2D _backgroundTexture;
        private SpriteFont font;
        private int playerLives = 3;
        private int pointsCounter = 0;

        //Positie en Size van de texture
        Coin coin = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(100, 400), new Vector2(100, 100));
        Coin coin2 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(400, 400), new Vector2(100, 100));
        Coin coin3 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(700, 400), new Vector2(100, 100));
        List<Coin> coins = new();

        Player player = new Player(Globals.content.Load<Texture2D>("Actors/Hero"), new Vector2(100,100), new Vector2(200,200));

        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
        }

        public override void LoadContent() 
        {
            font = Globals.content.Load<SpriteFont>("Fonts/Font");

            _backgroundTexture = Globals.content.Load<Texture2D>("Backgrounds/Background");

            coins.Add(coin);
            coins.Add(coin2);
            coins.Add(coin3);


            coin.LoadContent();
            coin2.LoadContent();
            coin3.LoadContent();

            player.LoadContent();

        }

        public override void Update(GameTime gameTime)
        {
            List<Coin> killList = new();

            foreach (var coin in coins)
            {
                coin.Update();

                if (coin.Rect.Intersects(player.Rect))
                {
                    killList.Add(coin);
                }
            }
            foreach (var coin in killList)
            {
                coins.Remove(coin);
                pointsCounter++;
            }

            player.Update(coins);


            // METHODE OM TE WINNEN MET PUNTEN
            //if (pointsCounter == 3)
            //{
                    
            //    GameStateManager.ChangeState(new GameOverScreen());

            //}
        }


        public override void Draw() 
        {
            Globals.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Tekenen
            Globals.spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            foreach (var coin in coins)
            {
                coin.Draw();
            }

            //coin.Draw();

            player.Draw();

            Globals.spriteBatch.DrawString(font, $"Game Mode: {difficulty}", new Vector2(10, 10), Color.White);
            Globals.spriteBatch.DrawString(font, $"Lives: {playerLives}", new Vector2(10, 40), Color.White);
            Globals.spriteBatch.DrawString(font, $"Points: {pointsCounter}", new Vector2(10, 70), Color.White);

            Globals.spriteBatch.End();


        }


    }
}
