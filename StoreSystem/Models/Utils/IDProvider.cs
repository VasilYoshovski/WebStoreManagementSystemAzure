using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Utils
{
    public static class IDProvider
    {
        private static int counter = 1;

        public static int GenerateUniqueID()
        {
            return counter++;
        }
    }
}
