using MySkills.DAL.Models;

namespace MySkills.DAL
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(string email);

        Task<UserModel> GetUser(int id);

        Task<int> CreateUser(UserModel model);
    }
}
