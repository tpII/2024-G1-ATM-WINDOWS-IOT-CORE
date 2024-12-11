using System;

namespace ATM.Application.Exceptions;

public class RepositoryException : Exception
{
    public RepositoryException(string? message) : base(message) { }
}
