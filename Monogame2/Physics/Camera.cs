using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.Physics
{
    public class Camera
    {
        private Vector2 position;

        public Camera(Vector2 position)
        {
            this.position = position;
        }

        public void Follow(Rectangle target, Vector2 screenSize)
        {
            position = new Vector2(
                -target.X + (screenSize.X / 2 - target.Width),
                -target.Y + (screenSize.Y / 2 - target.Height));


        }
    }
}
