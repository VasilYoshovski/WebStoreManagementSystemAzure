using System;

namespace StoreSystem.Services.Providers
{
    public interface IDateTimeNowProvider
    {
        DateTime Now { get; }
    }
}