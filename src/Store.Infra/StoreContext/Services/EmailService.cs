using Store.Domain.StoreContext.Services;

namespace Store.Infra.StoreContext.Repositories
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        {
            // TODO: Implementar
        }
    }
}