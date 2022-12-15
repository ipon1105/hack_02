-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: localhost    Database: x-mas_hack
-- ------------------------------------------------------
-- Server version	8.0.28

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `central_station`
--

DROP TABLE IF EXISTS `central_station`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `central_station` (
  `idcentral_station` int NOT NULL,
  `id_coord` int NOT NULL,
  `mission_list` json NOT NULL,
  `apparat_list` json NOT NULL,
  `repeater_list` json NOT NULL,
  `charging_station_list` json NOT NULL,
  `id_operator` int NOT NULL,
  PRIMARY KEY (`idcentral_station`),
  KEY `id_coord_idx` (`id_coord`),
  KEY `id_operator_idx` (`id_operator`),
  CONSTRAINT `cs_coord` FOREIGN KEY (`id_coord`) REFERENCES `coord` (`idcopterCoord`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `id_operator` FOREIGN KEY (`id_operator`) REFERENCES `operator` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `central_station`
--

LOCK TABLES `central_station` WRITE;
/*!40000 ALTER TABLE `central_station` DISABLE KEYS */;
/*!40000 ALTER TABLE `central_station` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `charging_station`
--

DROP TABLE IF EXISTS `charging_station`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `charging_station` (
  `idcharging_station` int NOT NULL,
  `name` varchar(45) NOT NULL,
  `id_coord` int NOT NULL,
  `charge_level` float NOT NULL,
  PRIMARY KEY (`idcharging_station`),
  KEY `id_coord_idx` (`id_coord`),
  CONSTRAINT `id_coord` FOREIGN KEY (`id_coord`) REFERENCES `coord` (`idcopterCoord`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `charging_station`
--

LOCK TABLES `charging_station` WRITE;
/*!40000 ALTER TABLE `charging_station` DISABLE KEYS */;
/*!40000 ALTER TABLE `charging_station` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `coord`
--

DROP TABLE IF EXISTS `coord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coord` (
  `idcopterCoord` int NOT NULL,
  `x` float NOT NULL,
  `y` float NOT NULL,
  `z` float NOT NULL,
  PRIMARY KEY (`idcopterCoord`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coord`
--

LOCK TABLES `coord` WRITE;
/*!40000 ALTER TABLE `coord` DISABLE KEYS */;
/*!40000 ALTER TABLE `coord` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `data_transmission_system`
--

DROP TABLE IF EXISTS `data_transmission_system`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `data_transmission_system` (
  `iddata_transmission_system` int NOT NULL,
  `range` float NOT NULL,
  `bandwidth` float NOT NULL,
  `LBDF` float NOT NULL,
  PRIMARY KEY (`iddata_transmission_system`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `data_transmission_system`
--

LOCK TABLES `data_transmission_system` WRITE;
/*!40000 ALTER TABLE `data_transmission_system` DISABLE KEYS */;
/*!40000 ALTER TABLE `data_transmission_system` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `figure`
--

DROP TABLE IF EXISTS `figure`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `figure` (
  `idfigure` int NOT NULL,
  `figure_name` varchar(45) NOT NULL,
  PRIMARY KEY (`idfigure`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `figure`
--

LOCK TABLES `figure` WRITE;
/*!40000 ALTER TABLE `figure` DISABLE KEYS */;
/*!40000 ALTER TABLE `figure` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `map`
--

DROP TABLE IF EXISTS `map`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `map` (
  `idmap` int NOT NULL,
  `location_name` varchar(60) NOT NULL,
  `id_map_list_coord` int NOT NULL,
  PRIMARY KEY (`idmap`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `map`
--

LOCK TABLES `map` WRITE;
/*!40000 ALTER TABLE `map` DISABLE KEYS */;
/*!40000 ALTER TABLE `map` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mission`
--

DROP TABLE IF EXISTS `mission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mission` (
  `idMission` int NOT NULL,
  `mission_type` varchar(45) NOT NULL,
  `mission_time` varchar(45) NOT NULL,
  `mission_task` varchar(45) NOT NULL,
  `command_list` json NOT NULL,
  `id_map` int NOT NULL,
  PRIMARY KEY (`idMission`),
  KEY `id_map_idx` (`id_map`),
  CONSTRAINT `id_map` FOREIGN KEY (`id_map`) REFERENCES `map` (`idmap`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mission`
--

LOCK TABLES `mission` WRITE;
/*!40000 ALTER TABLE `mission` DISABLE KEYS */;
/*!40000 ALTER TABLE `mission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `operator`
--

DROP TABLE IF EXISTS `operator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `operator` (
  `id` int NOT NULL,
  `login` varchar(45) NOT NULL,
  `pass` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 COMMENT='	';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `operator`
--

LOCK TABLES `operator` WRITE;
/*!40000 ALTER TABLE `operator` DISABLE KEYS */;
/*!40000 ALTER TABLE `operator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `repeater`
--

DROP TABLE IF EXISTS `repeater`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `repeater` (
  `idrepeater` int NOT NULL,
  `id_coord` int NOT NULL,
  `range` float NOT NULL,
  PRIMARY KEY (`idrepeater`),
  KEY `repeater_coord_idx` (`id_coord`),
  CONSTRAINT `repeater_coord` FOREIGN KEY (`id_coord`) REFERENCES `coord` (`idcopterCoord`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `repeater`
--

LOCK TABLES `repeater` WRITE;
/*!40000 ALTER TABLE `repeater` DISABLE KEYS */;
/*!40000 ALTER TABLE `repeater` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resources`
--

DROP TABLE IF EXISTS `resources`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `resources` (
  `idresources` int NOT NULL,
  `r_type` varchar(50) NOT NULL,
  `resources` varchar(45) NOT NULL,
  `weight` float NOT NULL,
  PRIMARY KEY (`idresources`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resources`
--

LOCK TABLES `resources` WRITE;
/*!40000 ALTER TABLE `resources` DISABLE KEYS */;
/*!40000 ALTER TABLE `resources` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specifications`
--

DROP TABLE IF EXISTS `specifications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `specifications` (
  `idspecifications` int NOT NULL,
  `id_figure` int NOT NULL,
  `diameter` float NOT NULL,
  `max_charge` time NOT NULL,
  `current_charge` time NOT NULL,
  `speed_up` float NOT NULL,
  `speed_down` float NOT NULL,
  `speed` float NOT NULL,
  `max_speed` float NOT NULL,
  `id_resource` int NOT NULL,
  `weight` float NOT NULL,
  PRIMARY KEY (`idspecifications`),
  KEY `id_resource_idx` (`id_resource`),
  KEY `id_figure_idx` (`id_figure`),
  CONSTRAINT `id_figure` FOREIGN KEY (`id_figure`) REFERENCES `figure` (`idfigure`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `id_resource` FOREIGN KEY (`id_resource`) REFERENCES `resources` (`idresources`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specifications`
--

LOCK TABLES `specifications` WRITE;
/*!40000 ALTER TABLE `specifications` DISABLE KEYS */;
/*!40000 ALTER TABLE `specifications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uav`
--

DROP TABLE IF EXISTS `uav`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `uav` (
  `iduav` int NOT NULL,
  `model_name` int NOT NULL,
  `id_coord` int NOT NULL,
  `id_specifications` int NOT NULL,
  `id_data_transmission_system` int NOT NULL,
  PRIMARY KEY (`iduav`),
  KEY `uav_coord_idx` (`id_coord`),
  KEY `uav_specifications_idx` (`id_specifications`),
  KEY `uav_DTS_idx` (`id_data_transmission_system`),
  CONSTRAINT `uav_coord` FOREIGN KEY (`id_coord`) REFERENCES `coord` (`idcopterCoord`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `uav_DTS` FOREIGN KEY (`id_data_transmission_system`) REFERENCES `data_transmission_system` (`iddata_transmission_system`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `uav_specifications` FOREIGN KEY (`id_specifications`) REFERENCES `specifications` (`idspecifications`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uav`
--

LOCK TABLES `uav` WRITE;
/*!40000 ALTER TABLE `uav` DISABLE KEYS */;
/*!40000 ALTER TABLE `uav` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ugv`
--

DROP TABLE IF EXISTS `ugv`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ugv` (
  `idugv` int NOT NULL,
  `model_name` varchar(45) NOT NULL,
  `id_coord` int NOT NULL,
  `id_specifications` int NOT NULL,
  `id_data_transmission_system` int NOT NULL,
  PRIMARY KEY (`idugv`),
  KEY `ugv_coord_idx` (`id_coord`),
  KEY `ugv_specifications_idx` (`id_specifications`),
  KEY `ugv_data_transmission_system_idx` (`id_data_transmission_system`),
  CONSTRAINT `ugv_coord` FOREIGN KEY (`id_coord`) REFERENCES `coord` (`idcopterCoord`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ugv_data_transmission_system` FOREIGN KEY (`id_data_transmission_system`) REFERENCES `data_transmission_system` (`iddata_transmission_system`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ugv_specifications` FOREIGN KEY (`id_specifications`) REFERENCES `specifications` (`idspecifications`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ugv`
--

LOCK TABLES `ugv` WRITE;
/*!40000 ALTER TABLE `ugv` DISABLE KEYS */;
/*!40000 ALTER TABLE `ugv` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-12-15 23:41:05
