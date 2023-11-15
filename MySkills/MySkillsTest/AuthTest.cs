using MySkills.BL;
using Resutest.Helpers;
using System.Transactions;

namespace Resutest
{
	public class AuthTest: BaseTest
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task AuthRegistrationTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
			{
				string email = Guid.NewGuid().ToString() + "@Test.com";

				// create user
				int userId = await auth.CreateUser(
					new MySkills.DAL.Models.UserModel()
					{
						Email = email,
						Password = "1q2w3e4r"
					});

				Assert.Throws<AuthorizationException>(delegate { auth.Authenticate("afsdacwaww", "111", false).GetAwaiter().GetResult(); });

				Assert.Throws<AuthorizationException>(delegate { auth.Authenticate(email, "111", false).GetAwaiter().GetResult(); });

				Assert.Throws<AuthorizationException>(delegate { auth.Authenticate("afsdacwaww", "1q2w3e4r", false).GetAwaiter().GetResult(); });

				await auth.Authenticate(email, "1q2w3e4r", false);
			}	
		}

		[Test]
		public async Task RememberMeTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
			{
				string email = Guid.NewGuid().ToString() + "@test.com";

				// create user
				int userId = await auth.CreateUser(
					new MySkills.DAL.Models.UserModel()
					{
						Email = email,
						Password = "qwer1234"
					});

				await auth.Authenticate(email, "qwer1234", true);

				string? authCookie = webCookie.Get(MySkills.BL.Auth.AuthConstants.SessionCookieName);
                Assert.That(authCookie, Is.Not.Null);

				string? rememberMeCookie = webCookie.Get(MySkills.BL.Auth.AuthConstants.RememberMeCookieName);
                Assert.That(rememberMeCookie, Is.Not.Null);
			}
		}
	}
}
