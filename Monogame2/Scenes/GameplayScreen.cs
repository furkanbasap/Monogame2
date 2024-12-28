using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Monogame2.GameObjects;
using Monogame2.Managers;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using Monogame2.GameObjects.Enemies;
using Monogame2.GameObjects.Objects;

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
        private int killCounter = 0;
        private static Random rnd = new Random();
        SoundEffect sfxCoin;
        Song songBackground;
        SoundEffect sfxBomb;
        SoundEffect sfxExplosionShip;
        SoundEffect sfxGettingHit;

        private KeyboardState currentKeyboardState, previousKeyboardState;
        private bool paused;

        //Position and Size of the texture
        List<Coin> coins = new();
        //COIN

        //PLAYER
        List<Projectile> playerProjectiles;
        Player player = new Player(Globals.Content.Load<Texture2D>("Actors/Hero3"), new Vector2(100, 100), new Vector2(200, 200), Globals.Content.Load<Texture2D>("Objects/rocket6"));
        //PLAYER

        //ENEMY
        List<Rock> enemies1 = new();

        List<Bomb> enemies2 = new();

        List<Shooter> enemies3 = new();

        List<Projectile> enemyProjectiles = new();
        //ENEMY

        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
            paused = false;
        }

        public override void LoadContent() 
        {
            font = Globals.Content.Load<SpriteFont>("Fonts/Font");

            myBackground = new ScrollingBackground();
            Texture2D background = Globals.Content.Load<Texture2D>("Backgrounds/starfield3");
            myBackground.Load(background);

            //COIN
            for (int j = 1; j < 100; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    var coin = new Coin(
                    new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j + 1)), rnd.Next(100, Globals.HeightScreen - 100)),
                    new Vector2(50, 50));
                    coins.Add(coin);
                }              
            }

            coins.ForEach(coin => coin.LoadContent());
            //COIN

            //PLAYER
            player.LoadContent();
            playerProjectiles = new List<Projectile>();
            player._projectiles = playerProjectiles;

            //PLAYER

            //ENEMY
                //ROCK
            for (int j = 1; j < 100; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    var rock = new Rock(
                    new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j+1)), rnd.Next(100, Globals.HeightScreen - 100)),
                    new Vector2(100, 100)
                );
                    enemies1.Add(rock);
                }              
            }

            enemies1.ForEach(enemy => enemy.LoadContent());
                //ROCK

                //BOMB
            for (int j = 1; j < 100; j++)
            {
                for (int i = 0; i < 1; i++)
                {
                    var bomb = new Bomb(
                                        new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j + 1)), rnd.Next(100, Globals.HeightScreen - 100)),
                                        new Vector2(100, 100)
                                    );
                    enemies2.Add(bomb);
                }                
            }

            enemies2.ForEach(enemy => enemy.LoadContent());
                //BOMB

                //SHOOTER
            var textureShooter = Globals.Content.Load<Texture2D>("Objects/rocket2");
            for (int j = 1; j < 100; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    var shooter = new Shooter(
                    new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j + 1)), rnd.Next(100, Globals.HeightScreen - 100)),
                    new Vector2(100, 100),
                    textureShooter);
                    enemies3.Add(shooter);
                }
            }

            enemies3.ForEach(enemy => enemy.LoadContent());
                //SHOOTER
            //ENEMY

            songBackground = Globals.Content.Load<Song>("Audio/Backgroundmusic");
            sfxCoin = Globals.Content.Load<SoundEffect>("Audio/coinpickup");
            sfxBomb = Globals.Content.Load<SoundEffect>("Audio/bomb");
            sfxExplosionShip = Globals.Content.Load<SoundEffect>("Audio/explosionShip");
            sfxGettingHit = Globals.Content.Load<SoundEffect>("Audio/gettinghit");

            MediaPlayer.Play(songBackground);


        }

        public override void Update(GameTime gameTime)
        {
            List<Coin> killListCoin = new();
            List<Bomb> killListEnemy2 = new();
            List<Shooter> killListEnemy3 = new();
            List<Projectile> killListPlayerProjectiles = new();
            List<Projectile> killListEnemyProjectiles = new();

            if (!paused)
            {
                foreach (var coin in coins)
                {
                    coin.Update();

                    if (coin.Rect.Intersects(player.Rect))
                    {
                        killListCoin.Add(coin);
                        sfxCoin.Play();
                    }
                }
                foreach (var coin in killListCoin)
                {
                    coins.Remove(coin);
                    pointsCounter++;
                }

                InputManager.Update();
                player.Update(enemies1, enemies2, enemies3);

                foreach (var enemy in enemies1)
                {
                    enemy.Update();
                }

                // COLLSION MET ENEMY 2
                foreach (var enemy in enemies2)
                {
                    enemy.Update(player._posPlayer);

                    if (enemy.Rect.Intersects(player.Rect))
                    {
                        killListEnemy2.Add(enemy);
                        sfxBomb.Play();
                        playerLives--;
                    }

                    foreach (var item in player._projectiles)
                    {
                        if (enemy.Rect.Intersects(item.Rect))
                        {
                            killListEnemy2.Add(enemy);
                            killListPlayerProjectiles.Add(item);
                            sfxBomb.Play();
                        }
                    }
                    
                }
                foreach (var enemy in killListEnemy2)
                {
                    enemies2.Remove(enemy);
                }
                

                foreach (var enemy in enemies3)
                {
                    enemy.Update();

                    foreach (var projectPlayer in player._projectiles)
                    {
                        if (enemy.Rect.Intersects(projectPlayer.Rect))
                        {
                            killListEnemy3.Add(enemy);
                            killListPlayerProjectiles.Add(projectPlayer);
                            killCounter++;
                        }
                        foreach (var projectEnemy in enemy._projectiles)
                        {
                            if (projectPlayer.Rect.Intersects(projectEnemy.Rect))
                            {
                                killListPlayerProjectiles.Add(projectPlayer);
                                killListEnemyProjectiles.Add(projectEnemy);
                                sfxBomb.Play();
                            }
                            
                        }

                        
                    }
                    foreach (var projectEnemy in enemy._projectiles)
                    {
                        if (player.Rect.Intersects(projectEnemy.Rect))
                        {
                            killListEnemyProjectiles.Add(projectEnemy);
                            sfxGettingHit.Play();
                            playerLives--;
                        }                        
                    }

                    foreach (var item in killListEnemyProjectiles)
                    {
                        enemy._projectiles.Remove(item);
                    }
                }
                foreach (var enemy in killListEnemy3)
                {
                    enemies3.Remove(enemy);
                    sfxExplosionShip.Play();
                }
                foreach (var item in killListPlayerProjectiles)
                {
                    player._projectiles.Remove(item);
                }

                myBackground.Update(1 * scrollingSpeed);
            }




            // METHODE OM TE WINNEN MET PUNTEN
            if (pointsCounter == 4 || killCounter == 10)
            {
                GameStateManager.ChangeState(new GameOverScreen(true));
            }

            // METHODE OM TE VERLIEZEN
            if (playerLives <= 0)
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
                Globals.SpriteBatch.DrawString(font, $"Kills: {killCounter}", new Vector2(10, 100), Color.White);
                Globals.SpriteBatch.DrawString(font, "(Press P to pause game)", new Vector2(Globals.WidthScreen - 210, 40), Color.White);
            }

            Globals.SpriteBatch.End();


        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }


    }
}
