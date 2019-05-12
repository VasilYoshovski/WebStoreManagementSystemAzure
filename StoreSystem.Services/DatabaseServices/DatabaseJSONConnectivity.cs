using Newtonsoft.Json;
using StoreSystem.Services.DatabaseServices.Contracts;
using System.Collections.Generic;
using System.IO;

namespace StoreSystem.Services.DatabaseServices
{
    public class DatabaseJSONConnectivity<T> : IDatabaseJSONConnectivity<T>
    {
        public List<T> ReadJSON(string jPath, string jName)
        {
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText($"{jPath}\\{jName}"));
        }

        public void WriteJSON(List<T> list, string jPath, string jName)
        {
            File.WriteAllText($"{jPath}\\{jName}", JsonConvert.SerializeObject(list, Formatting.Indented));
        }
    }
}
