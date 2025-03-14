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
            Point? startBuilder = null;
            Point? finishBuilder = null;

            var grid = new bool[lines.Length][];
            int currentRow = 0;

            foreach (var line in lines)
            {
                grid[currentRow] = new bool[line.Length];
                int currentCol = 0;

                foreach (var character in line)
                {
                    switch (character)
                    {
                        case '#':
                            grid[currentRow][currentCol] = false;
                            break;
                        case '.':
                            grid[currentRow][currentCol] = true;
                            break;
                        case 'S':
                            grid[currentRow][currentCol] = true;
                            startBuilder = new Point(currentCol, currentRow);
                            break;
                        case 'F':
                            grid[currentRow][currentCol] = true;
                            finishBuilder = new Point(currentCol, currentRow);
                            break;
                        default:
                            throw new Exception("Maze input string contains invalid characters");
                    }

                    currentCol++;
                }

                currentRow++;
            }

            if (startBuilder == null) throw new Exception("Maze should have a start position set.");
            if (finishBuilder == null) throw new Exception("Maze should have a finish position set.");


            var maze = new MazeGrid(grid, startBuilder, finishBuilder);
            return maze;
        }
    }
}