Alter table DriverDetails
Add CityID int NOT NULL Default(1)
CONSTRAINT FK_DriverDetails_CityID FOREIGN KEY (CityID)
REFERENCES Cities(CityID)