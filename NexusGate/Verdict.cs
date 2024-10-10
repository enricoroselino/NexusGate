namespace NexusGate;

public interface IVerdict
{
    public bool IsSuccess { get; }
    public bool IsFailure { get; }
    public Failure Failure { get; }
    public TResult Fold<TResult>(Func<TResult> onSuccess, Func<Failure, TResult> onFailure);
}

public interface IVerdict<out T> : IVerdict
{
    public T? Data { get; }
    public TResult Fold<TResult>(Func<T, TResult> onSuccess, Func<Failure, TResult> onFailure);
}

public class Verdict : IVerdict
{
    protected Verdict(bool isSuccess, Failure failure)
    {
        // ReSharper disable once ComplexConditionExpression
        if (isSuccess && failure != Failure.None ||
            !isSuccess && failure == Failure.None)
        {
            throw new VerdictException("Invalid Failure", nameof(failure));
        }

        IsSuccess = isSuccess;
        Failure = failure;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Failure Failure { get; }
    public static Verdict Success() => new Verdict(true, failure: Failure.None);
    public static Verdict<T> Success<T>(T value) where T : new() => new Verdict<T>(true, value, Failure.None);
    public static Verdict Failed(Failure failure) => new Verdict(false, failure: failure);
    public static Verdict<T> Failed<T>(Failure failure) where T : new() => new Verdict<T>(false, default, failure);
    public TResult Fold<TResult>(Func<TResult> onSuccess, Func<Failure, TResult> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Failure);
    }
}

public class Verdict<T> : Verdict, IVerdict<T> where T : new()
{
    internal Verdict(bool isSuccess, T? data, Failure failure) : base(isSuccess, failure)
    {
        Data = data;
    }
    public T? Data { get; }
    public TResult Fold<TResult>(Func<T, TResult> onSuccess, Func<Failure, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Data ?? new T()) : onFailure(Failure.None);
    }
}

public sealed class Failure(object? message, FailureType failureType) : IEquatable<Failure>
{
    public object? Message { get; } = message;
    public FailureType FailureType { get; } = failureType;
    public static Failure None => new(string.Empty, FailureType.None);
    public static Failure Validation(object? message = null) => new(message, FailureType.NotValid);
    public static Failure Forbidden(object? message = null) => new(message, FailureType.Forbidden);
    public static Failure NotFound(object? message = null) => new(message, FailureType.NotFound);
    public static Failure Conflict(object? message = null) => new(message, FailureType.Conflict);
    public static Failure Server(object? message = null) => new(message, FailureType.Server);

    public bool Equals(Failure? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Message, other.Message) && FailureType == other.FailureType;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is Failure other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Message, (int)FailureType);
    }

    public static bool operator ==(Failure? left, Failure? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Failure? left, Failure? right)
    {
        return !Equals(left, right);
    }
}

public enum FailureType
{
    NotFound,
    Conflict,
    NotValid,
    Server,
    Forbidden,
    None
}

public class VerdictException : Exception
{
    public VerdictException(string message) : base(message)
    {
    }

    public VerdictException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}