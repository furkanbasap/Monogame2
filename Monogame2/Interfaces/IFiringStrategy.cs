using Microsoft.Xna.Framework.Graphics;
using Monogame2.GameObjects.Items;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Monogame2.Interfaces
{
    public interface IFiringStrategy
    {
        void Fire(Vector2 position, List<Projectile> projectiles, Texture2D projectileTexture);
    }

}
