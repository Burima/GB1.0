CREATE TABLE [dbo].[OwnerDetailsActivityLogs](
	[OwnerDetailsActivityLogID] [bigint] IDENTITY(1,1) NOT NULL,
	[OwnerDetailID] [bigint] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](12) NOT NULL,
	[Pincode] [nvarchar](10) NULL,
	[UserID] [bigint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[LastUpdatedBy] [bigint] NOT NULL,
	[CityID] [int] NOT NULL,
	[FollowUpOn] [datetime] NULL,
	[FollowUpNotes] [varchar](max) NULL,
	[NextFollowUp] [datetime] NULL,
	CONSTRAINT PK_OwnerDetailsActivityLogs PRIMARY KEY (OwnerDetailsActivityLogID),
	CONSTRAINT FK_OwnerDetailsActivityLogs_OwnerDetailID FOREIGN KEY (OwnerDetailID) REFERENCES OwnerDetails(OwnerDetailID),
	CONSTRAINT FK_OwnerDetailsActivityLogs_CityID FOREIGN KEY (CityID) REFERENCES Cities(CityID),
	CONSTRAINT PK_OwnerDetailsActivityLogs_UserID FOREIGN KEY (UserID) REFERENCES Users(UserID)
)

 


