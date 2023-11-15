﻿using MySkills.DAL.Models;

namespace MySkills.DAL
{
    public class ProfileDAL : IProfileDAL
	{
		public async Task<int> Add(ProfileModel profile)
		{
			string sql = @"insert into Profile(UserId, ProfileName, FirstName, LastName, ProfileImage)
                    values(@UserId, @ProfileName, @FirstName, @LastName, @ProfileImage);
                    select SCOPE_IDENTITY() as 'ProfileId'";
			var result = await DbHelper.QueryAsync<int>(sql, profile);
			return result.First();
		}

        public async Task<IEnumerable<ProfileModel>> GetByUserId(int userId)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@"
                        select 	ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage 
                        from Profile
                        where UserId = @id", new { id = userId });
        }

        public async Task<ProfileModel> GetByProfileId(int profileId)
        {
            return await DbHelper.QueryScalarAsync<ProfileModel>(@"
                        select 	ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage 
                        from Profile
                        where ProfileId = @profileId", new { profileId });
        }

        public async Task Update(ProfileModel profile)
		{
			string sql = @"update Profile
					set ProfileName = @ProfileName, 
						FirstName = @FirstName, 
						LastName = @LastName, 
						ProfileImage = @ProfileImage
                    where ProfileId = @ProfileId";
			var result = await DbHelper.QueryAsync<int>(sql, profile);
		}

        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@$"
                        select @top ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage 
                        from Profile
                        order by 1 desc", new { top });
        }
    }
}
