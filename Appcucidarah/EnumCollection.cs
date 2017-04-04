using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appcucidarah
{
    class EnumCollection
    {
    }

    public enum Religion
    {
       None, Islam, Kristen, Katolik, Hindu, Budha
        
    }
    public enum Gender
    {
        None, Pria, Wanita

    }
    public enum Shif
    {
        None, Pagi, Siang, Sore
    }
    public enum Day
    {
        None, Senin, Selasa, Rabu, Kamis, Jumat, Sabtu
    }

    public enum ContactType
    {
        Umum, Pasient, Dokter, Perawat
    }

    public enum ProccessStatus
    {
        New,Start,Stop,Save
    }

}
