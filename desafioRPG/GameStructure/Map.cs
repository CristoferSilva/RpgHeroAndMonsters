using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desafioRPG.GameElements;
using desafioRPG.IObserverPattern;

namespace desafioRPG.GameStructure
{
    public class Map : IObserver
    {
        private Hero _hero;
        private Boss _boss;
        private IGameElement[,] _grid;
        private List<Monster> _monsters;
        private List<IGameElement> _elements;

        public Map(Hero hero, Boss boss, List<Monster> monsters, List<IGameElement> elements)
        {
            _hero = hero;
            _boss = boss;
            _monsters = monsters;
            _elements = elements;
            _grid = new IGameElement[20, 20];
        }
        public void Update()
        {
            IGameElement[,] map = new IGameElement[20, 20];
            int positionX;
            int positionY;

            foreach (IGameElement e in _elements)
            {
                positionX = e.PositionX;
                positionY = e.PositionY;

                map[positionX, positionY] = e;
            }
            foreach (Monster m in _monsters)
            {
                positionX = m.PositionX;
                positionY = m.PositionY;

                map[positionX, positionY] = m;
            }
            map[_hero.PositionX, _hero.PositionY] = _hero;

            if (_boss != null)
            {
                map[_boss.PositionX, _boss.PositionY] = _boss;
            }
            

            _grid = map;
            ShowMap();
        }

        public void ShowMap()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=========================================");
            Console.WriteLine($"Hero HP: {_hero.Life} Hero Damage: { _hero.Attack} Hero Score: {_hero.Score}");
            Console.WriteLine("=========================================\n");

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (_grid[i, j] == null)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("O ");
                    }
                    else
                    {
                        if (_grid[i, j] is Portion)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(_grid[i, j] + " ");
                        }
                        else if (_grid[i, j] is Monster)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(_grid[i, j] + " ");
                        }
                        else if (_grid[i, j] is Boss)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(_grid[i, j] + " ");
                        }
                        else if (_grid[i, j] is Hero)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(_grid[i, j] + " ");
                        }
                        else if (_grid[i, j] is Arm)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(_grid[i, j] + " ");
                        }
                        else if (_grid[i, j] is Door)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(_grid[i, j] + " ");
                        }

                    }
                }
                Console.WriteLine("");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=========================================");
            Console.WriteLine("  [A] to move left.   [D] to move rigth.");
            Console.WriteLine("  [W] to move up.     [S] to move down.");
            Console.WriteLine("  [SPACE] to attack.  [ESC] to exit.");
            Console.WriteLine("=========================================");
        }
        public IGameElement[,] Grid
        {
            get { return _grid; }
            set { _grid = value; }
        }

        public List<Monster> Monsters
        {
            get { return _monsters; }
            set { _monsters = value; }
        }
    }
}