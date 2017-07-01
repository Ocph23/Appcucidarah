using Appcucidarah.BaseCollection;
using Appcucidarah.Models.Data;
using System;

namespace Appcucidarah.ViewModels.Messages
{
    internal class OutboxVM
    {
        private kotakterkirim _outbox;

        public OutboxVM()
        {
            this.Collection = Helper.GetMainContex().Outboxs;
            this.Collection.OnChangeSource += Collection_onChangeSource;
            this.Collection.SourceView.Refresh();

            this.DeleteCommand = new CommandHandler
            {
                CanExecuteAction = x => {
                    if (this.SelectedItem != null)
                        return true;
                    else
                        return false;
                },
                ExecuteAction = x => {
                    using (var db = new OcphDbContext())
                    {
                        if (db.Outboxs.Delete(O => O.Id == SelectedItem.Id))
                        {
                            Collection.Source.Remove(SelectedItem);
                            Collection.SourceView.Refresh();
                        }
                    }

                }
            };


        }

        public kotakterkirim SelectedItem
        {
            get { return _outbox; }
            set
            {
                _outbox = value;
                using (var db = new OcphDbContext())
                {
                    if (_outbox.Status == false)
                    {
                        _outbox.Status = true;
                        if (!db.Outboxs.Update(O => new { O.Status }, _outbox, O => O.Id == _outbox.Id))
                        {
                            _outbox.Status = false;
                        }
                    }

                }

            }
        }

        private void Collection_onChangeSource(object t)
        {
            App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
            {
                var data = (kotakterkirim)t;
                this.Collection.Source.Add(data);
                this.Collection.SourceView.Refresh();
            }));
        }

        public Outboxs Collection { get; private set; }
        public CommandHandler DeleteCommand { get; private set; }
    }
}