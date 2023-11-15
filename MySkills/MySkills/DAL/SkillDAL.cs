using MySkills.DAL.Models;

namespace MySkills.DAL
{
    public class SkillDAL : ISkillDAL
    {
        public async Task<int> Create(string skillname)
        {
            string sql = @"insert into Skill (SkillName)
                    output inserted.SkillId 
                    values (@skillname)";

            return await DbHelper.QueryScalarAsync<int>(sql, new { skillname });
        }

        public async Task<IEnumerable<SkillModel>?> Search(int top, string skillname)
        {
            string sql = @"select @top SkillId, SkillName from Skill where SkillName like @skillname";
            return await DbHelper.QueryAsync<SkillModel>(sql, new { top, skillname = "%" + skillname + "%" });
        }

        public async Task<SkillModel> Get(string skillname)
        {
            string sql = @"select SkillId, SkillName from Skill where SkillName = @skillname";
            return await DbHelper.QueryScalarAsync<SkillModel>(sql, new { skillname });
        }

        public async Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId)
        {
            return await DbHelper.QueryAsync<ProfileSkillModel>(@$"
                    select ProfileSkillId, ProfileId, ps.SkillId, Level, SkillName
                    from ProfileSkill ps
					join Skill s on ps.SkillId = s.SkillId
                    where ProfileId = @profileId", new { profileId });
        }

        public async Task<int> AddProfileSkill(ProfileSkillModel model)
        {
            string sql = @"insert into ProfileSkill (ProfileId, SkillId, Level)
                    output inserted.ProfileSkillId
                    values (@ProfileId, @SkillId, @Level)";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }
    }
}

