using System;

namespace TrackerApplication.Contracts
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    };
}
