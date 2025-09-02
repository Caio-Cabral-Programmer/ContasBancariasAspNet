namespace ContasBancariasAspNet.Api.Exceptions;

public class NotFoundException : BusinessException
{
    public NotFoundException() : base("Resource not found.") { }
    public NotFoundException(string message) : base(message) { }
}