using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Services.DatabaseServices.Contracts
{
    public interface IDatabaseJSONConnectivity<T>
    {
        List<T> ReadJSON(string jPath, string jName);
        void WriteJSON(List<T> list, string jPath, string jName);
    }
}
