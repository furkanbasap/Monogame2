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
    public class StartScreen : GameScreen
    {
        private SpriteFont font;
        private KeyboardState currentKeyboardState, previousKeyboardState;
        private string[] menuItems = { "Normal", "Hard" };
        private int selectedIndex = 0;

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/Font");
        }

        public override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (IsKeyPressed(Keys.Up))
                selectedIndex = Math.Max(selectedIndex - 1, 0);

            if (IsKeyPressed(Keys.Down))
                selectedIndex = Math.Min(selectedIndex + 1, menuItems.Length - 1);

            if (IsKeyPressed(Keys.Enter))
            {
                if (selectedIndex == 0)
                    GameStateManager.ChangeState(new GameplayScreen(GameDifficulty.NORMAL));
                else
                    GameStateManager.ChangeState(new GameplayScreen(GameDifficulty.HARD));
            }


            previousKeyboardState = currentKeyboardState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Startscreen", new Vector2(100, 50), Color.White);
            spriteBatch.DrawString(font, "Difficulty", new Vector2(100, 100), Color.White);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Color color = (i == selectedIndex) ? Color.Yellow : Color.White;
                spriteBatch.DrawString(font, menuItems[i], new Vector2(100, 150 + i * 30), color);
            }
            spriteBatch.End();
        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
