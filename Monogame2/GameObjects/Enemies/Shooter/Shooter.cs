using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.GameObjects.Items;
using Monogame2.Interfaces;
using Monogame2.Managers;
using System.Collections.Generic;


namespace Monogame2.GameObjects.Enemies.Shooter
{
    public class Shooter
    {
        //STATEGY

        private Vector2 _posEnemy;
        private Vector2 _sizeEnemy;

        private Texture2D textureShooter;

        private float seconds;

        public Rectangle Rect => new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y);

        private IFiringStrategy _firingStrategy;

        public List<Projectile> _projectiles;
        private Texture2D _projectileTexture;

        public Shooter(Vector2 position, Vector2 size)
        {
            _posEnemy = position;
            _sizeEnemy = size;
            _projectileTexture = Globals.Content.Load<Texture2D>("Objects/rocket2");
            _projectiles = new List<Projectile>();
            _firingStrategy = new StraightShotStrategyShooter();
        }
        public void LoadContent()
        {
            textureShooter = Globals.Content.Load<Texture2D>("Objects/enemyShip1");
        }

        public void Update()
        {

            if (seconds % 200 == 0)
            {
                _firingStrategy.Fire(_posEnemy, _projectiles, _projectileTexture);
            }
            seconds++;

            foreach (var projectile in _projectiles)
            {
                projectile.Update();
            }

            // Enemy movement
            if (_posEnemy.X <= Globals.WidthScreen - 400)
            {
                _posEnemy.X = Globals.WidthScreen - 400;
            }
            else
            {
                _posEnemy.X -= 3f;
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                textureShooter,
                new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y),
                new Rectangle(0, 0, textureShooter.Width, textureShooter.Height),
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

        public void SetFiringStrategy(IFiringStrategy firingStrategy)
        {
            _firingStrategy = firingStrategy;
        }
    }
}