using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desafioRPG.GameStructure;
using Figgle;

namespace desafioRPG.GameState_StatePattern_
{
    public class EndGameWin : IGameState
    {
        private Game _game;
        public EndGameWin(Game game)
        {
            _game = game;
        }
        public void Run()
        {
            Console.WriteLine("\nCongratilations...\n");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(FiggleFonts.Slant.Render("\n\n\n\nYOU WIN!!!!\n"));
            Console.WriteLine($"->You Score: {_game.Hero.Score}!");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\n->By: Cristofer Silva and Matheus Coelho\n\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("press any key to exit the prompt!");
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
