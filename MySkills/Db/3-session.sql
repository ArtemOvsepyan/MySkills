if not exists (select * from sysobjects where name='DbSession' and xtype='U')
create table dbo.DbSession(
DbSessionId uniqueidentifier primary key,
SessionData text,
Created datetime,
LastAccessed datetime,
UserId int
);