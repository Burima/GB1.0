ALTER TABLE Users DROP CONSTRAINT [DF__Users__IsVSEmplo__4CA06362];

alter table Users
alter column OrganizationID int not null;