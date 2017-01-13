using System;

namespace MazeSolver
{
    public class DumbMazeWalker
    {
        private readonly MazeGrid _mMazeGrid;
        private Orientation _mDirec;

        public DumbMazeWalker(MazeGrid mazeGrid)
        {
            _mMazeGrid = mazeGrid;
            CurrentPosition = _mMazeGrid.StartPosition;
            _mDirec = Orientation.South;
        }

        public bool CanSeeLeftTurning()
        {
            var pointToOurLeft = new Point(CurrentPosition.X, CurrentPosition.Y);

            switch (_mDirec)
            {
                case Orientation.North:
                    pointToOurLeft.X -= 1;
                    break;
                case Orientation.South:
                    pointToOurLeft.X += 1;
                    break;
                case Orientation.East:
                    pointToOurLeft.Y -= 1;
                    break;
                case Orientation.West:
                    pointToOurLeft.Y += 1;
                    break;
                default:
                    throw new Exception();
            }

            return _mMazeGrid.Grid[pointToOurLeft.Y][pointToOurLeft.X];
        }

        public Point CurrentPosition { get; set; }

        public void TurnRight()
        {
            switch (_mDirec)
            {
                case Orientation.North:
                    _mDirec = Orientation.East;
                    break;
                case Orientation.East:
                    _mDirec = Orientation.South;
                    break;
                case Orientation.South:
                    _mDirec = Orientation.West;
                    break;
                case Orientation.West:
                    _mDirec = Orientation.North;
                    break;
                default:
                    throw new Exception();
            }
        }

        public void TurnLeft()
        {
            switch (_mDirec)
            {
                case Orientation.North:
                    _mDirec = Orientation.West;
                    break;
                case Orientation.West:
                    _mDirec = Orientation.South;
                    break;
                case Orientation.South:
                    _mDirec = Orientation.East;
                    break;
                case Orientation.East:
                    _mDirec = Orientation.North;
                    break;
                default:
                    throw new Exception();
            }
        }

        public bool MoveForward()
        {
            var desiredPoint = new Point(CurrentPosition.X, CurrentPosition.Y);

            switch (_mDirec)
            {
                case Orientation.North:
                    desiredPoint.Y -= 1;
                    break;
                case Orientation.South:
                    desiredPoint.Y += 1;
                    break;
                case Orientation.East:
                    desiredPoint.X += 1;
                    break;
                case Orientation.West:
                    desiredPoint.X -= 1;
                    break;
                default:
                    throw new Exception();
            }

            var canMoveForward = _mMazeGrid.Grid[desiredPoint.Y][desiredPoint.X];
            if (canMoveForward) CurrentPosition = desiredPoint;
            return canMoveForward;
        }
    }
}