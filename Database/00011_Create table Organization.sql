create table Organizations(
OrganizationID int IDENTITY(1,1) not null,
Name varchar(100) not null,
constraint PK_Organizations primary key(OrganizationID))