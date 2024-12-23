using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using Monogame2.GameObjects;
using Monogame2.Animation;
using Monogame2.Global;
using Microsoft.Xna.Framework.Input;

namespace Monogame2.Scenes
{
    public enum GameDifficulty { NORMAL, HARD}
    public class GameplayScreen : GameScreen
    {
        private GameDifficulty difficulty;

        private Texture2D _backgroundTexture;
        private int playerLives = 3;


        Coin coin = new Coin(new Vector2(100, 400), new Vector2(200,200));
        Player player = new Player(new Vector2(100,100), new Vector2(200,200));


        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
        }

        public override void LoadContent() 
        {
            // Laad de sprite van de speler
            _backgroundTexture = Globals.content.Load<Texture2D>("Backgrounds/Background");

            coin.LoadContent();

            player.LoadContent();

        }

        public override void Update(GameTime gameTime) 
        {
            coin.Update();

            player.Update();

        }

        public override void Draw() 
        {
            Globals.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Tekenen
            Globals.spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);



            coin.Draw();

            player.Draw();


            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("Fonts/Font"), $"Game Mode: {difficulty}", new Vector2(10, 10), Color.White);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("Fonts/Font"), $"Lives: {playerLives}", new Vector2(10, 40), Color.White);


            Globals.spriteBatch.End();


        }


    }
}
