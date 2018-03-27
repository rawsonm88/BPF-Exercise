using System;
using System.Runtime.Serialization;

namespace FileData.Exceptions
{
    [Serializable]
    internal class AmbiguousArgumentException : Exception
    {
        public AmbiguousArgumentException()
        {
        }

        public AmbiguousArgumentException(string argumentName) : base(argumentName)
        {
        }
    }
}