using Monogame2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame2.Strategy.Movement
{
    public class LinearMovementStrategy : IMovementStrategy
    {
        public void Move(ref Vector2 position)
        {
            position.X -= 2.5f; // Moves the coin to the left
        }
    }
}
