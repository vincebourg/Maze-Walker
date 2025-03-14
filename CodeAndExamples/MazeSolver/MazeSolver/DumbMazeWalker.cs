using System;

namespace MazeSolver
{
    public class DumbMazeWalker
    {
        private readonly MazeGrid _mMazeGrid;
        private Orientation _mDirec;
        public Point CurrentPosition { get; private set; }

        public DumbMazeWalker(MazeGrid mazeGrid)
        {
            _mMazeGrid = mazeGrid;
            CurrentPosition = _mMazeGrid.StartPosition;
            _mDirec = Orientation.South;
        }

        public bool CanSeeLeftTurning()
        {
            var pointToOurLeft = _mDirec switch
            {
                Orientation.North => new Point(CurrentPosition.X - 1, CurrentPosition.Y),
                Orientation.South => new Point(CurrentPosition.X + 1, CurrentPosition.Y),
                Orientation.East => new Point(CurrentPosition.X, CurrentPosition.Y - 1),
                Orientation.West => new Point(CurrentPosition.X, CurrentPosition.Y + 1),
                _ => throw new Exception(),
            };
            return _mMazeGrid.Grid[pointToOurLeft.Y][pointToOurLeft.X];
        }

        public void TurnRight()
        {
            _mDirec = _mDirec switch
            {
                Orientation.North => Orientation.East,
                Orientation.East => Orientation.South,
                Orientation.South => Orientation.West,
                Orientation.West => Orientation.North,
                _ => throw new Exception(),
            };
        }

        public void TurnLeft()
        {
            _mDirec = _mDirec switch
            {
                Orientation.North => Orientation.West,
                Orientation.West => Orientation.South,
                Orientation.South => Orientation.East,
                Orientation.East => Orientation.North,
                _ => throw new Exception(),
            };
        }

        public bool MoveForward()
        {
            var desiredPoint = _mDirec switch
            {
                Orientation.North => new Point(CurrentPosition.X, CurrentPosition.Y - 1),
                Orientation.South => new Point(CurrentPosition.X, CurrentPosition.Y + 1),
                Orientation.East => new Point(CurrentPosition.X + 1, CurrentPosition.Y),
                Orientation.West => new Point(CurrentPosition.X - 1, CurrentPosition.Y),
                _ => throw new NotImplementedException()
            };

            var canMoveForward = _mMazeGrid.Grid[desiredPoint.Y][desiredPoint.X];
            if (canMoveForward) CurrentPosition = desiredPoint;
            return canMoveForward;
        }
    }
}