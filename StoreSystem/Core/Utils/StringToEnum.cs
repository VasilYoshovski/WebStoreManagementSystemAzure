using System;

namespace WIMSystem.Core.Utils
{
    public static class StringToEnum<T> where T : struct
    {
        public static T Convert(string enumToParse)
        {
            T result = default(T);
            bool enumParseResult = false;
            enumParseResult = Enum.TryParse(enumToParse, true, out result);
            if (!enumParseResult)
            {
                throw new ArgumentOutOfRangeException($"Invalid {nameof(T)} type");
            }
            return result;
        }
    }
}
