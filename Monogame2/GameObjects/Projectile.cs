using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Managers;
using System.Collections.Generic;


namespace Monogame2.GameObjects
{
    public class Projectile
    {

        public Vector2 Position { get; private set; }
        public bool IsActive { get; private set; }
        public float Speed { get; set; }
        public Vector2 Destination { get; set; }
        private bool dest;
        private Texture2D _texture;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)_texture.Width, (int)_texture.Height);
            }
        }

        public Projectile(Texture2D texture, float speed)
        {
            _texture = texture;
            Speed = speed;
            IsActive = false;
        }

        public void Fire(Vector2 startPosition)
        {
            Position = startPosition;
            IsActive = true;
            dest = false;
        }

        // VOOR BOSS ENEMY
        public void Fire(Vector2 startPosition, Vector2 destination)
        {
            Position = startPosition;
            IsActive = true;
            Destination = destination;
            dest = true;
        }

        public void Update()
        {
            if (IsActive)
            {
                if (dest == false)
                {
                    Position = new Vector2(Position.X + Speed, Position.Y);
                }
                else if (dest == true)
                {
                    Position = new Vector2(Destination.X + Speed, Destination.Y);
                }


                // Deactivate the projectile if it moves off-screen (right side)
                if (Position.X > Globals.WidthScreen || Position.X < 0 || Position.Y < 0 || Position.Y > Globals.HeightScreen) // Assuming screen width is 1600
                {
                    IsActive = false;
                }
            }
        }

        public void Draw()
        {
            if (IsActive)
            {
                Globals.SpriteBatch.Draw(_texture, Position, Color.White);
            }
        }
    }
}
