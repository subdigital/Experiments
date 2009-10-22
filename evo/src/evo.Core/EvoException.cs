using System;

namespace evo.Core
{
    public class EvoException : Exception
    {
        public string FriendlyError { get; private set; }


        public EvoException(string friendlyError)
        {
            FriendlyError = friendlyError;
        }            

        public EvoException(string friendlyError, Exception innerException)
            :base(innerException.Message, innerException)
        {
            FriendlyError = friendlyError;
        }
    }
}