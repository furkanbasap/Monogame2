using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Monogame2.GameObjects;
using Monogame2.Managers;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using Monogame2.GameObjects.Objects;
using Monogame2.Enums;
using Monogame2.GameObjects.Items;
using Monogame2.GameObjects.Enemies;
using Monogame2.Abstract;

namespace Monogame2.Scenes
{
    public class GameplayScreen : GameScreen
    {
        private GameDifficulty difficulty;

        private ScrollingBackground myBackground;
        private float scrollingSpeed = 1;

        private SpriteFont font;
        private int playerLives;
        private int pointsCounter = 0;
        private int killCounter = 0;
        private static Random rnd = new Random();
        SoundEffect sfxCoin;
        Song songBackground;
        SoundEffect sfxBomb;
        SoundEffect sfxExplosionShip;
        SoundEffect sfxLosingLives;
        SoundEffect sfxHeartBeat;

        private bool isInvincible;
        private float invincibilityTimer;
        private const float InvincibilityDuration = 2.0f; // 2 seconds
        private float blinkTimer;
        private const float BlinkInterval = 0.2f; // Blink every 0.2 seconds
        private bool isVisible; // To toggle visibility during blinking

         

        private KeyboardState currentKeyboardState, previousKeyboardState;
        private bool paused;

        //Position and Size of the texture
        List<Coin> coins = new();
        //COIN

        //HEART
        List<Heart> hearts = new();
        //HEART

        //PLAYER
        List<Projectile> playerProjectiles;
        Player player = Player.GetInstance(
            new Vector2(100, 100),
            new Vector2(200, 200)
        );
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

            if (this.difficulty == GameDifficulty.NORMAL)
            {
                playerLives = 10;
            }
            else if (this.difficulty == GameDifficulty.HARD)
            {
                playerLives = 5;
            }
            isInvincible = false;
            isVisible = true;

        }

        public override void LoadContent()
        {
            font = Globals.Content.Load<SpriteFont>("Fonts/Font");

            myBackground = new ScrollingBackground();
            Texture2D background = Globals.Content.Load<Texture2D>("Backgrounds/starfield3");
            myBackground.Load(background);

            for (int j = 1; j < 101; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    var coin = new Coin(
                    new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j + 1)), rnd.Next(100, Globals.HeightScreen - 100)),
                    new Vector2(50, 50));
                    coins.Add(coin);

                    var rock = new Rock(
                    new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j + 1)), rnd.Next(100, Globals.HeightScreen - 100)),
                    new Vector2(100, 100));
                    enemies1.Add(rock);

                    var shooter = new Shooter(
                    new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j + 1)), rnd.Next(100, Globals.HeightScreen - 100)),
                    new Vector2(100, 100));
                    enemies3.Add(shooter);
                }

                for (int i = 0; i < 2; i++)
                {
                    var bomb = new Bomb(
                                        new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j + 1)), rnd.Next(100, Globals.HeightScreen - 100)),
                                        new Vector2(100, 100)
                                    );
                    enemies2.Add(bomb);
                }

                for (int i = 0; i < 1; i++)
                {
                    var heart = new Heart(
                    new Vector2(rnd.Next(Globals.WidthScreen * j, Globals.WidthScreen * (j + 1)), rnd.Next(100, Globals.HeightScreen - 100)),
                    new Vector2(50, 50));
                    hearts.Add(heart);
                }
            }

            coins.ForEach(coin => coin.LoadContent());
            hearts.ForEach(heart => heart.LoadContent());
            enemies1.ForEach(enemy => enemy.LoadContent());
            enemies2.ForEach(enemy => enemy.LoadContent());
            enemies3.ForEach(enemy => enemy.LoadContent());

            //PLAYER
            player.LoadContent();
            playerProjectiles = new List<Projectile>();
            player._projectiles = playerProjectiles;

            songBackground = Globals.Content.Load<Song>("Audio/Backgroundmusic");
            sfxCoin = Globals.Content.Load<SoundEffect>("Audio/coinpickup");
            sfxBomb = Globals.Content.Load<SoundEffect>("Audio/bomb");
            sfxExplosionShip = Globals.Content.Load<SoundEffect>("Audio/explosionShip");
            sfxLosingLives = Globals.Content.Load<SoundEffect>("Audio/gettinghit");
            sfxHeartBeat = Globals.Content.Load<SoundEffect>("Audio/heartBeat");

            MediaPlayer.Play(songBackground);


        }

        public override void Update(GameTime gameTime)
        {
            List<Coin> killListCoin = new();
            List<Bomb> killListEnemy2 = new();
            List<Shooter> killListEnemy3 = new();
            List<Projectile> killListPlayerProjectiles = new();
            List<Projectile> killListEnemyProjectiles = new();
            List<Heart> killListHeart = new();

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

                foreach (var heart in hearts)
                {
                    heart.Update();

                    if (heart.Rect.Intersects(player.Rect))
                    {
                        killListHeart.Add(heart);
                        sfxHeartBeat.Play();
                    }
                }
                foreach (var heart in killListHeart)
                {
                    hearts.Remove(heart);
                    playerLives++;
                }

                InputManager.Update();
                player.Update(enemies1, enemies2, enemies3, gameTime);

                foreach (var enemy in enemies1)
                {
                    enemy.Update();
                }

                if (isInvincible)
                {
                    invincibilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    blinkTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                    // Toggle visibility for blinking
                    if (blinkTimer <= 0)
                    {
                        isVisible = !isVisible;
                        blinkTimer = BlinkInterval;
                    }

                    if (invincibilityTimer <= 0)
                    {
                        isInvincible = false;
                        isVisible = true; // Ensure the player is visible when invincibility ends
                    }
                }

                // COLLSION MET ENEMY 2
                foreach (var enemy in enemies2)
                {
                    enemy.Update(player._posPlayer);



                    // Alleen levens aftrekken als de speler niet onschendbaar is
                    if (!isInvincible && enemy.Rect.Intersects(player.Rect))
                    {
                        playerLives--;
                        isInvincible = true;
                        invincibilityTimer = InvincibilityDuration;
                        blinkTimer = BlinkInterval;
                        isVisible = false;
                        killListEnemy2.Add(enemy);
                        sfxBomb.Play();
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

                            // Alleen levens aftrekken als de speler niet onschendbaar is
                            if (!isInvincible && projectEnemy.Rect.Intersects(player.Rect))
                            {
                                playerLives--;
                                isInvincible = true;
                                invincibilityTimer = InvincibilityDuration;
                                blinkTimer = BlinkInterval;
                                isVisible = false;
                                killListEnemyProjectiles.Add(projectEnemy);
                                sfxLosingLives.Play();
                            }
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
                foreach (var proj in player._projectiles)
                {
                    if (proj.Position.X >= Globals.WidthScreen - 200)
                    {
                        killListPlayerProjectiles.Add(proj);
                    }
                }

                myBackground.Update(1 * scrollingSpeed);


            }



            if (difficulty == GameDifficulty.NORMAL)
            {
                // METHODE OM TE WINNEN MET PUNTEN OF NAAR DE BOSSFIGHT TE GAAN MET DE KILLS
                if (pointsCounter >= 50)
                {
                    GameStateManager.ChangeState(new GameOverScreen(true));
                }
                else if (killCounter >= 50)
                {
                    GameStateManager.ChangeState(new BossFightScreen(playerLives, difficulty, player));
                }
            }
            else if (difficulty == GameDifficulty.HARD)
            {
                // METHODE OM TE WINNEN MET PUNTEN OF NAAR DE BOSSFIGHT TE GAAN MET DE KILLS
                if (pointsCounter >= 100)
                {
                    GameStateManager.ChangeState(new GameOverScreen(true));
                }
                else if (killCounter >= 100)
                {
                    GameStateManager.ChangeState(new BossFightScreen(playerLives, difficulty, player));
                }
            }


            // METHODE OM TE VERLIEZEN
            if (playerLives <= 0)
            {
                GameStateManager.ChangeState(new GameOverScreen(false));
            }
            if (player._posPlayer.X < -player._sizePlayer.X)
            {
                GameStateManager.ChangeState(new GameOverScreen(false));
                player._posPlayer.X = 100;
            }



            currentKeyboardState = Keyboard.GetState();

            if ((IsKeyPressed(Keys.P) && paused == true))
            {
                paused = false;
            }
            else if ((IsKeyPressed(Keys.P) && paused == false))
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

            foreach (var heart in hearts)
            {
                heart.Draw();
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

            if (isVisible)
            {
                player.Draw();
            }
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
