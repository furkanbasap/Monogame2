using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.GameObjects.Objects
{
    public class Heart
    {
        private Vector2 _posHeart;
        private Vector2 _sizeHeart;


        Texture2D spritesheetHeart;

        AnimationManager amHeart;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posHeart.X, (int)_posHeart.Y, (int)_sizeHeart.X, (int)_sizeHeart.Y);
            }
        }


        public Heart(Vector2 position, Vector2 size)
        {
            _posHeart = position;
            _sizeHeart = size;
        }
        public void LoadContent()
        {
            spritesheetHeart = Globals.Content.Load<Texture2D>("Objects/heart");

            //Number of frames, number of collimates, outline of sprite
            amHeart = new(11, 11, new Vector2(spritesheetHeart.Width / 11, spritesheetHeart.Height));

        }

        public void Update()
        {

            amHeart.Update();

            //BEWEGEN VAN DE HEART
            _posHeart.X -= 2.5f;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                spritesheetHeart,
                new Rectangle((int)_posHeart.X, (int)_posHeart.Y, (int)_sizeHeart.X, (int)_sizeHeart.Y),
                amHeart.GetFrame(),
                Color.White);

        }
    }
}
