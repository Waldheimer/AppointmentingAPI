using Appointmenting.API.Domain.Primitives;

namespace Appointmenting.API.Domain.ValueObjects
{
    public sealed class LastName : ValueObject
    {
        public const int MaxLength = 50;
        public const int MinLength = 2;
        public LastName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<LastName> Create(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return Result.Failure<LastName>(new Error("LastName.Empty", "LastName is empty"));
            }
            if (firstName.Length > MaxLength)
            {
                return Result.Failure<LastName>(new Error("LastName.TooLong", $"LastName must be max {MaxLength} characters!"));
            }
            if (firstName.Length < MinLength)
            {
                return Result.Failure<LastName>(new Error("LastName.TooShort", $"LastName must be at least {MinLength} characters!"));
            }
            return new LastName(firstName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
