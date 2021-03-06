USE [master]
GO
/****** Object:  Database [ExchangeRatesStatistics]    Script Date: 10/18/2012 14:17:09 ******/
CREATE DATABASE [ExchangeRatesStatistics] ON  PRIMARY 
( NAME = N'ExchangeRatesStatistics', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\ExchangeRatesStatistics.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ExchangeRatesStatistics_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\ExchangeRatesStatistics_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ExchangeRatesStatistics] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExchangeRatesStatistics].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExchangeRatesStatistics] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET ANSI_NULLS OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET ANSI_PADDING OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET ARITHABORT OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [ExchangeRatesStatistics] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [ExchangeRatesStatistics] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [ExchangeRatesStatistics] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET  DISABLE_BROKER
GO
ALTER DATABASE [ExchangeRatesStatistics] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [ExchangeRatesStatistics] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [ExchangeRatesStatistics] SET  READ_WRITE
GO
ALTER DATABASE [ExchangeRatesStatistics] SET RECOVERY SIMPLE
GO
ALTER DATABASE [ExchangeRatesStatistics] SET  MULTI_USER
GO
ALTER DATABASE [ExchangeRatesStatistics] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [ExchangeRatesStatistics] SET DB_CHAINING OFF
GO
USE [ExchangeRatesStatistics]
GO
/****** Object:  Table [dbo].[Currencies]    Script Date: 10/18/2012 14:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currencies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[ServiceCode] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExchangeRatesHistory]    Script Date: 10/18/2012 14:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeRatesHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[CurrencyRate] [float] NOT NULL,
 CONSTRAINT [PK_ExchangeRatesHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_ExchangeRatesHistory_ExchangeRatesHistory]    Script Date: 10/18/2012 14:17:12 ******/
ALTER TABLE [dbo].[ExchangeRatesHistory]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeRatesHistory_ExchangeRatesHistory] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO
ALTER TABLE [dbo].[ExchangeRatesHistory] CHECK CONSTRAINT [FK_ExchangeRatesHistory_ExchangeRatesHistory]
GO

USE [ExchangeRatesStatistics];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

SET IDENTITY_INSERT [dbo].[Currencies] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Currencies]([Id], [Name], [ServiceCode])
SELECT 3, N'United States dollar', N'USD' UNION ALL
SELECT 4, N'Pound Sterling', N'GBP' UNION ALL
SELECT 5, N'Japanese yen', N'JPY' UNION ALL
SELECT 6, N'Russian ruble', N'RUB' UNION ALL
SELECT 7, N'Euro', N'EUR'
COMMIT;
RAISERROR (N'[dbo].[Currencies]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Currencies] OFF;