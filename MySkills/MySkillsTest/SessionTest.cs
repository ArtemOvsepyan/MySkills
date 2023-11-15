using Resutest.Helpers;
using System.Text.Json;
using System.Transactions;

namespace Resutest
{
	public class SessionTest: BaseTest
	{
		[Test]
		[NonParallelizable]
		public async Task CreateSession()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
			{
				((TestCookie)webCookie).Clear();
				this.dbSession.ResetSessionCache();
				var session = await this.dbSession.GetSession();

				var dbSession = await dbSessionDAL.Get(session.DbSessionId);

				Assert.That(dbSession, Is.Not.Null);

				Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

				var session2 = await this.dbSession.GetSession();

				Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
			}

		}

		[Test]
		[NonParallelizable]
		public async Task CreateAuthorizedSessionTest()
		{
			using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
				dbSession.ResetSessionCache();
				var session = await dbSession.GetSession();
				await dbSession.SetUserId(10);

				var dbSessoion = await dbSessionDAL.Get(session.DbSessionId);

                Assert.That(dbSessoion, Is.Not.Null, "Session shoule not be null");
                Assert.Multiple(() =>
                {
                    Assert.That(dbSessoion.UserId!, Is.EqualTo(10));
                    Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));
                });
                var session2 = await dbSession.GetSession();
				Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));

				int? userid = await currentUser.GetCurrentUserId();
				Assert.That(userid, Is.EqualTo(10));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task AddValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
                dbSession.ResetSessionCache();
                var session = await dbSession.GetSession();
                dbSession.AddValue("Test", "TestValue");
                await dbSession.UpdateSessionData();

                Assert.That(dbSession.GetValueDef("Test", ""), Is.EqualTo("TestValue"));

                await dbSession.SetUserId(10);

                var dbSessoion = await dbSessionDAL.Get(session.DbSessionId);
                var SessionData = JsonSerializer.Deserialize<Dictionary<string, object>>(dbSessoion?.SessionData ?? "");
                Assert.Multiple(() =>
                {
                    Assert.That(SessionData?.ContainsKey("Test"), Is.True);
                    Assert.That(SessionData?["Test"].ToString(), Is.EqualTo("TestValue"));
                });
                dbSession.ResetSessionCache();
                session = await dbSession.GetSession();
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("TestValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task UpdateValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
                dbSession.ResetSessionCache();
                var session = await dbSession.GetSession();
                dbSession.AddValue("Test", "TestValue");
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("TestValue"));

                dbSession.AddValue("Test", "UpdatedValue");
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdatedValue"));

                await dbSession.UpdateSessionData();

                dbSession.ResetSessionCache();
                session = await dbSession.GetSession();
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo("UpdatedValue"));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task RemoveValue()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)webCookie).Clear();
                dbSession.ResetSessionCache();
                var session = await dbSession.GetSession();
                dbSession.AddValue("Test", "TestValue");
                await dbSession.UpdateSessionData();

                dbSession.RemoveValue("Test");
                await dbSession.UpdateSessionData();

                dbSession.ResetSessionCache();
                session = await dbSession.GetSession();
                Assert.That(dbSession.GetValueDef("Test", "").ToString(), Is.EqualTo(""));
            }
        }
    }
}
