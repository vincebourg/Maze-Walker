namespace MazeSolver
{
    public class DumbMazeWalker
    {
        private readonly Orientation[] orientations =
            [Orientation.North, Orientation.East, Orientation.South, Orientation.West];

        private readonly Dictionary<Orientation, Point> UnitVectors =
            new()
            {
                { Orientation.North, new Point(0, -1) },
                { Orientation.East, new Point(1, 0) },
                { Orientation.South, new Point(0, 1) },
                { Orientation.West, new Point(-1, 0) }
            };

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
            var pointToOurLeft = CurrentPosition + UnitVectors[GetLeftOrientation()];
            return _mMazeGrid.IsAllowed(pointToOurLeft);
        }

        public bool MoveForward()
        {
            var desiredPoint = UnitVectors[_mDirec] + CurrentPosition;
            var canMoveForward = _mMazeGrid.IsAllowed(desiredPoint);
            if (canMoveForward) CurrentPosition = desiredPoint;
            return canMoveForward;
        }

        public void TurnRight()
        {
            var index = Array.IndexOf(orientations, _mDirec);
            _mDirec = orientations[(index + 1) % orientations.Length];
        }

        public void TurnLeft()
        {
            Orientation orientation = GetLeftOrientation();
            _mDirec = orientation;
        }

        private Orientation GetLeftOrientation()
        {
            var index = Array.IndexOf(orientations, _mDirec);
            var orientation = orientations[(index - 1) % orientations.Length];
            return orientation;
        }

        internal bool AtFinish()
        {
            return CurrentPosition == _mMazeGrid.Finish;
        }
    }
}
