using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Monogame2.Animation;
using Monogame2.Managers;
using System.Collections.Generic;
using System;

namespace Monogame2.GameObjects
{
    public class Player
    {

        public Vector2 _posPlayer;
        public Vector2 _sizePlayer;


        Texture2D spritesheetPlayer;
        AnimationManager amPlayer;

        public static int WidthScreen = Globals.WidthScreen;
        public static int HeightScreen = Globals.HeightScreen;


        float acceleration = 0.1f; // How fast the movement accelerates
        float maxSpeed = 3f; // Max speed
        float currentSpeedX = 0f; // Current horizontal speed 
        float currentSpeedY = 0f; // Current vertical speed 

        float changeX = 0; 
        float changeY = 0;

        bool leftKey;
        bool rightKey;
        bool upKey;
        bool downKey;

        private KeyboardState currentKeyboardState, previousKeyboardState;
        private bool spacePressed = true;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posPlayer.X, (int)_posPlayer.Y, (int)_sizePlayer.X, (int)_sizePlayer.Y);
            }
        }


        public List<Projectile> _projectiles { get; set; }
        private Texture2D _projectileTexture;
        public Player(Texture2D texture, Vector2 position, Vector2 size, Texture2D projectileTexture)
        {
            _posPlayer = position;
            _sizePlayer = size;
            _projectileTexture = projectileTexture;
        }

        public Vector2 PosPlayer()
        {
            return _posPlayer;
        }

        public void LoadContent()
        {
            spritesheetPlayer = Globals.Content.Load<Texture2D>("Actors/Hero3");

            //Number of frames, number of collimates, outline of sprite
            amPlayer = new(8, 8, new Vector2(spritesheetPlayer.Width / 8 + 1, 106));

        }

        public void Update(List<Enemy1> collisionGroupEnemy1, List<Enemy2> collisionGroupEnemy2, List<Enemy3> collisionGroupEnemy3)
        {
            amPlayer.Update();

            var keyboardState = Keyboard.GetState();

            currentKeyboardState = Keyboard.GetState();

            // Check if Space is pressed and fire a projectile
            if ((IsKeyPressed(Keys.Space)) && (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space)))
            {
                FireProjectile();
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

                //if (prevDir != Direction)
                //{
                //    SrcRect.X += SrcRect.Width;
                //    SrcRect.Width
                //}

                // VOOR COLLISIONS 
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

                // VOOR COLLISIONS
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
            // Fire a new projectile if none are active
            Projectile newProjectile = new Projectile(_projectileTexture, 3f);
            newProjectile.Fire(new Vector2(_posPlayer.X + spritesheetPlayer.Width / 8, _posPlayer.Y + 50));
            _projectiles.Add(newProjectile);
        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }


    }
}
