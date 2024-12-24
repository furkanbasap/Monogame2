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
        public Vector2 Position;

        public Camera(Vector2 position)
        {
            this.Position = position;
        }

        public void Follow(Rectangle target, Vector2 screenSize)
        {
            Position = new Vector2(
                -target.X + (screenSize.X / 2 - target.Width),
                -target.Y + (screenSize.Y / 2 - target.Height));


        }
    }
}
