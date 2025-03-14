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
            var grid = new bool[_lines.Length][];

            for (int i = 0; i < _lines.Length; i++)
            {
                var line = _lines[i];
                grid[i] = new bool[line.Length];

                for (int j = 0; j < line.Length; j++)
                {
                    var character = line[j];
                    var parsedCharacter = ProcessMazeCharacter(character);
                    if (parsedCharacter.IsStart)
                    {
                        _start = new Point(j, i);
                    }
                    if (parsedCharacter.IsEnd)
                    {
                        _finish = new Point(j, i);
                    }
                    grid[i][j] = parsedCharacter.IsAllowed;
                }
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