-- phpMyAdmin SQL Dump
-- version 4.4.15.5
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Czas generowania: 03 Wrz 2019, 15:14
-- Wersja serwera: 5.5.49
-- Wersja PHP: 5.6.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `291903_nxy`
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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `AssaultSystem`
--

INSERT INTO `AssaultSystem` (`AssaultSystemId`, `Name`, `Damage`, `Recharge`, `Radius`, `Charges`, `SpecialEffect`, `ClassId`, `RarityId`) VALUES
(1, 'Avenger''s Boon', 5144, 6, 0, 1, 'Striker''s Strength: Hitting an enemy increases melee damage by 220% for 20 seconds', 1, 2),
(2, 'Venom Bomb', 113, 7, 6, 1, NULL, 4, 0),
(3, 'Serpent''s Veil', 201, 7, 6, 1, 'Mauler''s Venom: Melee weapon hits increase all acid damage by 200% for 10 seconds. ', 4, 1),
(4, 'Avenger''s Boon', 4478, 6, 0, 1, 'Striker''s Strength: Hitting an enemy increases melee damage by 220% for 20 seconds', 1, 1),
(5, 'Ten Thousand Suns', 4458, 3, 6, 5, 'Striker''s Wrath: After hitting an enemy, gain 10% increased damage for 10 seconds stacking to 10. ', 3, 2),
(6, 'Ten Thousand Suns', 3881, 3, 6, 5, 'Striker''s Wrath: After hitting an enemy, gain 10% increased damage for 10 seconds stacking to 10. ', 3, 1),
(7, 'Garred''s Hammer', 8210, 5, 1, 1, 'Garred''s Hammer permanently gains an extra charge. ', 2, 1),
(8, 'Black Powder', 783, 4, 1, 2, 'Regain a charge after defeating an enemy. Can occur once every 1 seconds. ', 2, 1);

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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Builds`
--

INSERT INTO `Builds` (`BuildId`, `Name`, `AdditionalNotes`, `AuthorId`, `Rating`, `StrikeSystemId`, `AssaultSystemId`, `SupportSystemId`, `ClassId`) VALUES
(2, 'Test Ranger Build', 'Test Notes', 5, 0, 3, 1, 2, 1),
(3, 'Interceptor Build', 'Test Notes', 5, 4, 4, 2, 4, 4),
(4, 'Kopia - Interceptor Build', 'Test Notes', 7, 0, 4, 2, 4, 4),
(5, 'Test Ranger build', 'Opis opis', 7, 1, 2, 1, 3, 1);

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
  `ComponentInBuildId` int(11) NOT NULL,
  `BuildId` int(11) NOT NULL,
  `ComponentId` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `ComponentInBuild`
--

INSERT INTO `ComponentInBuild` (`ComponentInBuildId`, `BuildId`, `ComponentId`) VALUES
(19, 2, 1),
(20, 2, 1),
(21, 2, 3),
(22, 2, 6),
(23, 2, 7),
(24, 2, 3),
(37, 3, 1),
(38, 3, 5),
(39, 3, 6),
(40, 3, 6),
(41, 3, 7),
(42, 3, 8),
(43, 4, 1),
(44, 4, 5),
(45, 4, 6),
(46, 4, 6),
(47, 4, 7),
(48, 4, 8),
(61, 5, 1),
(62, 5, 1),
(63, 5, 2),
(64, 5, 3),
(65, 5, 6),
(66, 5, 8);

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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Components`
--

INSERT INTO `Components` (`ComponentId`, `Name`, `ShieldReinforcement`, `ArmorReinforcement`, `NormalEffect`, `SpecialEffect`, `ClassId`, `RarityId`) VALUES
(1, 'Thermal Cooling', 527, 527, 'Increases the javelin''s heat capacity by 50% of the base capacity to allow the javelin to fly more effectively.', 'Reduces time you stay overheated significantly. ', 0, 2),
(2, 'Firearm Calibration Core', 509, 509, 'Increases damage by 25% of base damage.', '', 1, 0),
(3, 'Grenade Augment', 509, 509, 'Enhances grenades to increase damage by 15% of base damage.', '', 1, 0),
(4, 'Diverted Energy Circuit', 678, 339, 'Increases maximum shields by 35% of base and decreases armor by -35% of base.', '', 4, 0),
(5, 'Survival Algorithm', 3258, 1714, 'Increases maximum shields by 35% of base and decreases armor by -35% of base.', 'Clearing a negative status recharges shields by 40%. Can occur once every 10 seconds.', 4, 2),
(6, 'Melee Inscription', 113, 113, 'An inscribed component that increases melee weapon damage by 5% of base damage.', '', 0, 0),
(7, 'Bloodlust', 373, 373, 'An inscribed component that increases melee weapon damage by 5% of base damage.', 'Increases melee damage by _% for _ seconds when using melee attacks on an enemy. ', 0, 1),
(8, 'Thermal Cooling', 373, 373, 'Increases the javelin''s heat capacity by 50% of the base capacity to allow the javelin to fly more effectively.', 'Reduces time you stay overheated significantly. ', 0, 1),
(9, 'Elemental Synergy', 678, 339, 'Increases gear damage by 50% of base and lowers gear recharge rate by -20% of base.', NULL, 3, 0),
(10, 'Vanguard''s Token', 2836, 1492, 'Increases armor by 25%.', 'A shield break increases damage resistance by 10% for 20 seconds.', 3, 1),
(11, 'Grand Entrance', 4973, 12518, 'Increases duration of support abilities by 50%.', 'Upon hard-landing, creates an explosion at the point of impact.', 2, 2),
(12, 'Explosives Expert', 1131, 2828, 'All blast damage is increased by 15% of base damage.', '', 2, 0);

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
(2, 5),
(3, 5),
(3, 6),
(3, 7),
(4, 7),
(5, 7);

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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `StrikeSystem`
--

INSERT INTO `StrikeSystem` (`StrikeSystemId`, `Name`, `Damage`, `Recharge`, `Radius`, `Charges`, `SpecialEffect`, `ClassId`, `RarityId`) VALUES
(1, 'Sudden Death', 6258, 7, 1, 1, 'Hitting an enemy detonates a fire explosion.', 4, 2),
(2, 'Last Argument', 3731, 12, 5, 1, 'Hitting enemies adds 7 Ultimate charge. ', 1, 1),
(3, 'Last Argument', 4286, 12, 5, 1, 'Hitting enemies adds 7 Ultimate charge. ', 1, 2),
(4, 'Sudden Death', 5448, 7, 1, 1, 'Hitting an enemy detonates a fire explosion.', 4, 1),
(5, 'Ice Blast', 961, 7, 1, 1, '', 3, 0),
(6, 'Ponder Infinity', 4887, 10, 6, 1, 'Raptor''s Amplitude: While hovering, electric damage is increased by 165%.', 3, 2),
(7, 'Vassa''s Arc', 895, 2, 30, 10, 'Multiple hits cause a large electrical explosion. ', 2, 1),
(8, 'Final Judgment', 5572, 15, 4, 1, 'Demolisher''s Wrath: Hitting (2) enemies increases all damage by 55% for 20 seconds.', 2, 2);

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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `SupportSystem`
--

INSERT INTO `SupportSystem` (`SupportSystemId`, `Name`, `Charges`, `Recharge`, `Radius`, `ClassId`, `RarityId`) VALUES
(1, 'Rally Cry', 1, 60, 1, 4, 0),
(2, 'Bulwark Point', 1, 60, 1, 1, 0),
(3, 'Muster Point', 1, 60, 1, 1, 0),
(4, 'Target Beacon', 1, 30, 20, 4, 0),
(5, 'Quickening Field', 1, 72, 1, 3, 0),
(6, 'Wind Wall', 1, 30, 1, 3, 0),
(7, 'Battle Cry', 1, 30, 40, 2, 0),
(8, 'Shield Pulse', 1, 45, 1, 2, 0);

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
  `Salt` varchar(100) NOT NULL,
  `Description` varchar(256) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Users`
--

INSERT INTO `Users` (`UserId`, `UserName`, `Email`, `Login`, `Password`, `Salt`, `Description`) VALUES
(5, 'TestUser', 'test@mail.com', 'TestUser', 'd9389dbfd3244d014aa2aab8a613d5bf9566d13e76d0f1cfa76ce9c5278a7c72', 'dfc5d6137dd94685471678847c28df65', ''),
(6, 'TestUser2', 'testmail@mail.com', 'TestUser2', 'a664b9ff04c56c6f5dd1240d6ca673dc2e4d1926b3d715882613ce1b71cf657e', '6cd0a7e43ee74d550203a959a46de2f4', 'desc'),
(7, 'venarim', 'venarim30@gmail.com', 'kokosek', '749ab09fbca8ca61dd7ee8414b3829f991247c768bf730c5ba4776f192e53db6', '65283be561383204b75146bf57add81a', 'Opis');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `WeaponInBuild`
--

CREATE TABLE IF NOT EXISTS `WeaponInBuild` (
  `WeaponInBuildId` int(11) NOT NULL,
  `Buildid` int(11) NOT NULL,
  `Weaponid` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `WeaponInBuild`
--

INSERT INTO `WeaponInBuild` (`WeaponInBuildId`, `Buildid`, `Weaponid`) VALUES
(3, 2, 1),
(4, 2, 3),
(9, 3, 3),
(10, 3, 4),
(11, 4, 3),
(12, 4, 4),
(17, 5, 1),
(18, 5, 2);

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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `Weapons`
--

INSERT INTO `Weapons` (`WeaponId`, `Name`, `Damage`, `Rpm`, `Ammo`, `OptimalRange`, `SpecialEffect`, `ClassId`, `RarityId`) VALUES
(1, 'Divine Vengeance', 372, 600, 216, 45, 'Every third weak point hit causes large fire explosions.', 0, 2),
(2, 'Warden', 245, 600, 216, 45, '', 0, 0),
(3, 'Pyrrhic Victory', 238, 500, 320, 35, 'Defeating an enemy, or hitting them 10 times, causes a force explosion. ', 0, 1),
(4, 'Divine Vengeance', 323, 600, 216, 45, 'Every third weak point hit causes large fire explosions.', 0, 1),
(5, 'Avenging Herald', 1044, 240, 72, 35, 'Raptor''s Deadeye: Hovering increases weapon damage by 200%.', 0, 1),
(6, 'Renewed Courage', 500, 850, 300, 25, 'Survivor''s Balance: Last shot reduces recoil by -50% for the next 20 seconds. Stacks to 2. ', 0, 2);

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
  ADD PRIMARY KEY (`ComponentInBuildId`),
  ADD KEY `BuildId` (`BuildId`) USING BTREE,
  ADD KEY `componentinbuild_components_fk` (`ComponentId`) USING BTREE;

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
  ADD KEY `savedbuilds_users_fk` (`UserId`),
  ADD KEY `BuildId` (`BuildId`) USING BTREE;

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
  ADD PRIMARY KEY (`WeaponInBuildId`),
  ADD KEY `weaponinbuild_weapons_fk` (`Weaponid`),
  ADD KEY `Buildid` (`Buildid`) USING BTREE;

--
-- Indexes for table `Weapons`
--
ALTER TABLE `Weapons`
  ADD PRIMARY KEY (`WeaponId`),
  ADD KEY `weapons_class_fk` (`ClassId`),
  ADD KEY `weapons_rarity_fk` (`RarityId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT dla tabeli `AssaultSystem`
--
ALTER TABLE `AssaultSystem`
  MODIFY `AssaultSystemId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT dla tabeli `Builds`
--
ALTER TABLE `Builds`
  MODIFY `BuildId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT dla tabeli `ComponentInBuild`
--
ALTER TABLE `ComponentInBuild`
  MODIFY `ComponentInBuildId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=67;
--
-- AUTO_INCREMENT dla tabeli `Components`
--
ALTER TABLE `Components`
  MODIFY `ComponentId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=13;
--
-- AUTO_INCREMENT dla tabeli `StrikeSystem`
--
ALTER TABLE `StrikeSystem`
  MODIFY `StrikeSystemId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT dla tabeli `SupportSystem`
--
ALTER TABLE `SupportSystem`
  MODIFY `SupportSystemId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT dla tabeli `Users`
--
ALTER TABLE `Users`
  MODIFY `UserId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT dla tabeli `WeaponInBuild`
--
ALTER TABLE `WeaponInBuild`
  MODIFY `WeaponInBuildId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=19;
--
-- AUTO_INCREMENT dla tabeli `Weapons`
--
ALTER TABLE `Weapons`
  MODIFY `WeaponId` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=7;
--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `AssaultSystem`
--
ALTER TABLE `AssaultSystem`
  ADD CONSTRAINT `assaultsystem_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`),
  ADD CONSTRAINT `assaultsystem_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`);

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
  ADD CONSTRAINT `componentinbuild_builds_fk` FOREIGN KEY (`BuildId`) REFERENCES `Builds` (`BuildId`),
  ADD CONSTRAINT `componentinbuild_components_fk` FOREIGN KEY (`ComponentId`) REFERENCES `Components` (`ComponentId`);

--
-- Ograniczenia dla tabeli `Components`
--
ALTER TABLE `Components`
  ADD CONSTRAINT `components_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`),
  ADD CONSTRAINT `components_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`);

--
-- Ograniczenia dla tabeli `SavedBuilds`
--
ALTER TABLE `SavedBuilds`
  ADD CONSTRAINT `savedbuilds_builds_fk` FOREIGN KEY (`BuildId`) REFERENCES `Builds` (`BuildId`),
  ADD CONSTRAINT `savedbuilds_users_fk` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`);

--
-- Ograniczenia dla tabeli `StrikeSystem`
--
ALTER TABLE `StrikeSystem`
  ADD CONSTRAINT `strikesystem_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`),
  ADD CONSTRAINT `strikesystem_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`);

--
-- Ograniczenia dla tabeli `SupportSystem`
--
ALTER TABLE `SupportSystem`
  ADD CONSTRAINT `supportsystem_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`),
  ADD CONSTRAINT `supportsystem_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`);

--
-- Ograniczenia dla tabeli `WeaponInBuild`
--
ALTER TABLE `WeaponInBuild`
  ADD CONSTRAINT `weaponinbuild_builds_fk` FOREIGN KEY (`Buildid`) REFERENCES `Builds` (`BuildId`),
  ADD CONSTRAINT `weaponinbuild_weapons_fk` FOREIGN KEY (`Weaponid`) REFERENCES `Weapons` (`WeaponId`);

--
-- Ograniczenia dla tabeli `Weapons`
--
ALTER TABLE `Weapons`
  ADD CONSTRAINT `weapons_class_fk` FOREIGN KEY (`ClassId`) REFERENCES `Class` (`ClassId`),
  ADD CONSTRAINT `weapons_rarity_fk` FOREIGN KEY (`RarityId`) REFERENCES `Rarity` (`RarityId`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
