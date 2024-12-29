using Microsoft.Xna.Framework.Graphics;
using Monogame2.GameObjects.Items;
using Monogame2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame2.Strategy.Shot
{
    public class StraightShotStrategyShooter : IFiringStrategy
    {
        public void Fire(Vector2 position, List<Projectile> projectiles, Texture2D projectileTexture)
        {
            Projectile newProjectile = new Projectile(projectileTexture, -4f);
            newProjectile.Fire(new Vector2(position.X - 100, position.Y + 20));
            projectiles.Add(newProjectile);
        }
    }

    public class SingleShotStrategyPlayer : IFiringStrategy
    {
        public void Fire(Vector2 position, List<Projectile> projectiles, Texture2D projectileTexture)
        {
            Projectile newProjectile = new Projectile(projectileTexture, 4f);
            newProjectile.Fire(new Vector2(position.X + 50, position.Y + 50));
            projectiles.Add(newProjectile);
        }
    }
}
