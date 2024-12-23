using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame2.Animation;
using Monogame2.Global;
using Monogame2.Input;

namespace Monogame2.GameObjects
{
    public class Player
    {
        private Texture2D _playerTexture;
        private Vector2 _posPlayer;

        Texture2D spritesheetPlayer;
        AnimationManager amPlayer;

        public Player()
        {

        }

        public void LoadContent()
        {
            spritesheetPlayer = Globals.content.Load<Texture2D>("Actors/Hero");

            _posPlayer = new Vector2(100, 100);
            amPlayer = new(8, 8, new Vector2(82, 100));

        }

        public void Update()
        {
            amPlayer.Update();

            var keyboardState = Keyboard.GetState();
            if (keyboardState.GetPressedKeyCount() > 0)
            {
                if (keyboardState.IsKeyDown(Keys.Q) || keyboardState.IsKeyDown(Keys.Left)) _posPlayer.X += -3f;
                if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right)) _posPlayer.X += +3f;
                if (keyboardState.IsKeyDown(Keys.Z) || keyboardState.IsKeyDown(Keys.Up)) _posPlayer.Y += -3f;
                if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down)) _posPlayer.Y += +3f;
                

            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(
                            spritesheetPlayer,
                            new Rectangle((int)_posPlayer.X, (int)_posPlayer.Y, 200, 200),
                            amPlayer.GetFrame(),
                            Color.White);
        }
    }
}
