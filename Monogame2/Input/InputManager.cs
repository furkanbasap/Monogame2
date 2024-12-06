using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.Input
{
    public static class InputManager
    {
        private static Vector2 _direction;

        public static Vector2 Direction => _direction;

        public static bool Moving => _direction != Vector2.Zero;

        public static void Update()
        {
            _direction = Vector2.Zero;
            var keyboardState = Keyboard.GetState();

            if (keyboardState.GetPressedKeyCount() > 0) 
            {
                if (keyboardState.IsKeyDown(Keys.Q)) _direction.X = -3f;
                if (keyboardState.IsKeyDown(Keys.D)) _direction.X = +3f;
                if (keyboardState.IsKeyDown(Keys.Z)) _direction.Y = -3f;
                if (keyboardState.IsKeyDown(Keys.S)) _direction.Y = +3f;

            }
        }
    }
}
