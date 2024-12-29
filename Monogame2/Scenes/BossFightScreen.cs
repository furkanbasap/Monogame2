using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Monogame2.Managers;
using System.Collections.Generic;
using Monogame2.Enums;
using Monogame2.GameObjects;
using Microsoft.Xna.Framework.Audio;
using Monogame2.GameObjects.Items;
using Monogame2.GameObjects.Enemies;

namespace Monogame2.Scenes
{
    internal class BossFightScreen : GameScreen
    {
        private SpriteFont font;
        private ScrollingBackground myBackground;

        private EnemyBoss enemyBoss;

        private int playerLives;
        private int bossLives;
        private GameDifficulty difficulty;

        private bool paused;
        private KeyboardState currentKeyboardState, previousKeyboardState;

        List<Projectile> playerProjectiles;
        Player player;

        SoundEffect sfxBomb;
        SoundEffect sfxLosingLives;


        private bool isInvincible;
        private float invincibilityTimer;
        private const float InvincibilityDuration = 2.0f; // 2 seconds
        private float blinkTimer;
        private const float BlinkInterval = 0.2f; // Blink every 0.2 seconds
        private bool isVisible; // To toggle visibility during blinking


        public BossFightScreen(int playerLives, GameDifficulty difficulty, Player player)
        {
            this.playerLives = playerLives;
            paused = false;
            this.difficulty = difficulty;
            if (this.difficulty == GameDifficulty.NORMAL)
            {
                bossLives = 50;
            }
            else if (this.difficulty == GameDifficulty.HARD)
            {
                bossLives = 100;
            }
            enemyBoss = new EnemyBoss(new Vector2(Globals.WidthScreen - 500, 50), new Vector2(400, Globals.HeightScreen - 100));
            isInvincible = false;
            isVisible = true;
            this.player = player;
        }

        public override void LoadContent()
        {
            font = Globals.Content.Load<SpriteFont>("Fonts/Font");

            myBackground = new ScrollingBackground();
            Texture2D background = Globals.Content.Load<Texture2D>("Backgrounds/starfield3");
            myBackground.Load(background);

            enemyBoss.LoadContent();

            player.LoadContent();
            playerProjectiles = new List<Projectile>();
            player._projectiles = playerProjectiles;

            sfxBomb = Globals.Content.Load<SoundEffect>("Audio/bomb");
            sfxLosingLives = Globals.Content.Load<SoundEffect>("Audio/gettinghit");
        }

        public override void Update(GameTime gameTime)
        {
            List<Projectile> killListPlayerProjectiles = new();
            List<Projectile> killListEnemyProjectiles = new();



            enemyBoss.Update(player._posPlayer);
            player.Update(enemyBoss, gameTime);

            foreach (var projectPlayer in player._projectiles)
            {
                if (enemyBoss.Rect.Intersects(projectPlayer.Rect))
                {
                    killListPlayerProjectiles.Add(projectPlayer);
                    bossLives--;
                }
                foreach (var projectEnemy in enemyBoss._projectiles)
                {
                    if (projectPlayer.Rect.Intersects(projectEnemy.Rect))
                    {
                        killListPlayerProjectiles.Add(projectPlayer);
                        killListEnemyProjectiles.Add(projectEnemy);
                        sfxBomb.Play();
                    }

                }
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

            foreach (var projectEnemy in enemyBoss._projectiles)
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
                enemyBoss._projectiles.Remove(item);
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


            if (bossLives <= 0)
            {
                GameStateManager.ChangeState(new GameOverScreen(true));
            }
            if (playerLives <= 0)
            {
                GameStateManager.ChangeState(new GameOverScreen(false));
            }

            if (!paused)
            {
                myBackground.Update(1 * 1);
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
            Globals.SpriteBatch.Begin();
            myBackground.Draw(Globals.SpriteBatch, Color.White);
            enemyBoss.Draw();
            if (isVisible)
            {
                player.Draw();
            }
            //PAUSE METHOD
            if (paused)
            {
                Globals.SpriteBatch.DrawString(font, "Game Paused", new Vector2(Globals.WidthScreen / 2 - 50, Globals.HeightScreen / 2), Color.Red);
            }
            else
            {
                Globals.SpriteBatch.DrawString(font, $"Game Mode: {difficulty}", new Vector2(10, 10), Color.White);
                Globals.SpriteBatch.DrawString(font, $"Lives: {playerLives}", new Vector2(10, 40), Color.White);
                Globals.SpriteBatch.DrawString(font, $"Boss Lives: {bossLives}", new Vector2(Globals.WidthScreen / 2 - 50, 10), Color.Red);
                Globals.SpriteBatch.DrawString(font, "(Press M to mute song)", new Vector2(Globals.WidthScreen - 210, 10), Color.White);
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
