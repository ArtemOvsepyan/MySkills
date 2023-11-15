using MySkills.DAL.Models;

namespace MySkills.DAL
{
	public interface IUserTokenDAL
	{
		public Task<Guid> Create(int userId);

		public Task<int?> Get(Guid tokenid);
	}
}
