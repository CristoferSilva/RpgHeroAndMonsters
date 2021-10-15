using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioRPG.GameElements
{
    public class Portion : IGameElement
    {
        private int _hp;
        private int _positionX;
        private int _positionY;

        public Portion(int positionX, int positionY, int hp)
        {
            _hp = hp;
            _positionX = positionX;
            _positionY = positionY;
        }
        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        public int Hp 
        { 
            get { return _hp; } 
            set { _hp = value; } 
        }

        public override string ToString()
        {
            return "P";
        }
    }
}