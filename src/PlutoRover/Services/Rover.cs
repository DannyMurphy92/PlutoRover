using PlutoRover.Services.Interfaces;

namespace PlutoRover.Services
{
    public class Rover : IRover
    {
        private int PosX { get; set; }
        private int PosY { get; set; }
        private char Heading { get; set; }

        public Rover(int posX, int posY, char heading)
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
                PosX++;
            }
            else if (char.ToUpper(direction).Equals('B'))
            {
                PosX--;
            }
        }
    }
}
