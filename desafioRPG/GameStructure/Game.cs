using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desafioRPG.GameElements;
using desafioRPG.IObserverPattern;
using desafioRPG.GameState_StatePattern_;

namespace desafioRPG.GameStructure
{
    public class Game : ISubject
    {
        private Map _map;
        private Hero _hero;
        private Boss _boss;
        private IObserver _observer;
        private IGameState _gameState;
        private List<Monster> _monsters;
        private List<IGameElement> _gameElements;
        

        public Game()
        {
            BuildBoss(10, 2);
            _hero = new Hero(0, 0, 100, 1);
            _monsters = new List<Monster>();
            _gameElements = new List<IGameElement>();
            _map = new Map(_hero ,_boss, _monsters, _gameElements);
            _gameState = new PlayAlive(this);

            BuildArmsPortionsDoor();
            BuildMonsters(6, 5, 1);
            Attach(_map);
        }
        public void Run()
        {
            _gameState.Run();
        }
        public void BuildBoss(int life, int attack)
        {
            int randomPositionX;
            int randomPositionY;
            Random buildPositionX = new Random();
            Random buildPositionY = new Random();

            randomPositionX = buildPositionX.Next(2, 18);
            randomPositionY = buildPositionY.Next(2, 18);

           _boss = new Boss(randomPositionX, randomPositionY, life, attack);
        }
        public void BuildMonsters(int elementAmount, int life, int attack)
        {
            int randomPositionX;
            int randomPositionY;
            Random buildPositionX = new Random();
            Random buildPositionY = new Random();

            for (int i = 0; i < elementAmount; i++)
            {
                do
                {
                    randomPositionX = buildPositionX.Next(2, 18);
                    randomPositionY = buildPositionY.Next(2, 18);

                } while (_map.Grid[randomPositionX, randomPositionY] != null);

                _monsters.Add(new Monster(randomPositionX, randomPositionY, life, attack));
            }
        }
        
        public void BuildArmsPortionsDoor()
        {
            _gameElements.Add(new Portion(17, 17, 6));
            _gameElements.Add(new Portion(16, 16, 6));
            _gameElements.Add(new Portion(15, 15, 6));
            _gameElements.Add(new Portion(14, 14, 6));
            _gameElements.Add(new Portion(10, 10, 6));
            _gameElements.Add(new Portion(7, 7, 6));
            _gameElements.Add(new Portion(3, 3, 6));
            _gameElements.Add(new Portion(9, 9, 6));
            
            _gameElements.Add(new Arm(7, 2, 1));

            _gameElements.Add(new Door(19, 19));
        }
        public Map Map 
        {
            get { return _map; }
            set { _map = value; }
        }
        public Hero Hero 
        {
            get { return _hero; }
            set { _hero = value; }
        }
        public Boss Boss { 
            get { return _boss; }
            set { _boss = value; }
        }
        public IGameState GameState 
        { 
            get { return _gameState; }
            set { 
                _gameState = value;
                Run();
            }
        }

        public List<Monster> MonstersList 
        { 
            get { return _monsters; }
        }
        public List<IGameElement> GameElements 
        { 
            get { return _gameElements; }
        }
        public void Attach(IObserver observer)
        {
            _observer = observer;
        }

        public void Detach(IObserver observer)
        {
            throw new InvalidOperationException("The game must have the map as an observer");
        }

        public void Notify()
        {
            _observer.Update();
        }
    }
}
