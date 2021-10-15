using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioRPG.GameElements
{
    public interface IGameElement
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
