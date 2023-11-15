using Resutest.Helpers;
using System.Transactions;

namespace Resutest
{
	public class TokenTest : BaseTest
	{
		[Test]
		public async Task BaseTokenTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
			{
				var tokenId = await userTokenDAL.Create(10);
				var userid = await userTokenDAL.Get(tokenId);
				Assert.That(userid, Is.EqualTo(10));
			}
		}
	}
}