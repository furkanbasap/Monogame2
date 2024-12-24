using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Monogame2.Animation;
using Monogame2.Global;
using System.Collections.Generic;
using System;

namespace Monogame2.GameObjects
{
    public class Player : Sprite
    {

        private Vector2 _posPlayer;
        private Vector2 _sizePlayer;


        Texture2D spritesheetPlayer;
        AnimationManager amPlayer;

        static int widthScreen;
        static int heightScreen;


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


        public static void ScreenSize(int width, int height)
        {
            widthScreen = width;
            heightScreen = height;
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posPlayer.X, (int)_posPlayer.Y, (int)_sizePlayer.X, (int)_sizePlayer.Y);
            }
        }
        public Vector2 PosPlayer()
        {
            return _posPlayer;
        }


        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _posPlayer = position;
        }
        public Player(Texture2D texture, Vector2 position, Vector2 size) : base(texture, position)
        {
            _posPlayer = position;
            _sizePlayer = size;
        }

        public void LoadContent()
        {
            spritesheetPlayer = Globals.content.Load<Texture2D>("Actors/Hero");

            //Number of frames, number of collimates, outline of sprite
            amPlayer = new(8, 8, new Vector2(spritesheetPlayer.Width / 8, 100));

        }

        public void Update(List<Coin> collisionGroup)
        {
            amPlayer.Update();



            var keyboardState = Keyboard.GetState();
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
                    if (_posPlayer.X >= widthScreen - _sizePlayer.X)
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

                // VOOR COLLISIONS DUS NIET COINS MAAR DIT IS EEN VOORBEELD VAN HOE
                //foreach (var coin in collisionGroup)
                //{
                //    if (coin.Rect.Intersects(Rect))
                //    {
                //        _posPlayer.X -= changeX;
                //    }
                //}

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
                    if (_posPlayer.Y >= heightScreen - _sizePlayer.Y)
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

                // VOOR COLLISIONS DUS NIET COINS MAAR DIT IS EEN VOORBEELD VAN HOE
                //foreach (var coin in collisionGroup)
                //{
                //    if (coin.Rect.Intersects(Rect))
                //    {
                //        _posPlayer.Y -= changeY;
                //    }
                //}
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(
                            spritesheetPlayer,
                            new Rectangle((int)_posPlayer.X, (int)_posPlayer.Y, (int)_sizePlayer.X,(int)_sizePlayer.Y),
                            amPlayer.GetFrame(),
                            Color.White);
        }

        
    }
}
