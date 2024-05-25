using Appointmenting.API.Domain.Primitives;

namespace Appointmenting.API.Infrastructure.Validators
{
    public class DateTimeValidator
    {
        //----------------------------------------------------------------
        //  Check if the given parameter/s can be parsed to either
        //  DateOnly or TimeOnly and returns the result in a ConversionResult
        //  Object that contains the success and the the conversion values
        //----------------------------------------------------------------
        public ConversionResult TryValidate(string value)
        {
            return new ConversionResult(value);
        }
        public ConversionResult TryValidate(string value1, string value2)
        {
            return new ConversionResult(value1, value2);
        }

    }
}
