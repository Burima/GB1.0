update users
set IsVSEmployee = 1 
where UserID not in (select UserID from Users where email like '%@uber.com');