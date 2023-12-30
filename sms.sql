-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 30, 2023 at 09:46 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sms`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbl_m_companies`
--

CREATE TABLE `tbl_m_companies` (
  `guid` varchar(36) NOT NULL,
  `name` varchar(36) NOT NULL,
  `email` varchar(36) NOT NULL,
  `telp` varchar(24) NOT NULL,
  `image` varchar(64) NOT NULL,
  `business_type` varchar(100) DEFAULT NULL,
  `type` varchar(100) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL,
  `deleted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_m_projects`
--

CREATE TABLE `tbl_m_projects` (
  `guid` varchar(36) NOT NULL,
  `name` varchar(225) NOT NULL,
  `description` text NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date NOT NULL,
  `status` enum('OnPlan','OnProgress','Done','Canceled') NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL,
  `deleted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_m_roles`
--

CREATE TABLE `tbl_m_roles` (
  `guid` varchar(36) NOT NULL,
  `name` varchar(36) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL,
  `deleted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_m_users`
--

CREATE TABLE `tbl_m_users` (
  `guid` varchar(36) NOT NULL,
  `username` varchar(36) NOT NULL,
  `email` varchar(36) NOT NULL,
  `password` varchar(36) NOT NULL,
  `full_name` varchar(64) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL,
  `deleted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_tr_project_vendors`
--

CREATE TABLE `tbl_tr_project_vendors` (
  `project_guid` varchar(36) NOT NULL,
  `vendor_guid` varchar(36) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL,
  `deleted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_tr_user_roles`
--

CREATE TABLE `tbl_tr_user_roles` (
  `user_guid` varchar(36) NOT NULL,
  `role_guid` varchar(36) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL,
  `deleted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_tr_vendors`
--

CREATE TABLE `tbl_tr_vendors` (
  `guid` varchar(36) NOT NULL,
  `company_guid` varchar(36) NOT NULL,
  `status` enum('WaitingForApproval','Approval','Rejected') NOT NULL,
  `confirm_by` varchar(36) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL,
  `deleted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbl_m_companies`
--
ALTER TABLE `tbl_m_companies`
  ADD PRIMARY KEY (`guid`);

--
-- Indexes for table `tbl_m_projects`
--
ALTER TABLE `tbl_m_projects`
  ADD PRIMARY KEY (`guid`);

--
-- Indexes for table `tbl_m_roles`
--
ALTER TABLE `tbl_m_roles`
  ADD PRIMARY KEY (`guid`);

--
-- Indexes for table `tbl_m_users`
--
ALTER TABLE `tbl_m_users`
  ADD PRIMARY KEY (`guid`);

--
-- Indexes for table `tbl_tr_project_vendors`
--
ALTER TABLE `tbl_tr_project_vendors`
  ADD KEY `project_guid` (`project_guid`),
  ADD KEY `vendor_guid` (`vendor_guid`);

--
-- Indexes for table `tbl_tr_user_roles`
--
ALTER TABLE `tbl_tr_user_roles`
  ADD KEY `user_guid` (`user_guid`),
  ADD KEY `role_guid` (`role_guid`);

--
-- Indexes for table `tbl_tr_vendors`
--
ALTER TABLE `tbl_tr_vendors`
  ADD PRIMARY KEY (`guid`),
  ADD KEY `tbl_tr_vendor_ibfk_1` (`company_guid`),
  ADD KEY `confirm_by` (`confirm_by`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tbl_tr_project_vendors`
--
ALTER TABLE `tbl_tr_project_vendors`
  ADD CONSTRAINT `tbl_tr_project_vendors_ibfk_1` FOREIGN KEY (`project_guid`) REFERENCES `tbl_m_projects` (`guid`),
  ADD CONSTRAINT `tbl_tr_project_vendors_ibfk_2` FOREIGN KEY (`vendor_guid`) REFERENCES `tbl_tr_vendors` (`guid`);

--
-- Constraints for table `tbl_tr_user_roles`
--
ALTER TABLE `tbl_tr_user_roles`
  ADD CONSTRAINT `tbl_tr_user_roles_ibfk_1` FOREIGN KEY (`user_guid`) REFERENCES `tbl_m_users` (`guid`),
  ADD CONSTRAINT `tbl_tr_user_roles_ibfk_2` FOREIGN KEY (`role_guid`) REFERENCES `tbl_m_roles` (`guid`);

--
-- Constraints for table `tbl_tr_vendors`
--
ALTER TABLE `tbl_tr_vendors`
  ADD CONSTRAINT `tbl_tr_vendors_ibfk_1` FOREIGN KEY (`company_guid`) REFERENCES `tbl_m_companies` (`guid`),
  ADD CONSTRAINT `tbl_tr_vendors_ibfk_2` FOREIGN KEY (`confirm_by`) REFERENCES `tbl_m_users` (`guid`) ON DELETE SET NULL ON UPDATE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
