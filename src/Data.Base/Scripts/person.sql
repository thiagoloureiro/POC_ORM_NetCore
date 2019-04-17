IF EXISTS (SELECT * FROM sysobjects WHERE name='Person')
BEGIN
	DROP TABLE [POCDb].[dbo].[Person]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Person')
BEGIN
	CREATE TABLE [POCDb].[dbo].[Person]
	(
	_id VARCHAR(100),
	[index] INT IDENTITY(1,1) NOT NULL,

	[guid]                      uniqueidentifier,
	isActive bit,
	balance                     VARCHAR(100),
	picture VARCHAR(100),
	age INT,
	eyeColor                    VARCHAR(100),
	[name] VARCHAR(100),
	gender VARCHAR(100),
	company VARCHAR(100),
	email VARCHAR(100),
	phone VARCHAR(100),
	[address] VARCHAR(5000),
	about VARCHAR(1000),
	registered VARCHAR(100),
	latitude FLOAT,
	longitude                   FLOAT,
	[tags] VARCHAR(1000),
	greeting VARCHAR(1000),
	favoriteFruit VARCHAR(1000),
	 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[_id] ASC
)
	)ON[PRIMARY]
END