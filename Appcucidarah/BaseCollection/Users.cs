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
    public class Users : IBaseViewModel<user>
    {
        public user Selected { get; set; }

        public ObservableCollection<user> Source { get; set; }

        public CollectionView SourceView { get; set; }
        public Users()
        {
            Source = new ObservableCollection<user>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            var task = GetCollectionFromDatabase();
            this.CompleteTask(task);
        }

        private async void CompleteTask(Task<List<user>> task)
        {
            var x = await task;
            foreach (var item in x)
            {
                Source.Add(item);
                SourceView.Refresh();
            }
        }


        

        private Task< List<user>> GetCollectionFromDatabase()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Users.Select();
                foreach(var item in result)
                {
                    var c = ContactType.Admin;
                   item.Kontak= db.Contacts.Where(O => O.UserId==item.Id && O.TipeKontak == c).FirstOrDefault();
                }
                          
                return Task.FromResult(result.ToList());
            }
        }

        public bool Delete(user t)
        {
            throw new NotImplementedException();
        }
    }
}
