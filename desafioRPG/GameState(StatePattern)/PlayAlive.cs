using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desafioRPG.GameElements;
using desafioRPG.GameStructure;

namespace desafioRPG.GameState_StatePattern_
{
    public class PlayAlive : IGameState
    {
        private Game _game;
        private string _moveHeroString;
        private List<Monster> _fixedMonsters;
        private List<string> _stringMonsterAttack;

        public PlayAlive(Game game)
        {
            _game = game;
            _fixedMonsters = new List<Monster>();
            _stringMonsterAttack = new List<string>();
            _moveHeroString = "";
        }

        public void Run()
        {
            Action();
        }
        public void Action()
        {
            bool run = true;
            ConsoleKey action;
            IGameElement[,] grid;
            _game.Notify();

            do
            {
                int heroPositionX = _game.Hero.PositionX;
                int heroPositionY = _game.Hero.PositionY;
                grid = _game.Map.Grid;

                action = Console.ReadKey().Key;
                Console.Clear();

                switch (action)
                {
                    case ConsoleKey.A:
                        heroPositionY--;
                        _moveHeroString = _game.Hero.MoveHero(grid, heroPositionX, heroPositionY, _game);
                        MoveAllMonsters(grid, _game.MonstersList, _game.Boss);

                        ConsoleWriteAction();
                        break;

                    case ConsoleKey.D:
                        heroPositionY++;
                        _moveHeroString = _game.Hero.MoveHero(grid, heroPositionX, heroPositionY, _game);
                        MoveAllMonsters(grid, _game.MonstersList, _game.Boss);

                        ConsoleWriteAction();
                        break;

                    case ConsoleKey.W:
                        heroPositionX--;
                        _moveHeroString = _game.Hero.MoveHero(grid, heroPositionX, heroPositionY, _game);
                        MoveAllMonsters(grid, _game.MonstersList, _game.Boss);

                        ConsoleWriteAction();
                        break;

                    case ConsoleKey.S:
                        heroPositionX++;
                        _moveHeroString = _game.Hero.MoveHero(grid, heroPositionX, heroPositionY, _game);
                        MoveAllMonsters(grid, _game.MonstersList, _game.Boss);

                        ConsoleWriteAction();
                        break;

                    case ConsoleKey.Spacebar:
                        string heroAttack = HeroAttack(heroPositionX, heroPositionY, grid); ;
                        MoveAllMonsters(grid, _game.MonstersList, _game.Boss);

                        ConsoleWriteAction();
                        Console.WriteLine(heroAttack);
                        break;

                    case ConsoleKey.Escape:
                        heroPositionX = -7;
                        heroPositionY = -7;
                        break;

                    default:
                        _game.Notify();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("INVALID POSITION!!!! TYPE  AGAIN:");
                        break;
                }

                run = CheckGameState(heroPositionX, heroPositionY);
                UnpinMonstersAndBoss();
                _stringMonsterAttack = new List<string>();
                _moveHeroString = "";
            } while (run);
        }
        public bool CheckGameState(int heroPositionX, int heroPositionY)
        {
            if (heroPositionX == 19 && heroPositionY == 19)
            {
                EndGameWin();
                return false;
            }
            else if (_game.Hero.Life <= 0)
            {
                EndGameHeroDead();
                return false;
            }
            else if (heroPositionX == -7 && heroPositionY == -7)
            {
                EndGameESC();
                return false;
            }
            return true;
        }
        public void EndGameESC()
        {
            _game.GameState = new ExitGame(_game);

        }
        public void EndGameWin()
        {
            _game.GameState = new EndGameWin(_game);

        }
        public void EndGameHeroDead()
        {
            _game.GameState = new EndGameHeroDead(_game);

        }
        public void ConsoleWriteAction()
        {
            _game.Notify();
            Console.WriteLine(_moveHeroString);
            if (_stringMonsterAttack.Count > 0)
            {
                _stringMonsterAttack.ForEach(delegate (String s) { Console.WriteLine(s); });
            }

        }

        public void UnpinMonstersAndBoss()
        {
            if (_fixedMonsters.Count > 0)
            {
                foreach (Monster m in _fixedMonsters)
                {
                    m.FixedOnTheMap = false;

                }
                _fixedMonsters = new List<Monster>();
            }
            if (_game.Boss != null)
            {
                if (_game.Boss.FixedOnTheMap == true)
                {
                    _game.Boss.FixedOnTheMap = false;
                }
            }
        }
        private void MoveAllMonsters(IGameElement[,] grid, List<Monster> monsters, Boss boss)
        {
            foreach (Monster m in monsters)
            {
                if (m.FixedOnTheMap == false)
                {
                    m.MoveMonster(grid);
                }
                MonsterAttack(m, grid);
            }
            if (boss != null)
            {
                if (boss.FixedOnTheMap == false && boss.Life > 0)
                {
                    boss.MoveBoss(grid);
                }
                BossAttack(boss, grid);
            }
            
        }
        public string PerformAttackOnMonsterAndBoss(ICharacter monsterOrBossAttacked)
        {
            if (monsterOrBossAttacked is Monster)
            {
                Monster monsterAttacked = (Monster)monsterOrBossAttacked;
                monsterAttacked.FixedOnTheMap = true;
                _fixedMonsters.Add(monsterAttacked);
                monsterAttacked.Damage(_game.Hero.Attack);

                if (monsterAttacked.Life <= 0)
                {
                    _game.Hero.Score += monsterAttacked.AddScore;
                    _game.MonstersList.Remove(monsterAttacked);
                    _game.Boss.FixedOnTheMap = false;
                }
                return ConsoleWriteHeroAttack(monsterAttacked);
            }
            else
            {
                Boss boss = (Boss)monsterOrBossAttacked;
                boss.FixedOnTheMap = true;
                boss.Damage(_game.Hero.Attack);
                if (boss.Life <= 0)
                {
                    boss.FixedOnTheMap = false;
                    _game.Hero.Score += boss.AddScore;
                    _game.Boss = null;
                   // _game.Map.Grid[_game.Boss.PositionX, _game.Boss.PositionY] = null;
                }
                return ConsoleWriteHeroAttack(boss);
            }

        }

        public string HeroAttack(int heroPositionX, int heroPositionY, IGameElement[,] grid)
        {
            if (heroPositionX - 1 >= 0)
            {
                if (grid[heroPositionX - 1, heroPositionY] is Monster)
                {
                    return PerformAttackOnMonsterAndBoss((Monster)grid[heroPositionX - 1, heroPositionY]);


                }
            }
            if (heroPositionX + 1 <= 19)
            {
                if (grid[heroPositionX + 1, heroPositionY] is Monster)
                {
                    return PerformAttackOnMonsterAndBoss((Monster)grid[heroPositionX + 1, heroPositionY]);

                }
            }
            if (heroPositionY + 1 <= 19)
            {
                if (grid[heroPositionX, heroPositionY + 1] is Monster)
                {
                    return PerformAttackOnMonsterAndBoss((Monster)grid[heroPositionX, heroPositionY + 1]);

                }
            }
            if (heroPositionY - 1 >= 0)
            {
                if (grid[heroPositionX, heroPositionY - 1] is Monster)
                {
                    return PerformAttackOnMonsterAndBoss((Monster)grid[heroPositionX, heroPositionY - 1]);

                }

            }
            //Boss
            if (heroPositionX - 1 >= 0)
            {
                if (grid[heroPositionX - 1, heroPositionY] is Boss)
                {
                    return PerformAttackOnMonsterAndBoss((Boss)grid[heroPositionX - 1, heroPositionY]);

                }
            }
            if (heroPositionX + 1 <= 19)
            {
                if (grid[heroPositionX + 1, heroPositionY] is Boss)
                {
                    return PerformAttackOnMonsterAndBoss((Boss)grid[heroPositionX + 1, heroPositionY]);

                }
            }
            if (heroPositionY + 1 <= 19)
            {
                if (grid[heroPositionX, heroPositionY + 1] is Boss)
                {
                    return PerformAttackOnMonsterAndBoss((Boss)grid[heroPositionX, heroPositionY + 1]);
                }
            }
            if (heroPositionY - 1 >= 0)
            {
                if (grid[heroPositionX, heroPositionY - 1] is Boss)
                {
                    return PerformAttackOnMonsterAndBoss((Boss)grid[heroPositionX, heroPositionY - 1]);
                }
            }

            return "There is nothing to attack.";
        }
        public void MonsterAttack(Monster monster, IGameElement[,] grid)
        {
            if (monster.PositionX - 1 >= 0)
            {
                if (grid[monster.PositionX - 1, monster.PositionY] is Hero)
                {


                    _game.Hero.Damage(monster.Attack);
                    ConsoleWriteLineMonsterBossAttack(monster);
                }
            }
            if (monster.PositionX + 1 <= 19)
            {
                if (grid[monster.PositionX + 1, monster.PositionY] is Hero)
                {

                    _game.Hero.Damage(monster.Attack);
                    ConsoleWriteLineMonsterBossAttack(monster);
                }

            }
            if (monster.PositionY + 1 <= 19)
            {
                if (grid[monster.PositionX, monster.PositionY + 1] is Hero)
                {

                    _game.Hero.Damage(monster.Attack);
                    ConsoleWriteLineMonsterBossAttack(monster);
                }
            }
            if (monster.PositionY - 1 >= 0)
            {
                if (grid[monster.PositionX, monster.PositionY - 1] is Hero)
                {

                    _game.Hero.Damage(monster.Attack);
                    ConsoleWriteLineMonsterBossAttack(monster);
                }
            }
        }
        public void BossAttack(Boss boss, IGameElement[,] grid)
        {
            if (boss.PositionX - 1 >= 0)
            {
                if (grid[boss.PositionX - 1, boss.PositionY] is Hero)
                {
                    Hero h = (Hero)grid[boss.PositionX - 1, boss.PositionY];
                    h.Damage(boss.Attack);
                    ConsoleWriteLineMonsterBossAttack(boss);
                }
            }
            if (boss.PositionX + 1 <= 19)
            {
                if (grid[boss.PositionX + 1, boss.PositionY] is Hero)
                {
                    Hero h = (Hero)grid[boss.PositionX + 1, boss.PositionY];
                    h.Damage(boss.Attack);
                    ConsoleWriteLineMonsterBossAttack(boss);
                }

            }
            if (boss.PositionY + 1 <= 19)
            {
                if (grid[boss.PositionX, boss.PositionY + 1] is Hero)
                {
                    Hero h = (Hero)grid[boss.PositionX, boss.PositionY + 1];
                    h.Damage(boss.Attack);
                    ConsoleWriteLineMonsterBossAttack(boss);
                }
            }
            if (boss.PositionY - 1 >= 0)
            {
                if (grid[boss.PositionX, boss.PositionY - 1] is Hero)
                {
                    Hero h = (Hero)grid[boss.PositionX, boss.PositionY - 1];
                    h.Damage(boss.Attack);
                    ConsoleWriteLineMonsterBossAttack(boss);
                }
            }
        }
        public void ConsoleWriteLineMonsterBossAttack(ICharacter monsterOrBoss)
        {
            String monsterAttackString;
            if (monsterOrBoss is Monster)
            {
                Monster monster = (Monster)monsterOrBoss;
                monsterAttackString = $"You took {monster.Attack} from Monster!";
                _stringMonsterAttack.Add(monsterAttackString);

            }
            else
            {
                Boss boss = (Boss)monsterOrBoss;
                monsterAttackString = $"You took {boss.Attack} from Boss!";
                _stringMonsterAttack.Add(monsterAttackString);
            }

        }
        public string ConsoleWriteHeroAttack(ICharacter monsterOrBossAttacked)
        {
            string sMonster = monsterOrBossAttacked.ToString().Equals("M") ? "Monster" : "Boss";
            return $"You caused {_game.Hero.Attack} damage to {sMonster}.";
        }
    }
}