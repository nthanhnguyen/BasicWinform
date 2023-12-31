USE [master]
GO
/****** Object:  Database [WinFormDb]    Script Date: 09/24/2023 1:12:56 AM ******/
CREATE DATABASE [WinFormDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WinFormDb', FILENAME = N'C:\Users\Acer\WinFormDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WinFormDb_log', FILENAME = N'C:\Users\Acer\WinFormDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WinFormDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WinFormDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WinFormDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WinFormDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WinFormDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WinFormDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WinFormDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [WinFormDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WinFormDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WinFormDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WinFormDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WinFormDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WinFormDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WinFormDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WinFormDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WinFormDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WinFormDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WinFormDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WinFormDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WinFormDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WinFormDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WinFormDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WinFormDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WinFormDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WinFormDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WinFormDb] SET  MULTI_USER 
GO
ALTER DATABASE [WinFormDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WinFormDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WinFormDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WinFormDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WinFormDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WinFormDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WinFormDb] SET QUERY_STORE = OFF
GO
USE [WinFormDb]
GO
/****** Object:  Table [dbo].[user]    Script Date: 09/24/2023 1:12:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[UserID] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Tel] [nvarchar](max) NULL,
	[Disable] [tinyint] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[user] ([UserID], [UserName], [Password], [Email], [Tel], [Disable]) VALUES (N'U01', N'Nguyen', N'123456', N'nguyen@asoft.com', N'0123456789', 1)
INSERT [dbo].[user] ([UserID], [UserName], [Password], [Email], [Tel], [Disable]) VALUES (N'U02', N'Anh', N'123456', N'anh@asoft.com', N'01234567888', 0)
INSERT [dbo].[user] ([UserID], [UserName], [Password], [Email], [Tel], [Disable]) VALUES (N'U03', N'Thai', N'123', N'thai@asoft.com', N'0123456666', 0)
INSERT [dbo].[user] ([UserID], [UserName], [Password], [Email], [Tel], [Disable]) VALUES (N'U04', N'Vinh', N'123456', N'vinh@asoft.com', N'012345666', 1)
INSERT [dbo].[user] ([UserID], [UserName], [Password], [Email], [Tel], [Disable]) VALUES (N'U05', N'Bac', N'123', N'bac@asoft.com', N'056788888', 1)
GO
/****** Object:  StoredProcedure [dbo].[CheckUserExists]    Script Date: 09/24/2023 1:12:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckUserExists]
    @UserID nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserCount INT;

    -- Kiểm tra xem mã người dùng đã tồn tại trong bảng chưa
    SELECT @UserCount = COUNT(*)
    FROM [dbo].[user]
    WHERE [UserID] = @UserID;

    -- Nếu tồn tại, trả về thông báo lỗi
    IF @UserCount > 0
    BEGIN
        THROW 50000, 'Mã người dùng đã tồn tại. Vui lòng chọn mã người dùng khác.', 1;
        RETURN;
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[CheckValidEmail]    Script Date: 09/24/2023 1:12:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckValidEmail]
    @Email nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Pattern nvarchar(255) = '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$';

    IF @Email IS NULL
    BEGIN
        PRINT 'Email is NULL. Please provide a valid email address.';
        RETURN;
    END;

    IF @Email NOT LIKE @Pattern
    BEGIN
        PRINT 'Invalid email address. Please provide a valid email address.';
        RETURN;
    END;
END;
GO
/****** Object:  StoredProcedure [dbo].[CheckValidEmail1]    Script Date: 09/24/2023 1:12:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckValidEmail1]
    @Email nvarchar(50),
    @IsValid bit OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Khởi tạo biến đầu ra
    SET @IsValid = 0;

    -- Kiểm tra xem địa chỉ email có NULL không
    IF @Email IS NULL
    BEGIN
        RETURN;
    END;

    -- Kiểm tra xem địa chỉ email chứa ký tự '@' và có ít nhất một ký tự '.' sau ký tự '@'
    IF CHARINDEX('@', @Email) > 0 AND CHARINDEX('.', @Email, CHARINDEX('@', @Email)) > CHARINDEX('@', @Email)
    BEGIN
        SET @IsValid = 1;
    END;
END;
GO
USE [master]
GO
ALTER DATABASE [WinFormDb] SET  READ_WRITE 
GO
