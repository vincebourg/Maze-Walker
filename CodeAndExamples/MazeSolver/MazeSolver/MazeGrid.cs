namespace MazeSolver
{
    public class MazeGrid
    {
        // +--------> +ve X
        // |
        // |
        // |
        // |
        // v
        // +ve Y

        public MazeGrid(bool[][] grid, Point start, Point finish)
        {
            Grid = grid;
            StartPosition = start;
            Finish = finish;
        }

        public Point StartPosition { get; }

        public bool AtFinish(DumbMazeWalker walker)
        {
            return Finish.X == walker.CurrentPosition.X && Finish.Y == walker.CurrentPosition.Y;
        }

        public Point Finish { get; }

        public bool[][] Grid { get; }

        public bool IsAllowed(Point location) => Grid[location.Y][location.X];
    }
}