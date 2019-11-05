using Store.Domain.StoreContext.Services;

namespace Store.Tests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        { }
    }
}
