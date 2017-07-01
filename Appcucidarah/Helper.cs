namespace Appcucidarah
{
    using Appcucidarah.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class Helper
    {
        internal static MainWindowVM GetMainContex()
        {
            var mw = App.Current.Windows[0] as MainWindow;
            MainWindowVM dc = ((MainWindowVM)mw.DataContext);
            return dc;

        }

        public static List<T> GetEnumCollection<T>()
        {
            var enums = Enum.GetValues(typeof(T));
            List<T> list = new List<T>();
            foreach (T item in enums)
            {
                list.Add(item);
            }
            return list;
        }

        public static int MaxSlot
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["MaxSlot"]);
            }
        }


    }



    [ValueConversion(typeof(object), typeof(Type))]
    public class TypeConverter : ConverterMarkupExtension<TypeConverter>
    {
        public TypeConverter()
        {
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.GetType();
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class ConverterMarkupExtension<T> : MarkupExtension, IValueConverter where T : class, new()
    {
        private static T _converter = null;

        public ConverterMarkupExtension()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new T());
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
