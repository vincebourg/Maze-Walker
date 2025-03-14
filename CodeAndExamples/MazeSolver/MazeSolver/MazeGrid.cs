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

        private readonly Cell[][] Grid;
        public Point StartPosition { get; }
        public Point Finish { get; }

        public MazeGrid(Cell[][] grid, Point start, Point finish)
        {
            Grid = grid;
            StartPosition = start;
            Finish = finish;
        }

        public bool IsAllowed(Point location) => Grid[location.Y][location.X] is not Cell.Wall;
    }


}