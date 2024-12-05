using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame2.Scenes;
using Monogame2.Utils;

namespace Monogame2
{
    public class Game1 : Game
    {
        public static ContentManager ContentManager; // Publieke ContentManager voor resourcebeheer
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;


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
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Start met het StartScreen
            GameStateManager.ChangeState(new StartScreen());
        }

        protected override void Update(GameTime gameTime)
        {
            // Verwerk de logica van de actieve scène
            GameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Teken de actieve scène
            GameStateManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }

}
