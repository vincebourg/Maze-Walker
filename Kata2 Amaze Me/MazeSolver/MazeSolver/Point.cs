namespace MazeSolver
{
    public class Point
    {
        public int X { set; get; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }
    }
}