using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Monogame2.Scenes;

namespace Monogame2.Utils
{
    public static class GameStateManager
    {
        // Huidige actieve scène
        private static GameScreen currentScreen;

        /// <summary>
        /// Verandert de huidige scène naar een nieuwe scène.
        /// </summary>
        /// <param name="newScreen">De nieuwe GameScene die moet worden ingesteld.</param>
        public static void ChangeState(GameScreen newScreen)
        {
            // Huidige scène opruimen als dat nodig is
            currentScreen?.UnloadContent();

            // Stel de nieuwe scène in
            currentScreen = newScreen;

            // Laad content voor de nieuwe scène
            currentScreen?.LoadContent();
        }

        /// <summary>
        /// Roept de Update-methode aan voor de huidige scène.
        /// </summary>
        /// <param name="gameTime">GameTime-object voor timing.</param>
        public static void Update(GameTime gameTime)
        {
            currentScreen?.Update(gameTime);
        }

        /// <summary>
        /// Roept de Draw-methode aan voor de huidige scène.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch voor het tekenen van sprites.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            currentScreen?.Draw();
        }
    }
}

