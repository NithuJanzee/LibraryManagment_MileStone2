USE [master]
GO
/****** Object:  Database [LibraryManagment]    Script Date: 9/30/2024 4:58:30 PM ******/
CREATE DATABASE [LibraryManagment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryManagment', FILENAME = N'C:\Users\UT01268\LibraryManagment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryManagment_log', FILENAME = N'C:\Users\UT01268\LibraryManagment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LibraryManagment] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryManagment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryManagment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryManagment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryManagment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryManagment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryManagment] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryManagment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryManagment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryManagment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryManagment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryManagment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryManagment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryManagment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryManagment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryManagment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryManagment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibraryManagment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryManagment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryManagment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryManagment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryManagment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryManagment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryManagment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryManagment] SET RECOVERY FULL 
GO
ALTER DATABASE [LibraryManagment] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryManagment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryManagment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryManagment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryManagment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryManagment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibraryManagment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LibraryManagment] SET QUERY_STORE = ON
GO
ALTER DATABASE [LibraryManagment] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LibraryManagment]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 9/30/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Image] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 9/30/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[copies] [int] NOT NULL,
	[author_id] [uniqueidentifier] NOT NULL,
	[genre_id] [uniqueidentifier] NOT NULL,
	[publication_id] [uniqueidentifier] NOT NULL,
	[image] [varchar](max) NULL,
 CONSTRAINT [PK__Book__3213E83FCB25CC50] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookTransactions]    Script Date: 9/30/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookTransactions](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[LendingDate] [datetime] NULL,
	[ReturnDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 9/30/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publication]    Script Date: 9/30/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publication](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/30/2024 4:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[NIC] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Password] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF__Book__id__5535A963]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Genre] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Publication] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK__Book__author_id__5629CD9C] FOREIGN KEY([author_id])
REFERENCES [dbo].[Author] ([AuthorId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK__Book__author_id__5629CD9C]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK__Book__genre_id__571DF1D5] FOREIGN KEY([genre_id])
REFERENCES [dbo].[Genre] ([ID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK__Book__genre_id__571DF1D5]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK__Book__publicatio__5812160E] FOREIGN KEY([publication_id])
REFERENCES [dbo].[Publication] ([ID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK__Book__publicatio__5812160E]
GO
ALTER TABLE [dbo].[BookTransactions]  WITH CHECK ADD  CONSTRAINT [FK_BookTransactions_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([id])
GO
ALTER TABLE [dbo].[BookTransactions] CHECK CONSTRAINT [FK_BookTransactions_Books]
GO
ALTER TABLE [dbo].[BookTransactions]  WITH CHECK ADD  CONSTRAINT [FK_BookTransactions_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BookTransactions] CHECK CONSTRAINT [FK_BookTransactions_Users]
GO
ALTER TABLE [dbo].[BookTransactions]  WITH CHECK ADD CHECK  (([Status]='overdue' OR [Status]='lending' OR [Status]='Requested'))
GO
USE [master]
GO
ALTER DATABASE [LibraryManagment] SET  READ_WRITE 
GO
