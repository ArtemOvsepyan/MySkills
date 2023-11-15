if not exists (select * from sysobjects where name='UserToken' and xtype='U')
create table dbo.UserToken(
UserTokenId uniqueidentifier primary key,
UserId int,
Created datetime
)