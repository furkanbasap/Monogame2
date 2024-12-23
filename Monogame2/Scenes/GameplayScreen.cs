using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Monogame2.GameObjects;
using Monogame2.Global;

namespace Monogame2.Scenes
{
    public enum GameDifficulty { NORMAL, HARD}
    public class GameplayScreen : GameScreen
    {
        private GameDifficulty difficulty;

        private Texture2D _backgroundTexture;
        private int playerLives = 3;

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
            // Laad de sprite van de speler
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
            }

            player.Update();
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

            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("Fonts/Font"), $"Game Mode: {difficulty}", new Vector2(10, 10), Color.White);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("Fonts/Font"), $"Lives: {playerLives}", new Vector2(10, 40), Color.White);

            Globals.spriteBatch.End();


        }


    }
}
