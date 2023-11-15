using MySkills.BL;
using Resutest.Helpers;
using System.Transactions;

namespace Resutest
{
	public class RegisterTests: BaseTest
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task BaseRegistrationTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@Test.com";

				// validate: should not be in the DB
				auth.ValidateEmail(email).GetAwaiter().GetResult();

				// create user
				int userId = await auth.CreateUser(
					new MySkills.DAL.Models.UserModel()
					{
						Email = email,
						Password = "1q2w3e4r"
					});

				Assert.That(userId, Is.GreaterThan(0));

				var userDalResult = await authDal.GetUser(userId);
                Assert.Multiple(() =>
                {
                    Assert.That(userDalResult.Email, Is.EqualTo(email));
                    Assert.That(userDalResult.Salt, Is.Not.Null);
                });
                var userByEmailDalResult = await authDal.GetUser(email);
				Assert.That(userByEmailDalResult.Email, Is.EqualTo(email));

				// validate: should be in the DB
				Assert.Throws<DuplicateEmailException>(delegate { auth.ValidateEmail(email).GetAwaiter().GetResult(); });

				string encPassword = encrypt.HashPassword("1q2w3e4r", userByEmailDalResult.Salt);
				Assert.That(encPassword, Is.EqualTo(userByEmailDalResult.Password));
            }
        }
	}
}