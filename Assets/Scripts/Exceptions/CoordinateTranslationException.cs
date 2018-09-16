using System;

namespace Hackle.Util
{
    [Serializable]
    internal class CoordinateTranslationException : Exception
    {
        public CoordinateTranslationException()
        {
        }

        public CoordinateTranslationException(string message) : base(message)
        {
        }

        public CoordinateTranslationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}