using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame2.Managers
{
    public class InputManager
    {
        private static MouseState _lastMouseState;
        private static Vector2 _direction;

        private static KeyboardState _lastKeyboardState;
        private static KeyboardState _currentKeyboardState;

        public static Vector2 Direction => _direction;
        public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
        public static bool MouseClicked { get; private set; }

        public static void Update()
        {
            _currentKeyboardState = Keyboard.GetState();
            var currentMouseState = Mouse.GetState();

            _direction = Vector2.Zero;
            if (_currentKeyboardState.IsKeyDown(Keys.Z)) _direction.Y--;
            if (_currentKeyboardState.IsKeyDown(Keys.S)) _direction.Y++;
            if (_currentKeyboardState.IsKeyDown(Keys.Q)) _direction.X--;
            if (_currentKeyboardState.IsKeyDown(Keys.D)) _direction.X++;

            MouseClicked = (currentMouseState.LeftButton == ButtonState.Pressed) && (_lastMouseState.LeftButton == ButtonState.Released);

            _lastMouseState = currentMouseState;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && !_lastKeyboardState.IsKeyDown(key);

        }

        public static void PostUpdate()
        {
            _lastKeyboardState = _currentKeyboardState;
        }

    }
}
