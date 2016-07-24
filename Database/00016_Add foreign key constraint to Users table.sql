alter table Users
add constraint FK_Users_OrganizationID 
Foreign Key(OrganizationID)
references Organizations(OrganizationID)