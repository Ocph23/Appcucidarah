using Appcucidarah.Models.Data;
using System.Windows.Data;

namespace Appcucidarah.ViewModels.Messages
{
    internal class DaftarContactVM:DAL.BaseNotifyProperty
    {
        private string _search;

        public string Search {

            get { return _search; }
            set
            {
                _search = value;
                this.SourceView.Refresh();
                OnPropertyChange("Search");
            }
        }
        public DaftarContactVM()
        {
           var source = Helper.GetMainContex().Contacts.Source;
            this.SourceView = (CollectionView)CollectionViewSource.GetDefaultView(source);
            this.SourceView.Filter = this.Filter;
            this.SourceView.Refresh();
        }
        private bool Filter(object obj)
        {
            var pas = obj as kontak;
            if (!string.IsNullOrEmpty(Search))
            {
                if (pas.NamaKontak.ToLower().Contains(this.Search.ToLower()) || pas.NomorTelepon.ToLower().Contains(this.Search.ToLower()) || pas.TipeKontak.ToString().ToLower().Contains(Search.ToLower()))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }

        public CollectionView SourceView { get; private set; }
    }
}