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
        private bool _dest;
        private Texture2D _texture;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)_texture.Width, (int)_texture.Height);
            }
        }

        public Projectile(Texture2D texture, float speed, bool dest)
        {
            _texture = texture;
            Speed = speed;
            IsActive = false;
            _dest = dest;
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
                if (_dest == false)
                {
                    Position = new Vector2(Position.X + Speed, Position.Y);
                }

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
                Position = new Vector2(Position.X + -1.5f, Position.Y);
            }
            else if (Position.X <= _posPlayer.X)
            {
                Position = new Vector2(Position.X + -1.5f, Position.Y);
            }
            if (Position.Y < _posPlayer.Y)
            {
                Position = new Vector2(Position.X, Position.Y + 1.5f);
            }
            else if (Position.Y >= _posPlayer.Y && Position.X > _posPlayer.X)
            {
                Position = new Vector2(Position.X, Position.Y - 1.5f);
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
