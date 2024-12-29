using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Animation;
using Monogame2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.GameObjects.Enemies
{
    public class EnemyBoss
    {
        private Vector2 _posEnemy;
        private Vector2 _sizeEnemy;

        private Random rnd = new Random();
        Texture2D textureEnemyBoss;


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

        public EnemyBoss(Vector2 position, Vector2 size)
        {
            _posEnemy = position;
            _sizeEnemy = size;
            _projectileTexture = Globals.Content.Load<Texture2D>("Objects/rocket2");
            _projectiles = new List<Projectile>();
        }
        public void LoadContent()
        {
            textureEnemyBoss = Globals.Content.Load<Texture2D>("Objects/ogre");
        }

        public void Update(Vector2 posPlayer)
        {

            if (seconds % 100 == 0)
            {
                FireProjectile();
            }
            seconds++;

            foreach (var projectile in _projectiles)
            {
                projectile.Update(posPlayer);
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                textureEnemyBoss,
                new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y),
                new Rectangle(0, 0, textureEnemyBoss.Width, textureEnemyBoss.Height),
                Color.White);

            foreach (var projectile in _projectiles)
            {
                projectile.Draw();
            }

        }

        private void FireProjectile()
        {
            // Fire a new projectile if none are active
            Projectile newProjectile = new Projectile(_projectileTexture, -3f, true);
            newProjectile.Fire(new Vector2(_posEnemy.X - 100, rnd.Next(50, Globals.HeightScreen - 100)));
            _projectiles.Add(newProjectile);
        }
    }
}
