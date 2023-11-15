using MySkills.DAL.Models;
using MySkills.ViewModels;

namespace MySkills.ViewMapper
{
	public static class ProfileMapper
	{
		public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
		{
			return new ProfileModel()
			{
				ProfileId = model.ProfileId,
				ProfileName = model.ProfileName,
				FirstName = model.FirstName,
				LastName = model.LastName
			};
		}

		public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
		{
			return new ProfileViewModel()
			{
				ProfileId = model.ProfileId,
				ProfileName = model.ProfileName,
				FirstName = model.FirstName,
				LastName = model.LastName,
				ProfileImage = model.ProfileImage
			};
		}
	}
}
