--select into temp
select Name, Description, Contacts, Price, UserId, AddingTime, UpdateTime, TownId, IsActive
into TempProducts
from Products

select RegDate, FirstName, LastName, Password, Email, Cookie, ContactPhone, IsActive, Balance, ContactInfo
into TempUsers
from Users

select Image, ProductId
into TempImages
from Images
--

select * from TempUsers
select * from TempProducts
select * from TempImages
--
drop table Images
drop table Users
drop table Products
--
insert into Users (RegDate, FirstName, LastName, Password, Email, Cookie, ContactPhone, IsActive, Balance, ContactInfo)
select RegDate, FirstName, LastName, Password, Email, Cookie, ContactPhone, IsActive, Balance, ContactInfo
from TempUsers;

insert into Products (Name, Description, Contacts, Price, UserId, AddingTime, UpdateTime, TownId, IsActive)
select  Name, Description, Contacts, Price, UserId, AddingTime, UpdateTime, TownId, IsActive
from TempProducts;

insert into Images (Image, ProductId)
select Image, ProductId
from TempImages;
--

drop table TempUsers, TempProducts, TempImages