IF OBJECT_ID(N'[preddy].[dbo].[TweetLog]', N'U') IS NOT NULL
    DROP TABLE [dbo].[TweetLog]
GO

CREATE TABLE [dbo].[TweetLog] (
	[Id] [uniqueidentifier] NOT NULL,
	[StatusId] [nvarchar](40) NULL,
	[UserId] [nvarchar](40) NULL,
	[UserName] [nvarchar](40) NULL,
	[ScreenName] [nvarchar](40) NULL,
	[ProfileImageUrl] [nvarchar](1024) NULL,
	[MediaUrl] [nvarchar](1024) NULL,
	[Text] [nvarchar](200) NULL,
	[TweetedAt] [datetime2] NOT NULL,
	[CreatedAt] [datetime2] NOT NULL,
	[UpdatedAt] [datetime2] NOT NULL,
	CONSTRAINT [PK_TweetLog] PRIMARY KEY
	(
		[Id] ASC
	)
)
GO

IF OBJECT_ID(N'[preddy].[dbo].[TweetResult]', N'U') IS NOT NULL
    DROP TABLE [dbo].[TweetResult]
GO

CREATE TABLE [dbo].[TweetResult] (
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Day] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[CreatedAt] [datetime2] NOT NULL,
	[UpdatedAt] [datetime2] NOT NULL,
	CONSTRAINT [PK_TweetResult] PRIMARY KEY
	(
		[Id] ASC
	)
)
GO

IF OBJECT_ID(N'[preddy].[dbo].[TweetForecast]', N'U') IS NOT NULL
    DROP TABLE [dbo].[TweetForecast]
GO

CREATE TABLE [dbo].[TweetForecast] (
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Day] [int] NOT NULL,
	[Count] [float] NOT NULL,
	[CreatedAt] [datetime2] NOT NULL,
	[UpdatedAt] [datetime2] NOT NULL,
	CONSTRAINT [PK_TweetForecast] PRIMARY KEY
	(
		[Id] ASC
	)
)
GO

CREATE UNIQUE INDEX [IX_TweetLog_StatusId] ON [dbo].[TweetLog]
(
	[StatusId] ASC
)
GO

CREATE INDEX [IX_TweetLog_TweetedAt] ON [dbo].[TweetLog]
(
	[TweetedAt] ASC
)
GO

CREATE UNIQUE INDEX [IX_TweetResult_Date] ON [dbo].[TweetResult]
(
	[Date] ASC
)
GO

CREATE UNIQUE INDEX [IX_TweetForecast_Date] ON [dbo].[TweetForecast]
(
	[Date] ASC
)
GO
