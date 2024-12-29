using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Managers;
using System.Collections.Generic;
using System;
using Monogame2.Interfaces;
using Monogame2.Strategy.Movement;



namespace Monogame2.GameObjects.Enemies
{
    public class Bomb
    {
        //STATEGY

        private Vector2 _posEnemy;
        private Vector2 _sizeEnemy;
        private Texture2D _textureBomb;
        private IMovementStrategyBomb _movementStrategy;

        public Rectangle Rect => new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y);


        public Bomb(Vector2 position, Vector2 size)
        {
            _posEnemy = position;
            _sizeEnemy = size;
            _movementStrategy = new DirectMovementStrategy();
        }

        public void LoadContent()
        {
            _textureBomb = Globals.Content.Load<Texture2D>("Objects/bomb");
        }

        public void Update(Vector2 _posPlayer)
        {
            _movementStrategy.Move(ref _posEnemy, _posPlayer);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                _textureBomb,
                new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y),
                new Rectangle(0, 0, _textureBomb.Width, _textureBomb.Height),
                Color.White,
                0,
                new Vector2(-_textureBomb.Width / 2, -_textureBomb.Height / 2),
                SpriteEffects.None,
                0f);
        }

        public void SetMovementStrategy(IMovementStrategyBomb strategy)
        {
            _movementStrategy = strategy;
        }


    }
}