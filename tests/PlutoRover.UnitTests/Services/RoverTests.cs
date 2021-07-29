﻿using PlutoRover.Enums;
using Xunit;
using PlutoRover.Services;

namespace PlutoRover.UnitTests.Services
{
    public class RoverTests
    {
        [Fact]
        public void WhenCreateRover_PositionMatchesThatSetByCtorArguments()
        {
            var sut = new Rover(23, 12, Heading.N);

            Assert.Equal("23,12,N", sut.Position);
        }

        [Theory]
        [InlineData('f')]
        [InlineData('F')]
        public void WhenHeadingNPassFToRoverMove_RoverPositionIncreasesBy1(char direction)
        {
            var sut = new Rover(0, 0, Heading.N);

            sut.Move(direction);

            Assert.Equal("1,0,N", sut.Position);
        }



        [Theory]
        [InlineData('b')]
        [InlineData('B')]
        public void WhenHeadingNPassBToRoverMove_RoverPositionIncreasesBy1(char direction)
        {
            var sut = new Rover(10, 10, Heading.N);

            sut.Move(direction);

            Assert.Equal("9,10,N", sut.Position);
        }

        [Fact]
        public void WhenPassInvalidRoverMove_DoNotUpdatePosition()
        {
            var sut = new Rover(10, 10, Heading.N);

            sut.Move('x');

            Assert.Equal("10,10,N", sut.Position);
        }

        [Theory]
        [InlineData(Heading.N, Heading.E)]
        [InlineData(Heading.E, Heading.S)]
        [InlineData(Heading.S, Heading.W)]
        [InlineData(Heading.W, Heading.N)]
        public void WhenRotateRight_HeadingUpdatesCorrectly(Heading initHeading, Heading finalHeading)
        {
            var sut = new Rover(10, 10, initHeading);

            sut.Rotate('R');

            Assert.Equal($"10,10,{finalHeading}", sut.Position);
        }


        [Theory]
        [InlineData(Heading.N, Heading.W)]
        [InlineData(Heading.W, Heading.S)]
        [InlineData(Heading.S, Heading.E)]
        [InlineData(Heading.E, Heading.N)]
        public void WhenRotateLeft_HeadingUpdatesCorrectly(Heading initHeading, Heading finalHeading)
        {
            var sut = new Rover(10, 10, initHeading);

            sut.Rotate('L');

            Assert.Equal($"10,10,{finalHeading}", sut.Position);
        }

        [Fact]
        public void WhenPassInvalidRoverRotate_DoNotUpdatePosition()
        {
            var sut = new Rover(10, 10, Heading.N);

            sut.Rotate('x');

            Assert.Equal("10,10,N", sut.Position);
        }
    }
}
