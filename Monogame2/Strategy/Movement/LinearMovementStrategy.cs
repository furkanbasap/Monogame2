using Monogame2.Interfaces;
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
