using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame2.Abstract;
using Monogame2.Managers;

namespace Monogame2.Scenes
{
    public class InfoSettingsScreen : GameScreen
    {
        private SpriteFont font;
        private ScrollingBackground myBackground;

        public InfoSettingsScreen()
        {}

        public override void LoadContent()
        {
            myBackground = new ScrollingBackground();
            Texture2D background = Globals.Content.Load<Texture2D>("Backgrounds/starfield3");
            myBackground.Load(background);
            font = Globals.Content.Load<SpriteFont>("Fonts/Font");
        }
        public override void Update(GameTime gameTime)
        {
            myBackground.Update(1 * 1);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                GameStateManager.ChangeState(new StartScreen());

        }
        public override void Draw()
        {
            Globals.SpriteBatch.Begin();

            myBackground.Draw(Globals.SpriteBatch, Color.White);
            DrawText($"Up: Z or Arrow Up", new Vector2(10, 10), Color.White);
            DrawText("Down: S or Arrow Down", new Vector2(10, 40), Color.White);
            DrawText("Left: Q or Arrow Left", new Vector2(10, 70), Color.White);
            DrawText("Right: D or Arrow Right", new Vector2(10, 100), Color.White);
            DrawText("Shoot: Space", new Vector2(10, 130), Color.White);

            DrawText("Enemies", new Vector2(10, 190), Color.White);
            DrawText("Rock: Obstacle that pushes you. If you get pushed off the map, you lose.", new Vector2(10, 220), Color.White);
            DrawText("Bomb: Obstacle that comes to you and explodes when it hits you. If you get hit by a bomb you will lose a life and momentarilly become invincible.", new Vector2(10, 250), Color.White);
            DrawText("Shooter: Obstacle that shoots from far away with projectiles. If these projectiles hit you, you will lose a life and momentarilly become invincible.", new Vector2(10, 280), Color.White);
            DrawText("Boss: Behaves like the shooter but the projectiles are heat seeking missiles. Depending on your difficulty you will have to hit it different amount of times. Normal = 50, Hard = 100", new Vector2(10, 310), Color.White);

            DrawText("Pickups", new Vector2(10, 370), Color.White);
            DrawText("Coin: You can pick these up to win the game. If you win by collecting the coins you will NOT get a Boss Fight. Normal = 50, Hard = 100", new Vector2(10, 400), Color.White);
            DrawText("Health: You can pick these up to gain lives.", new Vector2(10, 430), Color.White);

            DrawText("Heads Up Display", new Vector2(10, 490), Color.White);
            DrawText("Lives: Here you can see how many lives you have left. Depending on the difficulty you will have different amount of lives. Normal = 10, Hard = 5", new Vector2(10, 520), Color.White);
            DrawText("Points: This is the amount of coins you have collected", new Vector2(10, 550), Color.White);
            DrawText("Kills: This is the amount of Shooters you have shot down. This doesn't add by shooting a bomb", new Vector2(10, 580), Color.White);

            DrawText("Press Escape to go back", new Vector2(Globals.WidthScreen - 210, 10), Color.Red);

            Globals.SpriteBatch.End();
        }

        private void DrawText(string text, Vector2 position, Color color)
        {
            Globals.SpriteBatch.DrawString(font, text, position, color);
        }

    }
}
