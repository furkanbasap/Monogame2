using Monogame2.GameObjects;
using Monogame2.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame2.Utils
{
    public class GameManager
    {
        //private Coin _coin;
        private Player _player;

        public void Init()
        {
            //_coin = new(new(300, 300));
            _player = new();
        }

        public void Update()
        {
            InputManager.Update();
            //_coin.Update();
            _player.Update();
        }

        public void Draw()
        {
            //_coin.Draw();
            _player.Draw();
        }
    }
}
