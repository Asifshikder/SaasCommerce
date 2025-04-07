using System.Net;

namespace Project.Core.Exceptions;
public class ForbiddenException : AppException
{
    public ForbiddenException()
        : base("unauthorized", [], HttpStatusCode.Forbidden)
    {
    }
    public ForbiddenException(string message)
       : base(message, [], HttpStatusCode.Forbidden)
    {
    }
}
