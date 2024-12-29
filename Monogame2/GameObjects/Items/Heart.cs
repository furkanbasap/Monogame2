using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Interfaces;
using Monogame2.Managers;
using Monogame2.Strategy.Movement;

namespace Monogame2.GameObjects.Objects
{
    public class Heart
    {
        private Vector2 _posHeart;
        private Vector2 _sizeHeart;


        Texture2D spritesheetHeart;

        AnimationManager amHeart;

        private IMovementStrategy _movementStrategy;

        public Rectangle Rect => new Rectangle((int)_posHeart.X, (int)_posHeart.Y, (int)_sizeHeart.X, (int)_sizeHeart.Y);



        public Heart(Vector2 position, Vector2 size)
        {
            _posHeart = position;
            _sizeHeart = size;
            _movementStrategy = new LinearMovementStrategy();
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
            _movementStrategy.Move(ref _posHeart);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                spritesheetHeart,
                this.Rect,
                amHeart.GetFrame(),
                Color.White);

        }

        public void SetMovementStrategy(IMovementStrategy movementStrategy)
        {
            _movementStrategy = movementStrategy;
        }
    }
}
