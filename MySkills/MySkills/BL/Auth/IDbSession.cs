using MySkills.DAL.Models;

namespace MySkills.BL.Auth
{
	public interface IDbSession
	{
        Task<SessionModel> GetSession();

        Task SetUserId(int userId);

        Task<int?> GetUserId();

        Task<bool> IsLoggedIn();

        Task Lock();

        void ResetSessionCache();

        Task UpdateSessionData();

        void AddValue(string key, object value);

        void RemoveValue(string key);

        void LogOut();

        object GetValueDef(string key, object defaultValue);
    }
}
