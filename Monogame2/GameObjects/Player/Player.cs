using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Monogame2.Managers;
using System.Collections.Generic;
using System;
using Monogame2.GameObjects.Items;
using Monogame2.Interfaces;
using Monogame2.Strategy.Shot;
using Monogame2.GameObjects.Enemies;

namespace Monogame2.GameObjects
{
    public class Player
    {
        // SINGLETON AND STRATEGY
        private static Player _instance;
        private static readonly object _lock = new object();

        public Vector2 _posPlayer;
        public Vector2 _sizePlayer;

        private Texture2D spritesheetPlayer;
        private AnimationManager amPlayer;

        public static int WidthScreen = Globals.WidthScreen;
        public static int HeightScreen = Globals.HeightScreen;

        private float acceleration = 0.1f; // How fast the movement accelerates
        private float maxSpeed = 3f; // Max speed
        private float currentSpeedX = 0f; // Current horizontal speed 
        private float currentSpeedY = 0f; // Current vertical speed 

        private float changeX = 0;
        private float changeY = 0;

        private bool leftKey;
        private bool rightKey;
        private bool upKey;
        private bool downKey;

        private float _fireCooldown = 0.5f; // Cooldown duration in seconds
        private float _timeSinceLastFire = 0f; // Time elapsed since last fire

        private KeyboardState currentKeyboardState, previousKeyboardState;

        public List<Projectile> _projectiles { get; set; }
        private Texture2D _projectileTexture;

        private IFiringStrategy _firingStrategy;

        public Rectangle Rect => new Rectangle((int)_posPlayer.X, (int)_posPlayer.Y, (int)_sizePlayer.X, (int)_sizePlayer.Y);


        // Private constructor to prevent instantiation from outside
        private Player(Vector2 position, Vector2 size)
        {
            _posPlayer = position;
            _sizePlayer = size;
            _projectiles = new List<Projectile>();
            _projectileTexture = Globals.Content.Load<Texture2D>("Objects/rocket6");
            _firingStrategy = new SingleShotStrategyPlayer();
        }

        // Singleton instance getter
        public static Player GetInstance(Vector2 position, Vector2 size)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Player(position, size);
                    }
                }
            }
            return _instance;
        }

        public Vector2 PosPlayer()
        {
            return _posPlayer;
        }

        public void LoadContent()
        {
            spritesheetPlayer = Globals.Content.Load<Texture2D>("Actors/Hero3");

            // Number of frames, number of collimates, outline of sprite
            amPlayer = new(8, 8, new Vector2(spritesheetPlayer.Width / 8 + 1, 106));
        }

        public void Update(List<Rock> collisionGroupEnemy1, List<Bomb> collisionGroupEnemy2, List<Shooter> collisionGroupEnemy3, GameTime gameTime)
        {
            amPlayer.Update();

            _timeSinceLastFire += (float)gameTime.ElapsedGameTime.TotalSeconds;

            var keyboardState = Keyboard.GetState();

            currentKeyboardState = Keyboard.GetState();

            // Check if Space is pressed and fire a projectile
            if ((IsKeyPressed(Keys.Space)) && (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) && _timeSinceLastFire >= _fireCooldown)
            {
                FireProjectile();
                _timeSinceLastFire = 0f; // Reset the cooldown timer
            }

            previousKeyboardState = currentKeyboardState;

            foreach (var projectile in _projectiles)
            {
                projectile.Update();
            }


            if (keyboardState.GetPressedKeyCount() >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Q) || keyboardState.IsKeyDown(Keys.Left))
                {
                    if (_posPlayer.X <= 0)
                    {
                        currentSpeedX = 0f;
                    }
                    else
                    {
                        // Gradually accelerate to the maximum (negative) speed
                        currentSpeedX = Math.Max(currentSpeedX - acceleration, -maxSpeed);
                        leftKey = true;
                    }
                }
                if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                {
                    if (_posPlayer.X >= WidthScreen - _sizePlayer.X)
                    {
                        currentSpeedX = 0f;
                    }
                    else
                    {
                        // Gradually accelerate to the maximum (positive) speed
                        currentSpeedX = Math.Min(currentSpeedX + acceleration, maxSpeed);
                        rightKey = true;
                    }
                }
                if (keyboardState.IsKeyUp(Keys.Q) && keyboardState.IsKeyUp(Keys.Left))
                    leftKey = false;

                if (currentSpeedX < 0f && leftKey == false)
                {
                    currentSpeedX = Math.Min(currentSpeedX + acceleration, 0);
                }

                if (keyboardState.IsKeyUp(Keys.D) && keyboardState.IsKeyUp(Keys.Right))
                    rightKey = false;

                if (currentSpeedX > 0f && rightKey == false)
                {
                    currentSpeedX = Math.Max(currentSpeedX - acceleration, 0);
                }

                // Update the change in position
                changeX = currentSpeedX;
                _posPlayer.X += changeX;

                // FOR COLLISIONS 
                foreach (var enemy in collisionGroupEnemy1)
                {
                    if (enemy.Rect.Intersects(Rect))
                    {
                        if (_posPlayer.X <= enemy._posEnemy.X)
                        {
                            _posPlayer.X = enemy._posEnemy.X - 200;
                        }
                        else if (_posPlayer.X > enemy._posEnemy.X)
                        {
                            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                            {
                                if (_posPlayer.X >= WidthScreen - _sizePlayer.X)
                                {
                                    currentSpeedX = 0f;
                                }
                                else
                                {
                                    // Gradually accelerate to the maximum (positive) speed
                                    currentSpeedX = Math.Min(currentSpeedX + acceleration, maxSpeed);
                                    rightKey = true;
                                }
                            }
                            if (keyboardState.IsKeyUp(Keys.D) && keyboardState.IsKeyUp(Keys.Right))
                                rightKey = false;

                            if (currentSpeedX > 0f && rightKey == false)
                            {
                                currentSpeedX = Math.Max(currentSpeedX - acceleration, 0);
                            }

                            // Update the change in position
                            changeX = currentSpeedX;
                            _posPlayer.X += changeX;
                        }
                        if (keyboardState.IsKeyDown(Keys.Z) || keyboardState.IsKeyDown(Keys.Up))
                        {
                            if (_posPlayer.Y <= 0)
                            {
                                currentSpeedY = 0f;
                            }
                            else
                            {
                                currentSpeedY = Math.Max(currentSpeedY - acceleration, -maxSpeed);
                                upKey = true;
                            }
                        }
                        if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                        {
                            if (_posPlayer.Y >= HeightScreen - _sizePlayer.Y)
                            {
                                currentSpeedY = 0f;
                            }
                            else
                            {
                                currentSpeedY = Math.Min(currentSpeedY + acceleration, maxSpeed);
                                downKey = true;
                            }
                        }
                        if (keyboardState.IsKeyUp(Keys.Z) && keyboardState.IsKeyUp(Keys.Up))
                            upKey = false;

                        if (currentSpeedY < 0f && upKey == false)
                        {
                            currentSpeedY = Math.Min(currentSpeedY + acceleration, 0);
                        }

                        if (keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.Down))
                            downKey = false;

                        if (currentSpeedY > 0f && downKey == false)
                        {
                            currentSpeedY = Math.Max(currentSpeedY - acceleration, 0);
                        }
                        // Update the change in position
                        changeY = currentSpeedY;
                        _posPlayer.Y += changeY;

                    }
                }

                if (keyboardState.IsKeyDown(Keys.Z) || keyboardState.IsKeyDown(Keys.Up))
                {
                    if (_posPlayer.Y <= 0)
                    {
                        currentSpeedY = 0f;
                    }
                    else
                    {
                        currentSpeedY = Math.Max(currentSpeedY - acceleration, -maxSpeed);
                        upKey = true;
                    }
                }
                if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                {
                    if (_posPlayer.Y >= HeightScreen - _sizePlayer.Y)
                    {
                        currentSpeedY = 0f;
                    }
                    else
                    {
                        currentSpeedY = Math.Min(currentSpeedY + acceleration, maxSpeed);
                        downKey = true;
                    }
                }
                if (keyboardState.IsKeyUp(Keys.Z) && keyboardState.IsKeyUp(Keys.Up))
                    upKey = false;

                if (currentSpeedY < 0f && upKey == false)
                {
                    currentSpeedY = Math.Min(currentSpeedY + acceleration, 0);
                }

                if (keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.Down))
                    downKey = false;

                if (currentSpeedY > 0f && downKey == false)
                {
                    currentSpeedY = Math.Max(currentSpeedY - acceleration, 0);
                }
                // Update the change in position
                changeY = currentSpeedY;
                _posPlayer.Y += changeY;

                // FOR COLLISIONS
                foreach (var enemy in collisionGroupEnemy1)
                {
                    if (enemy.Rect.Intersects(Rect))
                    {
                        _posPlayer.Y -= changeY;
                    }
                }

                foreach (var enemy in collisionGroupEnemy3)
                {
                    if (enemy.Rect.Intersects(Rect))
                    {
                        _posPlayer.X -= changeX;
                    }
                }

                foreach (var enemy in collisionGroupEnemy3)
                {
                    if (enemy.Rect.Intersects(Rect))
                    {
                        _posPlayer.Y  -= changeY;
                    }
                }

            }
        }

        public void Update(EnemyBoss collisionEnemyBosss, GameTime gameTime)
        {
            amPlayer.Update();

            _timeSinceLastFire += (float)gameTime.ElapsedGameTime.TotalSeconds;

            var keyboardState = Keyboard.GetState();

            currentKeyboardState = Keyboard.GetState();

            // Check if Space is pressed and fire a projectile
            if ((IsKeyPressed(Keys.Space)) && (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)) && _timeSinceLastFire >= _fireCooldown)
            {
                _firingStrategy.Fire(_posPlayer, _projectiles, _projectileTexture);
                _timeSinceLastFire = 0f; // Reset the cooldown timer
            }

            previousKeyboardState = currentKeyboardState;

            foreach (var projectile in _projectiles)
            {
                projectile.Update();
            }


            if (keyboardState.GetPressedKeyCount() >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Q) || keyboardState.IsKeyDown(Keys.Left))
                {
                    if (_posPlayer.X <= 0)
                    {
                        currentSpeedX = 0f;
                    }
                    else
                    {
                        // Gradually accelerate to the maximum (negative) speed
                        currentSpeedX = Math.Max(currentSpeedX - acceleration, -maxSpeed);
                        leftKey = true;
                    }
                }
                if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                {
                    if (_posPlayer.X >= WidthScreen - _sizePlayer.X)
                    {
                        currentSpeedX = 0f;
                    }
                    else
                    {
                        // Gradually accelerate to the maximum (positive) speed
                        currentSpeedX = Math.Min(currentSpeedX + acceleration, maxSpeed);
                        rightKey = true;
                    }
                }
                if (keyboardState.IsKeyUp(Keys.Q) && keyboardState.IsKeyUp(Keys.Left))
                    leftKey = false;

                if (currentSpeedX < 0f && leftKey == false)
                {
                    currentSpeedX = Math.Min(currentSpeedX + acceleration, 0);
                }

                if (keyboardState.IsKeyUp(Keys.D) && keyboardState.IsKeyUp(Keys.Right))
                    rightKey = false;

                if (currentSpeedX > 0f && rightKey == false)
                {
                    currentSpeedX = Math.Max(currentSpeedX - acceleration, 0);
                }

                // Update the change in position
                changeX = currentSpeedX;
                _posPlayer.X += changeX;


                if (keyboardState.IsKeyDown(Keys.Z) || keyboardState.IsKeyDown(Keys.Up))
                {
                    if (_posPlayer.Y <= 0)
                    {
                        currentSpeedY = 0f;
                    }
                    else
                    {
                        currentSpeedY = Math.Max(currentSpeedY - acceleration, -maxSpeed);
                        upKey = true;
                    }
                }
                if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                {
                    if (_posPlayer.Y >= HeightScreen - _sizePlayer.Y)
                    {
                        currentSpeedY = 0f;
                    }
                    else
                    {
                        currentSpeedY = Math.Min(currentSpeedY + acceleration, maxSpeed);
                        downKey = true;
                    }
                }
                if (keyboardState.IsKeyUp(Keys.Z) && keyboardState.IsKeyUp(Keys.Up))
                    upKey = false;

                if (currentSpeedY < 0f && upKey == false)
                {
                    currentSpeedY = Math.Min(currentSpeedY + acceleration, 0);
                }

                if (keyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.Down))
                    downKey = false;

                if (currentSpeedY > 0f && downKey == false)
                {
                    currentSpeedY = Math.Max(currentSpeedY - acceleration, 0);
                }
                // Update the change in position
                changeY = currentSpeedY;
                _posPlayer.Y += changeY;

                // FOR COLLISIONS

                if (collisionEnemyBosss.Rect.Intersects(Rect))
                {
                    _posPlayer.X -= changeX;
                }

                if (collisionEnemyBosss.Rect.Intersects(Rect))
                {
                    _posPlayer.Y -= changeY;
                }

            }
        }

            public void Draw()
        {
            Globals.SpriteBatch.Draw(
                            spritesheetPlayer,
                            new Rectangle((int)_posPlayer.X, (int)_posPlayer.Y, (int)_sizePlayer.X, (int)_sizePlayer.Y),
                            amPlayer.GetFrame(),
                            Color.White);
            foreach (var projectile in _projectiles)
            {
                projectile.Draw();
            }
        }

        private void FireProjectile()
        {
            Projectile newProjectile = new Projectile(_projectileTexture, 4f);
            newProjectile.Fire(new Vector2(_posPlayer.X + spritesheetPlayer.Width / 8, _posPlayer.Y + 50));
            _projectiles.Add(newProjectile);
        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public void SetFiringStrategy(IFiringStrategy firingStrategy)
        {
            _firingStrategy = firingStrategy;
        }

    }
}
