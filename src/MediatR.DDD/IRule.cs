namespace MediatR.DDD
{
    public interface IRule
    {
        string Message { get; }

        bool IsValid();
    }
}
