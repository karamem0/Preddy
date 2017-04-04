IF OBJECT_ID(N'[preddy].[dbo].[TweetLog]', N'U') IS NOT NULL
    DROP TABLE [dbo].[TweetLog]
GO

CREATE TABLE [dbo].[TweetLog] (
	[Id] [uniqueidentifier] NOT NULL,
	[StatusId] [nvarchar](40) NULL,
	[UserId] [nvarchar](40) NULL,
	[UserName] [nvarchar](20) NULL,
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

IF OBJECT_ID(N'[preddy].[dbo].[TweetSummary]', N'U') IS NOT NULL
    DROP TABLE [dbo].[TweetSummary]
GO

CREATE TABLE [dbo].[TweetSummary] (
	[Id] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Day] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[CreatedAt] [datetime2] NOT NULL,
	[UpdatedAt] [datetime2] NOT NULL,
	CONSTRAINT [PK_TweetSummary] PRIMARY KEY
	(
		[Id] ASC
	)
)
GO

CREATE UNIQUE INDEX [IX_StatusId] ON [dbo].[TweetLog]
(
	[StatusId] ASC
)
GO

CREATE UNIQUE INDEX [IX_Date] ON [dbo].[TweetSummary]
(
	[Date] ASC
)
GO
