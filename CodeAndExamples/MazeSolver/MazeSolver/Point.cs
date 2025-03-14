namespace MazeSolver
{
    public record Point(int X, int Y)
    {
        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
    }
}