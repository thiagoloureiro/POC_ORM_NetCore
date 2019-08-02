IF EXISTS (SELECT * FROM sysobjects WHERE name='Person')
BEGIN
	DROP TABLE [POCDb].[dbo].[Person]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Person')
BEGIN
	CREATE TABLE [POCDb].[dbo].[Person]
	(
	_id bigint IDENTITY(1,1) NOT NULL,
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

--IF EXISTS (SELECT * FROM sysobjects WHERE name='dummyrecord')
--BEGIN
--	DROP TABLE [POCDb].[dbo].[dummyrecord]
--END


CREATE TABLE [POCDb].[dbo].[dummyrecord]
(
    id bigint NOT NULL,
    revisionnumber integer NOT NULL,
	index2 bigint,
    owner bigint,
    element bigint,
    externalrecorddatetime datetime2,
    externalrecorddatetime2 datetime2,
    externalrecorddatetime3 datetime2,
    externalrecorddatetime4 datetime2,
    externalrecorddatetime5 datetime2)