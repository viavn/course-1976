using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.StoreContext.ValueObjects;

namespace Store.Tests.ValueObjects
{
    [TestClass]
    public class NameTests
    {
        private Name validName;
        private Name invalidName;

        public NameTests()
        {
            validName = new Name("Vinícius", "Avansini");
            invalidName = new Name("", "Avansini");
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsNotValid()
        {
            Assert.AreEqual(false, invalidName.Valid);
        }

        [TestMethod]
        public void ShouldNotReturnNotificationWhenNameIsValid()
        {
            Assert.AreEqual(true, validName.Valid);
        }
    }
}
