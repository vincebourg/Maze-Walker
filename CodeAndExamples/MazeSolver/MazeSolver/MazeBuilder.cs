namespace MazeSolver

{
    public partial class MazeBuilder
    {
        private readonly string[] _lines;
        private Point? _start;
        private Point? _finish;

        public MazeBuilder(IEnumerable<string> lines)
        {
            _lines = lines.ToArray();
        }


        public MazeGrid Build()
        {
            var grid = new Cell[_lines.Length][];

            for (int i = 0; i < _lines.Length; i++)
            {
                var line = _lines[i];
                grid[i] = new Cell[line.Length];

                for (int j = 0; j < line.Length; j++)
                {
                    var character = line[j];
                    var parsedCharacter = ProcessMazeCharacter(character);
                    grid[i][j] = parsedCharacter;
                    switch (parsedCharacter)
                    {
                        case Cell.Start:
                            _start = new Point(j, i);
                            break;
                        case Cell.Finish:
                            _finish = new Point(j, i);
                            break;
                    }
                }
            }

            if (_start == null) throw new Exception("Maze should have a start position set.");
            if (_finish == null) throw new Exception("Maze should have a finish position set.");


            var maze = new MazeGrid(grid, _start, _finish);
            return maze;
        }

        public record ProcessMazeCharacterResult(bool IsAllowed, bool IsStart, bool IsEnd);


        private static Cell ProcessMazeCharacter(char character)
        {
            return character switch
            {
                '#' => Cell.Wall,
                '.' => Cell.Path,
                'S' => Cell.Start,
                'F' => Cell.Finish,
                _ => throw new Exception("Maze input string contains invalid characters")
            };
        }
    }
}