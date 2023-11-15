using MySkills.DAL.Models;

namespace MySkills.BL.Resume
{
    public interface IResume
    {
        Task<IEnumerable<ProfileModel>> Search(int top);

        Task<ResumeModel> Get(int profileId);
    }
}
