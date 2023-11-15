if not exists (select * from sysobjects where name='Skills' and xtype='U')
create table dbo.Skill(
SkillId int identity,
SkillName nvarchar(50)
)

if not exists (select * from sysobjects where name='ProfileSkill' and xtype='U')
create table dbo.ProfileSkill(
ProfileSkillId int identity,
ProfileId int,
SkillId int,
Level int
)

if not exists (select name from sysindexes where name = 'IX_ProfileSkill_ProfileId')
create index IX_ProfileSkill_ProfileId on ProfileSkill(
ProfileId
)