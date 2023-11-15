using MySkills.DAL.Models;
using MySkills.ViewModels;

namespace MySkills.ViewMapper
{
    public static class SkillMapper
    {
        public static ProfileSkillModel MapSkillViewModelToProfileSkillModel(SkillViewModel model)
        {
            return new ProfileSkillModel()
            {
                SkillName = model.Name,
                Level = model.Level
            };
        }
    }
}
