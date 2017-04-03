using DAL.DContext;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Appcucidarah.Models.Data;

namespace Appcucidarah
{
    public class OcphDbContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public OcphDbContext(string constring)
        {

            //this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public OcphDbContext()
        {
            this.ConnectionString = "Server=localhost;database=dbcucidarah;uid=root;password=";
        }

        public IRepository<pasien> Pacients { get { return new Repository<pasien>(this); } }
        public IRepository<jadwal> Jadwals{ get { return new Repository<jadwal>(this); } }
        public IRepository<dokter> Doctors { get { return new Repository<dokter>(this); } }
        public IRepository<perawat> Nurses{ get { return new Repository<perawat>(this); } }
        public IRepository<kontak> Contacts { get { return new Repository<kontak>(this); } }

        public IRepository<jadwalpasien> JadwalPasients{ get { return new Repository<jadwalpasien>(this); } }

        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}
