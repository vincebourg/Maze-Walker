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

        private readonly bool[][] Grid;
        public Point StartPosition { get; }
        public Point Finish { get; }

        public MazeGrid(bool[][] grid, Point start, Point finish)
        {
            Grid = grid;
            StartPosition = start;
            Finish = finish;
        }

        public bool IsAllowed(Point location) => Grid[location.Y][location.X];
    }


}