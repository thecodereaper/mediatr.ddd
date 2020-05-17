namespace MediatR.DDD
{
    public interface IPager
    {
        int Offset { get; }
        int Limit { get; }
        string SortBy { get; }
        string SortOrder { get; }
        string Filter { get; }
    }
}