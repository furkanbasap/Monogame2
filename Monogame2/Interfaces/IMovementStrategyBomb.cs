using Microsoft.Xna.Framework;

namespace Monogame2.Interfaces
{
    public interface IMovementStrategyBomb
    {
        void Move(ref Vector2 position, Vector2 target);
    }
}
