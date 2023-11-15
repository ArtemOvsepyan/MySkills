using Microsoft.AspNetCore.Http;
using MySkills.BL.Auth;
using MySkills.BL.General;
using MySkills.BL.Profile;
using MySkills.DAL;

namespace Resutest.Helpers
{
	public class BaseTest
	{
        protected IAuthDAL authDal = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected IAuth auth;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IProfileDAL profileDAL = new ProfileDAL();
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected IProfile profile;
        protected ISkillDAL skillDAL = new SkillDAL();

        protected CurrentUser currentUser;

        public BaseTest()
        {
            DbHelper.ConnString = "Data Source=DESKTOP-B40JURU\\MSSQL; Initial Catalog=ResuDB; Integrated Security=true; TrustServerCertificate=True";

            webCookie = new TestCookie();
            dbSession = new DbSession(dbSessionDAL, webCookie);
            auth = new Auth(authDal, encrypt, webCookie, dbSession, userTokenDAL);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL, profileDAL);
            profile = new Profile(profileDAL, skillDAL);
        }
	}
}
