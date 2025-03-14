namespace MazeSolver
{
    public record Point(int X, int Y)
    {
        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }
    }
}