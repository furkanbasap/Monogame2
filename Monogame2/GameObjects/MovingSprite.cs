using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.GameObjects
{
    internal class MovingSprite : ScaledSprite
    {

        private float speed;
        public MovingSprite(Texture2D texture, Vector2 position, float speed) : base(texture, position)
        {
            this.speed = speed;
        }

        public override void Update()
        {
            base.Update();
            position.X += speed;
        }

    }
}
