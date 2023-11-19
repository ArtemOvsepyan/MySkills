namespace MySkills.DAL
{
    public class UserTokenDAL: IUserTokenDAL
	{
		public async Task<Guid> Create(int userid)
		{
			Guid tokenid = Guid.NewGuid();
			string sql = @"insert into UserToken (UserTokenID, UserId, Created)
                    values (@tokenid, @UserId, getdate())";

			await DbHelper.ExecuteAsync(sql, new { userid, tokenid });
			return tokenid;
		}

		public async Task<int?> Get(Guid tokenid)
		{
			string sql = @"select UserId from UserToken where UserTokenID = @tokenid";
			return await DbHelper.QueryScalarAsync<int>(sql, new { tokenid });
		}

	}
}
