using System.Collections.Immutable;

namespace MazeSolver

{

    public class MazeApp
    {
        public static void Main()
        {
            /*
             * maze1.txt works
             * maze2.txt gets stuck
             * If using NUnit 3 - You will need to append TestContext.CurrentContext.TestDirectory to front of path to make it work properly
             * And do not use Path.Combine. If Path1 contains a C:\ it will always just return path2? Ask MS why.
             */
            Run(@"MazeFiles\maze1.txt");
            Console.ReadLine();
        }

        public static void Run(string mazeFilePath)
        {
            var lines = File.ReadAllLines(mazeFilePath).Select(line => line.Replace(" ", "")).ToImmutableArray();

            MazeGrid maze = ExtractMaze(lines);
            var walker = new DumbMazeWalker(maze);

            while (!walker.AtFinish())
            {
                var couldMoveForward = walker.MoveForward();

                if (!couldMoveForward)
                {
                    walker.TurnRight();
                }
                else
                {
                    if (walker.CanSeeLeftTurning())
                    {
                        walker.TurnLeft();
                    }
                }

                Console.WriteLine(walker.CurrentPosition);
            }

            Console.WriteLine("Reached end of maze! :)");
        }

        private static MazeGrid ExtractMaze(ImmutableArray<string> lines)
        {
            var builder = new MazeBuilder(lines);

            return builder.Build();
        }

    }
}