using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Animation;
using Monogame2.Managers;


namespace Monogame2.GameObjects
{
    public class Enemy2
    {
        // ENEMY CLASS THAT HURTS PLAYER WHEN THEY INTERCEPT
        private Vector2 _posEnemy;
        private Vector2 _sizeEnemy;


        Texture2D spritesheetEnemy;

        AnimationManager amCoin;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y);
            }
        }

        public Enemy2(Vector2 position, Vector2 size) 
        {
            _posEnemy = position;
            _sizeEnemy = size;
        }
        public void LoadContent()
        {
            spritesheetEnemy = Globals.Content.Load<Texture2D>("Objects/bomb");
        }

        public void Update(Vector2 _posPlayer)
        {
            // BEWEGEN VAN DE ENEMY
            if (_posEnemy.X > _posPlayer.X)
            {
                _posEnemy.X -= 1f;
            }
            else if (_posEnemy.X <= _posPlayer.X)
            {
                _posEnemy.X -= 1f;
            }
            if (_posEnemy.Y < _posPlayer.Y)
            {
                _posEnemy.Y += 1f;
            }
            else if (_posEnemy.Y >= _posPlayer.Y && _posEnemy.X > _posPlayer.X)
            {
                _posEnemy.Y -= 1f;
            }
            else if (_posEnemy.Y >= _posPlayer.Y && _posEnemy.X <= _posPlayer.X)
            {
                _posEnemy.Y = _posEnemy.Y;
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                spritesheetEnemy,
                new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y),
                new Rectangle(0, 0, spritesheetEnemy.Width,spritesheetEnemy.Height),
                Color.White);

        }
    }
}