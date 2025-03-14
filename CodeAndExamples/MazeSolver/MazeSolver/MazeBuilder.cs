namespace MazeSolver

{
    public class MazeBuilder
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
            var currentRow = 0;
            var grid = new bool[_lines.Length][];

            foreach (var line in _lines)
            {
                int currentColumn = 0;
                grid[currentRow] = new bool[line.Length];
                foreach (var character in line)
                {
                    var parsedCharacter = ProcessMazeCharacter(character);
                    if (parsedCharacter.IsStart)
                    {
                        _start = new Point(currentColumn, currentRow);
                    }
                    if (parsedCharacter.IsEnd)
                    {
                        _finish = new Point(currentColumn, currentRow);
                    }
                    grid[currentRow][currentColumn] = parsedCharacter.IsAllowed;

                    currentColumn++;
                }
                currentRow++;
            }

            if (_start == null) throw new Exception("Maze should have a start position set.");
            if (_finish == null) throw new Exception("Maze should have a finish position set.");


            var maze = new MazeGrid(grid, _start, _finish);
            return maze;
        }

        public record ProcessMazeCharacterResult(bool IsAllowed, bool IsStart, bool IsEnd);
        private static ProcessMazeCharacterResult ProcessMazeCharacter(char character)
        {
            return character switch
            {
                '#' => new ProcessMazeCharacterResult(false, false, false),
                '.' => new ProcessMazeCharacterResult(true, false, false),
                'S' => new ProcessMazeCharacterResult(true, true, false),
                'F' => new ProcessMazeCharacterResult(true, false, true),
                _ => throw new Exception("Maze input string contains invalid characters")
            };
        }
    }
}