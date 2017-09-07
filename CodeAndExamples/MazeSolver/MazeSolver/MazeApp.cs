using System;
using System.IO;

namespace MazeSolver
{
    public class MazeApp
    {
        public static void Main()
        {
            var mazeApp = new MazeApp();
            /*
             * maze1.txt works
             * maze2.txt gets stuck
             * If using NUnit 3 - You will need to append TestContext.CurrentContext.TestDirectory to front of path to make it work properly
             * And do not use Path.Combine. If Path1 contains a C:\ it will always just return path2? Ask MS why.
             */
            mazeApp.Run(@"MazeFiles\maze1.txt");
            Console.ReadLine();
        }

        private void Run(string mazeFilePath)
        {
            // todo: handle \n newline characters instead of Environment.NewLine when you download a zip file
            var lines = new StreamReader(new FileStream(mazeFilePath, FileMode.Open)).ReadToEnd().Replace(" ", "").Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            Point start = null;
            Point finish = null;

            var grid = new bool[lines.Length][];
            int currentRow = 0;

            foreach (var line in lines)
            {
                grid[currentRow] = new bool[line.Length];
                int currentCol = 0;

                foreach (var point in line)
                {
                    switch (point)
                    {
                        case '#':
                            grid[currentRow][currentCol] = false;
                            break;
                        case '.':
                            grid[currentRow][currentCol] = true;
                            break;
                        case 'S':
                            grid[currentRow][currentCol] = true;
                            start = new Point(currentCol, currentRow);
                            break;
                        case 'F':
                            grid[currentRow][currentCol] = true;
                            finish = new Point(currentCol, currentRow);
                            break;
                        default:
                            throw new Exception("Maze input string contains invalid characters");
                    }

                    currentCol++;
                }

                currentRow++;
            }

            if (start == null) throw new Exception("Maze should have a start position set.");
            if (finish == null) throw new Exception("Maze should have a finish position set.");

            var maze = new MazeGrid(grid, start, finish);
            var entity = new DumbMazeWalker(maze);

            bool endOfMazeReached = false;

            while (!endOfMazeReached)
            {
                var couldMoveForward = entity.MoveForward();

                if (!couldMoveForward)
                {
                    entity.TurnRight();
                }
                else
                {
                    if (entity.CanSeeLeftTurning())
                    {
                        entity.TurnLeft();
                    }
                }

                endOfMazeReached = maze.AtFinish(entity);
                Console.WriteLine(entity.CurrentPosition);
            }

            Console.WriteLine("Reached end of maze! :)");
        }
    }
}