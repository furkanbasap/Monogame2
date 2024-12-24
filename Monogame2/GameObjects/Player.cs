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

        private Texture2D _playerTexture;
        private Vector2 _posPlayer;
        private Vector2 _sizePlayer;


        Texture2D spritesheetPlayer;
        AnimationManager amPlayer;

        static int widthScreen;
        static int heightScreen;


        float acceleration = 0.1f; // Hoe snel de beweging accelereert
        float maxSpeed = 3f; // De maximale snelheid
        float currentSpeed = 0f; // De huidige snelheid
        float changeX = 0;

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

            //Aantal frames, aantal kollomen, omtrek van sprite
            amPlayer = new(8, 8, new Vector2(spritesheetPlayer.Width / 8, 100));

        }

        public void Update(List<Coin> collisionGroup)
        {
            amPlayer.Update();


            var keyboardState = Keyboard.GetState();
            if (keyboardState.GetPressedKeyCount() > 0)
            {


                if (keyboardState.IsKeyDown(Keys.Q) || keyboardState.IsKeyDown(Keys.Left))
                {
                    if (_posPlayer.X <= 0)
                    {
                        currentSpeed = 0f;
                    }
                    else if (_posPlayer.X > 0)
                    {
                        // Versnel geleidelijk tot de maximale (negatieve) snelheid
                        currentSpeed = Math.Max(currentSpeed - acceleration, -maxSpeed);
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                {
                    if (_posPlayer.X >= widthScreen - _sizePlayer.X)
                    {
                        currentSpeed = 0f;
                    }
                    else if (_posPlayer.X < (widthScreen - _sizePlayer.X))
                    {
                        // Versnel geleidelijk tot de maximale (positieve) snelheid
                        currentSpeed = Math.Min(currentSpeed + acceleration, maxSpeed);
                    }
                }
                else
                {
                    if (currentSpeed < 0f)
                    {
                        currentSpeed = Math.Min(currentSpeed + acceleration, 0);
                    }
                    else if (currentSpeed >= 0f)
                    {
                        currentSpeed = Math.Max(currentSpeed - acceleration, 0);
                    }
                }

                // Update de verandering in positie
                changeX = currentSpeed;
                _posPlayer.X += changeX;


                // VOOR COLLISIONS DUS NIET COINS MAAR DIT IS EEN VOORBEELD VAN HOE
                //foreach (var coin in collisionGroup)
                //{
                //    if (coin.Rect.Intersects(Rect))
                //    {
                //        _posPlayer.X -= changeX;
                //    }
                //}


                float changeY = 0;
                if (keyboardState.IsKeyDown(Keys.Z) || keyboardState.IsKeyDown(Keys.Up))
                {
                    if (_posPlayer.Y <= 0)
                    {
                        changeY += 0f;
                    }
                    else
                        changeY += -3f;
                }
                if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                {
                    if (_posPlayer.Y >= heightScreen - _sizePlayer.Y)
                    {
                        changeY += 0f;
                    }
                    else
                        changeY += +3f;
                }
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
