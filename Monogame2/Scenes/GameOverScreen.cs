using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame2.Utils;

namespace Monogame2.Scenes
{
    internal class GameOverScreen : GameScreen
    {
        private SpriteFont font;

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Font");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                GameStateManager.ChangeState(new StartScreen());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Game Over! Druk op Enter om terug te keren naar het startscherm.", new Vector2(100, 100), Color.White);
            spriteBatch.End();
        }
    }
}
