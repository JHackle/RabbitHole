namespace Hackle.Objects
{
    using Hackle.Exceptions;
    using Hackle.Map;
    using Hackle.Util;
    using UnityEngine;


    /// <summary>
    /// Base class for all units which can be moved.
    /// </summary>
    public class MovableObject : SelectableObject, IMovable
    {
        public int Steps { get; set; }
        public float Speed { get; set; }

        private bool move = false;
        private Tile targetTile;
        private bool canMove = true;

        public void Move(Tile tile)
        {
            if (move)
            {
                throw new IllegalMoveException("Movement is currently in progress. Unable to start another movement to " + tile.Position);
            }

            targetTile = tile;
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
                Vector3 target = TileUtil.CoordToPosition(targetTile.Position);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * Speed);
                if (Vector3.Distance(transform.position, target) < 0.001f)
                {
                    move = false;
                    Location = targetTile;
                }
            }
        }
    }
}