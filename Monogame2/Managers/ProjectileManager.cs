using Microsoft.Xna.Framework.Graphics;
using Monogame2.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.Managers
{
    public static class ProjectileManager
    {
        private static Texture2D _texture;
        public static List<Projectile> Projectiles { get; } = new();

        public static void Init()
        {
            _texture = Globals.Content.Load<Texture2D>("Objects/coin");
        }

        public static void AddProjectile(ProjectileData data)
        {
            Projectiles.Add(new(_texture, data));
        }

        public static void Update()
        {
            foreach (var p in Projectiles) 
            {
                p.Update();
            }
            Projectiles.RemoveAll((p) => p.LifeSpan <= 0);
        }

        public static void Draw()
        {
            foreach (var p in Projectiles)
            {
                p.Draw();
            }
        }
    }
}
