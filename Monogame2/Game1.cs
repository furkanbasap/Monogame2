using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Monogame2.GameObjects;
using Monogame2.Managers;
using Monogame2.Physics;
using Monogame2.Scenes;
using Monogame2.Utils;
using System;

namespace Monogame2
{
    public class Game1 : Game
    {

        public static ContentManager ContentManager;
        private GraphicsDeviceManager graphics;
        bool play;
        Song song;
        private KeyboardState currentKeyboardState, previousKeyboardState;


        MouseState mState;

        //Boolean mReleased = true;
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();
            Player.ScreenSize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);


        }

        protected override void LoadContent()
        {
            Globals.Content = this.Content;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Start with the StartScreen
            GameStateManager.ChangeState(new StartScreen());

            song = Globals.Content.Load<Song>("Audio/Backgroundmusic");
            MediaPlayer.Play(song);

            

        }

        protected override void Update(GameTime gameTime)
        {
            // Process the logic of the active scene
            GameStateManager.Update(gameTime);

            currentKeyboardState = Keyboard.GetState();

            if ((IsKeyPressed(Keys.M) && play == true) && (currentKeyboardState.IsKeyDown(Keys.M) && !previousKeyboardState.IsKeyDown(Keys.M)))
            {
                MediaPlayer.Stop();
                play = false;
            }
            else if ((IsKeyPressed(Keys.M) && play == false) && (currentKeyboardState.IsKeyDown(Keys.M) && !previousKeyboardState.IsKeyDown(Keys.M)))
            {
                MediaPlayer.Play(song);
                play = true;
            }
            previousKeyboardState = currentKeyboardState;

            mState = Mouse.GetState();

            //if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            //{
            //float dist = Vector2.Distance(targetPosition, mState.Position.ToVector2());
            //if()
            //{
            //    score++;

            //    Random randomPosition = new Random();

            //    targetPosition.X = randomPosition.Next(0, graphics.PreferredBackBufferWidth);
            //    targetPosition.Y = randomPosition.Next(0, graphics.PreferredBackBufferHeight);
            //}
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

            GameStateManager.Draw(Globals.SpriteBatch);


            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);


            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public int WindowWidth()
        {
            return graphics.PreferredBackBufferWidth;
        }
        public int WindowHeight()
        {
            return graphics.PreferredBackBufferHeight;
        }
    }

}
