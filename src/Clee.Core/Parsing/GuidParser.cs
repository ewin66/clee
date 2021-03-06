using System;

namespace Clee.Parsing
{
    public class GuidParser : IValueParser
    {
        public bool TryParse(string input, IFormatProvider format, out object result)
        {
            Guid value;
            var isSuccess = Guid.TryParse(input, out value);

            result = value;
            return isSuccess;
        }
    }
}