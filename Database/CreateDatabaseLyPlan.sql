USE [master]
GO
/****** Object:  Database [LyPlanDatabase]    Script Date: 05/11/2017 21:04:51 ******/
CREATE DATABASE [LyPlanDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LyPlanDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\LyPlanDatabase.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LyPlanDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\LyPlanDatabase_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LyPlanDatabase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LyPlanDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LyPlanDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [LyPlanDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LyPlanDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LyPlanDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LyPlanDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LyPlanDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LyPlanDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [LyPlanDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LyPlanDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LyPlanDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LyPlanDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [LyPlanDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
USE [LyPlanDatabase]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 05/11/2017 21:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Task]    Script Date: 05/11/2017 21:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TypeId] [int] NOT NULL,
	[SuperTask] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Type]    Script Date: 05/11/2017 21:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Work]    Script Date: 05/11/2017 21:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Work](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[StartTime] [datetime] NOT NULL,
	[Deadline] [datetime] NULL,
	[AlertTime] [datetime] NULL,
	[StatusId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Status] ON 

GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (1, N'NotDone')
GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (2, N'Early')
GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (3, N'Doing')
GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (4, N'Late')
GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (5, N'Done')
GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (6, N'Removed')
GO
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Task] ON 

GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (60, N'Học java', NULL, 1, NULL)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (61, N'Học C#', NULL, 1, NULL)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (62, N'Tập thể dục', N'Bảo vệ sức khỏe', 2, NULL)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (63, N'Gập bụng 10 cái', N'', 2, 62)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (64, N'Xoạc chân 3 cái', N'', 2, 62)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (65, N'Hít đất 10 cái', N'', 2, 62)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (66, N'Học tiếng Anh', N'', 2, NULL)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (67, N'Nghe hội thoại 5 phút', N'', 2, 66)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (68, N'Đọc một đoạn 10 dòng', N'', 2, 66)
GO
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (69, N'Nói chuyện với người Anh', N'', 2, 66)
GO
SET IDENTITY_INSERT [dbo].[Task] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

GO
INSERT [dbo].[Type] ([Id], [Name]) VALUES (1, N'Todo')
GO
INSERT [dbo].[Type] ([Id], [Name]) VALUES (2, N'Daily')
GO
INSERT [dbo].[Type] ([Id], [Name]) VALUES (3, N'Removed')
GO
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
SET IDENTITY_INSERT [dbo].[Work] ON 

GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (23, 60, N'Java EE', CAST(N'2017-11-05 00:00:00.000' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (24, 61, N'Wpf hộc', CAST(N'2017-11-05 00:00:00.000' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (26, 63, NULL, CAST(N'2017-11-05 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (27, 63, NULL, CAST(N'2017-10-30 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (28, 65, NULL, CAST(N'2017-10-31 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (29, 65, NULL, CAST(N'2017-10-31 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (30, 65, NULL, CAST(N'2017-10-31 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (31, 64, NULL, CAST(N'2017-10-31 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (32, 66, NULL, CAST(N'2017-11-01 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (33, 67, NULL, CAST(N'2017-11-02 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (34, 68, NULL, CAST(N'2017-11-02 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (35, 69, NULL, CAST(N'2017-11-02 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (36, 67, NULL, CAST(N'2017-11-05 00:00:00.000' AS DateTime), NULL, NULL, 2)
GO
SET IDENTITY_INSERT [dbo].[Work] OFF
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_TASK_TASK] FOREIGN KEY([SuperTask])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_TASK_TASK]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_TASK_TYPE] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Type] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_TASK_TYPE]
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD  CONSTRAINT [FK_WORK_STATUS] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Work] CHECK CONSTRAINT [FK_WORK_STATUS]
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD  CONSTRAINT [FK_WORK_TASK] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[Work] CHECK CONSTRAINT [FK_WORK_TASK]
GO
USE [master]
GO
ALTER DATABASE [LyPlanDatabase] SET  READ_WRITE 
GO
