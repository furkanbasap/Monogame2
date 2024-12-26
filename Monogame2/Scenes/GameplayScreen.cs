using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Monogame2.GameObjects;
using Monogame2.Managers;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Monogame2.Utils;
using System;
using Microsoft.Xna.Framework.Audio;

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
        SoundEffect sfxBomb;

        private KeyboardState currentKeyboardState, previousKeyboardState;
        private bool paused;

        //Position and Size of the texture
        //Coin coin = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        //Coin coin2 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        //Coin coin3 = new Coin(Globals.content.Load<Texture2D>("Objects/coin3"), new Vector2(rnd.Next(400, 1500), rnd.Next(100, 600)), new Vector2(100, 100));
        Coin coin = new Coin(Globals.Content.Load<Texture2D>("Objects/coin6"), new Vector2(100, 400), new Vector2(100, 100));
        Coin coin2 = new Coin(Globals.Content.Load<Texture2D>("Objects/coin6"), new Vector2(400, 400), new Vector2(100, 100));
        Coin coin3 = new Coin(Globals.Content.Load<Texture2D>("Objects/coin6"), new Vector2(700, 400), new Vector2(100, 100));
        List<Coin> coins = new();
        //COIN

        //PLAYER
        Player player = new Player(Globals.Content.Load<Texture2D>("Actors/Hero3"), new Vector2(100,100), new Vector2(200,200));
        //PLAYER

        //ENEMY
        List<Enemy1> enemies1 = new();
        Enemy1 enemy1;

        List<Enemy2> enemies2 = new();
        Enemy2 enemy2;

        List<Enemy3> enemies3 = new();
        Enemy3 enemy3;
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
            enemy1 = new Enemy1(new Vector2(rnd.Next(1500,2000), rnd.Next(200, 700)), new Vector2(100, 100));
            enemies1.Add(enemy1);
            enemy1.LoadContent();

            enemy2 = new Enemy2(new Vector2(rnd.Next(1500, 2000), rnd.Next(200, 700)), new Vector2(100, 100));
            enemies2.Add(enemy2);
            enemy2.LoadContent();

            enemy3 = new Enemy3(new Vector2(rnd.Next(1600, 2000), rnd.Next(200, 700)), new Vector2(100, 100));
            enemies3.Add(enemy3);
            enemy3.LoadContent();

            //ENEMY

            songBackground = Globals.Content.Load<Song>("Audio/Backgroundmusic");
            sfxCoin = Globals.Content.Load<SoundEffect>("Audio/coinpickup");
            sfxBomb = Globals.Content.Load<SoundEffect>("Audio/bomb");

            MediaPlayer.Play(songBackground);


        }

        public override void Update(GameTime gameTime)
        {
            List<Coin> killList = new();
            List<Enemy2> killListEnemy2 = new();

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
                player.Update(enemies1, enemies2, enemies3);
                ProjectileManager.Update();

                myBackground.Update(1 * scrollingSpeed);

                foreach (var enemy in enemies1)
                {
                    enemy.Update();
                }

                foreach (var enemy in enemies2)
                {
                    enemy.Update(player._posPlayer);

                    if (enemy.Rect.Intersects(player.Rect))
                    {
                        killListEnemy2.Add(enemy);
                        sfxBomb.Play();
                    }
                }
                foreach (var enemy in killListEnemy2)
                {
                    enemies2.Remove(enemy);
                    playerLives--;
                }

                foreach (var enemy in enemies3)
                {
                    enemy.Update();
                }
            }




            // METHODE OM TE WINNEN MET PUNTEN
            if (pointsCounter == 4)
            {

                GameStateManager.ChangeState(new GameOverScreen(true));

            }

            // METHODE OM TE VERLIEZEN
            if (playerLives == 0)
            {
                GameStateManager.ChangeState(new GameOverScreen(false));
            }
            if (player._posPlayer.X < -player._sizePlayer.X) 
            {
                GameStateManager.ChangeState(new GameOverScreen(false));
            }



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

            foreach (var enemy in enemies1)
            {
                enemy.Draw();
            }

            foreach (var enemy in enemies2)
            {
                enemy.Draw();
            }
            foreach (var enemy in enemies3)
            {
                enemy.Draw();
            }


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
