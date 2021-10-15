using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desafioRPG.GameStructure;

namespace desafioRPG.GameState_StatePattern_
{
    public interface IGameState
    {
        public void Run();
    }
}
