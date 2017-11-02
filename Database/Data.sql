USE [LyPlanDatabase]
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([Id], [Name]) VALUES (1, N'Todo')
INSERT [dbo].[Type] ([Id], [Name]) VALUES (2, N'Daily')
SET IDENTITY_INSERT [dbo].[Type] OFF
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (1, N'clean cai con cac', N'Cleaning the house becasue it is too dirty', 1, NULL)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (2, N'Buying mouse', N'Buying new computer mouse because the last one is broken', 1, NULL)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (3, N'Teach math for baby', N'Tha´ng need to learn math', 2, NULL)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (4, N'Number', N'Teach Tha´ng number from 1 to 10', 2, 3)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (5, N'Add operation', N'Teach Tha´ng add number', 2, 3)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (6, N'Sub operation', N'Teach Tha´ng sub number', 2, 3)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (7, N'Feed baby', N'Feed Tha´ng', 2, NULL)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (8, N'Breakfast', N'Feed Breakfast for Tha´ng', 2, 7)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (9, N'Lunch', N'Feed Lunch for Tha´ng', 2, 7)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (10, N'Dinner', N'Feed Dinner for Tha´ng', 2, 7)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (18, N'Learning', NULL, 1, NULL)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (19, N'Learning', NULL, 1, NULL)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (20, N'Lalaland', N'Lalaland is a movie', 2, NULL)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (23, N'Chapter 1', N'Chapter1', 2, 20)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (24, N'Daily', N'Daily Work', 2, NULL)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (25, N'Monday Daily', N'Monday Daily', 2, 24)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (26, N'Tuesday Daily', N'Tuesday Daily', 2, 24)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (27, N'Wednesday Daily', N'Wednesday Daily', 2, 24)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (28, N'Thursday Daily', N'Thursday Daily', 2, 24)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (29, N'Firstday Daily', N'Firstday Daily', 2, 24)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (30, N'Saturday Daily', N'Saturday Daily', 2, 24)
INSERT [dbo].[Task] ([Id], [Title], [Description], [TypeId], [SuperTask]) VALUES (31, N'Sunday Daily', N'Sunday Daily', 2, 24)
SET IDENTITY_INSERT [dbo].[Task] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([Id], [Name]) VALUES (1, N'NotDone')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (2, N'Early')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (3, N'Doing')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (4, N'Late')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (5, N'Done')
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[Work] ON 

INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (6, 1, N'clean con cacccccccc', CAST(N'2017-01-01 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 5)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (7, 2, N'Buying new computer mouse because the last one is broken', CAST(N'2017-01-01 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (8, 18, N'Learn c#', CAST(N'2017-10-31 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (9, 19, N'Learn c#', CAST(N'2017-10-31 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (10, 23, N'Monday', CAST(N'2017-10-31 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (11, 24, N'Monday', CAST(N'2017-10-30 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (12, 24, N'Tuesday', CAST(N'2017-10-31 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (13, 24, N'Wednesday', CAST(N'2017-11-01 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (14, 24, N'Thursday', CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (15, 24, N'Firstday', CAST(N'2017-11-03 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (16, 24, N'Saturday', CAST(N'2017-11-04 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Work] ([Id], [TaskId], [Description], [StartTime], [Deadline], [AlertTime], [StatusId]) VALUES (17, 24, N'Sunday', CAST(N'2017-11-05 00:00:00.000' AS DateTime), CAST(N'2017-11-02 00:00:00.000' AS DateTime), CAST(N'2017-11-01 00:00:00.000' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Work] OFF