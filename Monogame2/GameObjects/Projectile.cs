using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.GameObjects
{
    public class Projectile : Sprite
    {
        public Vector2 Direction { get; set; }
        public float LifeSpan { get; private set; }

        public Projectile(Texture2D texture, ProjectileData data) : base(texture, data.Position)
        {
            Speed = data.Speed;
            Direction = new(1,0);
            LifeSpan = data.Lifespan;
        }

        public void Update()
        {
            Position += Direction * Speed * Globals.TotalSeconds;
            LifeSpan -= Globals.TotalSeconds;
        }
    }
}
