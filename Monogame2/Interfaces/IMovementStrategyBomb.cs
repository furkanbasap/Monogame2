using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame2.Interfaces
{
    public interface IMovementStrategyBomb
    {
        void Move(ref Vector2 position, Vector2 target);
    }
}
