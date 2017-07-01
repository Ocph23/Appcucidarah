using Appcucidarah.Models.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace Appcucidarah.ViewModels
{
    internal class UserVM:user
    {
        public UserVM()
        {
            Kontak = new kontak();
            this.ResetCommand = new CommandHandler
            {
                CanExecuteAction = x => true,
                ExecuteAction = x =>
                {
                    this.Id = 0;
                    this.NamaLengkap = string.Empty;
                    this.UserName = string.Empty;
                    this.Password = string.Empty;
                    this.PasswordConfirm = string.Empty;
                }
            };

            this.SaveCommand = new CommandHandler
            {
                CanExecuteAction = x => {
                    if(!string.IsNullOrEmpty(NamaLengkap)&& !string.IsNullOrEmpty(Password)&&Password==PasswordConfirm && !string.IsNullOrEmpty(Kontak.NomorTelepon))
                        return true;
                   else
                        return false;
                },
                ExecuteAction = x =>
                {
                    using (var db = new OcphDbContext())
                    {
                        var trans = db.Connection.BeginTransaction();
                        try
                        {
                            user u = this;
                            u.Kontak.NamaKontak = this.NamaLengkap;
                            var id = db.Users.InsertAndGetLastID(u);
                            if(id>0)
                            {
                                u.Kontak.UserId = id;
                                u.Kontak.TipeKontak = ContactType.Admin;
                               u.Kontak.IdKontak= db.Contacts.InsertAndGetLastID(u.Kontak);
                                trans.Commit();
                                var users = Helper.GetMainContex().Users;
                                users.Source.Add(u);
                                users.SourceView.Refresh();
                                ModernDialog.ShowMessage("User Berhasil Ditambah", "Info", MessageBoxButton.OK);
                            }
                            else
                            {
                                throw new System.Exception("User Gagal Ditambah");
                            }
                        }
                        catch (System.Exception ex)
                        {
                            trans.Rollback();
                            ModernDialog.ShowMessage(ex.Message, "Info", MessageBoxButton.OK);
                            
                        }
                      
                    }
                }
            };
        }

        public CommandHandler ResetCommand { get; private set; }
        public CommandHandler SaveCommand { get; private set; }
    }
}