USE [tyu]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 4/27/2020 6:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Group_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Group_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 4/27/2020 6:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[Page_Id] [int] IDENTITY(1,1) NOT NULL,
	[Group_Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[Page_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/27/2020 6:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersGroups]    Script Date: 4/27/2020 6:57:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersGroups](
	[User_Id] [int] NOT NULL,
	[Group_Id] [int] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([Group_Id], [Name]) VALUES (1, N'Группа1   ')
INSERT [dbo].[Group] ([Group_Id], [Name]) VALUES (2, N'Группа2   ')
INSERT [dbo].[Group] ([Group_Id], [Name]) VALUES (3, N'Группа3   ')
INSERT [dbo].[Group] ([Group_Id], [Name]) VALUES (4, N'Группа4   ')
INSERT [dbo].[Group] ([Group_Id], [Name]) VALUES (5, N'Группа5   ')
INSERT [dbo].[Group] ([Group_Id], [Name]) VALUES (6, N'Группа6   ')
SET IDENTITY_INSERT [dbo].[Group] OFF
SET IDENTITY_INSERT [dbo].[Page] ON 

INSERT [dbo].[Page] ([Page_Id], [Group_Id], [Name]) VALUES (1, 1, N'page1')
INSERT [dbo].[Page] ([Page_Id], [Group_Id], [Name]) VALUES (2, 2, N'page2')
INSERT [dbo].[Page] ([Page_Id], [Group_Id], [Name]) VALUES (3, 3, N'page3')
INSERT [dbo].[Page] ([Page_Id], [Group_Id], [Name]) VALUES (4, 4, N'page4')
INSERT [dbo].[Page] ([Page_Id], [Group_Id], [Name]) VALUES (5, 5, N'page5')
INSERT [dbo].[Page] ([Page_Id], [Group_Id], [Name]) VALUES (6, 6, N'page6')
INSERT [dbo].[Page] ([Page_Id], [Group_Id], [Name]) VALUES (7, 2, N'page7')
SET IDENTITY_INSERT [dbo].[Page] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([User_Id], [Name]) VALUES (1, N'Иван      ')
INSERT [dbo].[User] ([User_Id], [Name]) VALUES (2, N'Петр      ')
INSERT [dbo].[User] ([User_Id], [Name]) VALUES (3, N'Николай   ')
INSERT [dbo].[User] ([User_Id], [Name]) VALUES (4, N'Григорий  ')
INSERT [dbo].[User] ([User_Id], [Name]) VALUES (5, N'Денис     ')
INSERT [dbo].[User] ([User_Id], [Name]) VALUES (6, N'Виктор    ')
SET IDENTITY_INSERT [dbo].[User] OFF
INSERT [dbo].[UsersGroups] ([User_Id], [Group_Id]) VALUES (1, 1)
INSERT [dbo].[UsersGroups] ([User_Id], [Group_Id]) VALUES (2, 1)
INSERT [dbo].[UsersGroups] ([User_Id], [Group_Id]) VALUES (2, 2)
INSERT [dbo].[UsersGroups] ([User_Id], [Group_Id]) VALUES (3, 1)
INSERT [dbo].[UsersGroups] ([User_Id], [Group_Id]) VALUES (4, 5)
INSERT [dbo].[UsersGroups] ([User_Id], [Group_Id]) VALUES (6, 2)
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Group] FOREIGN KEY([Group_Id])
REFERENCES [dbo].[Group] ([Group_Id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Group]
GO
ALTER TABLE [dbo].[UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersGroups_Group] FOREIGN KEY([Group_Id])
REFERENCES [dbo].[Group] ([Group_Id])
GO
ALTER TABLE [dbo].[UsersGroups] CHECK CONSTRAINT [FK_UsersGroups_Group]
GO
ALTER TABLE [dbo].[UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK_UsersGroups_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([User_Id])
GO
ALTER TABLE [dbo].[UsersGroups] CHECK CONSTRAINT [FK_UsersGroups_User]
GO
