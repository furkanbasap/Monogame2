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
using Monogame2.Input;
using Monogame2.Global;

namespace Monogame2.Scenes
{
    public enum GameDifficulty { NORMAL, HARD}
    public class GameplayScreen : GameScreen
    {
        private GameDifficulty difficulty;

        private Texture2D _backgroundTexture;
        private int playerLives = 3;

        //coin
        private Texture2D _coinTexture;
        private Vector2 _position = new Vector2(200, 200);
        private Animations _anim;
        //coin

        //player
        private Texture2D _playerTexture;
        //player

        Texture2D spritesheetPlayer;
        Texture2D spritesheetCoin;

        AnimationManager amPlayer;
        AnimationManager amCoin;


        public GameplayScreen(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
        }

        public override void LoadContent() 
        {
            // Laad de sprite van de speler
            _backgroundTexture = Globals.content.Load<Texture2D>("Backgrounds/Background");
            _coinTexture = Globals.content.Load<Texture2D>("Objects/coin");

            spritesheetPlayer = Globals.content.Load<Texture2D>("Actors/Hero");
            spritesheetCoin = Globals.content.Load<Texture2D>("Objects/coin3");
            amPlayer = new(8, 8, new Vector2(82,100));
            amCoin = new(8, 8, new Vector2(100, 150));

        }

        public override void Update(GameTime gameTime) 
        {
            InputManager.Update();
            amPlayer.Update();
            amCoin.Update();
        }

        public override void Draw() 
        {
            Globals.spriteBatch.Begin();

            // Tekenen
            Globals.spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("Fonts/Font"),$"Game Mode: {difficulty}",new Vector2(10, 10),Color.White);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("Fonts/Font"), $"Lives: {playerLives}", new Vector2(10, 40), Color.White);


            Globals.spriteBatch.Draw(
                spritesheetPlayer,
                new Rectangle(100, 100, 200, 200),
                amPlayer.GetFrame(),
                Color.White);


            Globals.spriteBatch.Draw(
                spritesheetCoin,
                new Rectangle(100, 400, 200, 200),
                amCoin.GetFrame(),
                Color.White);
            
            Globals.spriteBatch.End();
        }


    }
}
