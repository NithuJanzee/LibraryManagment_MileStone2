USE [master]
GO
/****** Object:  Database [LibraryManagment]    Script Date: 10/29/2024 6:45:23 PM ******/
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
/****** Object:  Table [dbo].[Author]    Script Date: 10/29/2024 6:45:23 PM ******/
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
/****** Object:  Table [dbo].[Book]    Script Date: 10/29/2024 6:45:23 PM ******/
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
/****** Object:  Table [dbo].[BookTransactions]    Script Date: 10/29/2024 6:45:23 PM ******/
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
/****** Object:  Table [dbo].[Genre]    Script Date: 10/29/2024 6:45:23 PM ******/
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
/****** Object:  Table [dbo].[History]    Script Date: 10/29/2024 6:45:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[BookId] [uniqueidentifier] NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[RequestedDate] [datetime] NULL,
	[LendedDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[ReturnedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publication]    Script Date: 10/29/2024 6:45:23 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 10/29/2024 6:45:23 PM ******/
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
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'cfdfbad7-69fa-4929-ac3f-125ed4ec75e6', N'Simon Schama', N'/uploads/0a5b440e-0c0f-4c78-b039-9ba0f69a69c4_Simon Schama au.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'bb36cba7-9aac-4760-acac-22b962a972a3', N'marigin haverbeke', N'/uploads/1736c5bb-3a1f-44e9-b693-cfd8ad760cbf_eloquent_javascript_a_modern_introduction_to_programming_by_marijn_haverbeke.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'6aa46036-bd4c-466d-832e-283b4511a2a0', N'Darrel I Graham', N'/uploads/1e1c8424-6293-42de-9f09-6ca8cb42aceb_51lMH1gK5lL.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'a2b490eb-64b3-4ff0-b2a9-2aea1d7dcd10', N'Kalki', N'/uploads/17140b4e-b5fc-419a-abc2-f7224b7d9e86_kalki3-400x549.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'c34e25b3-3240-4067-abd2-2ccf4af2b638', N'Orilley', N'/uploads/4a8843da-86c5-4632-9374-3a76d0c7d554_bill-oreilly-divorce-documents-cheating-sealed-pp.webp')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'81dfba18-654e-44df-b4b6-5eb724a33f3c', N'shashbiare', N'/uploads/473382c8-d726-4bf7-8d46-32420b5a22d9_coverimage-9782380377910-bookwire-2023-09-18t10-00.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'888882b2-ca0b-4ead-a9ba-640c1fd9e2fb', N'Mother Goose ', N'/uploads/56cfc018-4123-4050-8bbb-0746caa4c13c_Mother Goose  original.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'59e41553-cfdd-4979-953c-70aa5f99f86e', N'Mein Kampf', N'/uploads/b2c28ca6-52c7-4c20-9199-9527e9d3789b_Nazi author.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'334bf234-22bd-4768-9bbd-7ad61191581d', N'Kati Marton', N'/uploads/56bc667e-b5bb-48f3-91d6-ce4c36a3c442_chancellor of germany Author.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'a53a30ed-3036-46d9-996e-7fb76ee2a0df', N'Adam Dressler', N'/uploads/f947542b-762d-4dde-aa66-06a6d8c3e25f_Führer of Germany author.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'fa5697c2-f875-4405-a95a-985c7f54599c', N'Jhonal sope', N'/uploads/966616af-b6b5-4477-9be7-477cf95ba2c0_Jon-Sopel.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'9433f6f4-d716-4f83-b77e-a63519c76f48', N'Gaylee', N'/uploads/8ab3c4f8-cdab-477f-a856-4666e461ea92_GettyImages-491037944-5bf2bc0bc9e77c00266a321e.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'a6d9894d-6575-4255-9258-ab9f023b9f1d', N'J. K. Rowling', N'/uploads/feee83e1-f474-4334-8883-3b06ecd80eb8_hp au.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'9fdd2fc5-cb5a-43ed-b439-aefc8c4b63f4', N'J.R.R Tolkien', N'/uploads/979ab91f-40f9-4615-9d1e-dfdd8457d980_OIP.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'dc315560-c4e4-4aa5-9c9a-cf37f9440a10', N'Simon Schama', N'/uploads/f5b7acdd-3a81-4a3e-a60a-a98a0b891890_Simon Schama au.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'da0a7bed-1a86-4942-a13b-ea34563e92bb', N'Jayakanthan', N'/uploads/e9b04b1c-788f-4eef-bf33-55ce053f5fe0_R.jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'cb23d0d3-e9f2-461c-93b3-efce24272ea3', N'HarperLee', N'/uploads/2f546489-de18-4f70-811c-51e1075768ff_OIP (2).jpg')
INSERT [dbo].[Author] ([AuthorId], [Name], [Image]) VALUES (N'dd8da6f6-9fea-4278-a744-fe28a493fcb3', N'Rudolf Hess', N'/uploads/2868b545-91f7-4966-8c7a-96cb1ca9e3df_Nazi author.jpg')
GO
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'38443b00-5fa3-4502-b6b8-1ba33ad4e57d', N'Sila Nerangalil Sila Manithargal', 50, N'da0a7bed-1a86-4942-a13b-ea34563e92bb', N'391fd705-a1b8-4454-a436-ed20dd66b9c4', N'374bd1fb-ad04-4181-9f72-488d3c3116d7', N'/uploads/57e0223d-b857-42cc-8478-7ab47ea950ff_7465733.jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'1daab847-f2df-4f2f-b7cc-4b9f37ba575c', N'Crack The Coding Interview', 70, N'9433f6f4-d716-4f83-b77e-a63519c76f48', N'cc59320d-27c1-469b-abe0-39570be42575', N'839909cd-e202-47a8-a72a-a1a7830e798b', N'/uploads/bb2705d1-f6f5-4c6e-9ec7-253e62da47f9_review-cracking-the-coding-interview-bookslegit.jpeg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'e16f6c09-de93-4d56-8f45-5a59e79d4223', N'Harry Potter', 100, N'a6d9894d-6575-4255-9258-ab9f023b9f1d', N'c443e9fc-2f62-476d-8084-b67fa907aa8b', N'b92cb0ef-e635-4aad-93b3-bc4db4998227', N'/uploads/890065c0-c76e-4b17-b213-fbc2edb63cb4_hp.jpg,/uploads/73714e06-10d9-440e-b794-9420ebba108e_hp2.jpg,/uploads/1458bed7-6929-48df-b49a-62917a0349de_hp3.jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'f7360654-ffc8-4cc7-80b5-64b610aed9d7', N'To Kill The Mocking Bird', 80, N'cb23d0d3-e9f2-461c-93b3-efce24272ea3', N'888178c8-0114-4b7a-80e9-dcaf7b091299', N'ac0fee92-2201-4772-8e85-a18ab4302860', N'/uploads/17baec95-915f-4bb4-bc75-ee4944470824_TKM.jpg,/uploads/0b1cb977-6fa6-4443-a660-bf3990820dc6_R (2).jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'a96a446d-43f9-4507-992d-705cd8ed265a', N'Ponniyin Selvan', 50, N'a2b490eb-64b3-4ff0-b2a9-2aea1d7dcd10', N'b2ee4310-87e7-423f-9294-4d851a404b46', N'374bd1fb-ad04-4181-9f72-488d3c3116d7', N'/uploads/bc2aa688-4c2b-405c-8a35-df914c48ca54_OIP.jpg,/uploads/7c818665-8dc9-46d9-b2da-05845272ece5_kalkis-ponniyin-selvan-00001-1-672x1024.jpg,/uploads/cc626ae5-cd84-49be-83f3-b9ef558d9457_ponniyin-selvan-part-1-to-5-original-imad6vsfubujwqzf.webp')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'c86341a7-b65f-44df-a7d3-78b5a331d20b', N'C Programming Language', 60, N'6aa46036-bd4c-466d-832e-283b4511a2a0', N'cc59320d-27c1-469b-abe0-39570be42575', N'e72d0db4-3d24-4f9f-9b47-5481d771008f', N'/uploads/aca1e525-da4c-46e4-a21f-70e04adbd9ea_R.jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'4ffb0e06-030d-4b27-ad6c-7ea3870cfa12', N'JavaScript EloQuent', 80, N'bb36cba7-9aac-4760-acac-22b962a972a3', N'cc59320d-27c1-469b-abe0-39570be42575', N'35e8649f-7b33-4c22-887f-e8cdcd11aa19', N'/uploads/170d1d75-544b-49fc-b35d-07cd1d11cacd_eloquent_javascript_book_cover_b-1550x2048.jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'd54f9068-db0a-46ca-9e6c-7f5fbee861b3', N'Harry Potter', 100, N'a6d9894d-6575-4255-9258-ab9f023b9f1d', N'c443e9fc-2f62-476d-8084-b67fa907aa8b', N'051ce115-1f2e-4ded-91ba-e3b608595d1d', N'/uploads/2d3d4de8-96c9-4e7f-ab51-4433b5dd0e9d_hp.jpg,/uploads/0acb2bd9-bacf-42b9-bb42-09652a8f06a0_hp2.jpg,/uploads/68455acc-2252-4fc2-ae70-8a81f1a2b6f8_hp3.jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'2d20681b-806f-46d3-8ece-8b9f040f2c30', N'Python', 60, N'c34e25b3-3240-4067-abd2-2ccf4af2b638', N'cc59320d-27c1-469b-abe0-39570be42575', N'ac0fee92-2201-4772-8e85-a18ab4302860', N'/uploads/6394faaf-4dbf-4852-b908-beaef95ea5d4_fa9574f2-2d73-43ba-b3c5-9e0ad2cffe5d_1.41d85ba8dc65323c6cd0d0485dfdb406.webp')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'5c610a1e-9ab1-48c3-8ae7-9810de5ab4e2', N'The Hobbits', 50, N'9fdd2fc5-cb5a-43ed-b439-aefc8c4b63f4', N'b2ee4310-87e7-423f-9294-4d851a404b46', N'4d4d0e63-ac2d-49aa-94ed-7e956007b271', N'/uploads/1d847f13-8083-45e3-95c6-b66f12e0d279_The_Hobbit-1668671004.jpg,/uploads/e4620c08-07a2-4ec8-bad0-e97e80192ea6_R (1).jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'bd80b6b6-4b93-41ee-8118-b4b86c636939', N'HTML-5', 60, N'dd8da6f6-9fea-4278-a744-fe28a493fcb3', N'cc59320d-27c1-469b-abe0-39570be42575', N'1511bb4f-d6f0-4880-97a6-f8611bd190cd', N'/uploads/08ab40c3-5ae1-4184-9bbe-b147261471c0_OIP (1).jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'ecec6d07-ffa6-4750-b716-b7fdfd75664e', N'Lord Of The Rings', 80, N'9fdd2fc5-cb5a-43ed-b439-aefc8c4b63f4', N'b2ee4310-87e7-423f-9294-4d851a404b46', N'4d4d0e63-ac2d-49aa-94ed-7e956007b271', N'/uploads/8d458f5d-cdf7-48a6-b46f-23252a9ecf83_OIP (1).jpg,/uploads/9063d0e8-9099-4ed0-aac8-68d95ddb1ef4_954877651d87dfd2e5386d298006effb.jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'947c0dcd-b835-4f48-90dd-c3b918d4cad4', N'C#', 40, N'c34e25b3-3240-4067-abd2-2ccf4af2b638', N'cc59320d-27c1-469b-abe0-39570be42575', N'e72d0db4-3d24-4f9f-9b47-5481d771008f', N'/uploads/46b5d090-f99f-4512-b15e-90940d01d8be_Programming-C-8.0-Build-Windows-Web-and-Desktop-Applications.jpg')
INSERT [dbo].[Book] ([id], [name], [copies], [author_id], [genre_id], [publication_id], [image]) VALUES (N'd5567cbf-e9f2-4e93-8ef7-fb2a70bcd906', N'Node Js', 60, N'fa5697c2-f875-4405-a95a-985c7f54599c', N'cc59320d-27c1-469b-abe0-39570be42575', N'e72d0db4-3d24-4f9f-9b47-5481d771008f', N'/uploads/f22e75c1-79c2-4eff-86bf-16fa86eeadfe_get-programming-with-node-js-9781617294747_xlg.jpg')
GO
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'cc59320d-27c1-469b-abe0-39570be42575', N'Education')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'b2ee4310-87e7-423f-9294-4d851a404b46', N'fantasy')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'b890af19-5925-4f6f-a35c-62ff2acb27a6', N'Comedy')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'5572cb3c-9d37-4041-8c05-73e3139b0daf', N'History')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'd85be73b-cf59-468d-b4ec-afd2c4c89a2b', N'Biography')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'c443e9fc-2f62-476d-8084-b67fa907aa8b', N'Fantasy')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'efc24c37-f1c8-400e-a7b7-c3f2f8f83794', N'True story')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'e6af0dd3-0637-422b-980f-c79f7d7eb197', N'Jewish history')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'ec890696-aeca-49e8-afa7-cdff3f437ab9', N'True Story')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'888178c8-0114-4b7a-80e9-dcaf7b091299', N'Fantasy')
INSERT [dbo].[Genre] ([ID], [Name]) VALUES (N'391fd705-a1b8-4454-a436-ed20dd66b9c4', N'Drama')
GO
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'c8e69571-890f-452e-89be-1b14465be932', N'German Constitution')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'eb106e9e-1558-4e8d-a0b5-3fcddee9a53b', N'catty. 1744 ')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'374bd1fb-ad04-4181-9f72-488d3c3116d7', N'India')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'e72d0db4-3d24-4f9f-9b47-5481d771008f', N'Aid for International')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'4d4d0e63-ac2d-49aa-94ed-7e956007b271', N'Lockwood Kipling')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'ac0fee92-2201-4772-8e85-a18ab4302860', N'Engalnd')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'839909cd-e202-47a8-a72a-a1a7830e798b', N'The Bodley Head (UK)')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'4bf5e279-59f3-4a18-9a5d-aeaac58588b5', N'Position Established')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'2c24c63b-8c6e-4ca2-a51a-b4c8458e5e45', N'Biography')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'b92cb0ef-e635-4aad-93b3-bc4db4998227', N'26 June 1997 – 21 July 2007')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'051ce115-1f2e-4ded-91ba-e3b608595d1d', N'Bloomsbury')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'35e8649f-7b33-4c22-887f-e8cdcd11aa19', N'haverbeke')
INSERT [dbo].[Publication] ([ID], [Name]) VALUES (N'1511bb4f-d6f0-4880-97a6-f8611bd190cd', N'LUKE Stevens')
GO
INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [NIC], [Email], [PhoneNumber], [Password]) VALUES (N'1d9137fd-20e0-43a6-a961-9d1aabf3a4a3', N'Gajanika', N'Ganeshalingam', N'2003', N'gaju@gmail.com', N'011', N'123')
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF__Book__id__5535A963]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Genre] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[History] ADD  DEFAULT (newid()) FOR [Id]
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
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([id])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_Book]
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_User]
GO
ALTER TABLE [dbo].[BookTransactions]  WITH CHECK ADD CHECK  (([Status]='overdue' OR [Status]='lending' OR [Status]='Requested'))
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD CHECK  (([Status]='returned-overdue' OR [Status]='returned-onTime' OR [Status]='lending' OR [Status]='requested'))
GO
USE [master]
GO
ALTER DATABASE [LibraryManagment] SET  READ_WRITE 
GO
