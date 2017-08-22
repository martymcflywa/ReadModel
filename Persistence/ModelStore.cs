using System;
using System.IO;
using Newtonsoft.Json;
using ReadModel;
using ReadModel.Models;

namespace Persistence
{
    public class ModelStore : IPersist
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();
        private string Path { get; }

        public ModelStore(string path)
        {
            Path = path;
        }

        public void Write(IModel model, string filename)
        {
            Directory.CreateDirectory(Path);
            var filepath = System.IO.Path.Combine(Path, filename);
            using (var file = File.CreateText(filepath))
            {
                using (var writer = new JsonTextWriter(file))
                {
                    writer.Formatting = Formatting.Indented;
                    Serializer.Serialize(writer, model);
                }
            }
        }

        public T Read<T>(string filename)
        {
            var filepath = System.IO.Path.Combine(Path, filename);
            using (var file = File.OpenText(filepath))
            {
                if (!File.Exists(filepath))
                {
                    return default(T);
                }
                using (var reader = new JsonTextReader(file))
                {
                    return Serializer.Deserialize<T>(reader);
                }
            }
        }
    }
}
