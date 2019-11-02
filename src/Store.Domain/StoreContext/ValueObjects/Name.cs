using FluentValidator;
using FluentValidator.Validation;

namespace Store.Domain.StoreContext.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(FirstName, 3, "FirstName", "O nome deve conter pelo menos 3 caractéres")
                .HasMaxLen(FirstName, 40, "FirstName", "O nome deve conter no máximo 40 caractéres")
                .HasMinLen(LastName, 3, "LastName", "O sobrenome deve conter pelo menos 3 caractéres")
                .HasMaxLen(LastName, 40, "LastName", "O sobrenome deve conter no máximo 40 caractéres")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}