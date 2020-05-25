using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Caching;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SampleRpg.Wpf.Converters
{
    public class FileToBitmapConverter : IValueConverter
    {
        public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if (value is string filename)
            {
                var bitmap = s_cache.Get(filename) as BitmapImage;
                if (bitmap == null)
                {
                    bitmap = new BitmapImage(new Uri(filename, UriKind.Absolute));
                    s_cache.Add(filename, bitmap, new CacheItemPolicy() {
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    });
                };

                return bitmap;
            } else
                return null;
        }

        public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture ) => null;


        #region Private Members

        private static readonly MemoryCache s_cache = new MemoryCache("FileToBitmapConverter");

        #endregion
    }
}
