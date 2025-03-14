using MazeSolver;
using System.Text;

namespace MazeSolverTests;

public class Test
{
    [Fact]
    public void Maze1_succeeds()
    {
        StringBuilder output = new StringBuilder();
        Console.SetOut(new StringWriter(output));
        var mazeApp = new MazeApp();
        MazeApp.Run(@"MazeFiles\maze1.txt");

        const string expectedOutput = """
            Point(1, 1)
            Point(2, 1)
            Point(3, 1)
            Point(4, 1)
            Point(4, 1)
            Point(4, 1)
            Point(3, 1)
            Point(2, 1)
            Point(2, 2)
            Point(2, 3)
            Point(3, 3)
            Point(4, 3)
            Point(5, 3)
            Reached end of maze! :)

            """;

        Assert.Equal(expectedOutput, output.ToString());
    }
}

