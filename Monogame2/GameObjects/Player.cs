using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.GameObjects
{
    public class Player
    {
        public Vector2 Position { get; private set; }
        private float speed = 200f;

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 movement = Vector2.Zero;

            if (state.IsKeyDown(Keys.Up)) movement.Y -= 1;
            if (state.IsKeyDown(Keys.Down)) movement.Y += 1;
            if (state.IsKeyDown(Keys.Left)) movement.X -= 1;
            if (state.IsKeyDown(Keys.Right)) movement.X += 1;

            Position += movement * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Position, Color.White);
            spriteBatch.End();
        }
    }
}
