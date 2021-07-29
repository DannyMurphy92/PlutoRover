using PlutoRover.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlutoRover.UnitTests.Services
{
    public class ObstacleServiceTests
    {
        [Fact]
        public void WhenTryMoveToPositionWithNoObstacles_ReturnTrue()
        {
            var sut = new ObstacleService(new List<(int, int)>());

            Assert.True(sut.CanMoveToPosition(0, 0));
        }

        [Fact]
        public void WhenTryMoveToPositionWithNoObstaclesAtPosition_ReturnTrue()
        {
            var obstacles = new List<(int, int)>
                {
                    (1, 0)
                };
            var sut = new ObstacleService(obstacles);

            Assert.True(sut.CanMoveToPosition(0, 0));
        }

        [Fact]
        public void WhenTryMoveToPositionWithObstacleAtPosition_ReturnFalse()
        {
            var obstacles = new List<(int, int)>
                {
                    (0, 0)
                };
            var sut = new ObstacleService(obstacles);

            Assert.False(sut.CanMoveToPosition(0, 0));
        }
    }
}
