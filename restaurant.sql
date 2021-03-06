USE [restaurant]
GO
/****** Object:  Table [dbo].[cuisines]    Script Date: 3/3/2016 4:38:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cuisines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[restaurants]    Script Date: 3/3/2016 4:38:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[restaurants](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[address] [varchar](255) NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[reviews]    Script Date: 3/3/2016 4:38:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[reviews](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](255) NULL,
	[post_date] [date] NULL,
	[comment] [text] NULL,
	[rating] [int] NULL,
	[restaurant_id] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cuisines] ON 

INSERT [dbo].[cuisines] ([id], [name]) VALUES (7, N'sushi')
INSERT [dbo].[cuisines] ([id], [name]) VALUES (8, N'Pho')
SET IDENTITY_INSERT [dbo].[cuisines] OFF
SET IDENTITY_INSERT [dbo].[restaurants] ON 

INSERT [dbo].[restaurants] ([id], [name], [address], [cuisine_id]) VALUES (6, N'bamboo', N'123 5th ave, Portland, Oregon', 7)
INSERT [dbo].[restaurants] ([id], [name], [address], [cuisine_id]) VALUES (7, N'pho zen', N'222 ne portand ave, portand oregon', 8)
SET IDENTITY_INSERT [dbo].[restaurants] OFF
