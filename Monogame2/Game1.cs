﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame2.GameObjects;
using Monogame2.Global;
using Monogame2.Physics;
using Monogame2.Scenes;
using Monogame2.Utils;
using System;
using System.Threading.Tasks.Sources;

namespace Monogame2
{
    public class Game1 : Game
    {

        public static ContentManager ContentManager;
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
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            // Start met het StartScreen
            GameStateManager.ChangeState(new StartScreen());
            _camera = new Camera();


        }

        protected override void Update(GameTime gameTime)
        {
            // Verwerk de logica van de actieve scène
            GameStateManager.Update(gameTime);

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

            GameStateManager.Draw(spriteBatch);

            //spriteBatch.Begin();

            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);



            Globals.spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
