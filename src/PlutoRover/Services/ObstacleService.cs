using PlutoRover.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PlutoRover.Services
{
    public class ObstacleService : IObstacleService
    {
        private IList<(int x, int y)> _obstacles;
        public ObstacleService(IList<(int x, int y)> obstacles)
        {
            _obstacles = obstacles;
        }

        public bool CanMoveToPosition(int x, int y)
        {
            return !_obstacles.Any(ob => ob.x == x && ob.y == y);
        }
    }
}
