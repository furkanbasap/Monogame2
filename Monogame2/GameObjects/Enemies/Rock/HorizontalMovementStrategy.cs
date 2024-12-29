using Monogame2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame2.GameObjects.Enemies.Rock
{
    public class HorizontalMovementStrategy : IMovementStrategyRock
    {
        public void Move(ref Vector2 position)
        {
            position.X -= 2f;
        }
    }
}
