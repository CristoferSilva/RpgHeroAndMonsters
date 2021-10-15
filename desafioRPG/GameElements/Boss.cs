using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioRPG.GameElements
{
    public class Boss : ICharacter
    {
        private int _life;
        private int _attack;
        private int _positionX;
        private int _positionY;
        private bool _fixedOnTheMap;
        private const int _addScore = 15;

        public Boss(int positionX, int positionY, int life, int attack)
        {
            _life = life;
            _attack = attack;
            _positionX = positionX;
            _positionY = positionY;
            _fixedOnTheMap = false;
        }
        public int AddScore { get { return _addScore; } }
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
        public bool FixedOnTheMap
        {
            get { return _fixedOnTheMap; }
            set { _fixedOnTheMap = value; }
        }
        public void Damage(int damage)
        {
            Life -= damage;
        }
        public override string ToString()
        {
            return "B";
        }
        public bool ValidatePositionBoss(IGameElement[,] grid, int bossPositioX, int bossPositioY)
        {
            if (bossPositioX >= 0 && bossPositioX <= 19 && bossPositioY >= 0 && bossPositioY <= 19)
            {
                if (grid[bossPositioX, bossPositioY] == null)
                {
                    return true;
                }
            }
            
            return false;
        }
        public void MoveBoss(IGameElement[,] grid)
        {
            int op;
            bool valid;
            Random buildOp = new Random();

            int bossPositionX = PositionX;
            int bossPositionY = PositionY;

            do
            {
                op = buildOp.Next(1, 5);
                valid = false;

                switch (op)
                {
                    case 1:
                        bossPositionY--;
                        valid = ValidatePositionBoss(grid, bossPositionX, bossPositionY);
                        if (valid)
                        {
                            PositionY = bossPositionY;
                        }
                        break;
                    case 2:
                        bossPositionY++;
                        valid = ValidatePositionBoss(grid, bossPositionX, bossPositionY);
                        if (valid)
                        {
                            PositionY = bossPositionY;
                        }
                        break;
                    case 3:
                        bossPositionX--;
                        valid = ValidatePositionBoss(grid, bossPositionX, bossPositionY);
                        if (valid)
                        {
                            PositionX = bossPositionX;
                        }
                        break;
                    case 4:
                        bossPositionX++;
                        valid = ValidatePositionBoss(grid, bossPositionX, bossPositionY);
                        if (valid)
                        {
                            PositionX = bossPositionX;
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
