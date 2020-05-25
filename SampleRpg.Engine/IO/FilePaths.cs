using System;
using System.IO;

namespace SampleRpg.Engine.IO
{
    public static class FilePaths
    {
        public static string NormalizePath ( string value ) => !String.IsNullOrEmpty(value) ? value.Replace('/', Path.DirectorySeparatorChar).Trim() : value;
    }
}
