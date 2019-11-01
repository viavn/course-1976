using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Enums;
using Store.Domain.StoreContext.ValueObjects;

namespace Store.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var name = new Name("Vinicius", "Avansini");
            var doc = new Document("12345678901");
            var email = new Email("avansini.vinicius@gmail.com");
            var c = new Customer(name, doc, email, "19999998798");
            var address = new Address(
                "Rua dos Testers",
                "743",
                null,
                "Bairro Teste",
                "Americana",
                "SP",
                "Brasil",
                "11111111",
                EAddressType.Billing
            );
            c.AddAddress(address);

            var order = new Order(c);
        }
    }
}
