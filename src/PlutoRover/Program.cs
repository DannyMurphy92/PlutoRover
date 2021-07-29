using PlutoRover.Enums;
using PlutoRover.Services;
using System;
using System.Collections.Generic;

namespace PlutoRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var obstacles = new List<(int x, int y)>();
            var obstacleService = new ObstacleService(obstacles);

            var maxX = 100;
            var maxY = 200;

            var rover = new Rover(0, 0, Heading.N, maxX, maxY, obstacleService);

            Console.WriteLine("After travelling many fractions of a lightyear you have finally made it to Pluto...");
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("You can move using the command F: go forward, B: go backward, L: turn left, R: turn right");
            Console.WriteLine("You can chain commands together such as \"FRFFBLR\"");
            Console.WriteLine("You can also leave this micro-planet by passing the command \"exit\" at any time");

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Now that thats out of the way, where do you want to go on this barren wasteland?");
            Console.WriteLine(Environment.NewLine);

            var input = Console.ReadLine();


            while(!string.Equals(input, "exit", StringComparison.CurrentCultureIgnoreCase))
            {
                rover.Command(input);
                Console.WriteLine($"Your current position is now: {rover.Position}");
                Console.WriteLine("What would you like to do next?");
                input = Console.ReadLine();
            }
        }
    }
}
