using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Managers;


namespace Monogame2.GameObjects
{
    public class Projectile
    {
        //public Vector2 Direction { get; set; }
        //public float LifeSpan { get; private set; }

        //public Projectile(Texture2D texture, ProjectileData data) : base(texture, data.Position)
        //{
        //    Speed = data.Speed;
        //    Direction = new(1,0);
        //    LifeSpan = data.Lifespan;
        //}

        //public void Update()
        //{
        //    Position += ;
        //    LifeSpan -= Globals.TotalSeconds;
        //}
        public Vector2 Position { get; private set; }
        public bool IsActive { get; private set; }
        public float Speed { get; set; }

        private Texture2D _texture;

        public Projectile(Texture2D texture)
        {
            _texture = texture;
            Speed = 3f;
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
                if (Position.X > Globals.WidthScreen) // Assuming screen width is 1600
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
