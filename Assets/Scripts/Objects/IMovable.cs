namespace Hackle.Objects
{
    using Hackle.Map;

    public interface IMovable : ISelectable
    {
        /// <summary>
        /// Describes how many steps a unit can move within one turn.
        /// </summary>
        int Steps { get; set; }

        float Speed { get; set; }

        /// <summary>
        /// Starts the movement from the current position to the target position.
        /// This method can not be called if a movement is currently in progress.
        /// This method returns immediately and does not await the end of the movement.
        /// </summary>
        /// <param name="coord">the target position to move to</param>
        void Move(Coord coord);

        void RestoreSteps();

        bool CanMove();
    }
}