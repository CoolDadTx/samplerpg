using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SampleRpg.Engine.IO
{
    public class JsonFileReader
    {
        public JsonFileReader ( string filename )
        {
            _filename = filename;                            
        }

        public IEnumerable<T> ReadArray<T> ( )
        {
            if (File.Exists(_filename))
            {
                var json = File.ReadAllText(_filename);
                var results = JsonSerializer.Deserialize<IEnumerable<T>>(json, _options);

                return results ?? Enumerable.Empty<T>();
            } else
                Trace.TraceWarning($"File '{_filename}' not found");

            return Enumerable.Empty<T>();
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
