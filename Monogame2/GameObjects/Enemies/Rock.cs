﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Interfaces;
using Monogame2.Managers;
using Monogame2.Strategy.Movement;

namespace Monogame2.GameObjects.Enemies
{
    public class Rock
    {
        // STRATEGY

        public Vector2 _posEnemy;
        private Vector2 _sizeEnemy;

        public Texture2D textureRock;
        private float rotation;
        private float seconds;
        private IMovementStrategy _movementStrategy;

        public Rectangle Rect => new Rectangle((int)_posEnemy.X, (int)_posEnemy.Y, (int)_sizeEnemy.X, (int)_sizeEnemy.Y);


        public Rock(Vector2 position, Vector2 size)
        {
            _posEnemy = position;
            _sizeEnemy = size;
            rotation = 0;
            _movementStrategy = new HorizontalMovementStrategy();
        }
        public void LoadContent()
        {
            textureRock = Globals.Content.Load<Texture2D>("Objects/rock");

        }

        public void Update()
        {
            _movementStrategy.Move(ref _posEnemy);

            if (seconds % 3 == 0)
            {
                rotation += 0.04f;
            }
            seconds++;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(
                textureRock,
                this.Rect,
                new Rectangle(0, 0, textureRock.Width, textureRock.Height),
                Color.White,
                rotation,
                new Vector2(textureRock.Width / 2, textureRock.Height / 2),
                SpriteEffects.None,
                0f);

        }

        public void SetMovementStrategy(IMovementStrategy strategy)
        {
            _movementStrategy = strategy;
        }



    }
}
