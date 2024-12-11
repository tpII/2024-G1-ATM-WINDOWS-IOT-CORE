using System;

namespace ATM.Application.Exceptions;

public class SessionException : Exception
{
    public SessionException(string? message) : base(message) { }
}
