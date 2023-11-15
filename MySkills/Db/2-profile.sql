if not exists (select * from sysobjects where name='Profile' and xtype='U')
create table dbo.Profile(
ProfileId int identity primary key,
UserId int,
ProfileName nvarchar(20),
FirstName nvarchar(50),
LastName nvarchar(50),
ProfileImage nvarchar(100)
);