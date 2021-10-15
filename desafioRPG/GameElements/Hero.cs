using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desafioRPG.GameStructure;

namespace desafioRPG.GameElements
{
    public class Hero : ICharacter
    {
        private int _life;
        private int _attack;
        private int _score;
        private int _positionX;
        private int _positionY;
        public Hero(int positionX, int positionY, int life, int attack)
        {
            _life = life;
            _score = life;
            _attack = attack;
            _positionX = positionX;
            _positionY = positionY;
        }
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        public int Life 
        { 
            get { return _life; }
            set { _life = value; } 
        }
        public int Attack 
        { 
            get { return _attack; } 
            set { _attack = value; }
        }
        public int PositionX { 
            get { return _positionX; } 
            set { _positionX = value;}
        }
        public int PositionY 
        { 
            get { return _positionY; }
            set { _positionY = value; }
        }

        public void Damage(int damage)
        {
            Life = (Life - damage) < 0 ? 0 : Life - damage;
            Score -= damage;
        }
        public void AddLife(int hp)
        {
            Life += hp;
        }
        public void AddAttack(int addAttack)
        {
            Attack += addAttack;
        }
        public override string ToString()
        {
            return "H";
        }
        public string MoveHero(IGameElement[,] grid, int heroPositioX, int heroPositioY, Game game)
        {
            bool valid = ValidatePositionHero(grid, heroPositioX, heroPositioY, game);

            if (valid)
            {
                Life--;
                grid[PositionX, PositionY] = null;
                PositionX = heroPositioX;
                PositionY = heroPositioY;
                grid[PositionX, PositionY] = this;
                Score--; 
                return $"You moved to [{PositionX},{PositionY}]";
            }
            return $"Invalid movement action.";
        }
        public bool ValidatePositionHero(IGameElement[,] grid, int heroPositioX, int heroPositioY, Game game)
        {
            if (heroPositioX >= 0 && heroPositioX <= 19 && heroPositioY >= 0 && heroPositioY <= 19)
            {
                if (grid[heroPositioX, heroPositioY] is Portion)
                {
                    Portion p = (Portion)grid[heroPositioX, heroPositioY];
                    AddLife(p.Hp);
                    Score += p.Hp;
                    game.GameElements.Remove(p);

                    return true;
                }
                else if (grid[heroPositioX, heroPositioY] is Arm)
                {
                    Arm a = (Arm)grid[heroPositioX, heroPositioY];
                    AddAttack(a.AddAttack);
                    game.GameElements.Remove(a);
    
                    return true;
                }
                else if (grid[heroPositioX, heroPositioY] is Door)
                {
                    return true;
                }
                else if (grid[heroPositioX, heroPositioY] == null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
