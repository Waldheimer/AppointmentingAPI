using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Appointmenting.API.Domain.Primitives
{
    public class ConversionResult
    {
        public bool DateSuccess { get; set; } = false;
        public bool TimeSuccess { get;set; } = false;
        public DateOnly Day { get; set; } = DateOnly.MinValue;
        public TimeOnly Time { get; set; } = TimeOnly.MinValue;

        public ConversionResult(string value) 
        {
            TryConversion(value);
        }
        public ConversionResult(string value1, string value2)
        {
            TryConversion(value1);
            TryConversion(value2);
        }

        private void TryConversion(string value)
        {
            if (!DateSuccess)
            {
                TryDateConversion(value);
            }
            if(!TimeSuccess)
            {
                TryTimeConversion(value);
            }
        }

        private void TryDateConversion(string value)
        {
            DateOnly result;
            var success = DateOnly.TryParse(value, out result);
            if (success)
            {
                DateSuccess = true;
                Day = result;
            }
        }
        private void TryTimeConversion(string value)
        {
            TimeOnly result;
            var success = TimeOnly.TryParse(value, out result);
            if (success)
            {
                TimeSuccess = true;
                Time = result;
            }
        }
    }
}
