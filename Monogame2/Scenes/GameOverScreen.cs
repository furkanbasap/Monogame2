using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Monogame2.Managers;
using Monogame2.Abstract;

namespace Monogame2.Scenes
{
    internal class GameOverScreen : GameScreen
    {
        private SpriteFont font;
        private Texture2D _backgroundTexture;
        private bool gameState;


        public GameOverScreen(bool win)
        {
            gameState = win;
        }

        public override void LoadContent()
        {
            font = Globals.Content.Load<SpriteFont>("Fonts/Font");
            _backgroundTexture = Globals.Content.Load<Texture2D>("Backgrounds/starfield2");

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                GameStateManager.ChangeState(new StartScreen());
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Begin();
            Globals.SpriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            string message = gameState ?
                "GEWONNEN! Druk op Enter om terug te keren naar het startscherm." :
                "VERLOREN! Druk op Enter om terug te keren naar het startscherm.";
            Globals.SpriteBatch.DrawString(font, message, new Vector2(100, 100), Color.White);

            Globals.SpriteBatch.End();
        }
    }
}
