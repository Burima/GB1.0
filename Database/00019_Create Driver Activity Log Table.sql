CREATE TABLE [dbo].[DriverDetailsActivityLogs](
	[DriverDetailsActivityLogID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](12) NOT NULL,
	[LicenceTypeID] [int] NOT NULL,
	[LicenceNo] [nvarchar](50) NULL,
	[Pincode] [nvarchar](10) NULL,
	[Uber] [bit] NOT NULL,
	[Ola] [bit] NOT NULL,
	[IsReferred] [bit] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[LastUpdatedBy] [bigint] NOT NULL,
	[DriverStatusID] [int] NOT NULL,
	[ExpectedSalary] [nvarchar](max) NULL,
	[CityID] [int] NOT NULL,
	[AttachedOn] [datetime] NULL,
	[PartnerMatchedOn] [datetime] NULL,
	[CompletedFirstTripOn] [datetime] NULL,
 CONSTRAINT [PK_DriverDetailsActivityLogs] PRIMARY KEY CLUSTERED 
(
	[DriverDetailsActivityLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs] ADD  DEFAULT ((1)) FOR [CityID]
GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs]  WITH CHECK ADD  CONSTRAINT [FK_DriverDetailsActivityLogs_CityID] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([CityID])
GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs] CHECK CONSTRAINT [FK_DriverDetailsActivityLogs_CityID]
GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs]  WITH CHECK ADD  CONSTRAINT [FK_DriverDetailsActivityLogs_CreatedBy] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs] CHECK CONSTRAINT [FK_DriverDetailsActivityLogs_CreatedBy]
GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs]  WITH CHECK ADD  CONSTRAINT [FK_DriverDetailsActivityLogs_DriverStatusID] FOREIGN KEY([DriverStatusID])
REFERENCES [dbo].[DriverStatus] ([DriverStatusID])
GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs] CHECK CONSTRAINT [FK_DriverDetailsActivityLogs_DriverStatusID]
GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs]  WITH CHECK ADD  CONSTRAINT [FK_DriverDetailsActivityLogs_LicenceTypeID] FOREIGN KEY([LicenceTypeID])
REFERENCES [dbo].[LicenceTypes] ([LicenceTypeID])
GO

ALTER TABLE [dbo].[DriverDetailsActivityLogs] CHECK CONSTRAINT [FK_DriverDetailsActivityLogs_LicenceTypeID]
GO


