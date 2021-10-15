using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioRPG.GameElements
{
    public interface ICharacter : IGameElement
    {
        public int Life { get; set; }
        public int Attack { get; set; }
        public void Damage(int dano);

    }
}
