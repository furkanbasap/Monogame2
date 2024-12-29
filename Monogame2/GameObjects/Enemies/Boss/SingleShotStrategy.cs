using Microsoft.Xna.Framework.Graphics;
using Monogame2.GameObjects.Items;
using Monogame2.Interfaces;
using Monogame2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Monogame2.GameObjects.Enemies.Boss
{
    public class SingleShotStrategy : IFiringStrategy
    {
        private Random rnd = new Random();

        public void Fire(Vector2 position, List<Projectile> projectiles, Texture2D projectileTexture)
        {
            Projectile newProjectile = new Projectile(projectileTexture, 1.5f);
            newProjectile.Fire(new Vector2(position.X - 100, rnd.Next(50, Globals.HeightScreen - 100)));
            projectiles.Add(newProjectile);
        }
    }
}
