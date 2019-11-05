using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Repositories;

namespace Store.Tests.Fakes
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public bool CheckDocument(string document)
        {
            return false;
        }

        public bool CheckEmail(string email)
        {
            return false;
        }

        public void Save(Customer customer)
        { }
    }
}
