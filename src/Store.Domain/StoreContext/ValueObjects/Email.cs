using FluentValidator;
using FluentValidator.Validation;

namespace Store.Domain.StoreContext.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            Address = address;

            new ValidationContract()
                .Requires()
                .IsEmail(Address, "Address", "O e-mail é inválido.");
        }

        public string Address { get; private set; }

        public override string ToString() => Address;
    }
}