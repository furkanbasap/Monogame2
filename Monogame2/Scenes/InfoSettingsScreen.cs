using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.Scenes
{
    public class InfoSettingsScreen : GameScreen
    {
        private SpriteFont font;
        private Texture2D _backgroundTexture;
        private ScrollingBackground myBackground;

        public InfoSettingsScreen()
        {

        }

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
            Globals.SpriteBatch.DrawString(font, $"Up: Z or Arrow Up", new Vector2(10, 10), Color.White);
            Globals.SpriteBatch.DrawString(font, $"Down: S or Arrow Down", new Vector2(10, 40), Color.White);
            Globals.SpriteBatch.DrawString(font, $"Left: Q or Arrow Left", new Vector2(10, 70), Color.White);
            Globals.SpriteBatch.DrawString(font, "Right: D or Arrow Right", new Vector2(10, 100), Color.White);
            Globals.SpriteBatch.DrawString(font, "Shoot: Space", new Vector2(10, 130), Color.White);

            Globals.SpriteBatch.DrawString(font, "Enemies", new Vector2(10, 190), Color.White);
            Globals.SpriteBatch.DrawString(font, "Rock: Obstacle that pushes you. If you get pushed off the map, you lose.", new Vector2(10, 220), Color.White);
            Globals.SpriteBatch.DrawString(font, "Bomb: Obstacle that comes to you and explodes when it hits you. If you get hit by a bomb you will lose a life and momentarilly become invincible.", new Vector2(10, 250), Color.White);
            Globals.SpriteBatch.DrawString(font, "Shooter: Obstacle that shoots from far away with projectiles. If these projectiles hit you, you will lose a life and momentarilly become invincible.", new Vector2(10, 280), Color.White);
            Globals.SpriteBatch.DrawString(font, "Boss: Behaves like the shooter but the projectiles are shot in your direction. Depending on your difficulty you will have to hit it different amount of times. Normal = 50, Hard = 100", new Vector2(10, 310), Color.White);

            Globals.SpriteBatch.DrawString(font, "Pickups", new Vector2(10, 370), Color.White);
            Globals.SpriteBatch.DrawString(font, "Coin: You can pick these up to win the game. If you win by collecting the coins you will NOT get a Boss Fight. Normal = 50, Hard = 100", new Vector2(10, 400), Color.White);
            Globals.SpriteBatch.DrawString(font, "Health: You can pick these to gain lives.", new Vector2(10, 430), Color.White);

            Globals.SpriteBatch.DrawString(font, "Heads Up Display", new Vector2(10, 490), Color.White);
            Globals.SpriteBatch.DrawString(font, "Lives: Here you can see how many lives you have left. Depending on the difficulty you will have different amount of lives. Normal = 10, Hard = 5", new Vector2(10, 520), Color.White);
            Globals.SpriteBatch.DrawString(font, "Points: This is the amount of coins you have collected", new Vector2(10, 550), Color.White);
            Globals.SpriteBatch.DrawString(font, "Kills: This is the amount of Shooters you have shot down. This doesn't add by shooting a bomb", new Vector2(10, 580), Color.White);

            Globals.SpriteBatch.DrawString(font, "Press Escape to go back", new Vector2(Globals.WidthScreen - 210, 10), Color.Red);

            Globals.SpriteBatch.End();
        }
    }
}
