using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appcucidarah.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appcucidarah.Models.Data;
using static Appcucidarah.ViewModels.MainWindowVM;
using Appcucidarah.Forms.Messages;
using OcphSMSLib;

namespace Appcucidarah.ViewModels.Tests
{
    [TestClass()]
    public class MainWindowVMTests
    {
        MainWindowVM vm;
        Models.Data.user user = new Models.Data.user { Id = 0, UserName = "Ocph" };
        [TestMethod()]
        public void MainWindowVMTest()
        {
         
            vm = new MainWindowVM(user);
            Assert.AreEqual(true, vm.Modem.IsConnected);
            Assert.IsNotNull(vm.UserLogin);
            Assert.IsNotNull(vm.Doctors);
            Assert.IsNotNull(vm.Inboxs);
            Assert.IsNotNull(vm.Jadwals);
            Assert.IsNotNull(vm.Nurses);
            Assert.IsNotNull(vm.Outboxs);
            Assert.IsNotNull(vm.Users);
           



        }


        [TestMethod]
        public void TestDoctorsCollection()
        {
            vm = new MainWindowVM(user);
            var d = new Models.Data.dokter { IdDokter = 0, Nama = "Ocph" };
            var count = vm.Doctors.Source.Count;
            vm.Doctors.Source.Add(d);
            Assert.AreEqual(count + 1, vm.Doctors.Source.Count);
            Assert.AreEqual(count + 1, vm.Doctors.SourceView.Count);



        }
    }
}