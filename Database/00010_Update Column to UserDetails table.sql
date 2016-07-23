alter table UserDetails
alter column PresentAddress varchar(50) NULL;

alter table UserDetails
alter column  PermanentAddress varchar(50) NULL;

alter table UserDetails
alter column [GovtIDType] [int] NULL;

alter table UserDetails
alter column [GovtID] [varchar](50) NULL;

alter table UserDetails
alter column [UserProfession] [int] NULL;

alter table UserDetails
alter column [CurrentEmployer] [varchar](50) NULL;

alter table UserDetails
alter column [OfficeLocation] [varchar](60) NULL;

alter table UserDetails
alter column [EmployeeID] [varchar](10) NULL;

alter table UserDetails
alter column [HighestEducation] [varchar](20) NULL;

alter table UserDetails
alter column [InstitutionName] [varchar](30) NULL;

alter table UserDetails
alter column [CreatedOn] [datetime] NOT NULL;
	
alter table UserDetails
alter column [LastUpdatedOn] [datetime] NOT NULL;