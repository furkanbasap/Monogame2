using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Monogame2.Interfaces;

namespace Monogame2.GameObjects.Enemies.Bomb
{
    public class DirectMovementStrategy : IMovementStrategyBomb
    {
        public void Move(ref Vector2 position, Vector2 target)
        {
            if (position.X > target.X)
            {
                position.X -= 1.5f;
            }
            else if (position.X <= target.X)
            {
                position.X += 1.5f;
            }

            if (position.Y < target.Y)
            {
                position.Y += 1.5f;
            }
            else if (position.Y > target.Y)
            {
                position.Y -= 1.5f;
            }
        }
    }

}
