if not exists (select * from sysobjects where name='AppUser' and xtype='U')
create table dbo.AppUser (
UserId int identity primary key,
Email nvarchar(50),
Password nvarchar(100),
Salt nvarchar(50),
Status int
); 

if not exists (select * from sysobjects where name='UserSecurity' and xtype='U')  
create table dbo.UserSecurity(
UserSecurityId int identity primary key,
UserId int,
VerificationCode nvarchar(50)
);

if not exists (select * from sysobjects where name='EmailQueue' and xtype='U')   
create table dbo.EmailQueue(
EmailQueueId int identity primary key,
EmailTo nvarchar(200),
EmailFrom nvarchar(200),
EmailSubject nvarchar(200),
EmailBody text,
Created time,
ProcessingId nvarchar(100),
Retry int
);

if not exists (select name from sysindexes where name = 'IX_AppUser_Email')
create index IX_AppUser_Email on AppUser (
Email);