using System.Collections;

namespace WIMSystem.Utils
{
    public static class Validators
    {
        public static bool IsLengthValid(string stringForChecking, int min, int max)
        {
            return (stringForChecking.Length < min || stringForChecking.Length > max);
        }

        public static bool IsValidQuantityOfObjects(ICollection list, int min)
        {
            return (list.Count < min);
        }

        public static bool IsNullValue(object objectForChecking)
        {
            return (objectForChecking == null);
        }

        public static bool IsNullorEmptyValue(string stringForChecking)
        {
            return string.IsNullOrEmpty(stringForChecking);
        }
    }
}

