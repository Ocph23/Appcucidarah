using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Appcucidarah.ViewModels;
using OcphSMSLib.Models;
using Appcucidarah.Models.Data;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Appcucidarah.BaseCollection
{
    public delegate void OnChange(object view);
   
    public class Inboxs : IBaseViewModel<kotakmasuk>
    {
        public virtual kotakmasuk Selected { get; set; }

        public ObservableCollection<kotakmasuk> Source { get; set; }

        public CollectionView SourceView { get; set; }

        public event OnChange OnChangeSource;
        public Inboxs()
        {
            Source = new ObservableCollection<kotakmasuk>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        public void AddNewItem(kotakmasuk t)
        {
           
            if (OnChangeSource != null)
            {
                OnChangeSource(t);
            }
        }
        public bool Delete(kotakmasuk t)
        {
            throw new NotImplementedException();
        }

        private async void CompleteTask(Task<List<kotakmasuk>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        private Task<List<kotakmasuk>> GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var res = db.Inboxs.Select().OrderByDescending(O => O.WaktuTerima).ToList();
                return Task.FromResult(res);
            }
        }

    }
}
