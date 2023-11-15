using MySkills.DAL.Models;

namespace MySkills.BL.Profile
{
    public interface ISkill
    {
        Task<IEnumerable<SkillModel>?> Search(int top, string skillname);
    }
}
