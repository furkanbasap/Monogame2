using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Monogame2.GameObjects;
using Monogame2.Global;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Monogame2.Utils;
using System.Diagnostics.Metrics;
using System;
using Microsoft.Xna.Framework.Audio;
using Monogame2.Physics;

namespace Monogame2.Scenes
{
    public enum GameDifficulty { NORMAL, HARD}
    public class GameplayScreen : GameScreen
    {
        private GameDifficulty difficulty;

        private ScrollingBackground myBackground;
        private float scrollingSpeed = 1;

        private SpriteFont font;
        private int playerLives = 3;
        private int pointsCounter = 0;
        private static Random rnd = new Random();
        SoundEffect sfxCoin;
        Song songBackground;
        private Camera camera;


        //Position and Size of the texture
        //Coin coin = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        //Coin coin2 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        //Coin coin3 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        Coin coin = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(100, 400), new Vector2(100, 100));
        Coin coin2 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(400, 400), new Vector2(100, 100));
        Coin coin3 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(700, 400), new Vector2(100, 100));
        List<Coin> coins = new();

        Player player = new Player(Globals.content.Load<Texture2D>("Actors/Hero"), new Vector2(100,100), new Vector2(200,200));

        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
            camera = new(Vector2.Zero);
        }

        public override void LoadContent() 
        {
            font = Globals.content.Load<SpriteFont>("Fonts/Font");

            myBackground = new ScrollingBackground();
            Texture2D background = Globals.content.Load<Texture2D>("Backgrounds/starfield3");
            myBackground.Load(background);

            coins.Add(coin);
            coins.Add(coin2);
            coins.Add(coin3);

            
            coin.LoadContent();
            coin2.LoadContent();
            coin3.LoadContent();

            player.LoadContent();

            sfxCoin = Globals.content.Load<SoundEffect>("Audio/coinpickup");
            songBackground = Globals.content.Load<Song>("Audio/Backgroundmusic");

            MediaPlayer.Play(songBackground);


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
                    sfxCoin.Play();
                }
            }
            foreach (var coin in killList)
            {
                coins.Remove(coin);
                pointsCounter++;
            }

            player.Update(coins);

            //camera.Follow(player._posPlayer, new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

            // METHODE OM TE WINNEN MET PUNTEN
            //if (pointsCounter == 3)
            //{

            //    GameStateManager.ChangeState(new GameOverScreen());

            //}

            myBackground.Update(1 * scrollingSpeed);
        }


        public override void Draw() 
        {
            Globals.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Tekenen
            myBackground.Draw(Globals.spriteBatch, Color.White);

            foreach (var coin in coins)
            {
                coin.Draw();
            }

            //player.Draw(camera.Position);

            player.Draw();
            Globals.spriteBatch.DrawString(font, $"Game Mode: {difficulty}", new Vector2(10, 10), Color.White);
            Globals.spriteBatch.DrawString(font, $"Lives: {playerLives}", new Vector2(10, 40), Color.White);
            Globals.spriteBatch.DrawString(font, $"Points: {pointsCounter}", new Vector2(10, 70), Color.White);

            Globals.spriteBatch.End();


        }


    }
}
