using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame2.Animation;
using Monogame2.Global;
using Monogame2.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.GameObjects
{
    public class Coin
    {
        private static Texture2D _texture;

        private Vector2 _position;

        private readonly Animations _anim;

        public Coin(Vector2 pos)
        {
            //_texture ??= Globals.Content.Load<Texture2D>("/Sprites/Player");
            //_anim = new(_texture, 16, 1, 0.1f);
            //_position = pos;
        }
        public void Update()
        {
            //_anim.Update();
            //InputManager.Update();

        }

        public void Draw()
        {
            //_anim.Draw(_position);
        }
    }
}

