using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using Monogame2.Managers;
using Monogame2.Enums;

namespace Monogame2.Scenes
{
    public class StartScreen : GameScreen
    {
        private SpriteFont font;
        private KeyboardState currentKeyboardState, previousKeyboardState;
        private string[] menuItems = { "Normal", "Hard", "Controls and How the game works" };
        private int selectedIndex = 0;


        private ScrollingBackground myBackground;
        private float scrollingSpeed = 1;

        MouseState mState;

        public override void LoadContent()
        {
            font = Globals.Content.Load<SpriteFont>("Fonts/Font");

            myBackground = new ScrollingBackground();
            Texture2D background = Globals.Content.Load<Texture2D>("Backgrounds/starfield3");
            myBackground.Load(background);
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
                else if (selectedIndex == 2)
                    GameStateManager.ChangeState(new InfoSettingsScreen());


            }


            previousKeyboardState = currentKeyboardState;

            myBackground.Update(1 * scrollingSpeed);
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Begin();
            myBackground.Draw(Globals.SpriteBatch, Color.White);

            Globals.SpriteBatch.DrawString(font, "FlyBy", new Vector2(750, 250), Color.White);
            Globals.SpriteBatch.DrawString(font, "Choose difficulty (Space or Left Click)", new Vector2(700, 300), Color.White);
            Globals.SpriteBatch.DrawString(font, "(Press M to mute song)", new Vector2(10, 10), Color.White);

            for (int i = 0; i < menuItems.Length; i++)
            {
                Color color = (i == selectedIndex) ? Color.Yellow : Color.White;
                Globals.SpriteBatch.DrawString(font, menuItems[i], new Vector2(750, 350 + i * 30), color);
            }
            Globals.SpriteBatch.End();
        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
