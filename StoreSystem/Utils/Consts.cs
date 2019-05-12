using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Utils
{
    internal static class Consts
    {
        internal const string INVALID_QUANTITY = "{0} {1} have to be more than {2}! It was passed {3}";
        internal const string INVALID_PROPERTY_LENGTH = "{0} {1} can not be less than {2} or more than {3} chars! It was passed {4}";
        internal const string NULL_PROPERTY_NAME = "{0} {1} can not be null!";
        internal const string NULL_OBJECT = "Passed {0} can not be null!";
        internal const string NOT_FOUND_OBJECT = "Passed {0} can not be found!";
    }
}