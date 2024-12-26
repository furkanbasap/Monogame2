using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Managers;


namespace Monogame2.GameObjects
{
    public class Sprite
    {
        protected readonly Texture2D texture;
        protected readonly Vector2 origin;

        public Vector2 Position { get; set; }
        public int Speed { get; set; }
        public float Rotation { get; set; }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            this.Speed = 300;
            this.origin = new(texture.Width / 2, texture.Height / 2);

        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Position, null, Color.White, Rotation, origin, 1, SpriteEffects.None, 1);
        }

    }
}
