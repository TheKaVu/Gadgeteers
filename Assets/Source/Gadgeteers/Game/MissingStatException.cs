using System;

namespace Source.Gadgeteers.Game
{
    public class MissingStatException : Exception
    {
        public MissingStatException(string msg) : base(msg)
        {
        }
    }
}