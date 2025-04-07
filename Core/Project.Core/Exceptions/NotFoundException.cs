using System.Collections.ObjectModel;
using System.Net;

namespace Project.Core.Exceptions;
public class NotFoundException : AppException
{
    public NotFoundException(string message)
        : base(message, new Collection<string>(), HttpStatusCode.NotFound)
    {
    }
}
