USE master

CREATE DATABASE KuProStore
ON
(
NAME = 'KuProStore',
FILENAME = 'D:\VisualStudioProjects\KuPro\Base\KuProStore.mdf',
SIZE = 10MB,
FILEGROWTH = 5MB
)
LOG ON
(
NAME = 'PhoneStoreLog',
FILENAME = 'D:\VisualStudioProjects\KuPro\Base\KuProStore.ldf',
SIZE = 10MB,
FILEGROWTH = 5MB
)
COLLATE Cyrillic_General_CI_AS
GO

USE KuProStore

CREATE TABLE Users
(
 UserId int NOT NULL IDENTITY
 PRIMARY KEY,
 RegDate datetime2(7) NOT NULL,
 FirstName nvarchar(30) NOT NULL,
 LastName nvarchar(30) NULL,
 [Password] nvarchar(30) NOT NULL,
 Email nvarchar(256) NULL,
 Cookie nvarchar(80) NOT NULL,
 ContactPhone nvarchar(15) NOT NULL,
 IsActive bit NOT NULL,
 Balance decimal NOT NULL,
 ContactInfo NVARCHAR(1024) NULL
)

CREATE TABLE Regions
(
ID int NOT NULL IDENTITY
PRIMARY KEY,
Region nvarchar(64) NOT NULL
)

CREATE TABLE Towns
(
ID int NOT NULL IDENTITY
PRIMARY KEY,
RegionId int NOT NULL,
CONSTRAINT FK_Town_Region FOREIGN KEY (RegionId) REFERENCES Regions(ID),
Town nvarchar(128) NOT NULL
)

CREATE TABLE Products
(
  ProductId int NOT NULL IDENTITY
  PRIMARY KEY,
  Name nvarchar(32) NOT NULL,  
  [Description] nvarchar(1024) NULL,
  Contacts nvarchar(512) NOT NULL,
  Price decimal NOT NULL,
  CONSTRAINT CHK_Products_Price	CHECK (Price > 0),
  UserId int NOT NULL,
  AddingTime datetime2(7) NOT NULL,
  UpdateTime datetime2(7) NOT NULL,
  CONSTRAINT FK_Product_User FOREIGN KEY (UserId) REFERENCES Users(UserId),
  TownId int NOT NULL,
  CONSTRAINT FK_Product_Town FOREIGN KEY (TownId) REFERENCES Towns(ID),
  IsActive BIT not null DEFAULT 1
)

CREATE NONCLUSTERED INDEX nc_idx_Products_UserId ON Products(UserId)
CREATE NONCLUSTERED INDEX nc_idx_Products_TownId ON Products(TownId)
CREATE NONCLUSTERED INDEX nc_idx_Products_UpdateTime ON Products(UpdateTime)

CREATE TABLE Images
(
ID int NOT NULL IDENTITY
PRIMARY KEY,
[Image] nvarchar(300) NOT NULL,
ProductId int NOT NULL
CONSTRAINT FK_Image_Product FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
)

insert into Regions (Region)
values ('��� �������'),('�� ����'), ('���������'),('���������'),('����������������'), ('��������'),
('�����������'),('������������'),('�����������'),('�����-�����������'),('��������'),
('��������������'),('���������'),('���������'),('������������'),('��������'),
('����������'),('���������'),('�������'),('�������������'),('�����������'),
('����������'),('�����������'),('����������'),('������������'), ('�����������')

insert into Towns (RegionId, Town)
values (2, '�����������'),(3,'�������'),(4, '����'),(5, '�����'),(6, '������'), (7, '�������'), (8, '�������'), (9, '���������'), (10, '�����-���������'),
(11, '����'),(12, '������������'),(13, '�������'),(14, '�����'),(15, '��������'),(16, '������'),(17, '�������'),(18, '�����')
,(19, '����'),(20, '���������'),(21, '�������'),(22, '������'),(23, '�����������'),(24, '�������'),(25, '��������'),(26, '��������')

GO
