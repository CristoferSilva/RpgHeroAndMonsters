using desafioRPG.GameStructure;
using Figgle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioRPG.GameState_StatePattern_
{
    public class ExitGame : IGameState
    {
        private Game _game;
        public ExitGame(Game game)
        {
            _game = game;
        }
        public void Run()
        {
            Console.WriteLine("a");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(FiggleFonts.Slant.Render("\n\n\n\nThe End"));
            Console.WriteLine("\n-> You exit the game!!!!!\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\n-> By: Cristofer Silva and Matheus Coelho\n\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("press any key to exit the prompt!");
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
