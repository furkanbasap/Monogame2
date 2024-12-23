using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame2.GameObjects;
using Monogame2.Global;
using Monogame2.Input;
using Monogame2.Physics;
using Monogame2.Scenes;
using Monogame2.Utils;
using System;

namespace Monogame2
{
    public class Game1 : Game
    {

        public static ContentManager ContentManager; // Publieke ContentManager voor resourcebeheer
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;

        private Camera _camera;
        Texture2D playerSprite;
        Texture2D coinSprite;

        MouseState mState;

        Boolean mReleased = true;
        

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ContentManager = Content;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 741;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Start met het StartScreen
            GameStateManager.ChangeState(new StartScreen());
            _camera = new Camera();

            //playerSprite = Content.Load<Texture2D>("Actors/Hero");
            //coinSprite = Content.Load<Texture2D>("Objects/coin");

        }

        protected override void Update(GameTime gameTime)
        {
            // Verwerk de logica van de actieve scène
            GameStateManager.Update(gameTime);
            //player.Update();
            mState = Mouse.GetState();

            //if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            //{
            //score++;
            //mReleased = false;
            //}

            //if (mState.LeftButton == ButtonState.Released)
            //{
            //    mReleased = true
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameStateManager.Draw(spriteBatch);

            spriteBatch.Begin();
            //spriteBatch.Draw(coinSprite, new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(playerSprite, new Vector2(200, 200), Color.White);



            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
