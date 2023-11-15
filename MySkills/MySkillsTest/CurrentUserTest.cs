using MySkills.BL;
using MySkills.BL.Auth;
using Resutest.Helpers;
using System.Transactions;

namespace Resutest
{
	public class CurrentUserTest: BaseTest
	{
		[Test]
		public async Task BaseRegistrationTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
			{
				await CreateAndAuthUser();

				bool isLoggedIn = await currentUser.IsLoggedIn();
				Assert.That(isLoggedIn, Is.True);

				webCookie.Delete(AuthConstants.SessionCookieName);
				dbSession.ResetSessionCache();

				isLoggedIn = await currentUser.IsLoggedIn();
				Assert.That(isLoggedIn, Is.True);

				webCookie.Delete(AuthConstants.SessionCookieName);
				webCookie.Delete(AuthConstants.RememberMeCookieName);
				dbSession.ResetSessionCache();

				isLoggedIn = await currentUser.IsLoggedIn();
				Assert.That(isLoggedIn, Is.False);
			}
		}

		private async Task<int> CreateAndAuthUser()
		{
			string email = Guid.NewGuid().ToString() + "@test.com";

			//create user
			int userId = await auth.CreateUser(
				new MySkills.DAL.Models.UserModel()
				{
					Email = email,
					Password = "qwer1234"
				});
			return await auth.Authenticate(email, "qwer1234", true);
		}
	}
}