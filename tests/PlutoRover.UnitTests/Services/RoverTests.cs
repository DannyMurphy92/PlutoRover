using PlutoRover.Enums;
using Xunit;
using PlutoRover.Services;
using PlutoRover.Services.Interfaces;
using NSubstitute;

namespace PlutoRover.UnitTests.Services
{
    public class RoverTests
    {
        private readonly IObstacleService _obstacleSvc;
        public RoverTests()
        {
            _obstacleSvc = Substitute.For<IObstacleService>();
            _obstacleSvc.CanMoveToPosition(default, default).ReturnsForAnyArgs(true);

        }
        private IRover CreateSut(int currX = 10, int currY = 20, Heading heading = Heading.N)
        {
            return new Rover(currX, currY, heading, 100, 200, _obstacleSvc);
        }

        [Fact]
        public void WhenCreateRover_PositionMatchesThatSetByCtorArguments()
        {
            var sut = CreateSut(23, 12, Heading.N);

            Assert.Equal("23,12,N", sut.Position);
        }

        [Theory]
        [InlineData('f')]
        [InlineData('F')]
        public void WhenHeadingNPassFToRoverMove_RoverPositionIncreasesBy1(char direction)
        {
            var sut = CreateSut(0, 0);

            sut.Move(direction);

            Assert.Equal("0,1,N", sut.Position);
        }

        [Theory]
        [InlineData('b')]
        [InlineData('B')]
        public void WhenHeadingNPassBToRoverMove_RoverPositionIncreasesBy1(char direction)
        {
            var sut = CreateSut();

            sut.Move(direction);

            Assert.Equal("10,19,N", sut.Position);
        }

        [Fact]
        public void WhenPassInvalidRoverMove_DoNotUpdatePosition()
        {
            var sut = CreateSut();

            sut.Move('x');

            Assert.Equal("10,20,N", sut.Position);
        }

        [Theory]
        [InlineData(Heading.N, Heading.E)]
        [InlineData(Heading.E, Heading.S)]
        [InlineData(Heading.S, Heading.W)]
        [InlineData(Heading.W, Heading.N)]
        public void WhenRotateRight_HeadingUpdatesCorrectly(Heading initHeading, Heading finalHeading)
        {
            var sut = CreateSut(heading: initHeading);

            sut.Rotate('R');

            Assert.Equal($"10,20,{finalHeading}", sut.Position);
        }


        [Theory]
        [InlineData(Heading.N, Heading.W)]
        [InlineData(Heading.W, Heading.S)]
        [InlineData(Heading.S, Heading.E)]
        [InlineData(Heading.E, Heading.N)]
        public void WhenRotateLeft_HeadingUpdatesCorrectly(Heading initHeading, Heading finalHeading)
        {
            var sut = CreateSut(heading: initHeading);

            sut.Rotate('L');

            Assert.Equal($"10,20,{finalHeading}", sut.Position);
        }

        [Fact]
        public void WhenPassInvalidRoverRotate_DoNotUpdatePosition()
        {
            var sut = CreateSut();

            sut.Rotate('x');

            Assert.Equal("10,20,N", sut.Position);
        }

        [Theory]
        [InlineData(Heading.N, "10,21,N")]
        [InlineData(Heading.E, "11,20,E")]
        [InlineData(Heading.S, "10,19,S")]
        [InlineData(Heading.W, "9,20,W")]
        public void WhenMoveForwardFacingDirection_MovesToCorrectPosition(Heading heading, string expectedPosition)
        {
            var sut = CreateSut(heading: heading);

            sut.Move('f');

            Assert.Equal(expectedPosition, sut.Position);
        }


        [Theory]
        [InlineData(Heading.N, "10,19,N")]
        [InlineData(Heading.E, "9,20,E")]
        [InlineData(Heading.S, "10,21,S")]
        [InlineData(Heading.W, "11,20,W")]
        public void WhenMoveBackwardFacingDirection_MovesToCorrectPosition(Heading heading, string expectedPosition)
        {
            var sut = CreateSut(heading: heading);

            sut.Move('b');

            Assert.Equal(expectedPosition, sut.Position);
        }

        [Fact]
        public void WhenAt0OnYAxisFacingNorth_WhenGoBackwards_NowAtMaxYPosition()
        {
            var sut = CreateSut(0, 0);

            sut.Move('b');

            Assert.Equal("0,200,N", sut.Position);
        }


        [Fact]
        public void WhenAtMaxPositionOYAxisFacingNorth_WhenGoForwards_NowAt0YPosition()
        {
            var sut = CreateSut(0, 200);

            sut.Move('f');

            Assert.Equal("0,0,N", sut.Position);
        }

        [Fact]
        public void WhenAt0OnXAxisFacingEast_WhenGoBackwards_NowAtMaxXPosition()
        {
            var sut = CreateSut(0, 0, Heading.E);

            sut.Move('b');

            Assert.Equal("100,0,E", sut.Position);
        }


        [Fact]
        public void WhenAtMaxPositionOXAxisFacingEast_WhenGoForwards_NowAt0XPosition()
        {
            var sut = CreateSut(100, 0, Heading.E);

            sut.Move('f');

            Assert.Equal("0,0,E", sut.Position);
        }

        [Theory]
        [InlineData(Heading.N)]
        [InlineData(Heading.E)]
        [InlineData(Heading.S)]
        [InlineData(Heading.W)]
        public void WhenTryMoveForwardsToPositionWithObstacle_DoNotMove(Heading heading)
        {
            _obstacleSvc.CanMoveToPosition(default, default).ReturnsForAnyArgs(false);

            var sut = CreateSut(heading: heading);

            sut.Move('f');

            Assert.Equal($"10,20,{heading}", sut.Position);
        }

        [Theory]
        [InlineData(Heading.N)]
        [InlineData(Heading.E)]
        [InlineData(Heading.S)]
        [InlineData(Heading.W)]
        public void WhenTryMoveBackwardsToPositionWithObstacle_DoNotMove(Heading heading)
        {
            _obstacleSvc.CanMoveToPosition(default, default).ReturnsForAnyArgs(false);

            var sut = CreateSut(heading: heading);

            sut.Move('b');

            Assert.Equal($"10,20,{heading}", sut.Position);
        }

        [Fact]
        public void WhenPassChainOfCommandsToRover_ItMovesRoverAccordingly()
        {
            var command = "FFLFxFRBRR";
            var expectedPosition = "99,1,S";

            var sut = CreateSut(0, 0);

            sut.Command(command);

            Assert.Equal(expectedPosition, sut.Position);
        }
    }
}
