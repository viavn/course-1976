using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.StoreContext.CustomerCommands.Inputs;

namespace Store.Tests.ValueObjects
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Vinicius";
            command.LastName = "Avansini";
            command.Document = "13322256081";
            command.Email = "vinicius.avansini@gmail.com";
            command.Phone = "19998988998";

            Assert.AreEqual(true, command.Valid());
        }
    }
}
