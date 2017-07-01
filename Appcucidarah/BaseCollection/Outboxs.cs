using Appcucidarah.Models.Data;
using Appcucidarah.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Appcucidarah.BaseCollection
{
    public class Outboxs : IBaseViewModel<kotakterkirim>
    {
        public kotakterkirim Selected { get; set; }
        public ObservableCollection<kotakterkirim> Source { get; set; }
        public event OnChange OnChangeSource;
        public CollectionView SourceView { get; set; }
        public Outboxs()
        {
            Source = new ObservableCollection<kotakterkirim>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        private async void CompleteTask(Task<List<kotakterkirim>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }

        public void AddNewItem(kotakterkirim t)
        {
            if (OnChangeSource != null)
            {
                OnChangeSource(t);
            }
        }

        private Task<List<kotakterkirim>> GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var res = db.Outboxs.Select().OrderBy(O => O.WaktuTerkirim).ToList();

                return Task.FromResult(res);
            }
        }
        public bool Delete(kotakterkirim t)
        {
            throw new NotImplementedException();
        }
    }
}
