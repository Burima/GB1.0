CREATE TABLE [dbo].[OwnerDetails](
	[OwnerDetailID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](12) NOT NULL,
	[Pincode] [nvarchar](10) NULL,
	[Email][nvarchar](max) NULL,
	[Notes][nvarchar](max) NULL,
	[UserID] [bigint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[LastUpdatedBy] [bigint] NOT NULL,
	[CityID] [int] NOT NULL,
	[FollowUpOn] [datetime] NULL,
	[FollowUpNotes] [varchar](max) NULL,
	[NextFollowUp] [datetime] NULL,
	CONSTRAINT PK_OwnerDetails PRIMARY KEY (OwnerDetailID),
	CONSTRAINT FK_OwnerDetails_CityID FOREIGN KEY (CityID) REFERENCES Cities(CityID),
	CONSTRAINT PK_OwnerDetails_UserID FOREIGN KEY (UserID) REFERENCES Users(UserID)
)

 


