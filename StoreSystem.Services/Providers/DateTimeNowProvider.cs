using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Services.Providers
{
    public class DateTimeNowProvider : IDateTimeNowProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
