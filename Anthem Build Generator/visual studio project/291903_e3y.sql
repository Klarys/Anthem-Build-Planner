-- phpMyAdmin SQL Dump
-- version 4.4.15.5
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Czas generowania: 27 Cze 2019, 18:17
-- Wersja serwera: 5.5.49
-- Wersja PHP: 5.6.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `291903_e3y`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `AssaultSystem`
--

CREATE TABLE IF NOT EXISTS `AssaultSystem` (
  `AssaultSystemId` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Damage` int(11) NOT NULL,
  `Recharge` int(11) NOT NULL,
  `Radius` int(11) DEFAULT NULL,
  `Charges` int(11) NOT NULL,
  `SpecialEffect` varchar(256) DEFAULT NULL,
  `ClassId` int(11) NOT NULL,
  `RarityId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `AssaultSystem`
--

INSERT INTO `AssaultSystem` (`AssaultSystemId`, `Name`, `Damage`, `Recharge`, `Radius`, `Charges`, `SpecialEffect`, `ClassId`, `RarityId`) VALUES
(0, 'Avenger''s Boon', 4478, 6, 0, 1, 'Striker''s Strength: Hitting an enemy increases melee damage by 220% for 20 seconds', 1, 1),
(1, 'Avenger''s Boon', 5144, 6, 0, 1, 'Striker''s Strength: Hitting an enemy increases melee damage by 220% for 20 seconds', 1, 2),
(2, 'Venom Bomb', 113, 7, 6, 1, NULL, 4, 0),
(3, 'Serpent''s Veil', 201, 7, 6, 1, 'Mauler''s Venom: Melee weapon hits increase all acid damage by 200% for 10 seconds. ', 4, 1);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Builds`
--

CREATE TABLE IF NOT EXISTS `Builds` (
  `BuildId` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `AdditionalNotes` varchar(256) DEFAULT NULL,
  `AuthorId` int(11) NOT NULL,
  `Rating` int(11) NOT NULL,
  `StrikeSystemId` int(11) DEFAULT NULL,
  `AssaultSystemId` int(11) DEFAULT NULL,
  `SupportSystemId` int(11) DEFAULT NULL,
  `ClassId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Builds`
--

INSERT INTO `Builds` (`BuildId`, `Name`, `AdditionalNotes`, `AuthorId`, `Rating`, `StrikeSystemId`, `AssaultSystemId`, `SupportSystemId`, `ClassId`) VALUES
(0, 'Test Build 1', 'Test notes 1', 1, 0, 0, 2, 1, 4),
(1, 'Test Build 2', 'Test notes 2', 1, 0, 2, 0, 2, 1);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Class`
--

CREATE TABLE IF NOT EXISTS `Class` (
  `ClassId` int(11) NOT NULL,
  `Name` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Class`
--

INSERT INTO `Class` (`ClassId`, `Name`) VALUES
(0, 'Universal'),
(1, 'Ranger'),
(2, 'Colossus'),
(3, 'Storm'),
(4, 'Interceptor');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `ComponentInBuild`
--

CREATE TABLE IF NOT EXISTS `ComponentInBuild` (
  `BuildId` int(11) NOT NULL,
  `ComponentId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `ComponentInBuild`
--

INSERT INTO `ComponentInBuild` (`BuildId`, `ComponentId`) VALUES
(0, 0),
(1, 0),
(0, 1),
(1, 1),
(1, 3),
(0, 4),
(1, 4),
(0, 5),
(0, 6),
(1, 6),
(0, 7),
(1, 7);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Components`
--

CREATE TABLE IF NOT EXISTS `Components` (
  `ComponentId` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `ShieldReinforcement` int(11) NOT NULL,
  `ArmorReinforcement` int(11) NOT NULL,
  `NormalEffect` varchar(256) NOT NULL,
  `SpecialEffect` varchar(256) DEFAULT NULL,
  `ClassId` int(11) NOT NULL,
  `RarityId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Components`
--

INSERT INTO `Components` (`ComponentId`, `Name`, `ShieldReinforcement`, `ArmorReinforcement`, `NormalEffect`, `SpecialEffect`, `ClassId`, `RarityId`) VALUES
(0, 'Thermal Cooling', 373, 373, 'Increases the javelin''s heat capacity by 50% of the base capacity to allow the javelin to fly more effectively.', 'Reduces time you stay overheated significantly. ', 0, 1),
(1, 'Thermal Cooling', 527, 527, 'Increases the javelin''s heat capacity by 50% of the base capacity to allow the javelin to fly more effectively.', 'Reduces time you stay overheated significantly. ', 0, 2),
(2, 'Firearm Calibration Core', 509, 509, 'Increases damage by 25% of base damage.', '', 1, 0),
(3, 'Grenade Augment', 509, 509, 'Enhances grenades to increase damage by 15% of base damage.', '', 1, 0),
(4, 'Diverted Energy Circuit', 678, 339, 'Increases maximum shields by 35% of base and decreases armor by -35% of base.', '', 4, 0),
(5, 'Survival Algorithm', 3258, 1714, 'Increases maximum shields by 35% of base and decreases armor by -35% of base.', 'Clearing a negative status recharges shields by 40%. Can occur once every 10 seconds.', 4, 2),
(6, 'Melee Inscription', 113, 113, 'An inscribed component that increases melee weapon damage by 5% of base damage.', '', 0, 0),
(7, 'Bloodlust', 373, 373, 'An inscribed component that increases melee weapon damage by 5% of base damage.', 'Increases melee damage by _% for _ seconds when using melee attacks on an enemy. ', 0, 1);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Rarity`
--

CREATE TABLE IF NOT EXISTS `Rarity` (
  `RarityId` int(11) NOT NULL,
  `Name` varchar(25) NOT NULL,
  `Power` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Rarity`
--

INSERT INTO `Rarity` (`RarityId`, `Name`, `Power`) VALUES
(0, 'Epic', 38),
(1, 'Masterwork', 61),
(2, 'Legendary', 75);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `SavedBuilds`
--

CREATE TABLE IF NOT EXISTS `SavedBuilds` (
  `BuildId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `SavedBuilds`
--

INSERT INTO `SavedBuilds` (`BuildId`, `UserId`) VALUES
(0, 1),
(1, 1);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `StrikeSystem`
--

CREATE TABLE IF NOT EXISTS `StrikeSystem` (
  `StrikeSystemId` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Damage` int(11) NOT NULL,
  `Recharge` int(11) NOT NULL,
  `Radius` int(11) DEFAULT NULL,
  `Charges` int(11) NOT NULL,
  `SpecialEffect` varchar(256) DEFAULT NULL,
  `ClassId` int(11) NOT NULL,
  `RarityId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `StrikeSystem`
--

INSERT INTO `StrikeSystem` (`StrikeSystemId`, `Name`, `Damage`, `Recharge`, `Radius`, `Charges`, `SpecialEffect`, `ClassId`, `RarityId`) VALUES
(0, 'Sudden Death', 5448, 7, 1, 1, 'Hitting an enemy detonates a fire explosion.', 4, 1),
(1, 'Sudden Death', 6258, 7, 1, 1, 'Hitting an enemy detonates a fire explosion.', 4, 2),
(2, 'Last Argument', 3731, 12, 5, 1, 'Hitting enemies adds 7 Ultimate charge. ', 1, 1),
(3, 'Last Argument', 4286, 12, 5, 1, 'Hitting enemies adds 7 Ultimate charge. ', 1, 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `SupportSystem`
--

CREATE TABLE IF NOT EXISTS `SupportSystem` (
  `SupportSystemId` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Charges` int(11) NOT NULL,
  `Recharge` int(11) NOT NULL,
  `Radius` int(11) DEFAULT NULL,
  `ClassId` int(11) NOT NULL,
  `RarityId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `SupportSystem`
--

INSERT INTO `SupportSystem` (`SupportSystemId`, `Name`, `Charges`, `Recharge`, `Radius`, `ClassId`, `RarityId`) VALUES
(0, 'Target Beacon', 1, 30, 20, 4, 0),
(1, 'Rally Cry', 1, 60, 1, 4, 0),
(2, 'Bulwark Point\r\n', 1, 60, 1, 1, 0),
(3, 'Muster Point', 1, 60, 1, 1, 0);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `test`
--

CREATE TABLE IF NOT EXISTS `test` (
  `Id` int(11) NOT NULL,
  `testcol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `test`
--

INSERT INTO `test` (`Id`, `testcol`) VALUES
(1, 111),
(2, 222);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Users`
--

CREATE TABLE IF NOT EXISTS `Users` (
  `UserId` int(11) NOT NULL,
  `UserName` varchar(25) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Login` varchar(25) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `Salt` varchar(10) NOT NULL,
  `Description` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Users`
--

INSERT INTO `Users` (`UserId`, `UserName`, `Email`, `Login`, `Password`, `Salt`, `Description`) VALUES
(1, 'TestUser', 'TestMail@123.com', 'TestUser1', 'TestPassword', 'TestSalt', 'TestDesc');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `WeaponInBuild`
--

CREATE TABLE IF NOT EXISTS `WeaponInBuild` (
  `Buildid` int(11) NOT NULL,
  `Weaponid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `WeaponInBuild`
--

INSERT INTO `WeaponInBuild` (`Buildid`, `Weaponid`) VALUES
(0, 1),
(1, 2),
(0, 3),
(1, 3);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `Weapons`
--

CREATE TABLE IF NOT EXISTS `Weapons` (
  `WeaponId` int(11) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Damage` int(11) NOT NULL,
  `Rpm` int(11) NOT NULL,
  `Ammo` int(11) NOT NULL,
  `OptimalRange` int(11) NOT NULL,
  `SpecialEffect` varchar(256) DEFAULT NULL,
  `ClassId` int(11) NOT NULL,
  `RarityId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Weapons`
--

INSERT INTO `Weapons` (`WeaponId`, `Name`, `Damage`, `Rpm`, `Ammo`, `OptimalRange`, `SpecialEffect`, `ClassId`, `RarityId`) VALUES
(0, 'Divine Vengeance', 323, 600, 216, 45, 'Every third weak point hit causes large fire explosions.', 0, 1),
(1, 'Divine Vengeance', 372, 600, 216, 45, 'Every third weak point hit causes large fire explosions.', 0, 2),
(2, 'Warden', 245, 600, 216, 45, '', 0, 0),
(3, 'Pyrrhic Victory', 238, 500, 320, 35, 'Defeating an enemy, or hitting them 10 times, causes a force explosion. ', 0, 1);

--
-- Indeksy dla zrzutów tabel
--

--
-- Indexes for table `AssaultSystem`
--
ALTER TABLE `AssaultSystem`
  ADD PRIMARY KEY (`AssaultSystemId`),
  ADD KEY `assaultsystem_class_fk` (`ClassId`),
  ADD KEY `assaultsystem_rarity_fk` (`RarityId`);

--
-- Indexes for table `Builds`
--
ALTER TABLE `Builds`
  ADD PRIMARY KEY (`BuildId`),
  ADD KEY `builds_assaultsystem_fk` (`AssaultSystemId`),
  ADD KEY `builds_class_fk` (`ClassId`),
  ADD KEY `builds_strikesystem_fk` (`StrikeSystemId`),
  ADD KEY `builds_supportsystem_fk` (`SupportSystemId`);

--
-- Indexes for table `Class`
--
ALTER TABLE `Class`
  ADD PRIMARY KEY (`ClassId`);

--
-- Indexes for table `ComponentInBuild`
--
ALTER TABLE `ComponentInBuild`
  ADD PRIMARY KEY (`BuildId`,`ComponentId`),
  ADD KEY `componentinbuild_components_fk` (`ComponentId`);

--
-- Indexes for table `Components`
--
ALTER TABLE `Components`
  ADD PRIMARY KEY (`ComponentId`),
  ADD KEY `components_class_fk` (`ClassId`),
  ADD KEY `components_rarity_fk` (`RarityId`);

--
-- Indexes for table `Rarity`
--
ALTER TABLE `Rarity`
  ADD PRIMARY KEY (`RarityId`);

--
-- Indexes for table `SavedBuilds`
--
ALTER TABLE `SavedBuilds`
  ADD PRIMARY KEY (`BuildId`,`UserId`),
  ADD KEY `savedbuilds_users_fk` (`UserId`);

--
-- Indexes for table `StrikeSystem`
--
ALTER TABLE `StrikeSystem`
  ADD PRIMARY KEY (`StrikeSystemId`),
  ADD KEY `strikesystem_class_fk` (`ClassId`),
  ADD KEY `strikesystem_rarity_fk` (`RarityId`);

--
-- Indexes for table `SupportSystem`
--
ALTER TABLE `SupportSystem`
  ADD PRIMARY KEY (`SupportSystemId`),
  ADD KEY `supportsystem_class_fk` (`ClassId`),
  ADD KEY `supportsystem_rarity_fk` (`RarityId`);

--
-- Indexes for table `test`
--
ALTER TABLE `test`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`UserId`),
  ADD UNIQUE KEY `Login` (`Login`);

--
-- Indexes for table `WeaponInBuild`
--
ALTER TABLE `WeaponInBuild`
  ADD PRIMARY KEY (`Buildid`,`Weaponid`),
  ADD KEY `weaponinbuild_weapons_fk` (`Weaponid`);

--
-- Indexes for table `Weapons`
--
ALTER TABLE `Weapons`
  ADD PRIMARY KEY (`WeaponId`),
  ADD KEY `weapons_class_fk` (`ClassId`),
  ADD KEY `weapons_rarity_fk` (`RarityId`);

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `AssaultSystem`
--
ALTER TABLE `AssaultSystem`
  ADD CONSTRAINT `assaultsystem_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`),
  ADD CONSTRAINT `assaultsystem_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`);

--
-- Ograniczenia dla tabeli `Builds`
--
ALTER TABLE `Builds`
  ADD CONSTRAINT `builds_assaultsystem_fk` FOREIGN KEY (`AssaultSystemId`) REFERENCES `AssaultSystem` (`AssaultSystemId`),
  ADD CONSTRAINT `builds_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`),
  ADD CONSTRAINT `builds_strikesystem_fk` FOREIGN KEY (`StrikeSystemId`) REFERENCES `StrikeSystem` (`StrikeSystemId`),
  ADD CONSTRAINT `builds_supportsystem_fk` FOREIGN KEY (`SupportSystemId`) REFERENCES `SupportSystem` (`SupportSystemId`);

--
-- Ograniczenia dla tabeli `ComponentInBuild`
--
ALTER TABLE `ComponentInBuild`
  ADD CONSTRAINT `componentinbuild_components_fk` FOREIGN KEY (`ComponentId`) REFERENCES `Components` (`ComponentId`),
  ADD CONSTRAINT `componentinbuild_builds_fk` FOREIGN KEY (`BuildId`) REFERENCES `Builds` (`BuildId`);

--
-- Ograniczenia dla tabeli `Components`
--
ALTER TABLE `Components`
  ADD CONSTRAINT `components_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`),
  ADD CONSTRAINT `components_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`);

--
-- Ograniczenia dla tabeli `SavedBuilds`
--
ALTER TABLE `SavedBuilds`
  ADD CONSTRAINT `savedbuilds_users_fk` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`),
  ADD CONSTRAINT `savedbuilds_builds_fk` FOREIGN KEY (`BuildId`) REFERENCES `Builds` (`BuildId`);

--
-- Ograniczenia dla tabeli `StrikeSystem`
--
ALTER TABLE `StrikeSystem`
  ADD CONSTRAINT `strikesystem_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`),
  ADD CONSTRAINT `strikesystem_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`);

--
-- Ograniczenia dla tabeli `SupportSystem`
--
ALTER TABLE `SupportSystem`
  ADD CONSTRAINT `supportsystem_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`),
  ADD CONSTRAINT `supportsystem_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`);

--
-- Ograniczenia dla tabeli `WeaponInBuild`
--
ALTER TABLE `WeaponInBuild`
  ADD CONSTRAINT `weaponinbuild_weapons_fk` FOREIGN KEY (`Weaponid`) REFERENCES `Weapons` (`WeaponId`),
  ADD CONSTRAINT `weaponinbuild_builds_fk` FOREIGN KEY (`Buildid`) REFERENCES `Builds` (`BuildId`);

--
-- Ograniczenia dla tabeli `Weapons`
--
ALTER TABLE `Weapons`
  ADD CONSTRAINT `weapons_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`),
  ADD CONSTRAINT `weapons_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
