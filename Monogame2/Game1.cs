﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Monogame2.Managers;
using Monogame2.Scenes;

namespace Monogame2
{
    public class Game1 : Game
    {

        public static ContentManager ContentManager;
        private GraphicsDeviceManager graphics;
        bool play;
        Song song;
        private KeyboardState currentKeyboardState, previousKeyboardState;    

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
            Globals.ScreenSize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
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

            if ((IsKeyPressed(Keys.M) && play == true))
            {
                MediaPlayer.Stop();
                play = false;
            }
            else if ((IsKeyPressed(Keys.M) && play == false))
            {
                MediaPlayer.Play(song);
                play = true;
            }
            previousKeyboardState = currentKeyboardState;

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
