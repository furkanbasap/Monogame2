using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame2.Animation;
using Monogame2.Managers;
using System.Collections.Generic;


namespace Monogame2.GameObjects.Enemies
{
    public class Shooter
    {
        private Vector2 _posEnemy;
        private Vector2 _sizeEnemy;


        Texture2D spritesheetEnemy;

        AnimationManager amCoin;

        private float seconds;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y);
            }
        }

        public List<Projectile> _projectiles;
        private Texture2D _projectileTexture;

        public Shooter(Vector2 position, Vector2 size, Texture2D projectileTexture)
        {
            _posEnemy = position;
            _sizeEnemy = size;
            _projectileTexture = projectileTexture;
            _projectiles = new List<Projectile>();
        }
        public void LoadContent()
        {
            spritesheetEnemy = Globals.Content.Load<Texture2D>("Objects/enemyShip1");
        }

        public void Update()
        {

            if (seconds % 200 == 0)
            {
                FireProjectile();
            }
            seconds++;

            foreach (var projectile in _projectiles)
            {
                projectile.Update();
            }

            // BEWEGEN VAN DE ENEMY
            if (_posEnemy.X == Globals.WidthScreen - 400)
            {
                _posEnemy.X = Globals.WidthScreen - 400;
            }
            else
            {
                _posEnemy.X -= 1f;
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                spritesheetEnemy,
                new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y),
                new Rectangle(0, 0, spritesheetEnemy.Width, spritesheetEnemy.Height),
                Color.White);

            foreach (var projectile in _projectiles)
            {
                projectile.Draw();
            }

        }

        private void FireProjectile()
        {
            // Fire a new projectile if none are active
            Projectile newProjectile = new Projectile(_projectileTexture, -4f);
            newProjectile.Fire(new Vector2(_posEnemy.X - 100, _posEnemy.Y + 20));
            _projectiles.Add(newProjectile);
        }
    }
}