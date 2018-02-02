# Host: localhost  (Version 5.6.14)
# Date: 2017-11-02 04:00:54
# Generator: MySQL-Front 6.0  (Build 2.20)


#
# Structure for table "dokter"
#

DROP TABLE IF EXISTS `dokter`;
CREATE TABLE `dokter` (
  `IdDokter` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(50) DEFAULT NULL,
  `TempatLahir` varchar(100) DEFAULT NULL,
  `TanggalLahir` date DEFAULT NULL,
  `JenisKelamin` enum('Pria','Wanita') DEFAULT 'Pria',
  `Agama` enum('Islam','Kristen','Katolik','Hindu','Budha') NOT NULL DEFAULT 'Islam',
  `Alamat` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`IdDokter`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

#
# Data for table "dokter"
#

INSERT INTO `dokter` VALUES (9,'Ajenk Krisyanto Kungkung','Makassar','1988-03-26','Wanita','Kristen','Tanah Hitam Saja'),(10,'asdasd','asdasd','1970-01-01','Wanita','Islam','asdasd');

#
# Structure for table "jadwal"
#

DROP TABLE IF EXISTS `jadwal`;
CREATE TABLE `jadwal` (
  `IdJadwal` int(11) NOT NULL AUTO_INCREMENT,
  `HariPertama` enum('Senin','Selasa','Rabu','Kamis','Jumat','Sabtu','Selasa','Rabu','Kamis','Jumat','Sabt') DEFAULT 'Senin',
  `HariKedua` enum('Senin','Selasa','Rabu','Kamis','Jumat','Sabtu','Selasa','Rabu','Kamis','Jumat','Sabt') DEFAULT 'Senin',
  `Shif` enum('Pagi','Siang','Sore') DEFAULT 'Pagi',
  `JamMulai` time DEFAULT NULL,
  `JamAkhir` time DEFAULT NULL,
  PRIMARY KEY (`IdJadwal`),
  UNIQUE KEY `HariPertama` (`HariPertama`,`HariKedua`,`Shif`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

#
# Data for table "jadwal"
#

INSERT INTO `jadwal` VALUES (1,'Senin','Rabu','Pagi','08:00:00','11:00:00'),(4,'Senin','Rabu','Siang','01:00:00','03:00:00'),(8,'Selasa','Jumat','Pagi','08:00:00','11:00:00'),(9,'Selasa','Jumat','Sore','15:00:00','17:00:00');

#
# Structure for table "kontak"
#

DROP TABLE IF EXISTS `kontak`;
CREATE TABLE `kontak` (
  `IdKontak` int(11) NOT NULL AUTO_INCREMENT,
  `NomorTelepon` varchar(20) NOT NULL DEFAULT '0',
  `TipeKontak` enum('Umum','Pasient','Dokter','Perawat','Admin') NOT NULL DEFAULT 'Umum',
  `UserId` int(11) NOT NULL DEFAULT '0',
  `NamaKontak` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`IdKontak`),
  UNIQUE KEY `NomorTelepon` (`NomorTelepon`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;

#
# Data for table "kontak"
#

INSERT INTO `kontak` VALUES (8,'081148100000','Dokter',9,'081148100000'),(10,'085656545','Dokter',10,'asdasd'),(11,'081145454545','Perawat',1,'asdas'),(12,'08114801030','Pasient',1,'Poke'),(13,'082248501162','Pasient',2,'Niny'),(14,'081148545454','Pasient',3,'Ida Irjayanti'),(15,'082248249012','Pasient',4,'Manot'),(22,'08114810279','Admin',1,'Ocph'),(23,'082248248980','Admin',2,'Sumanto'),(24,'081318385768','Pasient',5,'Nayaq');

#
# Structure for table "kotakmasuk"
#

DROP TABLE IF EXISTS `kotakmasuk`;
CREATE TABLE `kotakmasuk` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdKontak` int(11) DEFAULT NULL,
  `WaktuTerima` datetime DEFAULT NULL,
  `IsiPesan` text,
  `Status` enum('True','False') DEFAULT 'False',
  `Pengirim` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=latin1;

#
# Data for table "kotakmasuk"
#

INSERT INTO `kotakmasuk` VALUES (54,0,'2017-05-11 09:24:43','Testimoni','True','+628114810279'),(55,0,'2017-05-11 11:21:08',' Selamat! Anda berhak membeli PAKET PAS untuk Nelpon','True','846976757977836976'),(56,0,'2017-05-11 09:08:47',' Pindah#sore','True','+628114810279'),(57,0,'2017-05-12 23:05:12',' Pindah#sore','True','+628114810279'),(58,0,'2017-05-15 11:30:10',' Info#minumobat','True','+628114810279'),(60,0,'2017-05-18 10:26:26','Pindah sore','True','+6282248249012'),(62,0,'2017-05-18 09:47:17','Ketik pindah#pagi kirim ke 085244117204','True','+6281318385768'),(63,0,'2017-05-18 09:45:01','Ketik pindah#pagi','True','+6281318385768');

#
# Structure for table "kotakterkirim"
#

DROP TABLE IF EXISTS `kotakterkirim`;
CREATE TABLE `kotakterkirim` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdKontak` int(11) DEFAULT NULL,
  `WaktuTerkirim` datetime DEFAULT NULL,
  `IsiPesan` text,
  `Status` enum('True','False') DEFAULT 'True',
  `Penerima` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=latin1;

#
# Data for table "kotakterkirim"
#

INSERT INTO `kotakterkirim` VALUES (1,12,'2017-05-07 19:53:20','asdasd','True','Poke'),(15,0,'2017-05-12 23:23:15','Ada Permintaan Pinda Nomor Pasien 0565654','True','014801030'),(16,0,'2017-05-12 23:23:55','Permintaan Anda Kami Izinkan','True','014810279'),(17,0,'2017-05-12 23:24:50','Shif Sore Tidak Tersedia','True','014810279'),(19,0,'2017-05-13 00:40:45','Anda Sudah  Mengajukan Permohonan Pindah Sebelumnya','True','014810279'),(20,0,'2017-05-13 01:08:57','Perintah Anda Salah','True','014810279'),(21,0,'2017-05-13 01:09:41','0. 08:31:531. 14:31:532. 20:31:533. 1.02:31:53','True','014810279'),(22,0,'2017-05-19 12:00:00','Terjadi Kesalahan Hubungi Admin','True','0248501162'),(30,0,'2017-05-19 12:24:07','Anda Sudah Ada di Shif Pagi','True','0248249012'),(31,0,'2017-05-19 12:24:47','Ada Permintaan Pinda Nomor Pasien 051212','True','0248248980'),(32,0,'2017-05-19 12:26:30','Shif Sore Tidak Tersedia','True','0248249012'),(36,0,'2017-05-19 12:39:22','Permintaan Anda Tidak Diizinkan','True','0248249012'),(37,0,'2017-05-19 12:39:40','Permintaan Anda Tidak Diizinkan','True','0248501162'),(38,0,'2017-05-19 12:42:00','Permintaan Anda Tidak Diizinkan','True','0248249012'),(39,0,'2017-05-19 12:44:26','Ada Permintaan Pindah, Nomor Pasien 051212','True','0248248980'),(40,0,'2017-05-19 12:44:26','Ada Permintaan Pindah, Nomor Pasien 051212','True','0248248980'),(41,0,'2017-05-19 12:46:18','Permintaan Anda Tidak Diizinkan','True','0248249012'),(42,0,'2017-05-19 12:48:58','Permintaan Anda Tidak Diizinkan','True','0248249012'),(43,0,'2017-05-19 12:49:26','Ada Permintaan Pindah, Nomor Pasien 051212','True','0248248980'),(44,0,'2017-05-19 12:51:20','Ada Permintaan Pindah, Nomor Pasien 051212','True','014810279'),(45,0,'2017-05-19 12:51:20','Ada Permintaan Pindah, Nomor Pasien 051212','True','014810279'),(46,0,'2017-05-19 12:51:31','Ada Permintaan Pindah, Nomor Pasien 051212','True','0248248980'),(47,0,'2017-05-19 12:52:45','Ada Permintaan Pindah, Nomor Pasien 051212','True','0248248980'),(50,0,'2017-05-19 12:56:12','Permintaan Anda Tidak Diizinkan','True','0248249012'),(51,0,'2017-05-19 12:56:43','Ada Permintaan Pindah, Nomor Pasien 051212','True','0248248980'),(52,0,'2017-05-19 12:57:37','Ada Permintaan Pindah, Nomor Pasien 051212','True','0248248980'),(53,0,'2017-05-19 12:59:31','Jam Mimum Obat :\r\n1. 17:07:11\r\n2. 23:07:11\r\n3. 1.05:07:11\r\n4. 1.11:07:11\r\n','True','0248249012'),(54,0,'2017-05-19 13:01:41','Jadwal Anda Setiap Hari : Selasa&JumatShif Pagi','True','0248249012'),(55,0,'2017-06-02 08:30:17','Saatnya Anda Minum Obat','True','014801030'),(56,0,'2017-06-02 08:31:17','Saatnya Anda Minum Obat','True','014801030'),(57,0,'2017-06-02 08:32:17','Saatnya Anda Minum Obat','True','014801030'),(58,0,'2017-06-02 08:33:17','Saatnya Anda Minum Obat','True','014801030'),(60,0,'2017-06-07 10:30:59','test','True','014810279'),(61,0,'2017-06-10 08:50:35','Saatnya Anda Minum Obat','True','014801030'),(62,0,'2017-06-11 08:58:18','Saatnya Anda Minum Obat','True','014801030'),(63,0,'2017-06-11 09:00:18','Saatnya Anda Minum Obat','True','014801030'),(64,0,'2017-06-11 09:04:18','Saatnya Anda Minum Obat','True','014801030'),(65,0,'2017-06-11 09:06:18','Saatnya Anda Minum Obat','True','014801030'),(66,0,'2017-06-12 09:10:13','Saatnya Anda Minum Obat','True','014801030'),(67,0,'2017-06-12 09:12:13','Saatnya Anda Minum Obat','True','014801030');

#
# Structure for table "pasien"
#

DROP TABLE IF EXISTS `pasien`;
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
  `Status` enum('true','false') DEFAULT 'true',
  PRIMARY KEY (`IdPasien`),
  KEY `IdDokter` (`IdDokter`),
  CONSTRAINT `pasien_ibfk_1` FOREIGN KEY (`IdDokter`) REFERENCES `dokter` (`IdDokter`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

#
# Data for table "pasien"
#

INSERT INTO `pasien` VALUES (1,'0565654',9,'Poke','Makassar','2001-01-02','Pria','Islam','Jln. Klofcamp','true'),(2,'085458523',9,'Niny','Arso','2001-01-12','Wanita','Islam','Tanah Hitam','true'),(3,'08565',9,'Ida Irjayanti','Jayapura','2001-12-06','Wanita','Islam','Kloffcamp','true'),(4,'051212',10,'Manto Pasient','Jayapura','2001-01-25','Pria','Islam','Tanah Hitam','true'),(5,'12321',9,'Nayaq','Jayapura','1990-01-01','Wanita','Kristen','Jln. Organda','true');

#
# Structure for table "jadwalpasien"
#

DROP TABLE IF EXISTS `jadwalpasien`;
CREATE TABLE `jadwalpasien` (
  `IdJadwalPasien` int(11) NOT NULL AUTO_INCREMENT,
  `IdPasien` int(11) DEFAULT NULL,
  `IdJadwal` int(11) DEFAULT NULL,
  `Status` enum('Normal','Pindah','None') NOT NULL DEFAULT 'Normal',
  `IdPindah` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`IdJadwalPasien`),
  UNIQUE KEY `IdPasien` (`IdPasien`),
  KEY `IdJadwal` (`IdJadwal`),
  CONSTRAINT `jadwalpasien_ibfk_2` FOREIGN KEY (`IdJadwal`) REFERENCES `jadwal` (`IdJadwal`),
  CONSTRAINT `jadwalpasien_ibfk_3` FOREIGN KEY (`IdPasien`) REFERENCES `pasien` (`IdPasien`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

#
# Data for table "jadwalpasien"
#

INSERT INTO `jadwalpasien` VALUES (2,1,4,'Normal',0),(6,2,1,'Normal',0),(7,3,9,'Normal',0),(8,4,8,'Normal',0);

#
# Structure for table "perawat"
#

DROP TABLE IF EXISTS `perawat`;
CREATE TABLE `perawat` (
  `IdPerawat` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(50) DEFAULT NULL,
  `TempatLahir` varchar(100) DEFAULT NULL,
  `TanggalLahir` date DEFAULT NULL,
  `JenisKelamin` enum('Pria','Wanita') DEFAULT 'Pria',
  `Agama` enum('Islam','Kristen','Katolik','Hindu','Budha') DEFAULT 'Islam',
  `Alamat` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`IdPerawat`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

#
# Data for table "perawat"
#

INSERT INTO `perawat` VALUES (1,'Amrin','Button','1984-03-31','Pria','Islam','Sentani');

#
# Structure for table "cucidarah"
#

DROP TABLE IF EXISTS `cucidarah`;
CREATE TABLE `cucidarah` (
  `IdCuciDarah` int(11) NOT NULL AUTO_INCREMENT,
  `IdPasien` int(11) NOT NULL DEFAULT '0',
  `IdJadwal` int(11) DEFAULT NULL,
  `JamMulai` datetime DEFAULT NULL,
  `JamAkhir` datetime DEFAULT NULL,
  `IdPerawat` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`IdCuciDarah`),
  KEY `IdPerawat` (`IdPerawat`),
  KEY `IdJadwal` (`IdJadwal`),
  KEY `IdPasien` (`IdPasien`),
  CONSTRAINT `cucidarah_ibfk_2` FOREIGN KEY (`IdPerawat`) REFERENCES `perawat` (`IdPerawat`),
  CONSTRAINT `cucidarah_ibfk_3` FOREIGN KEY (`IdJadwal`) REFERENCES `jadwal` (`IdJadwal`),
  CONSTRAINT `cucidarah_ibfk_4` FOREIGN KEY (`IdPasien`) REFERENCES `pasien` (`IdPasien`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;

#
# Data for table "cucidarah"
#

INSERT INTO `cucidarah` VALUES (9,1,4,'2017-04-05 02:31:52','2017-04-05 02:31:53',1),(10,2,4,'2017-04-05 02:34:07','2017-04-05 02:34:10',1),(11,4,8,'2017-04-07 11:07:04','2017-04-07 11:07:11',1),(12,3,9,'2017-05-30 23:22:37','2017-05-30 23:22:50',1),(15,2,1,'2017-05-31 03:24:47','2017-05-31 03:24:58',1),(16,3,9,'2017-06-06 13:08:05','2017-06-06 13:08:17',1),(17,2,1,'2017-06-12 09:07:35','2017-06-12 09:08:57',1),(18,1,4,'2017-06-12 09:08:06','2017-06-12 09:08:22',1);

#
# Structure for table "permintaanpindah"
#

DROP TABLE IF EXISTS `permintaanpindah`;
CREATE TABLE `permintaanpindah` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdPasien` int(11) NOT NULL DEFAULT '0',
  `Dari` int(11) NOT NULL DEFAULT '0',
  `Ke` int(11) NOT NULL DEFAULT '0',
  `DisetujuiOleh` int(11) NOT NULL DEFAULT '0',
  `TanggalPengajuan` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;

#
# Data for table "permintaanpindah"
#

INSERT INTO `permintaanpindah` VALUES (4,1,4,1,0,'2017-05-12 23:23:15'),(5,2,1,4,0,'2017-05-19 12:06:41'),(6,4,8,9,0,'2017-05-19 12:15:33'),(7,4,8,9,0,'2017-05-19 12:24:47'),(8,4,8,9,0,'2017-05-19 12:35:03'),(9,4,8,9,0,'2017-05-19 12:40:46'),(10,4,8,9,0,'2017-05-19 12:42:30'),(11,4,8,9,0,'2017-05-19 12:44:15'),(12,4,8,9,0,'2017-05-19 12:46:56'),(13,4,8,9,0,'2017-05-19 12:49:24'),(14,4,8,9,0,'2017-05-19 12:51:05'),(15,4,8,9,0,'2017-05-19 12:52:43'),(16,4,8,9,0,'2017-05-19 12:55:03'),(17,4,8,9,0,'2017-05-19 12:56:41'),(18,4,8,9,0,'2017-05-19 12:57:34'),(19,2,1,4,0,'2017-05-19 13:23:41'),(20,4,8,9,0,'2017-05-19 13:24:32');

#
# Structure for table "user"
#

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NamaLengkap` varchar(50) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

#
# Data for table "user"
#

INSERT INTO `user` VALUES (1,'Yoseph Kungkung','Ocph23','Sony'),(2,'Sumanto','Manto','Sony');

#
# View "viewpermintaan"
#

DROP VIEW IF EXISTS `viewpermintaan`;
CREATE
  ALGORITHM = UNDEFINED
  VIEW `viewpermintaan`
  AS
SELECT
  `pasien`.`NomorPasien`,
  `pasien`.`Nama`,
  `jadwalpasien`.`Status`,
  `jadwalpasien`.`IdPindah`,
  `jadwal`.`HariPertama`,
  `jadwal`.`HariKedua`,
  `jadwal`.`Shif` AS 'Awal',
  `jadwal1`.`Shif` AS 'Permintaan',
  `permintaanpindah`.`Id`,
  `permintaanpindah`.`IdPasien`,
  `jadwalpasien`.`IdJadwalPasien`
FROM
  ((((`permintaanpindah`
    LEFT JOIN `pasien` ON ((`permintaanpindah`.`IdPasien` = `pasien`.`IdPasien`)))
    LEFT JOIN `jadwalpasien` ON ((`permintaanpindah`.`Id` = `jadwalpasien`.`IdPindah`)))
    LEFT JOIN `jadwal` ON ((`permintaanpindah`.`Dari` = `jadwal`.`IdJadwal`)))
    LEFT JOIN `jadwal` jadwal1 ON ((`permintaanpindah`.`Ke` = `jadwal1`.`IdJadwal`)))
WHERE
  (`jadwalpasien`.`IdPindah` > 0);
