# Host: localhost  (Version: 5.6.21)
# Date: 2017-03-30 00:27:40
# Generator: MySQL-Front 5.3  (Build 4.4)

/*!40101 SET NAMES utf8 */;

#
# Source for table "dokter"
#

CREATE TABLE `dokter` (
  `IdDokter` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(50) DEFAULT NULL,
  `TempatLahir` varchar(100) DEFAULT NULL,
  `TanggalLahir` date DEFAULT NULL,
  `JenisKelamin` enum('Pria','Wanita') DEFAULT 'Pria',
  `Agama` enum('Islam','Kristen','Katolik','Hindu','Budha') NOT NULL DEFAULT 'Islam',
  `Alamat` varchar(100) DEFAULT NULL,
  `IdKontak` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdDokter`),
  UNIQUE KEY `NomorTelepon` (`IdKontak`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

#
# Data for table "dokter"
#

INSERT INTO `dokter` VALUES (5,'Test','Test','2017-03-16','Pria','Kristen','Test',3);

#
# Source for table "jadwal"
#

CREATE TABLE `jadwal` (
  `IdJadwal` int(11) NOT NULL AUTO_INCREMENT,
  `HariPertama` enum('Senin','Selasa','Rabu','Kamis','Jumat','Sabtu','Selasa','Rabu','Kamis','Jumat','Sabt') DEFAULT 'Senin',
  `HariKedua` enum('Senin','Selasa','Rabu','Kamis','Jumat','Sabtu','Selasa','Rabu','Kamis','Jumat','Sabt') DEFAULT 'Senin',
  `Shif` enum('Pagi','Siang','Malam') DEFAULT 'Pagi',
  `JamMulai` datetime DEFAULT NULL,
  `JamAkhir` datetime DEFAULT NULL,
  PRIMARY KEY (`IdJadwal`),
  UNIQUE KEY `HariPertama` (`HariPertama`,`HariKedua`,`Shif`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "jadwal"
#


#
# Source for table "kontak"
#

CREATE TABLE `kontak` (
  `IdKontak` int(11) NOT NULL AUTO_INCREMENT,
  `NomorTelepon` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`IdKontak`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

#
# Data for table "kontak"
#

INSERT INTO `kontak` VALUES (1,'01'),(2,'02'),(3,'11'),(4,'12');

#
# Source for table "kotakmasuk"
#

CREATE TABLE `kotakmasuk` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdKontak` int(11) DEFAULT NULL,
  `WaktuTerima` datetime DEFAULT NULL,
  `IsiPesan` text,
  `Status` enum('True','False') DEFAULT 'False',
  PRIMARY KEY (`Id`),
  KEY `IdKontak` (`IdKontak`),
  CONSTRAINT `kotakmasuk_ibfk_1` FOREIGN KEY (`IdKontak`) REFERENCES `kontak` (`IdKontak`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "kotakmasuk"
#


#
# Source for table "kotakterkirim"
#

CREATE TABLE `kotakterkirim` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdKontak` int(11) DEFAULT NULL,
  `WaktuTerkirim` datetime DEFAULT NULL,
  `IsiPesan` text,
  `Status` enum('True','False') DEFAULT 'True',
  PRIMARY KEY (`Id`),
  KEY `IdKontak` (`IdKontak`),
  CONSTRAINT `kotakterkirim_ibfk_1` FOREIGN KEY (`IdKontak`) REFERENCES `kontak` (`IdKontak`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "kotakterkirim"
#


#
# Source for table "pasien"
#

CREATE TABLE `pasien` (
  `IdPasien` int(11) NOT NULL AUTO_INCREMENT,
  `NomorPasien` varchar(20) DEFAULT NULL,
  `IdDokter` int(11) DEFAULT NULL,
  `Nama` varchar(50) DEFAULT NULL,
  `TempatLahir` varchar(100) DEFAULT NULL,
  `TanggalLahir` date DEFAULT NULL,
  `JenisKelamin` enum('Pria','Wanita') DEFAULT 'Pria',
  `Agama` enum('Islam','Kristen','Katolik','Hindu','Budha') DEFAULT 'Islam',
  `Alamat` varchar(100) DEFAULT NULL,
  `IdKontak` int(11) DEFAULT NULL,
  `Status` enum('True','False') DEFAULT 'True',
  PRIMARY KEY (`IdPasien`),
  KEY `IdDokter` (`IdDokter`),
  CONSTRAINT `pasien_ibfk_1` FOREIGN KEY (`IdDokter`) REFERENCES `dokter` (`IdDokter`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

#
# Data for table "pasien"
#

INSERT INTO `pasien` VALUES (1,NULL,NULL,'c',NULL,NULL,NULL,NULL,NULL,11,'True'),(2,NULL,NULL,'d',NULL,NULL,NULL,NULL,NULL,12,'True');

#
# Source for table "perawat"
#

CREATE TABLE `perawat` (
  `IdPerawat` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(50) DEFAULT NULL,
  `TempatLahir` varchar(100) DEFAULT NULL,
  `TanggalLahir` date DEFAULT NULL,
  `JenisKelamin` enum('Pria','Wanita') DEFAULT 'Pria',
  `Agama` enum('Islam','Kristen','Katolik','Hindu','Budha') DEFAULT 'Islam',
  `Alamat` varchar(100) DEFAULT NULL,
  `IdKontak` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdPerawat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "perawat"
#


#
# Source for table "transaksijadwal"
#

CREATE TABLE `transaksijadwal` (
  `IdTransaksiJadwal` int(11) NOT NULL AUTO_INCREMENT,
  `IdPasien` int(11) DEFAULT NULL,
  `IdJadwal` int(11) DEFAULT NULL,
  `Status` enum('True','False') DEFAULT 'True',
  PRIMARY KEY (`IdTransaksiJadwal`),
  KEY `IdPasien` (`IdPasien`),
  KEY `IdJadwal` (`IdJadwal`),
  CONSTRAINT `transaksijadwal_ibfk_1` FOREIGN KEY (`IdPasien`) REFERENCES `pasien` (`IdPasien`),
  CONSTRAINT `transaksijadwal_ibfk_2` FOREIGN KEY (`IdJadwal`) REFERENCES `jadwal` (`IdJadwal`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "transaksijadwal"
#


#
# Source for table "cucidarah"
#

CREATE TABLE `cucidarah` (
  `IdCuciDarah` int(11) NOT NULL AUTO_INCREMENT,
  `IdTransaksiJadwal` int(11) DEFAULT NULL,
  `JamMulai` datetime DEFAULT NULL,
  `JamAkhir` datetime DEFAULT NULL,
  PRIMARY KEY (`IdCuciDarah`),
  KEY `IdTransaksiJadwal` (`IdTransaksiJadwal`),
  CONSTRAINT `cucidarah_ibfk_1` FOREIGN KEY (`IdTransaksiJadwal`) REFERENCES `transaksijadwal` (`IdTransaksiJadwal`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "cucidarah"
#


#
# Source for table "transaksicucidarah"
#

CREATE TABLE `transaksicucidarah` (
  `IdTransaksiCuciDarah` int(11) NOT NULL AUTO_INCREMENT,
  `IdCuciDarah` int(11) DEFAULT NULL,
  `IdPerawat` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdTransaksiCuciDarah`),
  KEY `IdCuciDarah` (`IdCuciDarah`),
  KEY `IdPerawat` (`IdPerawat`),
  CONSTRAINT `transaksicucidarah_ibfk_1` FOREIGN KEY (`IdCuciDarah`) REFERENCES `cucidarah` (`IdCuciDarah`),
  CONSTRAINT `transaksicucidarah_ibfk_2` FOREIGN KEY (`IdPerawat`) REFERENCES `perawat` (`IdPerawat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "transaksicucidarah"
#


#
# Source for table "user"
#

CREATE TABLE `user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NamaLengkap` varchar(50) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "user"
#

