using System;

namespace Hahn.ApplicatonProcess.July2021.Domain.Exceptions
{
    public class BussinessException : Exception
    {
        public int statusCode { get; set; }
        public string message { get; set; }

        public BussinessException(string Message, int StatusCode) : base(Message)
        {
            message = Message;
            statusCode = StatusCode;
        }
    }
}
