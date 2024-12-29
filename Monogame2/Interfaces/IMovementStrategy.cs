using Microsoft.Xna.Framework;

namespace Monogame2.Interfaces
{
    public interface IMovementStrategy
    {
        void Move(ref Vector2 position);
    }
}
