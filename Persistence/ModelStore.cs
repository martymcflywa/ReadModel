using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ReadModel;
using ReadModel.Models.CustomerPayment;

namespace Persistence
{
    public class ModelStore : IPersist
    {
        public static JsonSerializer Serializer = new JsonSerializer();

        public ModelStore()
        {
            Serializer.Formatting = Formatting.Indented;
        }

        public void Write(IModel model, string path, string file)
        {
            Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, file), Serialize(model));
        }

        private static string Serialize(IModel model)
        {
            return JsonConvert.SerializeObject(model, Formatting.Indented);
        }
    }
}
