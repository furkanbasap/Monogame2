using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Animation;
using Monogame2.Global;
using Monogame2.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.GameObjects
{
    public class Coin
    {
        private Texture2D _coinTexture;
        private Vector2 _posCoin;


        Texture2D spritesheetCoin;

        AnimationManager amCoin;

        public Coin()
        {

        }
        public Coin(Vector2 pos)
        {
            _posCoin = pos;
        }
        public void LoadContent()
        {
            spritesheetCoin = Globals.content.Load<Texture2D>("Objects/coin3");

            amCoin = new(8, 8, new Vector2(100, 150));
        }

        public void Update()
        {
            amCoin.Update();
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(
                            spritesheetCoin,
                            new Rectangle((int)_posCoin.X, (int)_posCoin.Y, 200, 200),
                            amCoin.GetFrame(),
                            Color.White);
        }
    }
}

