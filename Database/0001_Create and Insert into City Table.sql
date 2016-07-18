create table Cities
(
CityID int IDENTITY(1,1) NOT NULL,
Name varchar(50),
CONSTRAINT PK_CityID PRIMARY KEY (CityID)
);

insert into Cities(Name) values ('Kolkata');
insert into Cities(Name) values ('Mumbai');