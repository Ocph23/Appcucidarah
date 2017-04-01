using Appcucidarah.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appcucidarah
{
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
    }
}
