using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Appcucidarah.ViewModels
{
    public interface IBaseViewModel<T>
    {
        ObservableCollection<T> Source { get; set; }
        CollectionView SourceView { get; set; }
        T Selected { get; set; }
        bool Delete(T t);


    }
}