using MySkills.DAL.Models;
using MySkills.ViewModels;

namespace MySkills.ViewMapper
{
    public static class AuthMapper
    {
        public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
        {
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}
