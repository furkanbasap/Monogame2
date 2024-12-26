using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Animation;
using Monogame2.Managers;

namespace Monogame2.GameObjects
{
    public class Enemy1
    {
        // ENEMY CLASS THAT MOVES TOWARDS PLAYERS X POSITION NOT Y (LIKE A ROCK IN SPACE)
        
        public Vector2 _posEnemy;
        private Vector2 _sizeEnemy;

        public Texture2D spritesheetEnemy;
        private float rotation;
        private float seconds;

        AnimationManager amCoin;

         

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y);
            }
        }

        public Enemy1(Vector2 position, Vector2 size)
        {
            _posEnemy = position;
            _sizeEnemy = size;
            rotation = 0;
        }
        public void LoadContent()
        {
            spritesheetEnemy = Globals.Content.Load<Texture2D>("Objects/rock");

        }

        public void Update()
        {


            // BEWEGEN VAN DE ENEMY
            _posEnemy.X -= 1f;

            if (seconds % 3 == 0)
            {
                rotation+= 0.01f;
            }
            seconds++;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                spritesheetEnemy,
                new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y),
                new Rectangle(0, 0, spritesheetEnemy.Width, spritesheetEnemy.Height),
                Color.White,
                rotation,
                new Vector2(spritesheetEnemy.Width / 2, spritesheetEnemy.Height / 2),
                SpriteEffects.None,
                0f);

        }
    }
}
