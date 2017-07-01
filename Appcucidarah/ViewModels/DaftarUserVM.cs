using Appcucidarah.BaseCollection;

namespace Appcucidarah.ViewModels
{
    internal class DaftarUserVM
    {
        public DaftarUserVM()
        {
            this.Collection = Helper.GetMainContex().Users;
            Collection.SourceView.Refresh();
        }

        public Users Collection { get;  set; }
    }
}