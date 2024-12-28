using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Monogame2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame2.Enums;

namespace Monogame2.Scenes
{
    internal class BossFightScreen : GameScreen
    {
        private SpriteFont font;
        private Texture2D _backgroundTexture;
        private ScrollingBackground myBackground;

        private int playerLives;
        private int bossLives;
        private GameDifficulty difficulty;
        private bool paused;
        private KeyboardState currentKeyboardState, previousKeyboardState;



        public BossFightScreen(int playerLives, GameDifficulty difficulty)
        {
            this.playerLives = playerLives;
            paused = false;
            this.difficulty = difficulty;
            if (this.difficulty == GameDifficulty.NORMAL)
            {
                bossLives = 50;
            }
            else if (this.difficulty == GameDifficulty.HARD)
            {
                bossLives = 100;
            }

        }

        public override void LoadContent()
        {
            font = Globals.Content.Load<SpriteFont>("Fonts/Font");

            myBackground = new ScrollingBackground();
            Texture2D background = Globals.Content.Load<Texture2D>("Backgrounds/starfield3");
            myBackground.Load(background);

        }

        public override void Update(GameTime gameTime)
        {
            if (bossLives == 0)
            {
                GameStateManager.ChangeState(new GameOverScreen(true));
            }
            if (playerLives == 0)
            {
                GameStateManager.ChangeState(new GameOverScreen(false));
            }

            if (!paused)
            {
                myBackground.Update(1 * 1);
            }

            currentKeyboardState = Keyboard.GetState();
            if ((IsKeyPressed(Keys.P) && paused == true) && (currentKeyboardState.IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P)))
            {
                paused = false;
            }
            else if ((IsKeyPressed(Keys.P) && paused == false) && (currentKeyboardState.IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P)))
            {
                paused = true;
            }
            previousKeyboardState = currentKeyboardState;

        }

        public override void Draw()
        {
            Globals.SpriteBatch.Begin();
            myBackground.Draw(Globals.SpriteBatch, Color.White);

            //PAUSE METHOD
            if (paused)
            {
                Globals.SpriteBatch.DrawString(font, "Game Paused", new Vector2(Globals.WidthScreen / 2 - 50, Globals.HeightScreen / 2), Color.Red);
            }
            else
            {
                Globals.SpriteBatch.DrawString(font, $"Game Mode: {difficulty}", new Vector2(10, 10), Color.White);
                Globals.SpriteBatch.DrawString(font, $"Lives: {playerLives}", new Vector2(10, 40), Color.White);
                Globals.SpriteBatch.DrawString(font, $"Kills: {bossLives}", new Vector2(Globals.WidthScreen / 2 - 100, 100), Color.Red);
                Globals.SpriteBatch.DrawString(font, "(Press P to pause game)", new Vector2(Globals.WidthScreen - 210, 40), Color.White);
            }


            Globals.SpriteBatch.End();
        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
