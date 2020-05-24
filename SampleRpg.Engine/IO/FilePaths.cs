using System;
using System.IO;

namespace SampleRpg.Engine.IO
{
    public static class FilePaths
    {
        public static bool FileExists ( string filePath ) => File.Exists(filePath);

        public static string GetDataFilePath ( string relativePath ) => Path.Combine("data", relativePath);            
    }
}
