namespace Appointmenting.API.Domain.Primitives
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null");

        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public static implicit operator string(Error error) => error.Code;

        public static bool operator ==(Error? e1, Error? e2)
        {
            if(e1 is null && e2 is null)
            {
                return true;
            }
            if(e1 is null || e2 is null)
            {
                return false;
            }
            return e1.Equals(e2);
        }
        public static bool operator !=(Error? e1, Error? e2) => !(e1 == e2);

        public bool Equals(Error? other)
        {
            if (other is null)
            {
                return false;
            }

            return Code == other.Code && Message == other.Message;
        }
        public override bool Equals(object? obj) => obj is Error error && Equals(error);

        public override int GetHashCode() => HashCode.Combine(Code, Message);

        public override string ToString() => Code;
    }
}
