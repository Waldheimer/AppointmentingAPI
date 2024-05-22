using Appointmenting.API.Domain.Primitives;

namespace Appointmenting.API.Domain.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public const int MaxLength = 50;
        public const int MinLength = 3;
        public FirstName(string value) 
        { 
            Value = value; 
        }

        public string Value { get; }

        public static Result<FirstName> Create(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return Result.Failure<FirstName>(new Error("FirstName.Empty", "FirstName is empty"));
            }
            if(firstName.Length > MaxLength)
            {
                return Result.Failure<FirstName>(new Error("FirstName.TooLong", $"FirstName must be max {MaxLength} characters!"));
            }
            if(firstName.Length < MinLength)
            {
                return Result.Failure<FirstName>(new Error("FirstName.TooShort", $"FirstName must be at least {MinLength} characters!"));
            }
            return new FirstName(firstName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
