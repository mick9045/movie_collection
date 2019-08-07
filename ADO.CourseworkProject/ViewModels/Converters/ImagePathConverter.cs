namespace FilmRent.ViewModels.Converter
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Data;

    public class ImagePathConverter
        : IValueConverter
    {
        #region IValueConverter members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value as string;
            if (path != null)
            {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion //IvalueConverter members

    }
}
