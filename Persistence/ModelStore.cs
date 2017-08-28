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
        private readonly long _writePageSize;
        private long _processed;

        public ModelStore(string path, long writePageSize)
        {
            Path = path;
            _writePageSize = writePageSize;
            _processed = 0;
        }

        /// <summary>
        /// Serializes model to json file. Uses page size in constructor to determine write interval.
        /// </summary>
        /// <param name="model"></param>
        public void Write(IModel model)
        {
            // TODO: Add logic to deal with default WritePageSize == 0.
            // Should only write when model fully populated.
            _processed++;
            if (_processed > _writePageSize)
            {
                Directory.CreateDirectory(Path);
                var filepath = System.IO.Path.Combine(Path, model.Filename);
                using (var file = File.CreateText(filepath))
                {
                    using (var writer = new JsonTextWriter(file))
                    {
                        writer.Formatting = Formatting.Indented;
                        Serializer.Serialize(writer, model);
                    }
                }
                _processed = 0;
            }
        }

        /// <summary>
        /// Deserializes model from json file. Handles resuming by checking if file exists.
        /// If it does, generate model from file, else return new, empty instance of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public T Read<T>(string filename)
        {
            // Resume from json if file exists
            if (FileExists(filename))
            {
                var filepath = System.IO.Path.Combine(Path, filename);
                using (var file = File.OpenText(filepath))
                {
                    using (var reader = new JsonTextReader(file))
                    {
                        return Serializer.Deserialize<T>(reader);
                    }
                }
            }
            // Otherwise return a new instance, starts from sequenceId 1.
            return (T) Activator.CreateInstance(typeof(T), filename);
        }

        private bool FileExists(string filename)
        {
            return File.Exists(System.IO.Path.Combine(Path, filename));
        }
    }
}
