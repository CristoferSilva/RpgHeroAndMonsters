using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioRPG.GameElements
{
    public class Arm : IGameElement
    {
        private int _addAttack;
        private int _positionX;
        private int _positionY;

        public Arm(int positionX, int positionY, int addAttack)
        {
            _positionX = positionX;
            _positionY = positionY;
            _addAttack = addAttack;
        }
        public int PositionX {
            get { return _positionX; }
            set { _positionX = value; }
        }

        public int PositionY {
            get { return _positionY; }
            set { _positionY = value; }
        }

        public int AddAttack 
        { 
            get { return _addAttack; } 
            set { _addAttack = value; }
        }

        public override string ToString()
        {
            return "W";
        }
    }
}