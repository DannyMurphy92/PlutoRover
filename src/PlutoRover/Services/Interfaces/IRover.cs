namespace PlutoRover.Services.Interfaces
{
    public interface IRover
    {
        string Position { get; }

        void Move(char direction);
    }
}
