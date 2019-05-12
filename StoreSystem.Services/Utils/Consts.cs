using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Services.Utils
{
    class Consts
    {
        internal static string ObjectIDNotExist = "Can not find {0} with ID {1}.";
        internal static string ObjectNameNotExist = "Can not find {0} with name {1}.";

        internal static string QuantityNotEnough = "There is not enough quantity of product \"{0}\". There is {1} in store.";
        internal static string DateError = "Invalid date. Expired date {0} can not be before order date {0}";

        internal const string AllText = "*";
    }
}
