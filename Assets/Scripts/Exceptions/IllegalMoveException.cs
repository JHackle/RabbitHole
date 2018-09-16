namespace Hackle.Exceptions
{
    using System;

    [Serializable]
    internal class IllegalMoveException : Exception
    {
        public IllegalMoveException()
        {
        }

        public IllegalMoveException(string message) : base(message)
        {
        }

        public IllegalMoveException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}