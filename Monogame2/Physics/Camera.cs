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

        public void Follow(Vector2 target)
        {
            position = new Vector2(
                -target.X,
                -target.Y);


        }
    }
}
