using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlutoRover.Services.Interfaces
{
    public interface IObstacleService
    {
        bool CanMoveToPosition(int x, int y);
    }
}
