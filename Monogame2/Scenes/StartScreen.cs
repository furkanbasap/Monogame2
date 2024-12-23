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
using System.ComponentModel;
using System.Reflection.Metadata;
using Monogame2.Global;

namespace Monogame2.Scenes
{
    public class StartScreen : GameScreen
    {
        private SpriteFont font;
        private KeyboardState currentKeyboardState, previousKeyboardState;
        private string[] menuItems = { "Normal", "Hard" };
        private int selectedIndex = 0;

        private Texture2D _backgroundTexture;

        MouseState mState;

        public override void LoadContent()
        {
            font = Globals.content.Load<SpriteFont>("Fonts/Font");
            _backgroundTexture = Globals.content.Load<Texture2D>("Backgrounds/Background");

        }

        public override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            mState = Mouse.GetState();

            if (IsKeyPressed(Keys.Up) || IsKeyPressed(Keys.Z))
                selectedIndex = Math.Max(selectedIndex - 1, 0);

            if (IsKeyPressed(Keys.Down) || IsKeyPressed(Keys.S))
                selectedIndex = Math.Min(selectedIndex + 1, menuItems.Length - 1);

            if (IsKeyPressed(Keys.Space) || (mState.LeftButton == ButtonState.Pressed))
            {
                if (selectedIndex == 0)
                    GameStateManager.ChangeState(new GameplayScreen(GameDifficulty.NORMAL));
                else if (selectedIndex == 1)
                    GameStateManager.ChangeState(new GameplayScreen(GameDifficulty.HARD));
                
            }



            previousKeyboardState = currentKeyboardState;
        }

        public override void Draw()
        {
            Globals.spriteBatch.Begin();
            Globals.spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);
            Globals.spriteBatch.DrawString(font, "Startscreen", new Vector2(600, 150), Color.White);
            Globals.spriteBatch.DrawString(font, "Choose difficulty (Space or Left Click)", new Vector2(500, 200), Color.White);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Color color = (i == selectedIndex) ? Color.Yellow : Color.White;
                Globals.spriteBatch.DrawString(font, menuItems[i], new Vector2(600, 250 + i * 30), color);
            }
            Globals.spriteBatch.End();
        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
