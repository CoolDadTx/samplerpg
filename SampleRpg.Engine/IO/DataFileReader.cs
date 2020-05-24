using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SampleRpg.Engine.IO
{
    public class DataFileReader
    {
        public DataFileReader ( string filename )
        {
            _filename = filename;                 
        }

        public T Read<T> ()
        {
            var json = File.ReadAllText(_filename);

            return JsonSerializer.Deserialize<T>(json, _options);
        }

        public IEnumerable<T> ReadAll<T> ( )
        {
            var json = File.ReadAllText(_filename);
            var results = JsonSerializer.Deserialize<IEnumerable<T>>(json, _options);

            return results ?? Enumerable.Empty<T>();
        }

        #region Private Members

        private readonly string _filename;

        private readonly JsonSerializerOptions _options = new JsonSerializerOptions() {
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true,
            ReadCommentHandling = JsonCommentHandling.Skip            
        };
            
        #endregion
    }
}
