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
        public string Path { get; }
        public long WritePageSize { get; }
        public long NextPage { get; set; }

        public ModelStore(string path, long writePageSize)
        {
            Path = path;
            WritePageSize = writePageSize;
            NextPage += WritePageSize;
        }

        /// <summary>
        /// Serializes model to json file. Uses page size in constructor to determine write interval.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filename"></param>
        public void Write(IModel model, string filename)
        {
            // TODO: Add logic to deal with default WritePageSize == 0.
            // Should only write when model fully populated.
            if (model.CurrentSequenceId >= NextPage || WritePageSize == 0)
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
                NextPage += WritePageSize;
            }
        }

        /// <summary>
        /// Deserializes model from json file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
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
