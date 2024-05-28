using Appointmenting.API.Domain.Primitives;

namespace Appointmenting.API.Domain.ValueObjects
{
    public sealed class LastName : ValueObject
    {
        public const int MaxLength = 50;
        public LastName(string value)
        {
            Value = value;
        }

        public string Value { get; }
        public static LastName Default => new LastName(string.Empty);

        public static Result<LastName> Create(string lastName)
        {
            if (lastName.Length > MaxLength)
            {
                return Result.Failure<LastName>(new Error("LastName.TooLong", $"LastName must be max {MaxLength} characters!"));
            }
            return new LastName(lastName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
