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
using Monogame2.Global;

namespace Monogame2.Scenes
{
    internal class GameOverScreen : GameScreen
    {
        private SpriteFont font;
        private Texture2D _backgroundTexture;

        public override void LoadContent()
        {
            font = Globals.content.Load<SpriteFont>("Fonts/Font");
            _backgroundTexture = Globals.content.Load<Texture2D>("Backgrounds/Background");

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                GameStateManager.ChangeState(new StartScreen());
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();
            Globals.spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            Globals.spriteBatch.DrawString(font, "Game Over! Druk op Enter om terug te keren naar het startscherm.", new Vector2(100, 100), Color.White);
            Globals.spriteBatch.End();
        }
    }
}
