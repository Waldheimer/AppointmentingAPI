using Appointmenting.API.Domain.Primitives;

namespace Appointmenting.API.Domain.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public const int MaxLength = 50;
        public FirstName(string value) 
        { 
            Value = value; 
        }

        public string Value { get; }
        public static FirstName Default = new FirstName(string.Empty);

        public static Result<FirstName> Create(string firstName)
        {
            if(firstName.Length > MaxLength)
            {
                return Result.Failure<FirstName>(new Error("FirstName.TooLong", $"FirstName must be max {MaxLength} characters!"));
            }
            return new FirstName(firstName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
