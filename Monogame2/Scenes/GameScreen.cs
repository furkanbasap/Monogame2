using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.Scenes
{
    public abstract class GameScreen
    {
        /// <summary>
        /// Method to load content such as fonts, sprites, etc.
        /// Is implemented specifically by each scene.
        /// </summary>
        /// <param name="content">The ContentManager used to load assets.</param>
        public abstract void LoadContent();

        /// <summary>
        /// Update logic for the scene. Called every frame.
        /// </summary>
        /// <param name="gameTime">GameTime object for timing.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draw logic for the scene. Called every frame.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch object to draw sprites.</param>
        public abstract void Draw();

        /// <summary>
        /// Optional: Method to clean up the scene when it closes.
        /// </summary>
        public virtual void UnloadContent()
        {
            // Can be overwritten by specific scenes if needed
        }


    }
}

