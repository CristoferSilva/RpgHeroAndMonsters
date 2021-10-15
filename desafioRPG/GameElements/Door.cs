using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioRPG.GameElements
{
    public class Door : IGameElement
    {
        private int _positionX;
        private int _positionY;

        public Door(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;
        }
        public int PositionX 
        {   get { return _positionX ; }
            set { _positionX = value; } 
        }
        public int PositionY 
        { 
            get { return _positionY; }
            set { _positionY = value; }
        }
        public override string ToString()
        {
            return "D";
        }
    }
}
