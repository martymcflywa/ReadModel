using System.IO;
using Newtonsoft.Json;
using ReadModel;
using ReadModel.Models;

namespace Persistence
{
    public class ModelStore : IPersist
    {
        public static JsonSerializer Serializer = new JsonSerializer();
        private string Path { get; }

        public ModelStore(string path)
        {
            Path = path;
        }

        public void Write(IModel model, string filename)
        {
            Directory.CreateDirectory(Path);
            File.WriteAllText(System.IO.Path.Combine(Path, filename), Serialize(model));
        }

        private static string Serialize(IModel model)
        {
            return JsonConvert.SerializeObject(model, Formatting.Indented);
        }
    }
}
