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
        /// Methode om content zoals fonts, sprites, etc. te laden.
        /// Wordt door elke scene specifiek geïmplementeerd.
        /// </summary>
        /// <param name="content">De ContentManager die gebruikt wordt om assets te laden.</param>
        public abstract void LoadContent(ContentManager content);

        /// <summary>
        /// Update logica voor de scene. Wordt elke frame aangeroepen.
        /// </summary>
        /// <param name="gameTime">GameTime object voor timing.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Teken logica voor de scene. Wordt elke frame aangeroepen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch object om sprites te tekenen.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Optioneel: Methode om de scene op te ruimen als deze wordt afgesloten.
        /// </summary>
        public virtual void UnloadContent()
        {
            // Kan worden overschreven door specifieke scenes indien nodig
        }


    }
}

