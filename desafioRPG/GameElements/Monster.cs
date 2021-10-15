using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioRPG.GameElements
{
    public class Monster : ICharacter
    {
        private int _life;
        private int _attack;
        private int _positionX;
        private int _positionY;
        private bool _fixedOnTheMap;
        private const int _addScore = 5;

        public Monster(int positionX, int positionY, int life, int attack)
        {
            _life = life;
            _attack = attack;
            _positionX = positionX;
            _positionY = positionY;
            _fixedOnTheMap = false;
        }
        public int AddScore { get { return _addScore; }}
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
        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }
        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }
        public bool FixedOnTheMap { 
            get { return _fixedOnTheMap; }
            set { _fixedOnTheMap = value; }
        }

        public void Damage(int damage)
        {
            Life -= damage;
        }
        public override string ToString()
        {
            return "M";
        }
        public bool ValidatePositionMonster(IGameElement[,] grid, int monsterPositioX, int monsterPositioY)
        {
            if (monsterPositioX >= 0 && monsterPositioX <= 19 && monsterPositioY >= 0 && monsterPositioY <= 19)
            {
                if (grid[monsterPositioX, monsterPositioY] == null)
                {
                    return true;
                }

            }
            
            return false;
        }
        public void MoveMonster(IGameElement[,] grid)
        {
            int op;
            bool valid = true;
            Random buildOp = new Random();

            int monsterPositionX = PositionX;
            int monsterPositionY = PositionY;

            do
            {
                op = buildOp.Next(1, 5);

                switch (op)
                {
                    case 1:
                        monsterPositionY--;
                        valid = ValidatePositionMonster(grid, monsterPositionX, monsterPositionY);
                        if (valid)
                        {
                            grid[PositionX, PositionY] = null;
                            PositionY = monsterPositionY;
                        }
                        break;
                    case 2:
                        monsterPositionY++;
                        valid = ValidatePositionMonster(grid, monsterPositionX, monsterPositionY);
                        if (valid)
                        {
                            grid[PositionX, PositionY] = null;
                            PositionY = monsterPositionY;
                        }
                        break;
                    case 3:
                        monsterPositionX--;
                        valid = ValidatePositionMonster(grid, monsterPositionX, monsterPositionY);
                        if (valid)
                        {
                            grid[PositionX, PositionY] = null;
                            PositionX = monsterPositionX;
                        }
                        break;
                    case 4:
                        monsterPositionX++;
                        valid = ValidatePositionMonster(grid, monsterPositionX, monsterPositionY);
                        if (valid)
                        {
                            grid[PositionX, PositionY] = null;
                            PositionX = monsterPositionX;
                        }
                        break;
                    default:
                        break;
                }
                grid[PositionX, PositionY] = this;
            } while (!valid);
        }
    }
}
