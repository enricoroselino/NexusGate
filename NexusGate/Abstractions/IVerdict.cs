namespace NexusGate.Abstractions;

public interface IVerdict
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    Failure Failure { get; }
}

public interface IVerdict<out T> : IVerdict
{
    T? Data { get; }
}