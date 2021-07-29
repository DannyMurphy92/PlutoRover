namespace PlutoRover.Services.Interfaces
{
    public interface IRover
    {
        string Position { get; }

        void Move(char direction);

        void Rotate(char direction);

        void Command(string command);
    }
}
