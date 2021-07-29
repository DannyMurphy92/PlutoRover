using PlutoRover.Enums;
using PlutoRover.Services.Interfaces;

namespace PlutoRover.Services
{
    public class Rover : IRover
    {
        private int PosX { get; set; }
        private int PosY { get; set; }
        private Heading Heading { get; set; }

        public Rover(int posX, int posY, Heading heading)
        {
            PosX = posX;
            PosY = posY;
            Heading = heading;
        }

        public string Position => $"{PosX},{PosY},{Heading}";

        public void Move(char direction)
        {
            if (char.ToUpper(direction).Equals('F'))
            {
                var change = Heading == Heading.N || Heading == Heading.E ? 1 : -1;

                if (Heading == Heading.E || Heading == Heading.W)
                {
                    PosY += change;
                }
                else if (Heading == Heading.N || Heading == Heading.S)
                {
                    PosX += change;
                }
            }
            else if (char.ToUpper(direction).Equals('B'))
            {
                var change = Heading == Heading.N || Heading == Heading.E ? -1 : 1;

                if (Heading == Heading.E || Heading == Heading.W)
                {
                    PosY += change;
                }
                else if (Heading == Heading.N || Heading == Heading.S)
                {
                    PosX += change;
                }
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
    }
}
