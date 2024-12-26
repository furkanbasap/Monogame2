using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Monogame2.GameObjects;
using Monogame2.Managers;
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

        private KeyboardState currentKeyboardState, previousKeyboardState;
        private bool paused;

        //Position and Size of the texture
        //Coin coin = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        //Coin coin2 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        //Coin coin3 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        Coin coin = new Coin(Globals.Content.Load<Texture2D>("Objects/coin3"), new Vector2(100, 400), new Vector2(100, 100));
        Coin coin2 = new Coin(Globals.Content.Load<Texture2D>("Objects/coin3"), new Vector2(400, 400), new Vector2(100, 100));
        Coin coin3 = new Coin(Globals.Content.Load<Texture2D>("Objects/coin3"), new Vector2(700, 400), new Vector2(100, 100));
        List<Coin> coins = new();
        //COIN

        //PLAYER
        Player player = new Player(Globals.Content.Load<Texture2D>("Actors/Hero3"), new Vector2(100,100), new Vector2(200,200));
        //PLAYER

        //ENEMY
        List<Enemy1> enemies = new();
        Enemy1 enemy;
        //ENEMY

        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
            ProjectileManager.Init();
        }

        public override void LoadContent() 
        {
            font = Globals.Content.Load<SpriteFont>("Fonts/Font");

            myBackground = new ScrollingBackground();
            Texture2D background = Globals.Content.Load<Texture2D>("Backgrounds/starfield3");
            myBackground.Load(background);

            //COIN
            coins.Add(coin);
            coins.Add(coin2);
            coins.Add(coin3);

            
            coin.LoadContent();
            coin2.LoadContent();
            coin3.LoadContent();
            //COIN

            //PLAYER
            player.LoadContent();
            //PLAYER

            //ENEMY
            enemy = new Enemy1(new Vector2(rnd.Next(1500,2000), rnd.Next(200, 700)), new Vector2(100, 100));
            enemies.Add(enemy);
            enemy.LoadContent();
            //ENEMY

            sfxCoin = Globals.Content.Load<SoundEffect>("Audio/coinpickup");
            songBackground = Globals.Content.Load<Song>("Audio/Backgroundmusic");

            MediaPlayer.Play(songBackground);


        }

        public override void Update(GameTime gameTime)
        {
            List<Coin> killList = new();

            if (!paused)
            {
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

                InputManager.Update();
                player.Update(enemies);
                ProjectileManager.Update();

                myBackground.Update(1 * scrollingSpeed);

                enemy.Update();
            }




            // METHODE OM TE WINNEN MET PUNTEN
            //if (pointsCounter == 3)
            //{

            //    GameStateManager.ChangeState(new GameOverScreen());

            //}



            currentKeyboardState = Keyboard.GetState();

            if ((IsKeyPressed(Keys.P) && paused == true) && (currentKeyboardState.IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P)))
            {
                paused = false;
            }
            else if ((IsKeyPressed(Keys.P) && paused == false) && (currentKeyboardState.IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P)))
            {
                paused = true;
            }
            previousKeyboardState = currentKeyboardState;
        }


        public override void Draw() 
        {
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Tekenen
            myBackground.Draw(Globals.SpriteBatch, Color.White);

            foreach (var coin in coins)
            {
                coin.Draw();
            }

            enemy.Draw();

            ProjectileManager.Draw();
            player.Draw();
            Globals.SpriteBatch.DrawString(font, "(Press M to mute song)", new Vector2(Globals.WidthScreen - 200, 10), Color.White);



            //PAUSE METHODE
            if (paused)
            {
                Globals.SpriteBatch.DrawString(font, "Game Paused", new Vector2(Globals.WidthScreen / 2 - 50, Globals.HeightScreen / 2), Color.Red);
            }
            else
            {
                Globals.SpriteBatch.DrawString(font, $"Game Mode: {difficulty}", new Vector2(10, 10), Color.White);
                Globals.SpriteBatch.DrawString(font, $"Lives: {playerLives}", new Vector2(10, 40), Color.White);
                Globals.SpriteBatch.DrawString(font, $"Points: {pointsCounter}", new Vector2(10, 70), Color.White);
            }

            Globals.SpriteBatch.End();


        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }


    }
}
