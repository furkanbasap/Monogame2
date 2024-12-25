using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Animation;
using Monogame2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.GameObjects
{
    public class Enemy3
    {
        private Vector2 _posEnemy;
        private Vector2 _sizeEnemy;


        Texture2D spritesheetEnemy;

        AnimationManager amCoin;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y);
            }
        }

        public Enemy3(Texture2D texture, Vector2 position, Vector2 size) 
        {
            _posEnemy = position;
            _sizeEnemy = size;
        }
        public void LoadContent()
        {
            spritesheetEnemy = Globals.Content.Load<Texture2D>("Objects/coin3");

            //Number of frames, number of collimates, outline of sprite
            amCoin = new(8, 8, new Vector2(spritesheetEnemy.Width / 8, 150));

        }

        public void Update()
        {

            amCoin.Update();

            // BEWEGEN VAN DE COINS
            //_posCoin.X -= 1f;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                spritesheetEnemy,
                new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y),
                amCoin.GetFrame(),
                Color.White);

        }
    }
}