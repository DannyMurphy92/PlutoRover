using PlutoRover.Enums;
using PlutoRover.Services.Interfaces;

namespace PlutoRover.Services
{
    public class Rover : IRover
    {
        private int PosX { get; set; }
        private int PosY { get; set; }
        private Heading Heading { get; set; }

        private readonly int _maxY;

        private readonly int _maxX;

        private readonly IObstacleService _obstacleService;

        public Rover(int currPosX, int currPosY, Heading heading, int maxX, int maxY, IObstacleService obstacleService)
        {
            PosX = currPosX;
            PosY = currPosY;
            Heading = heading;
            _maxX = maxX;
            _maxY = maxY;
            _obstacleService = obstacleService;
        }

        public string Position => $"{PosX},{PosY},{Heading}";

        public void Move(char direction)
        {
            if(!char.ToUpper(direction).Equals('B') && !char.ToUpper(direction).Equals('F'))
            {
                return;
            }

            var change = Heading == Heading.N || Heading == Heading.E ? 1 : -1;

            if (char.ToUpper(direction).Equals('B'))
            {
                change = change * -1;
            }

            if (Heading == Heading.E || Heading == Heading.W)
            {
                MoveOnXAxis(change);
            }
            else if (Heading == Heading.N || Heading == Heading.S)
            {
                MoveOnYAxis(change);
            }

        }

        public void Rotate(char direction)
        {
            if (char.ToUpper(direction).Equals('R'))
            {
                switch (Heading)
                {
                    case Heading.N:
                        Heading = Heading.E;
                        break;
                    case Heading.E:
                        Heading = Heading.S;
                        break;
                    case Heading.S:
                        Heading = Heading.W;
                        break;
                    case Heading.W:
                        Heading = Heading.N;
                        break;
                }
            }
            else if (char.ToUpper(direction).Equals('L'))
            {
                switch (Heading)
                {
                    case Heading.N:
                        Heading = Heading.W;
                        break;
                    case Heading.E:
                        Heading = Heading.N;
                        break;
                    case Heading.S:
                        Heading = Heading.E;
                        break;
                    case Heading.W:
                        Heading = Heading.S;
                        break;
                }
            }
        }

        private void MoveOnYAxis(int change)
        {
            var tempNewPos = PosY + change;

            if(!_obstacleService.CanMoveToPosition(PosX, tempNewPos))
            {
                return;
            }

            if (tempNewPos < 0)
            {
                PosY = _maxY;
                return;
            }

            if (tempNewPos > _maxY)
            {
                PosY = 0;
                return;
            }

            PosY = tempNewPos;
        }

        private void MoveOnXAxis(int change)
        {
            var tempNewPos = PosX + change;

            if (!_obstacleService.CanMoveToPosition(tempNewPos, PosY))
            {
                return;
            }

            if (tempNewPos < 0)
            {
                PosX = _maxX;
                return;
            }

            if (tempNewPos > _maxX)
            {
                PosX = 0;
                return;
            }

            PosX = tempNewPos;
        }
    }
}
