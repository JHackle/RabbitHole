namespace Hackle.Objects
{
    using Hackle.Exceptions;
    using Hackle.Map;
    using Hackle.Util;
    using UnityEngine;


    /// <summary>
    /// Base class for all units which can be moved.
    /// </summary>
    public class MovableUnit : SelectableUnit, IMovable
    {
        public int Steps { get; set; }
        public float Speed { get; set; }

        private bool move = false;
        private Vector3 target;
        private Coord targetPosition;
        private bool canMove = true;

        public void Move(Coord coord)
        {
            if (move)
            {
                throw new IllegalMoveException("Movement is currently in progress. Unable to start another movement to " + coord);
            }

            target = TileUtil.CoordToPosition(coord);
            targetPosition = coord;
            move = true;
            canMove = false;
        }

        public void RestoreSteps()
        {
            canMove = true;
        }

        public bool CanMove()
        {
            return canMove;
        }

        protected void Update()
        {
            if (move)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * Speed);
                if (Vector3.Distance(transform.position, target) < 0.001f)
                {
                    move = false;
                    xPos = targetPosition.x;
                    yPos = targetPosition.y;
                }
            }
        }
    }
}