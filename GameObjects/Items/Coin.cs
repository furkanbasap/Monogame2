using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Managers;



namespace Monogame2.GameObjects.Objects
{
    public class Coin
    {
        private Vector2 _posCoin;
        private Vector2 _sizeCoin;


        Texture2D spritesheetCoin;

        AnimationManager amCoin;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posCoin.X, (int)_posCoin.Y, (int)_sizeCoin.X, (int)_sizeCoin.Y);
            }
        }


        public Coin(Vector2 position, Vector2 size) 
        {
            _posCoin = position;
            _sizeCoin = size;
        }
        public void LoadContent()
        {
            spritesheetCoin = Globals.Content.Load<Texture2D>("Objects/coin6");

            //Number of frames, number of collimates, outline of sprite
            amCoin = new(13, 13, new Vector2(spritesheetCoin.Width / 13, spritesheetCoin.Height));

        }

        public void Update()
        {

            amCoin.Update();

            //BEWEGEN VAN DE COINS
            _posCoin.X -= 1.5f;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                spritesheetCoin,
                new Rectangle((int)_posCoin.X, (int)_posCoin.Y, (int)_sizeCoin.X, (int)_sizeCoin.Y),
                amCoin.GetFrame(),
                Color.White);

        }
    }
}

