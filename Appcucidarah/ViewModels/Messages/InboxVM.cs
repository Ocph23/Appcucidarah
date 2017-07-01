using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Appcucidarah.BaseCollection;
using Appcucidarah.Models.Data;
using FirstFloor.ModernUI.Windows.Controls;

namespace Appcucidarah.ViewModels.Messages
{
   
    public class InboxVM
    {
        private kotakmasuk _inbox;

        public InboxVM()
        {
            this.Collection = Helper.GetMainContex().Inboxs;
            this.Collection.OnChangeSource += Collection_onChangeSource;
            this.Collection.SourceView.Refresh();

            this.DeleteCommand = new CommandHandler
            { CanExecuteAction = x => {
                if (this.SelectedItem != null)
                    return true;
                else
                    return false;
            }, ExecuteAction = x => {
                using (var db = new OcphDbContext())
                {
                   if( db.Inboxs.Delete(O => O.Id == SelectedItem.Id))
                    {
                        Collection.Source.Remove(SelectedItem);
                        Collection.SourceView.Refresh();
                    }
                }

            } };


        }

        public kotakmasuk SelectedItem
        {
            get { return _inbox; }
            set { _inbox = value;
                using (var db = new OcphDbContext())
                {
                    if(_inbox.Status==false)
                    {
                        _inbox.Status = true;
                        if (!db.Inboxs.Update(O => new { O.Status }, _inbox, O => O.Id == _inbox.Id))
                        {
                            _inbox.Status = false;
                        }
                    }
                 
                }

            }
        }

        private void Collection_onChangeSource(object t)
        {
            var data = (kotakmasuk)t;
            App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
             {
                 this.Collection.Source.Add(data);
                 this.Collection.SourceView.Refresh();
             }));
        }

        public Inboxs Collection { get; private set; }
        public CommandHandler DeleteCommand { get; private set; }
        public CommandHandler ForwardCommand { get; private set; }
    }
}
