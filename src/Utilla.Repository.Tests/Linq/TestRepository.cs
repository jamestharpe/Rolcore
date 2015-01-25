using System.Data.Linq;
using System.Linq;
using Utilla.Repository.LinqImpl;
using Utilla.Repository.Tests.Mocks;

namespace Utilla.Repository.Tests.Linq
{
    public class TestRepository : LinqRepository<TestDataContext, TestItem, MockEntity<Binary>, Binary>
    {
        public TestRepository()
            : base(
                () => { return new TestDataContext(); },
                (entity, key, concurrency, pKey) =>
                {
                    entity.RowKey = key;
                    entity.Timestamp = concurrency;
                },
                (entity, table) =>
                (entity.RowKey != null) && (table.Any(item => item.RowKey == entity.RowKey)))
        {

        }
    }
}
