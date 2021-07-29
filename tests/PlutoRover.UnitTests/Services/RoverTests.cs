
using Xunit;
using PlutoRover.Services;

namespace PlutoRover.UnitTests.Services
{
    public class RoverTests
    {
        [Fact]
        public void WhenCreateRover_PositionMatchesThatSetByCtorArguments()
        {
            var sut = new Rover(23, 12, 'N');
            
            Assert.Equal("23,12,N", sut.Position);
        }

        [Theory]
        [InlineData('f')]
        [InlineData('F')]
        public void WhenHeadingNPassFToRoverMove_RoverPositionIncreasesBy1(char direction)
        {
            var sut = new Rover(0, 0, 'N');

            sut.Move(direction);

            Assert.Equal("1,0,N", sut.Position);
        }



        [Theory]
        [InlineData('b')]
        [InlineData('B')]
        public void WhenHeadingNPassBToRoverMove_RoverPositionIncreasesBy1(char direction)
        {
            var sut = new Rover(10, 10, 'N');

            sut.Move(direction);

            Assert.Equal("9,10,N", sut.Position);
        }

        [Fact]
        public void WhenPassInvalidRoverMove_DoNotUpdatePosition()
        {
            var sut = new Rover(10, 10, 'N');

            sut.Move('x');

            Assert.Equal("10,10,N", sut.Position);
        }
    }
}
