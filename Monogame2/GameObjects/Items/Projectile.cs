using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Managers;
using System.Collections.Generic;


namespace Monogame2.GameObjects.Items
{
    public class Projectile
    {

        public Vector2 Position { get; private set; }
        public bool IsActive { get; private set; }
        public float Speed { get; set; }
        private Texture2D _texture;

        public Rectangle Rect => new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

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
        }


        public void Update()
        {
            if (IsActive)
            {
                Position = new Vector2(Position.X + Speed, Position.Y);

                // Deactivate the projectile if it moves off-screen (right side)
                if (Position.X > Globals.WidthScreen - 100 || Position.X < 0 || Position.Y < 0 || Position.Y > Globals.HeightScreen) // Assuming screen width is 1600
                {
                    IsActive = false;
                }
            }

        }

        public void Update(Vector2 _posPlayer)
        {
            // BEWEGEN VAN DE ENEMY
            if (Position.X > _posPlayer.X)
            {
                Position = new Vector2(Position.X - Speed, Position.Y);
            }
            else if (Position.X <= _posPlayer.X)
            {
                Position = new Vector2(Position.X - Speed, Position.Y);
            }
            if (Position.Y < _posPlayer.Y)
            {
                Position = new Vector2(Position.X, Position.Y + Speed);
            }
            else if (Position.Y >= _posPlayer.Y && Position.X > _posPlayer.X)
            {
                Position = new Vector2(Position.X, Position.Y - Speed);
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
