CREATE DATABASE  IF NOT EXISTS `project_db` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `project_db`;
-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: project_db
-- ------------------------------------------------------
-- Server version	5.7.11-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `aircontry`
--

DROP TABLE IF EXISTS `aircontry`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `aircontry` (
  `chinese_city` varchar(35) NOT NULL,
  `eng_city` varchar(45) NOT NULL,
  `shortform` varchar(3) NOT NULL,
  `airport_name` varchar(45) NOT NULL,
  PRIMARY KEY (`shortform`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aircontry`
--

LOCK TABLES `aircontry` WRITE;
/*!40000 ALTER TABLE `aircontry` DISABLE KEYS */;
INSERT INTO `aircontry` VALUES ('曼谷','BANGKOK','BKK','曼谷國際機場'),('香港','HONG KONG','HKG','香港國際機場'),('大阪','OSAKA-KANSAI INTERNATIONAL','KIX','關西機場'),('上海','SHANGHAI','PVG','上海浦東機場'),('上海','SHANGHAI','SHA','上海虹橋機場'),('新加坡','SINGAPORE','SIN','樟宜機場'),('台北','TAIPEI','TPE','桃園國際機場');
/*!40000 ALTER TABLE `aircontry` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `airline`
--

DROP TABLE IF EXISTS `airline`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `airline` (
  `AirlineCode` varchar(2) NOT NULL,
  `Password` varchar(10) NOT NULL,
  `airlineName` varchar(20) NOT NULL,
  `icon` varchar(20) NOT NULL,
  PRIMARY KEY (`AirlineCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `airline`
--

LOCK TABLES `airline` WRITE;
/*!40000 ALTER TABLE `airline` DISABLE KEYS */;
INSERT INTO `airline` VALUES ('BR','123456','長榮航空','Carrier2.png'),('CI','123456','中華航空','Carrier1.png'),('CX','123456','國泰航空','Carrier3.png'),('EK','123456','阿聯酋航空','Carrier11.png'),('HX','123456','香港航空','Carrier4.png'),('JL','123456','日本航空','Carrier7.png'),('KA','123456','港龍航空','Carrier5.png'),('MU','123456','中國東方航空','Carrier8.png'),('NH','123456','全日空航空','Carrier6.png'),('SQ','123456','新加坡航空','Carrier9.png'),('TG','123456','泰國國際航空','Carrier12.png'),('UA','123456','美國聯合航空','Carrier10.png');
/*!40000 ALTER TABLE `airline` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attractbooking`
--

DROP TABLE IF EXISTS `attractbooking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attractbooking` (
  `BookID` int(11) NOT NULL AUTO_INCREMENT,
  `Attaction` varchar(45) NOT NULL,
  `City` varchar(45) NOT NULL,
  `adultPrice` int(11) NOT NULL,
  `childPrice` int(11) NOT NULL,
  `adultNum` int(11) NOT NULL,
  `childNum` int(11) NOT NULL,
  `TotalAmt` int(11) NOT NULL,
  `staffID` varchar(10) NOT NULL,
  `custID` varchar(10) NOT NULL,
  `orderDate` date NOT NULL,
  `status` varchar(45) DEFAULT 'Self Organized',
  PRIMARY KEY (`BookID`),
  KEY `fk_ab_staffID_idx` (`staffID`),
  KEY `fk_ab_attractName_idx` (`Attaction`),
  KEY `fk_ab_custID_idx` (`custID`),
  CONSTRAINT `fk_ab_attractName` FOREIGN KEY (`Attaction`) REFERENCES `attraction` (`AttractName`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_ab_custID` FOREIGN KEY (`custID`) REFERENCES `customer` (`CustID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_ab_staffID` FOREIGN KEY (`staffID`) REFERENCES `staff` (`StaffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attractbooking`
--

LOCK TABLES `attractbooking` WRITE;
/*!40000 ALTER TABLE `attractbooking` DISABLE KEYS */;
/*!40000 ALTER TABLE `attractbooking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attraction`
--

DROP TABLE IF EXISTS `attraction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attraction` (
  `AttractName` varchar(35) NOT NULL,
  `Duration` varchar(6) NOT NULL,
  `Cancellation` varchar(7) NOT NULL,
  `AttractPhoto` varchar(50) DEFAULT NULL,
  `City` varchar(9) NOT NULL,
  PRIMARY KEY (`AttractName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attraction`
--

LOCK TABLES `attraction` WRITE;
/*!40000 ALTER TABLE `attraction` DISABLE KEYS */;
INSERT INTO `attraction` VALUES ('AdventureCove Waterpark','1d','Charged','AdventureCove.png','Singapore'),('BlueMountain Day Tour','9h','Free','BlueMountain.png','Syndey'),('BridgeClimb Experience','3h30m','Free','BridgeClimb.png','Syndey'),('Disneyland Admission','10h','Free','Disneyland.png','Tokyo'),('DisneySea Admission','10h','Free','DisneySea.png','Tokyo'),('MountFuji Lake Ashi','10h30m','Free','MountFuji.png','Tokyo'),('NightSafari Adventure','4h30m','Free','NightSafari.png','Singapore'),('OperaHouse Guided Working Tour','1h','Free','OperaHouse.png','Syndey'),('SEAAquarium','1d','Charged','SEAAquarium.png','Singapore'),('ShrimpFishing Barbecue','4h30m','Free','ShrimpFishing.png','Taipei'),('SingaporeZoee Adventure','4h30m','Free','SingaporeZoo.png','Singapore'),('TaipeiTea Culture Day','9h','Free','TaipeiTea.png','Taipei'),('TokyoMorning Tour','4h30m','Charged','TokyoMorning.png','Tokyo'),('TraditionalChinese Performing Arts','1h','Charged','TraditionalChinese.png','Taipei'),('TraditionalJapanese Performing Arts','2h','Free','TraditionalJapanese.png','Tokyo'),('UniversalStudios Singapore','1d','Charged','UniversalStudios.png','Singapore'),('Yangmingshan National Park','9h','Free','Yangmingshan.png','Taipei');
/*!40000 ALTER TABLE `attraction` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attractprice`
--

DROP TABLE IF EXISTS `attractprice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attractprice` (
  `AttractName` varchar(35) NOT NULL,
  `ChildPrice` int(11) NOT NULL,
  `AdultPrice` int(11) NOT NULL,
  KEY `fk_ap_AttractName_idx` (`AttractName`),
  CONSTRAINT `fk_ap_AttractName` FOREIGN KEY (`AttractName`) REFERENCES `attraction` (`AttractName`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attractprice`
--

LOCK TABLES `attractprice` WRITE;
/*!40000 ALTER TABLE `attractprice` DISABLE KEYS */;
INSERT INTO `attractprice` VALUES ('BridgeClimb Experience',800,1300),('OperaHouse Guided Working Tour',100,200),('BlueMountain Day Tour',600,1000),('Yangmingshan National Park',400,600),('TraditionalChinese Performing Arts',900,900),('ShrimpFishing Barbecue',600,600),('TaipeiTea Culture Day',300,600),('UniversalStudios Singapore',300,400),('SEAAquarium',150,200),('NightSafari Adventure',260,350),('AdventureCove Waterpark',120,200),('SingaporeZoee Adventure',160,300),('Disneyland Admission',200,300),('MountFuji Lake Ashi',130,200),('DisneySea Admission',300,400),('TokyoMorning Tour',350,350),('TraditionalJapanese Performing Arts',300,500);
/*!40000 ALTER TABLE `attractprice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cruise`
--

DROP TABLE IF EXISTS `cruise`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cruise` (
  `CruiseNo` varchar(6) NOT NULL,
  `CruiseName` varchar(68) NOT NULL,
  `RefPrice` int(11) NOT NULL,
  `TourDay` int(11) NOT NULL,
  `OrganID` int(11) NOT NULL,
  `StartDate` date NOT NULL,
  `pdf` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`CruiseNo`),
  KEY `fk_c_OrganID_idx` (`OrganID`),
  CONSTRAINT `fk_c_OrganID` FOREIGN KEY (`OrganID`) REFERENCES `cruiseorganizer` (`OrganID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cruise`
--

LOCK TABLES `cruise` WRITE;
/*!40000 ALTER TABLE `cruise` DISABLE KEYS */;
INSERT INTO `cruise` VALUES ('EMM10A','地中海郵輪集團~華麗號, 意大利(威尼斯、布林迪西)、, 希臘(卡塔科隆)、土耳其(伊茲密爾、伊斯坦堡)、克羅地亞, 10天豪華郵輪假期',14800,10,3,'2015-06-07','EMM10A.pdf'),('HCW85A','2015年全球首個香港啟航環球郵輪, 跨越18個國家及地區，28個目的地，85天環遊世界瑰麗假期',285899,85,2,'2015-08-25','HCW85A.pdf'),('HRO08A','香港啟德郵輪碼頭往返, 皇家加勒比國際遊輪~海洋航行者號, 香港、中國(廈門)、日本(長崎、沖繩), 8天豪華郵輪假期',8999,8,2,'2015-07-21','HRO08A.pdf'),('HRT04A','香港啟德郵輪碼頭往返, 皇家加勒比國際遊輪~海洋航行者號, 香港、台灣(高雄)4天豪華郵輪假期',2899,4,1,'2015-07-25','HRT04A.pdf'),('HRX06A','香港啟德郵輪碼頭往返, 皇家加勒比國際遊輪~海洋航行者號, 香港、中國(廈門)、日本(沖繩), 6天豪華郵輪假期',5999,6,1,'2015-07-19','HRX06A.pdf'),('HSC04A','麗星郵輪～處女星號, 香港、台灣(高雄、台中)4天郵輪假期, 【香港尖沙咀海運碼頭往返】',3988,4,4,'2015-06-26','HSC04A.pdf'),('HSG04A','麗星郵輪～處女星號, 香港、中國(三亞)、越南(下龍灣), 4天豪華郵輪假期, 【香港尖沙咀海運碼頭往返】',4999,4,4,'2015-07-18','HSG04A.pdf'),('HSH06A','麗星郵輪～處女星號, 香港、台灣(高雄、台中、基隆), 6天郵輪假期',6999,6,4,'2015-06-23','HSH06A.pdf'),('JPD07A','公主遊輪～鑽石公主號, 日本(東京、橫濱、長崎)、韓國(釜山), 7天深度日本豪華郵輪假期',13099,7,3,'2015-08-15','JPD07A.pdf'),('SSG06A','麗星郵輪~雙子星號, 新加坡(Gardens by the Bay、環球影城)、, 馬來西亞(檳城、浮羅交怡)6天豪華郵輪假期',5999,6,4,'2015-07-30','SSG06A.pdf'),('TSA06A','麗星郵輪～寶瓶星號, 台灣(台北、基隆)、日本(石垣島、沖繩島) 6天團',6999,6,4,'2015-09-08','TSA06A.pdf'),('URM12A','環遊海上世界亞洲段12天豪華郵輪假期',17999,12,1,'2015-07-14','URM12A.pdf');
/*!40000 ALTER TABLE `cruise` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cruisebooking`
--

DROP TABLE IF EXISTS `cruisebooking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cruisebooking` (
  `BookingID` int(11) NOT NULL AUTO_INCREMENT,
  `cruiseNo` varchar(6) NOT NULL,
  `cruiseName` varchar(68) NOT NULL,
  `TourDay` int(11) NOT NULL,
  `StartDate` date NOT NULL,
  `ChildNum` int(11) NOT NULL,
  `AdultNum` int(11) NOT NULL,
  `ChildPrice` decimal(6,1) NOT NULL,
  `AdultPrice` int(11) NOT NULL,
  `TotalAmt` decimal(7,1) NOT NULL,
  `staffID` varchar(10) NOT NULL,
  `custID` varchar(10) NOT NULL,
  `orderDate` date NOT NULL,
  PRIMARY KEY (`BookingID`),
  KEY `fk_cb_staffID_idx` (`staffID`),
  KEY `fk_cb_custID_idx` (`custID`),
  KEY `fk_cb_cruiseID_idx` (`cruiseNo`),
  CONSTRAINT `fk_cb_cruiseNo` FOREIGN KEY (`cruiseNo`) REFERENCES `cruise` (`CruiseNo`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_cb_custID` FOREIGN KEY (`custID`) REFERENCES `customer` (`CustID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_cb_staffID` FOREIGN KEY (`staffID`) REFERENCES `staff` (`StaffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cruisebooking`
--

LOCK TABLES `cruisebooking` WRITE;
/*!40000 ALTER TABLE `cruisebooking` DISABLE KEYS */;
/*!40000 ALTER TABLE `cruisebooking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cruiseorganizer`
--

DROP TABLE IF EXISTS `cruiseorganizer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cruiseorganizer` (
  `OrganID` int(11) NOT NULL,
  `OrganizerE` varchar(11) NOT NULL,
  `OrganizerC` varchar(10) NOT NULL,
  PRIMARY KEY (`OrganID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cruiseorganizer`
--

LOCK TABLES `cruiseorganizer` WRITE;
/*!40000 ALTER TABLE `cruiseorganizer` DISABLE KEYS */;
INSERT INTO `cruiseorganizer` VALUES (1,'RCCI','皇家加勒比海國際郵輪'),(2,'Costa','歌詩達郵輪'),(3,'MSC','地中海郵輪'),(4,'Star Cruise','麗星郵輪');
/*!40000 ALTER TABLE `cruiseorganizer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer` (
  `CustID` varchar(4) NOT NULL DEFAULT '',
  `Surname` varchar(15) NOT NULL,
  `GivenName` varchar(30) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `Gender` varchar(1) NOT NULL,
  `Passport` varchar(15) NOT NULL,
  `MobileNo` varchar(15) NOT NULL,
  `Nationality` varchar(15) NOT NULL,
  PRIMARY KEY (`CustID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES `customer` WRITE;
/*!40000 ALTER TABLE `customer` DISABLE KEYS */;
INSERT INTO `customer` VALUES ('C001','Chan','Chi Ming','1982-03-12','F','F1842154','95215852','Chinese'),('C002','Wong','Chun Tin','1991-03-31','F','G6645132','96254685','Chinese'),('C003','Tang','Tai Chi','1979-09-24','M','T2165158','91254854','Chinese'),('C004','Man','Chi Kuen','1952-01-18','M','G2514144','92548475','Chinese'),('C005','Lee','Man Tao','1983-04-16','M','A1254855','92165845','Chinese'),('C006','Leung','Shun Yee','1991-02-19','F','B1215485','91236545','Chinese'),('C007','Lee','Ka Man','1998-06-05','F','R2315845','92548548','Chinese'),('C008','Lung','Chi Kin','1985-12-06','M','R1254856','97584254','Chinese'),('C009','Chan','Siu Dong','1973-08-19','M','G6584251','94652514','Chinese'),('C010','Cheung','Tai Tim','1978-08-17','M','J56698452','94521575','Chinese'),('C011','Fung','Chi Tak','1977-02-15','M','T15515155','96251675','Chinese'),('C012','Chan','Man Sheung','1976-06-18','F','F21251515','95462415','Chinese'),('C013','sad','asd','2016-06-01','F','123456879','12345678','asd'),('C014','asd','asd','2016-06-01','F','asdas','123','asd');
/*!40000 ALTER TABLE `customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `driver`
--

DROP TABLE IF EXISTS `driver`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `driver` (
  `StaffID` varchar(6) NOT NULL,
  `StaffName` varchar(12) NOT NULL,
  `Gender` varchar(1) NOT NULL,
  `Center` varchar(2) NOT NULL,
  `Email` varchar(12) NOT NULL,
  `Pass` int(11) NOT NULL,
  `Position` varchar(6) NOT NULL,
  `Ctype` varchar(6) NOT NULL,
  `Salary` int(11) NOT NULL,
  `Late` int(11) NOT NULL,
  PRIMARY KEY (`StaffID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `driver`
--

LOCK TABLES `driver` WRITE;
/*!40000 ALTER TABLE `driver` DISABLE KEYS */;
INSERT INTO `driver` VALUES ('ho1','Ho Yin','M','HQ','ho@tt.com',123456,'Driver','Driver',900,5),('kwong1','Kwong Johnny','M','HQ','kwong@tt.com',123456,'Driver','Driver',800,1),('lam1','Lam Sze Kit','M','HQ','lam@tt.com',123456,'Driver','Driver',750,0),('lam2','Lam Chris','M','HQ','chris@tt.com',123456,'Driver','Driver',650,1),('law1','Law Ming Fai','M','HQ','law@tt.com',123456,'Driver','Driver',800,2),('wu1','Wu Richard','M','HQ','weu@tt.com',123456,'Driver','Driver',850,0);
/*!40000 ALTER TABLE `driver` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `driverroster`
--

DROP TABLE IF EXISTS `driverroster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `driverroster` (
  `StaffID` varchar(6) NOT NULL,
  `Timeslot` int(11) NOT NULL,
  `Available` varchar(7) NOT NULL,
  PRIMARY KEY (`StaffID`),
  CONSTRAINT `fk_dr_staffID` FOREIGN KEY (`StaffID`) REFERENCES `driver` (`StaffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `driverroster`
--

LOCK TABLES `driverroster` WRITE;
/*!40000 ALTER TABLE `driverroster` DISABLE KEYS */;
INSERT INTO `driverroster` VALUES ('ho1',3,'Weekday'),('kwong1',1,'Weekday'),('lam1',2,'Weekday'),('lam2',2,'Weekday'),('law1',1,'Weekday'),('wu1',3,'Weekday');
/*!40000 ALTER TABLE `driverroster` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipment`
--

DROP TABLE IF EXISTS `equipment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `equipment` (
  `EquipID` varchar(3) NOT NULL,
  `Equipment` varchar(17) NOT NULL,
  `Suitable` varchar(5) NOT NULL,
  `Price` decimal(4,1) NOT NULL,
  PRIMARY KEY (`EquipID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipment`
--

LOCK TABLES `equipment` WRITE;
/*!40000 ALTER TABLE `equipment` DISABLE KEYS */;
INSERT INTO `equipment` VALUES ('e01','Booster seat','car',28.6),('e02','Additional driver','car',71.6),('e03','Baby seat','car',78.8),('e04','GPS','car',100.3),('e05','Drinking water','coach',20.0);
/*!40000 ALTER TABLE `equipment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipmentlist`
--

DROP TABLE IF EXISTS `equipmentlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `equipmentlist` (
  `VehicleBookingID` int(11) NOT NULL,
  `EquipID` varchar(35) NOT NULL,
  `EquipBookPrice` decimal(10,2) NOT NULL,
  `EquipBookingID` int(11) NOT NULL AUTO_INCREMENT,
  `Orderdate` date NOT NULL,
  PRIMARY KEY (`EquipBookingID`),
  KEY `fk_eqlist_vehicleBookingID_idx` (`VehicleBookingID`),
  KEY `fk_eqlist_equipID_idx` (`EquipID`),
  CONSTRAINT `fk_eqlist_vehicleBookingID` FOREIGN KEY (`VehicleBookingID`) REFERENCES `vehiclebooking` (`VehicleBookingID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipmentlist`
--

LOCK TABLES `equipmentlist` WRITE;
/*!40000 ALTER TABLE `equipmentlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `equipmentlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `flightbooking`
--

DROP TABLE IF EXISTS `flightbooking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `flightbooking` (
  `BookingID` int(11) NOT NULL AUTO_INCREMENT,
  `FlightNo` varchar(6) NOT NULL,
  `DepDateTime` datetime NOT NULL,
  `Class` varchar(10) NOT NULL,
  `OrderDate` datetime NOT NULL,
  `StaffID` varchar(10) NOT NULL,
  `CustID` varchar(4) NOT NULL,
  `AdultNum` int(11) NOT NULL,
  `ChildNum` int(11) NOT NULL,
  `InfantNum` int(11) NOT NULL,
  `AdultPrice` int(11) NOT NULL,
  `ChildPrice` int(11) NOT NULL,
  `InfantPrice` int(11) NOT NULL,
  `TotalAmt` decimal(6,1) NOT NULL,
  `From` varchar(45) NOT NULL,
  `To` varchar(45) NOT NULL,
  PRIMARY KEY (`BookingID`,`FlightNo`,`DepDateTime`),
  KEY `idx_fb_flightschedule` (`FlightNo`,`DepDateTime`),
  KEY `idx_fb_flightclass` (`FlightNo`,`Class`),
  KEY `idx_fb_CustID` (`CustID`),
  KEY `fk_fb_staffID_idx` (`StaffID`),
  CONSTRAINT `fk_fb_customer` FOREIGN KEY (`CustID`) REFERENCES `customer` (`CustID`),
  CONSTRAINT `fk_fb_flightclass` FOREIGN KEY (`FlightNo`, `Class`) REFERENCES `flightclass` (`FlightNo`, `Class`),
  CONSTRAINT `fk_fb_staffID` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `flightbooking`
--

LOCK TABLES `flightbooking` WRITE;
/*!40000 ALTER TABLE `flightbooking` DISABLE KEYS */;
/*!40000 ALTER TABLE `flightbooking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `flightclass`
--

DROP TABLE IF EXISTS `flightclass`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `flightclass` (
  `FlightNo` varchar(6) NOT NULL,
  `Class` varchar(10) NOT NULL,
  `AirlineCode` varchar(2) NOT NULL,
  `Price_Adult` int(11) NOT NULL,
  `Price_Children` int(11) NOT NULL,
  `Price_Infant` int(11) NOT NULL,
  `Tax` decimal(4,1) NOT NULL,
  PRIMARY KEY (`FlightNo`,`Class`),
  KEY `idx_flightclass_AirlineCode` (`AirlineCode`),
  CONSTRAINT `fk_flightclass_AirlineCode` FOREIGN KEY (`AirlineCode`) REFERENCES `airline` (`AirlineCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `flightclass`
--

LOCK TABLES `flightclass` WRITE;
/*!40000 ALTER TABLE `flightclass` DISABLE KEYS */;
INSERT INTO `flightclass` VALUES ('BR856','Economy','BR',1023,774,596,7.5),('BR858','Business','BR',3088,3088,3088,7.5),('BR858','Economy','BR',1023,774,596,7.5),('BR872','Economy','BR',1023,774,596,7.5),('CI602','Business','CI',2999,2999,500,7.5),('CI602','Economy','CI',999,746,500,7.5),('CI614','Economy','CI',931,694,500,7.5),('CI680','Business','CI',2977,2977,500,7.5),('CI680','Economy','CI',1098,792,500,7.5),('CX360','Business','CX',6500,6500,2000,7.5),('CX360','Economy','CX',3990,2990,1500,7.5),('CX364','Business','CX',6500,6500,2000,7.5),('CX364','Economy','CX',3990,2990,1500,7.5),('CX400','Economy','CX',1490,1090,740,7.5),('CX406','Economy','CX',1544,1155,740,7.5),('CX510','Economy','CX',1554,1115,740,7.5),('CX564','Economy','CX',1305,1174,760,7.5),('CX617','Economy','CX',4300,3100,1300,7.5),('CX659','Business','CX',3500,3500,800,7.5),('CX659','Economy','CX',1760,1760,800,7.5),('CX703','Economy','CX',4300,3100,1300,7.5),('CX713','Business','CX',7500,7500,2000,7.5),('CX713','Economy','CX',4300,3100,1300,7.5),('CX715','Business','CX',3500,3500,800,7.5),('CX715','Economy','CX',1880,1880,800,7.5),('CX735','Economy','CX',1880,1880,800,7.5),('EK385','Economy','EK',2200,1652,600,7.5),('EK386','Economy','EK',2200,1652,600,7.5),('EK395','Economy','EK',2200,1652,600,7.5),('HX232','Business','HX',5600,5600,3000,7.5),('HX232','Economy','HX',2850,1800,1200,7.5),('HX234','Economy','HX',2850,1800,1200,7.5),('HX236','Economy','HX',27850,1800,1200,7.5),('HX246','Business','HX',5600,5600,3000,7.5),('HX246','Economy','HX',2850,1800,1200,7.5),('HX252','Economy','HX',1584,1152,500,7.5),('HX264','Economy','HX',1584,1152,500,7.5),('HX282','Economy','HX',1584,1152,500,7.5),('HX284','Business','HX',2499,2499,530,7.5),('HX284','Economy','HX',1584,1160,530,7.5),('JL7050','Business','JL',11000,7111,800,7.5),('JL7050','Economy','JL',7111,5400,600,7.5),('JL7054','Economy','JL',7111,5400,750,7.5),('JL7060','Business','JL',11000,7111,800,7.5),('JL7060','Economy','JL',7111,5400,750,7.5),('KA482','Economy','KA',1699,1140,670,7.5),('KA499','Economy','KA',1699,1162,970,7.5),('KA565','Economy','KA',1069,1254,670,7.5),('KA802','Business','KA',8000,7900,1200,7.5),('KA802','Economy','KA',4100,3050,1000,7.5),('KA858','Business','KA',8050,7900,1200,7.5),('KA864','Economy','KA',4100,3050,1000,7.5),('KA876','Economy','KA',4100,3050,1000,7.5),('MU502','Economy','MU',1990,1190,900,7.5),('MU503','Economy','MU',1688,1550,400,7.5),('MU509','Economy','MU',1658,1550,400,7.5),('MU702','Economy','MU',1990,1190,800,7.5),('MU722','Economy','MU',1893,1140,800,7.5),('MU724','Economy','MU',2690,2400,1000,7.5),('MU728','Economy','MU',1458,1140,400,7.5),('SQ857','Economy','SQ',2950,2950,800,7.5),('SQ861','Business','SQ',6500,6500,800,7.5),('SQ861','Economy','SQ',3000,3050,800,7.5),('SQ865','Business','SQ',6500,6500,800,7.5),('SQ865','Economy','SQ',3050,3050,800,7.5),('SQ871','Economy','SQ',3000,3050,800,7.5),('SQ891','Economy','SQ',3010,3010,800,7.5),('TG601','Business','TG',5600,5600,1200,7.5),('TG601','Economy','TG',2400,1850,1000,7.5),('TG603','Economy','TG',2400,1850,1000,7.5),('TG607','Economy','TG',2400,1850,1000,7.5),('TG639','Business','TG',5600,5600,1200,7.5),('TG639','Economy','TG',2400,1850,1000,7.5);
/*!40000 ALTER TABLE `flightclass` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `flightschedule`
--

DROP TABLE IF EXISTS `flightschedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `flightschedule` (
  `FlightNo` varchar(6) NOT NULL,
  `DepDateTime` datetime NOT NULL,
  `ArrDateTime` datetime NOT NULL,
  `DepAirport` varchar(3) NOT NULL,
  `ArrAirport` varchar(3) NOT NULL,
  `FlyMinute` int(11) NOT NULL,
  `Aircraft` varchar(10) NOT NULL,
  PRIMARY KEY (`FlightNo`,`DepDateTime`),
  KEY `idx_fs_FlightNp` (`FlightNo`),
  CONSTRAINT `fk_fs_flightclass` FOREIGN KEY (`FlightNo`) REFERENCES `flightclass` (`FlightNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `flightschedule`
--

LOCK TABLES `flightschedule` WRITE;
/*!40000 ALTER TABLE `flightschedule` DISABLE KEYS */;
INSERT INTO `flightschedule` VALUES ('BR856','2015-06-22 17:00:00','2015-06-22 18:45:00','HKG','TPE',105,'A330-300'),('BR858','2015-06-22 20:55:00','2015-06-22 22:40:00','HKG','TPE',105,'A330-300'),('BR872','2015-06-23 19:25:00','2015-06-23 21:10:00','HKG','TPE',105,'A330-300'),('CI602','2015-06-20 10:15:00','2015-06-20 11:55:00','HKG','TPE',100,'747-400'),('CI614','2015-06-20 17:35:00','2015-06-20 19:15:00','HKG','TPE',100,'A330-300'),('CI680','2015-06-20 13:25:00','2015-06-20 15:05:00','HKG','TPE',100,'A330-300'),('CX360','2015-06-25 13:55:00','2015-06-25 16:25:00','HKG','PVG',150,'A330-200'),('CX364','2015-06-24 17:35:00','2015-06-24 20:10:00','HKG','PVG',150,'777-300'),('CX400','2015-06-22 16:25:00','2015-06-22 18:15:00','HKG','TPE',110,'A330-300'),('CX406','2015-06-20 12:15:00','2015-06-20 14:15:00','HKG','TPE',120,'A330-300'),('CX510','2015-06-22 14:55:00','2015-06-22 16:45:00','HKG','TPE',110,'A330-300'),('CX564','2015-06-20 13:10:00','2015-06-20 15:05:00','HKG','TPE',115,'A330-300'),('CX617','2015-07-24 21:20:00','2015-07-24 23:10:00','HKG','BKK',175,'777-300'),('CX659','2015-07-28 01:45:00','2015-07-28 05:25:00','HKG','SIN',220,'A330-300'),('CX703','2015-07-23 17:05:00','2015-07-23 19:00:00','HKG','BKK',170,'A330-300'),('CX713','2015-07-22 08:50:00','2015-07-22 10:40:00','HKG','BKK',170,'A330-300'),('CX715','2015-07-28 20:25:00','2015-07-28 00:15:00','HKG','SIN',230,'777-300'),('CX715','2015-07-29 20:25:00','2015-07-29 00:15:00','HKG','SIN',230,'777-300'),('CX735','2015-07-30 14:30:00','2015-07-30 18:20:00','HKG','SIN',230,'A340-300'),('EK385','2015-07-22 21:50:00','2015-07-22 23:45:00','HKG','BKK',175,'A380-800'),('EK385','2015-07-23 21:50:00','2015-07-23 23:45:00','HKG','BKK',175,'A380-800'),('EK386','2015-07-24 19:50:00','2015-07-24 21:45:00','HKG','BKK',175,'A380-800'),('EK395','2016-07-25 17:50:00','2016-07-25 19:45:00','HKG','BKK',175,'A380-800'),('HX232','2015-06-29 17:25:00','2015-06-29 19:55:00','HKG','PVG',150,'A330-200'),('HX234','2015-06-29 21:00:00','2015-06-29 23:25:00','HKG','PVG',145,'A330-200'),('HX236','2015-06-30 08:15:00','2015-06-30 10:50:00','HKG','PVG',155,'A330-200'),('HX246','2015-06-28 13:15:00','2015-06-28 15:45:00','HKG','PVG',150,'A330-200'),('HX252','2015-06-21 09:30:00','2015-06-21 11:25:00','HKG','TPE',115,'A330-300'),('HX264','2015-06-20 12:10:00','2015-06-20 13:50:00','HKG','TPE',100,'A330-300'),('HX282','2015-06-20 17:40:00','2015-06-20 19:30:00','HKG','TPE',110,'A330-300'),('HX284','2015-06-23 22:50:00','2015-06-24 00:40:00','HKG','TPE',110,'A330-300'),('JL7050','2015-06-23 01:45:00','2015-06-23 06:25:00','HKG','KIX',220,'A320'),('JL7054','2015-06-25 13:10:00','2015-06-25 20:00:00','HKG','KIX',350,'A320'),('JL7060','2015-06-23 07:55:00','2015-06-23 12:45:00','HKG','KIX',230,'A320'),('KA482','2015-06-20 18:25:00','2015-06-20 20:15:00','HKG','TPE',115,'A330-300'),('KA499','2015-06-21 14:55:00','2015-06-21 16:45:00','HKG','TPE',110,'A330-300'),('KA565','2015-06-24 17:00:00','2015-06-24 18:45:00','HKG','TPE',105,'A330-300'),('KA802','2015-06-24 08:00:00','2015-06-24 10:30:00','HKG','PVG',150,'A330-200'),('KA858','2015-09-30 10:00:00','2015-09-30 12:20:00','HKG','SHA',140,'A330-200'),('KA864','2015-07-15 09:25:00','2015-07-15 12:00:00','HKG','SHA',155,'A330-200'),('KA876','2015-06-22 09:55:00','2015-06-22 12:30:00','HKG','PVG',155,'A321'),('MU502','2015-06-25 12:50:00','2015-06-25 15:25:00','HKG','PVG',155,'A321'),('MU702','2015-06-26 13:55:00','2015-06-26 16:25:00','HKG','PVG',150,'A320'),('MU724','2015-06-25 09:35:00','2015-06-25 11:45:00','HKG','PVG',130,'A321'),('SQ857','2015-07-28 09:05:00','2015-07-28 12:50:00','HKG','SIN',225,'777-300'),('SQ861','2015-07-26 15:20:00','2015-07-26 19:10:00','HKG','SIN',230,'A380-800'),('SQ865','2015-07-26 18:50:00','2015-07-26 22:35:00','HKG','SIN',225,'777-300'),('SQ871','2015-07-26 19:55:00','2015-07-26 23:40:00','HKG','SIN',225,'777-200'),('SQ891','2015-07-30 12:30:00','2015-07-30 16:15:00','HKG','SIN',225,'A380-800'),('TG601','2015-07-24 13:25:00','2015-07-24 15:05:00','HKG','BKK',160,'A380-800'),('TG603','2016-07-25 07:45:00','2016-07-25 09:25:00','HKG','BKK',160,'A330-300'),('TG607','2015-07-22 20:45:00','2015-07-22 22:25:00','HKG','BKK',160,'747-400'),('TG639','2015-07-22 19:05:00','2015-07-22 20:45:00','HKG','BKK',160,'A330-300');
/*!40000 ALTER TABLE `flightschedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hotel`
--

DROP TABLE IF EXISTS `hotel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hotel` (
  `HotelID` int(11) NOT NULL DEFAULT '0',
  `Password` varchar(10) NOT NULL,
  `ChiName` varchar(50) NOT NULL,
  `EngName` varchar(80) NOT NULL,
  `Star` decimal(4,1) DEFAULT NULL,
  `Rating` decimal(4,1) NOT NULL DEFAULT '0.0',
  `Country` varchar(30) NOT NULL,
  `City` varchar(30) NOT NULL,
  `District` varchar(30) NOT NULL,
  `Address` varchar(255) NOT NULL,
  `Tel` varchar(20) NOT NULL,
  PRIMARY KEY (`HotelID`),
  UNIQUE KEY `ChiName_unique` (`ChiName`),
  UNIQUE KEY `EngName_unique` (`EngName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hotel`
--

LOCK TABLES `hotel` WRITE;
/*!40000 ALTER TABLE `hotel` DISABLE KEYS */;
INSERT INTO `hotel` VALUES (1,'123456','台北君悅酒店','Grand Hyatt Taipei',4.5,4.3,'Taiwan','Taipei','信義','2 SongShou Road Taipei 11051 台灣 ','30774857'),(2,'123456','台北凱撒大飯店','Caesar Park Taipei',4.0,3.9,'Taiwan','Taipei','中正','38 Chung Hsiao West Road Section 1 Zhongzheng Taipei 100 台灣','30774857'),(3,'123456','台北福華大飯店','The Howard Plaza Hotel Taipei',4.0,3.9,'Taiwan','Taipei','大安','160, Sec.3, Ren Ai Rd Taipei 10657 台灣 ','30774857'),(4,'123456','台北中山意舍酒店','amba Taipei Zhongshan',3.5,4.0,'Taiwan','Taipei','中山','57-1 Zhongshan North Road Section 2 Taipei 10412 台灣 ','30774857'),(5,'123456','台北晶華酒店','Regent Taipei',5.0,4.4,'Taiwan','Taipei','中山','No 3, Lane 39, Section 2 ZhongShan North, Road Taipei 104 台灣 ','30774857'),(6,'123456','台北西華飯店','The Sherwood Taipei',4.5,4.5,'Taiwan','Taipei','松山','111 Min Sheng East Road Taipei 104 台灣 ','30774857'),(7,'123456','黑部觀光酒店','Kurobe Kanko Hotel',3.0,3.9,'Japan','Nagano','Omachi','2822 Taira Omachi Nagano-ken 398-0001 日本 ','30774857'),(8,'123456','落葉松莊酒店','Hotel Karamatsuso',3.0,3.0,'Japan','Nagano','Omachi','2884-109 Taira Omachi Nagano-ken 398-0001 日本 ','30774857'),(9,'123456','東根拉雪酒店','Hotel La Neige Higashi-kan',5.0,4.5,'Japan','Nagano','Hakuba','Happo Wadanonomori Hakuba Nagano-ken 399-9301 日本','30774857'),(10,'123456','松本多米酒店','Dormy Inn Matsumoto',4.0,4.6,'Japan','Nagano','Matsumoto','2-2-1 Fukashi Matsumoto Nagano-ken 390-0815 日本 ','30774857'),(11,'123456','新加坡唐購物中心萬豪酒店','Singapore Marriott Tang Plaza Hotel',5.0,4.4,'Singapore','Singapore','烏節路','320 Orchard Road Singapore 238865 新加坡','30774857'),(12,'123456','新加坡瑞士史丹福酒店','Swissotel The Stamford, Singapore',5.0,4.3,'Singapore','Singapore','殖民區 - 美芝路','2 Stamford Road Singapore 178882 新加坡 ','30774857'),(13,'123456','克萊蒙梭公園大道酒店','Park Avenue Clemenceau',4.0,3.8,'Singapore','Singapore','新加坡河','81A Clemenceau Avenue # 05 - 18 UE Square Singapore 239918 新加坡','30774857'),(14,'123456','新加坡國敦河畔大酒店','Grand Copthorne Waterfront Hotel Singapore',4.0,4.1,'Singapore','Singapore','新加坡河','392 Havelock Road Singapore 169663 新加坡','30774857'),(15,'123456','聖里吉斯曼谷酒店','The St. Regis Bangkok',5.0,4.6,'Thailand','Bangkok','市中心 - 暹邏廣場','159 Rajadamri Road Bangkok Bangkok 10330 泰國','30774857'),(16,'123456','帕色哇公主飯店','Pathumwan Princess Hotel',4.5,4.5,'Thailand','Bangkok','市中心 - 暹邏廣場','444 MBK Center, Phayathai Rd., Wangmai Pathumwan Bangkok Bangkok 10330 泰國','30774857'),(17,'123456','曼谷悅榕莊','Banyan Tree Bangkok',5.0,4.6,'Thailand','Bangkok','是隆路 - 沙吞','21/100 South Sathon Road Bangkok Bangkok 10120 泰國 ','30774857'),(18,'123456','D&D 旅館','D&D Inn',3.0,4.1,'Thailand','Bangkok','舊城','68-70 Khaosan Road, Pranakorn Bangkok 10200 泰國 ','30774857'),(19,'123456','曼谷東方公寓','Oriental Residence Bangkok',5.0,4.7,'Thailand','Bangkok','素坤逸路','110 Wireless Road, Lumpini, Pathumwan Bangkok Bangkok 10330 泰國 ','30774857'),(20,'123456','上海虹橋元一希爾頓酒店','Hilton Shanghai Hongqiao',5.0,4.2,'China','Shanghai','Minhang','No.1116 Hong Song East Road Minhang Shanghai 201103 中國','30774857'),(21,'123456','和平飯店','Fairmont Peace Hotel',5.0,4.7,'China','Shanghai','黃浦 - 外灘','20 Nanjing Road East, Huang Pu District Shanghai Shanghai 200002 中國','30774857'),(22,'123456','上海世茂皇家艾美酒店','Le Royal Meridien Shanghai',5.0,4.3,'China','Shanghai','黃浦 - 外灘','789 Nanjing Road East Shanghai Shanghai 200001 中國 ','30774857'),(23,'123456','華亨賓館','Jin Jiang Hua Ting Hotel & Towers',4.0,3.6,'China','Shanghai','徐匯','1200 Cao Xi Bei Road Shanghai Shanghai 200030 中國','30774857'),(24,'123456','上海靜安香格里拉大酒店','Jing An Shangri-La, West Shanghai',4.5,4.7,'China','Shanghai','長寧','1218 Middle Yan\'an Road Jing An Kerry Centre, West Nanjing Shanghai Shanghai 200040 中國','30774857');
/*!40000 ALTER TABLE `hotel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hotelbooking`
--

DROP TABLE IF EXISTS `hotelbooking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hotelbooking` (
  `BookingID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderDate` date NOT NULL,
  `StaffID` varchar(10) NOT NULL,
  `CustID` varchar(4) NOT NULL,
  `HotelID` int(11) NOT NULL,
  `RoomType` varchar(45) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `RoomSize` int(11) NOT NULL,
  `TotalAmt` decimal(10,2) NOT NULL,
  `Checkin` date NOT NULL,
  `Checkout` date NOT NULL,
  `Remark` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`BookingID`),
  KEY `idx_hb_room` (`HotelID`,`RoomType`),
  KEY `idx_hb_CustID` (`CustID`),
  KEY `fk_hb_StaffID_idx` (`StaffID`),
  CONSTRAINT `fk_hb_CustID` FOREIGN KEY (`CustID`) REFERENCES `customer` (`CustID`),
  CONSTRAINT `fk_hb_StaffID` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_hb_room` FOREIGN KEY (`HotelID`, `RoomType`) REFERENCES `room` (`HotelID`, `RoomType`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hotelbooking`
--

LOCK TABLES `hotelbooking` WRITE;
/*!40000 ALTER TABLE `hotelbooking` DISABLE KEYS */;
/*!40000 ALTER TABLE `hotelbooking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room`
--

DROP TABLE IF EXISTS `room`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `room` (
  `HotelID` int(11) NOT NULL,
  `RoomType` varchar(45) NOT NULL,
  `NonSmoking` tinyint(1) NOT NULL,
  `RoomNum` int(11) NOT NULL,
  `RoomSize` int(11) NOT NULL,
  `AdultNum` int(11) NOT NULL,
  `ChildNum` int(11) NOT NULL,
  `RoomDesc` varchar(50) NOT NULL,
  `Price` int(11) NOT NULL,
  PRIMARY KEY (`HotelID`,`RoomType`),
  KEY `idx_room_HotelID` (`HotelID`),
  CONSTRAINT `fk_room_HotelID` FOREIGN KEY (`HotelID`) REFERENCES `hotel` (`HotelID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room`
--

LOCK TABLES `room` WRITE;
/*!40000 ALTER TABLE `room` DISABLE KEYS */;
INSERT INTO `room` VALUES (1,'君悅客房 - 一大床',1,2,24,3,2,'1 張特大雙人床',2344),(1,'君悅行政套房 - 一大床',1,4,70,3,2,'1 張特大雙人床',5485),(1,'君悅豪華客房',1,3,40,3,2,'1 張特大雙人床',2493),(1,'君悅豪華客房 - 二小床',1,4,40,3,2,'2 張單人床',2493),(1,'嘉賓軒客房 一大床',1,3,56,3,2,'1 張特大雙人床',3241),(1,'頂級套房, 1 張特大雙人床',0,4,80,3,2,'1 張特大雙人床',3989),(1,'頂級標準客房',1,5,36,3,2,'2 張單人床',2344),(2,'Metro Room',1,3,30,2,2,'1 張雙人床',1142),(2,'四人房',1,5,45,4,2,'2 張床',1655),(2,'套房',1,4,30,3,2,'1 張雙人床',1364),(2,'高級客房',1,5,30,3,2,'1 張雙人床',948),(2,'高級雙人房',1,5,30,3,2,'2 張單人床',1007),(3,'尊爵高級套房',1,2,26,3,2,'1 張特大雙人床',1398),(3,'尊爵高級雙人單床房',0,3,26,3,2,'1 張特大雙人床',1392),(3,'標準單人房',1,5,23,2,1,'1 張單人床',991),(3,'豪華雙人房',1,5,32,3,2,'1 張雙人床',906),(4,'中房一中床',1,4,34,3,2,'1 張加大雙人床',758),(4,'標準客房',1,4,35,3,2,'1 張特大雙人床',1646),(4,'酷景房一中床',1,5,35,3,2,'1 張加大雙人床',971),(4,'陽台房二單床',1,5,35,3,2,'2 張單人床',1112),(5,'寰宇客房一大床',1,3,60,3,2,'1 張特大雙人床',2016),(5,'精緻套房',1,4,60,3,2,'1 張特大雙人床',3747),(5,'豪華客房一特大床客房',1,5,55,3,2,'1 張特大雙人床',1747),(5,'高級客房',1,5,45,3,2,'1 張特大雙人床',1635),(6,'普通套房',0,5,25,3,2,'1 張特大雙人床',1908),(6,'行政標準客房',1,5,60,3,2,'1 張特大雙人床',1603),(6,'豪華三人房',1,3,39,4,2,'3 張單人床',1460),(6,'豪華標準客房',1,5,70,3,2,'1 張特大雙人床',1221),(6,'豪華雙人房',0,5,30,3,2,'2 張單人床',1259),(7,'傳統客房',1,2,30,3,2,'1 張日式床墊',724),(7,'傳統客房 (8 Tatami mat)',1,5,30,3,2,'1 張日式床墊',706),(8,'傳統客房 (Japanese Style Room)',1,5,60,6,2,'4 張日式床墊',918),(8,'傳統客房 (Run of the House Japanese Style Room)',0,5,35,3,2,'1 張日式床墊',965),(9,'普通套房 (B Type)',0,5,50,3,2,'2 張單人床',3175),(9,'標準小木屋',1,5,70,6,2,'4 張床',3686),(9,'豪華標準客房 (A Type)',1,3,50,3,2,'2 張單人床',2658),(9,'豪華標準客房, 轉角',1,3,50,3,2,'2 張單人床',3522),(10,'雙人房, 1 張雙人床',1,5,16,2,1,'1 張雙人床',680),(10,'雙人房, 2 張單人床',1,5,22,3,1,'2 張單人床',781),(11,'標準客房, 露台',1,5,60,3,2,'1 張特大雙人床',4452),(11,'行政標準客房',1,5,60,3,2,'1 張特大雙人床 或 2 張單人床',3090),(11,'豪華標準客房',1,5,56,3,2,'1 張特大雙人床 或 2 張單人床',2079),(11,'開放式客房',1,5,60,3,2,'1 張特大雙人床',2859),(12,'Swiss, 行政標準客房',1,5,34,3,2,'1 張特大雙人床 或 2 張單人床',1915),(12,'標準客房 (Swiss Advantage)',1,2,40,3,2,'1 張雙人床或 1 張單人床',1571),(12,'經典標準客房',0,5,61,3,2,'1 張特大雙人床 或 2 張單人床',1424),(12,'頂級標準客房, 1 張特大雙人床',1,5,50,3,2,'1 張特大雙人床',1866),(13,'公寓, 1 間臥室, 廚房',1,5,43,3,2,'1 張加大雙人床',1328),(13,'公寓, 2 間臥室, 廚房',1,5,57,3,2,'1 張加大雙人床 或 1 張單人床',1733),(14,'俱樂部標準客房',1,1,28,3,2,'1 張特大雙人床 或 2 張單人床',2285),(14,'行政套房, 1 張特大雙人床',1,5,55,3,2,'1 張特大雙人床',2634),(14,'豪華標準客房',1,5,28,3,2,'1 張特大雙人床 或 2 張單人床',1274),(14,'高級客房, 海灣景',0,5,32,3,2,'1 張特大雙人床',1182),(15,'Caroline Astor Suite, 1 King bed',0,5,90,3,2,'1 張特大雙人床',4732),(15,'St. Regis Suite, 1 King Bed',1,5,100,3,2,'1 張特大雙人床',2883),(15,'豪華標準客房, 1 張特大雙人床',1,5,45,3,2,'1 張特大雙人床',1546),(15,'頂級標準客房, 1 張特大雙人床',1,5,55,3,2,'1 張特大雙人床',1898),(16,'Execuplus Suite, 1 Double or 2 Single Beds',1,5,64,3,2,'1 張雙人床 或 2 張單人床',1137),(16,'高級單人房',1,5,34,3,2,'1 張雙人床 或 2 張單人床',768),(16,'高級雙人房',1,5,34,3,2,'1 張雙人床 或 2 張單人床',705),(17,'套房, 1 間臥室',0,5,59,3,2,'1 張特大雙人床',1281),(17,'套房, 2 間臥室',1,5,119,3,2,'2 張特大雙人床',2870),(17,'尊貴標準客房, 1 張特大雙人床',1,5,48,3,2,'1 張特大雙人床',2120),(17,'豪華標準客房, 1 張特大雙人床',1,5,48,3,2,'1 張特大雙人床',1006),(18,'Family with Balcony',1,5,28,4,2,'2 張雙人床',372),(18,'標準客房, 2 張單人床',1,5,21,2,2,'2 張單人床',196),(18,'豪華標準客房, 1 張單人床',1,5,19,1,1,'1 張單人床',176),(18,'豪華標準客房, 1 張雙人床',1,5,23,2,1,'1 張雙人床',244),(18,'豪華標準客房, 3 張單人床',1,5,26,4,2,'3 張單人床',303),(19,'Grand Deluxe',1,1,40,3,2,'1 張特大雙人床 或 2 張單人床',939),(19,'套房',1,5,120,3,2,'1 張特大雙人床及 1 張加大雙人床',2504),(19,'套房, 2 間臥室',1,5,120,3,2,'1 張特大雙人床及 2 張單人床',3452),(20,'一樓客房',1,5,86,3,2,'1 張特大雙人床',3303),(20,'一特大床客房',1,5,46,3,2,'1 張特大雙人床',1164),(20,'二單人床客房',1,5,46,2,1,'2 張單人床',1164),(20,'豪華標準客房',1,5,46,3,2,'1 張特大雙人床',2685),(21,'一卧室套房',1,5,89,3,2,'1 張特大雙人床',3278),(21,'特色江景套房',1,5,178,3,2,'1 張特大雙人床',5619),(21,'費爾蒙房',1,5,49,3,2,'1 張特大雙人床 或 2 張加大雙人床',1873),(21,'費爾蒙金尊客房',1,1,49,3,2,'1 張特大雙人床',2110),(22,'帝皇套房',1,5,72,3,2,'1 張特大雙人床',11035),(22,'皇家套房',1,5,92,4,2,'1 張特大雙人床',48494),(22,'纯净客房',1,5,38,3,2,'1 張特大雙人床',1341),(22,'艾美房',1,5,48,3,2,'1 張特大雙人床',1403),(22,'豪華客房',1,5,38,3,2,'1 張特大雙人床 或 2 張單人床',1153),(22,'高級豪華房',1,5,38,3,2,'1 張特大雙人床 或 2 張單人床',1278),(23,'行政標準客房',1,5,30,3,2,'1 張特大雙人床 或 2 張單人床',911),(23,'行政豪华房',1,5,45,3,2,'1 張特大雙人床',1041),(23,'豪華大床房',1,5,28,3,2,'1 張特大雙人床',638),(24,'尊貴套房',1,5,45,3,2,'1 張特大雙人床',5344),(24,'標準客房',1,5,28,3,2,'2 張單人床',2732),(24,'豪華套房, 1 張特大雙人床',1,5,45,3,2,'1 張特大雙人床',5344);
/*!40000 ALTER TABLE `room` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staff`
--

DROP TABLE IF EXISTS `staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `staff` (
  `StaffID` varchar(45) NOT NULL,
  `StaffName` varchar(45) NOT NULL,
  `Gender` varchar(1) NOT NULL,
  `Center` varchar(1) NOT NULL,
  `Email` varchar(55) NOT NULL,
  `Pass` varchar(45) NOT NULL,
  `Position` varchar(45) NOT NULL,
  `Salary` int(11) NOT NULL,
  `Ctype` varchar(7) NOT NULL,
  `Late` int(11) DEFAULT NULL,
  `Photo` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`StaffID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staff`
--

LOCK TABLES `staff` WRITE;
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;
INSERT INTO `staff` VALUES ('admin','Leung Sau Nok','M','A','chris@tt.com','123456','Administrator',10000,'Officer',0,'Yum.jpg'),('cheong1','Lam Sin Cheong','M','A','cheong@tt.com','123456','Customer Service Officer',13500,'Officer',3,'Cheong.jpg'),('fai1','Siu Yao Fai','M','A','fai@tt.com','123456','Customer Service Officer',18800,'Officer',5,'Fai.jpg'),('fan1','Tang Cheuk Fan','M','A','fan@tt.com','123456','Customer Service Officer',23000,'Officer',20,'Fan.jpg'),('guest','Guest','N','N','N','123456','Guest',0,'Guest',0,NULL),('han1','Chan Yuen Han','F','C','han@tt.com','123456','Customer Service Officer',22500,'Officer',3,'Han.jpg'),('jk','k','M','A','k@tt.com','k','Customer Service Officer',3,'Officer',0,NULL),('kam1','Yuk Siu Kam','F','A','kam@tt.com','123456','Center Manager',29000,'Officer',5,'Kam.jpg'),('kuen1','Leung Siu Kuen','M','B','kuen@tt.com','123456','Center Manager',30000,'Officer',5,'Kuen.jpg'),('lung1','Kam Hiu Lung','M','C','lung@tt.com','123456','Customer Service Officer',16600,'Officer',8,'Lung.jpg'),('ming1','Cheung Ming','M','B','ming@tt.com','123456','Customer Service Officer',12500,'Officer',10,'Ming.jpg'),('ning1','Leung Chin Ning','F','B','ning@tt.com','123456','Customer Service Officer',18200,'Officer',25,'Ning.jpg'),('sad','asd','M','A','asa2a@tt.com','12321','Customer Service Officer',21312,'Officer',0,NULL),('sad1','asd','M','A','asa2aa@tt.com','12321','Customer Service Officer',21312,'Officer',0,NULL),('sad11','asd','M','A','asa2aaqqq@tt.com','12321','Customer Service Officer',21312,'Officer',0,NULL),('shun1','Chan Tai Shun','M','A','shun@tt.com','123456','Customer Service Officer',22000,'Officer',6,'Shun.jpg'),('tak1','Au Siu Tak','M','C','tak@tt.com','123456','Customer Service Officer',12800,'Officer',6,'Tak.jpg'),('yum1','Chan See Yum','F','A','yum@tt.com','123456','Customer Service Officer',14000,'Officer',2,'Yum.jpg');
/*!40000 ALTER TABLE `staff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicle`
--

DROP TABLE IF EXISTS `vehicle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vehicle` (
  `Vehicle_Name` varchar(29) NOT NULL,
  `Price` decimal(5,2) NOT NULL,
  `Vehicle_Type` varchar(5) NOT NULL,
  `Capacity` int(11) NOT NULL,
  `Gear` varchar(5) NOT NULL,
  `Color` varchar(19) NOT NULL,
  `Vehicle_ID` varchar(5) NOT NULL,
  PRIMARY KEY (`Vehicle_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicle`
--

LOCK TABLES `vehicle` WRITE;
/*!40000 ALTER TABLE `vehicle` DISABLE KEYS */;
INSERT INTO `vehicle` VALUES ('Mini (Manual)',26.94,'car',4,'AT','blue','car01'),('Economy (Manual)',27.18,'car',4,'AT','blue','car02'),('Compact',51.09,'car',4,'AT/MT','blue','car03'),('Midsize (Manual)',29.69,'car',4,'MT','red','car04'),('Midsize SUV',104.60,'car',5,'MT','silver','car05'),('Standard Wagon',60.39,'car',5,'AT/MT','silver','car06'),('Airport Shuttle Bus',98.00,'coach',49,'AT','pink, orange, white','car07'),('Coach Bus',150.00,'coach',60,'AT/MT','white','car08'),('23 Seaters Deluxe Shuttle Bus',134.00,'coach',23,'AT','green, white','car09'),('29 Seaters Shuttle Bus',100.00,'coach',29,'MT','green, white','car10'),('49 Seaters Shuttle Bus',130.00,'coach',49,'AT/MT','green, white','car11'),('20-28 Seaters Shuttle Bus',100.00,'coach',28,'AT','green, white','car12');
/*!40000 ALTER TABLE `vehicle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehiclebooking`
--

DROP TABLE IF EXISTS `vehiclebooking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vehiclebooking` (
  `Vehicle_ID` varchar(5) NOT NULL,
  `AttractionBookingID` int(11) NOT NULL,
  `BookDay` int(11) NOT NULL,
  `VehicleBookPrice` decimal(10,2) NOT NULL,
  `VehicleBookingID` int(11) NOT NULL AUTO_INCREMENT,
  `Orderdate` date NOT NULL,
  PRIMARY KEY (`VehicleBookingID`),
  KEY `fk_vb_vehicleID_idx` (`Vehicle_ID`),
  KEY `fk_vb_attratBookingID_idx` (`AttractionBookingID`),
  CONSTRAINT `fk_vb_attratBookingID` FOREIGN KEY (`AttractionBookingID`) REFERENCES `attractbooking` (`BookID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_vb_vehicleID` FOREIGN KEY (`Vehicle_ID`) REFERENCES `vehicle` (`Vehicle_ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehiclebooking`
--

LOCK TABLES `vehiclebooking` WRITE;
/*!40000 ALTER TABLE `vehiclebooking` DISABLE KEYS */;
/*!40000 ALTER TABLE `vehiclebooking` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-07-20  0:50:30
