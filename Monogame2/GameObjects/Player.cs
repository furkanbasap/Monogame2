using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Monogame2.Animation;
using Monogame2.Global;

namespace Monogame2.GameObjects
{
    public class Player : Sprite
    {
        private Texture2D _playerTexture;
        private Vector2 _posPlayer;
        private Vector2 _sizePlayer;

        Texture2D spritesheetPlayer;
        AnimationManager amPlayer;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)_posPlayer.X, (int)_posPlayer.Y, (int)_sizePlayer.X, (int)_sizePlayer.Y);
            }
        }

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _posPlayer = position;
        }
        public Player(Texture2D texture, Vector2 position, Vector2 size) : base(texture, position)
        {
            _posPlayer = position;
            _sizePlayer = size;
        }
        public void LoadContent()
        {
            spritesheetPlayer = Globals.content.Load<Texture2D>("Actors/Hero");

            amPlayer = new(8, 8, new Vector2(spritesheetPlayer.Width / 8, 100));

        }

        public void Update()
        {
            amPlayer.Update();

            var keyboardState = Keyboard.GetState();
            if (keyboardState.GetPressedKeyCount() > 0)
            {
                if (keyboardState.IsKeyDown(Keys.Q) || keyboardState.IsKeyDown(Keys.Left)) 
                {
                    if (_posPlayer.X <= 0)
                    {
                        _posPlayer.X += 0f;
                    }
                    else 
                        _posPlayer.X += -3f; 
                }
                if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                {
                    if (_posPlayer.X >= 1280 - _sizePlayer.X)
                    {
                        _posPlayer.X += 0f;
                    }
                    else
                        _posPlayer.X += +3f;
                }
                if (keyboardState.IsKeyDown(Keys.Z) || keyboardState.IsKeyDown(Keys.Up))
                {
                    if (_posPlayer.Y <= 0)
                    {
                        _posPlayer.Y += 0f;
                    }
                    else
                        _posPlayer.Y += -3f;
                }
                if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                {
                    if (_posPlayer.Y >= 741 - _sizePlayer.Y)
                    {
                        _posPlayer.Y += 0f;
                    }
                    else
                        _posPlayer.Y += +3f;
                }
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(
                            spritesheetPlayer,
                            new Rectangle((int)_posPlayer.X, (int)_posPlayer.Y, (int)_sizePlayer.X,(int)_sizePlayer.Y),
                            amPlayer.GetFrame(),
                            Color.White);
        }

        
    }
}
