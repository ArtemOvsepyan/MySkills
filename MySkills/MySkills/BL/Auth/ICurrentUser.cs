using MySkills.DAL.Models;

namespace MySkills.BL.Auth
{
	public interface ICurrentUser
	{
		Task<bool> IsLoggedIn();

		Task<int?> GetCurrentUserId();

		Task<IEnumerable<ProfileModel>> GetProfiles();
	}
}
