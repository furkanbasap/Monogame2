using Monogame2.Interfaces;
using Microsoft.Xna.Framework;

namespace Monogame2.Strategy.Movement
{
    public class HorizontalMovementStrategy : IMovementStrategy
    {
        public void Move(ref Vector2 position)
        {
            position.X -= 2f;
        }
    }


}
