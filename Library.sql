USE [master]
GO

/****** Object:  Database [Library]    Script Date: 3/23/2016 11:12:24 PM ******/
CREATE DATABASE [Library]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Library', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Library.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Library_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Library_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [Library] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Library].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Library] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Library] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Library] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Library] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Library] SET ARITHABORT OFF 
GO

ALTER DATABASE [Library] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Library] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Library] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Library] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Library] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Library] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Library] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Library] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Library] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Library] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Library] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Library] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Library] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Library] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Library] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Library] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Library] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Library] SET RECOVERY FULL 
GO

ALTER DATABASE [Library] SET  MULTI_USER 
GO

ALTER DATABASE [Library] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Library] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Library] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Library] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [Library] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Library] SET  READ_WRITE 
GO

/****************************TABLES************************************/

USE [Library]
GO

/****** Object:  Table [dbo].[Books]    Script Date: 3/23/2016 11:12:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Author] [varchar](50) NOT NULL,
	[ISBN] [varchar](50) NOT NULL,
	[PageCount] [int] NOT NULL,
	[PublishDate] [smalldatetime] NOT NULL,
	[IsAvailable] [bit] NOT NULL CONSTRAINT [DF_Books_IsAvailable]  DEFAULT ((1)),
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Users]    Script Date: 3/23/2016 11:12:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[CardNumber] [varchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[UsersBooks]    Script Date: 3/23/2016 11:12:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsersBooks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[CheckOutDate] [smalldatetime] NOT NULL,
	[CheckInDate] [smalldatetime] NULL,
 CONSTRAINT [PK_UsersBooks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UsersBooks]  WITH NOCHECK ADD  CONSTRAINT [FK_UsersBooks_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO

ALTER TABLE [dbo].[UsersBooks] CHECK CONSTRAINT [FK_UsersBooks_Books]
GO

ALTER TABLE [dbo].[UsersBooks]  WITH NOCHECK ADD  CONSTRAINT [FK_UsersBooks_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UsersBooks] CHECK CONSTRAINT [FK_UsersBooks_Users]
GO

/*********************VIEW******************************************/

USE [Library]
GO

/****** Object:  View [dbo].[vCheckedOutBooks]    Script Date: 3/23/2016 11:13:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vCheckedOutBooks]
AS
SELECT        dbo.UsersBooks.UserId, dbo.UsersBooks.BookId AS Id, dbo.UsersBooks.CheckOutDate, dbo.Books.Title, dbo.Books.Author, dbo.Books.ISBN, dbo.Books.PageCount, 
                         dbo.Books.PublishDate, dbo.Books.IsAvailable, dbo.Users.FirstName, dbo.Users.LastName, dbo.Users.CardNumber, dbo.UsersBooks.CheckInDate, 
                         dbo.UsersBooks.Id AS LinkId
FROM            dbo.UsersBooks INNER JOIN
                         dbo.Books ON dbo.UsersBooks.BookId = dbo.Books.Id INNER JOIN
                         dbo.Users ON dbo.UsersBooks.UserId = dbo.Users.Id

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[52] 4[17] 2[15] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UsersBooks"
            Begin Extent = 
               Top = 8
               Left = 15
               Bottom = 137
               Right = 185
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Books"
            Begin Extent = 
               Top = 156
               Left = 240
               Bottom = 285
               Right = 410
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 26
               Left = 425
               Bottom = 155
               Right = 595
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vCheckedOutBooks'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vCheckedOutBooks'
GO

/************************************SPs**********************************/

USE [Library]
GO

/****** Object:  StoredProcedure [dbo].[prUsersList]    Script Date: 3/23/2016 11:14:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[prUsersList]
AS
SELECT * FROM Users
GO

/****** Object:  StoredProcedure [dbo].[spBookCheckIn]    Script Date: 3/23/2016 11:14:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spBookCheckIn]
	@Id int
AS
	UPDATE dbo.UsersBooks
	SET CheckInDate = GETDATE()
	WHERE Id = @Id

	UPDATE Books SET IsAvailable = 1
	WHERE Id = (SELECT BookId FROM UsersBooks WHERE Id = @Id)

GO

/****** Object:  StoredProcedure [dbo].[spBookCheckOut]    Script Date: 3/23/2016 11:14:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spBookCheckOut]
	@UserId int,
	@BookId int
AS
	INSERT INTO dbo.UsersBooks(UserId, BookId, CheckOutDate)
	VALUES(@UserId, @BookId, GETDATE())

	UPDATE Books SET IsAvailable = 0
	WHERE Id = @BookId
GO

/****** Object:  StoredProcedure [dbo].[spBooksFindByAuthor]    Script Date: 3/23/2016 11:14:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spBooksFindByAuthor]
	@Author varchar(50)
AS
	SELECT * FROM Books 
	WHERE Author LIKE '%' + @Author + '%'

GO

/****** Object:  StoredProcedure [dbo].[spBooksFindByISBN]    Script Date: 3/23/2016 11:14:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spBooksFindByISBN]
	@ISBN varchar(50)
AS
	SELECT * FROM Books 
	WHERE ISBN = @ISBN

GO

/****** Object:  StoredProcedure [dbo].[spBooksFindByTitle]    Script Date: 3/23/2016 11:14:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spBooksFindByTitle]
	@Title varchar(100)
AS
	SELECT * FROM Books 
	WHERE Title LIKE '%' + @Title + '%'

GO

/****** Object:  StoredProcedure [dbo].[spBooksListByUser]    Script Date: 3/23/2016 11:14:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spBooksListByUser]
	@UserId int
AS
	SELECT * FROM vCheckedOutBooks
	WHERE UserId = @UserId AND CheckInDate IS NULL

GO

