using Resutest.Helpers;
using System.Transactions;

namespace Resutest
{
	public class ProfileTest : BaseTest
	{
		[Test]
		public async Task AddTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await profile.AddOrUpdate(
					new MySkills.DAL.Models.ProfileModel()
					{
						UserId = 19,
						FirstName = "����",
						LastName = "������",
						ProfileName = "����"
					});

				var results = await profile.Get(19);
				Assert.That(results.Count(), Is.EqualTo(1));

				var result = results.First();
                Assert.Multiple(() =>
                {
                    Assert.That(result.FirstName, Is.EqualTo("����"));
                    Assert.That(result.LastName, Is.EqualTo("������"));
                    Assert.That(result.ProfileName, Is.EqualTo("����"));
                    Assert.That(result.UserId, Is.EqualTo(19));
                });
            }
        }

		[Test]
		public async Task UpdateTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var profileModel = new MySkills.DAL.Models.ProfileModel()
				{
					UserId = 19,
					FirstName = "����",
					LastName = "������",
					ProfileName = "����"
				};

				await profile.AddOrUpdate(profileModel);

				profileModel.FirstName = "����1";

				await profile.AddOrUpdate(profileModel);

				var results = await profile.Get(19);
				Assert.That(results.Count(), Is.EqualTo(1));

				var result = results.First();
                Assert.Multiple(() =>
                {
                    Assert.That(result.FirstName, Is.EqualTo("����1"));
                    Assert.That(result.UserId, Is.EqualTo(19));
                });
            }
        }
	}
}