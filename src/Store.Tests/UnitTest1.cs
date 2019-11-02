// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Store.Domain.StoreContext.Entities;
// using Store.Domain.StoreContext.Enums;
// using Store.Domain.StoreContext.ValueObjects;

// namespace Store.Tests
// {
//     [TestClass]
//     public class UnitTest1
//     {
//         [TestMethod]
//         public void TestMethod1()
//         {
//             var name = new Name("Vinicius", "Avansini");
//             var doc = new Document("12345678901");
//             var email = new Email("avansini.vinicius@gmail.com");
//             var c = new Customer(name, doc, email, "19999998798");
//             var address = new Address(
//                 "Rua dos Testers",
//                 "743",
//                 null,
//                 "Bairro Teste",
//                 "Americana",
//                 "SP",
//                 "Brasil",
//                 "11111111",
//                 EAddressType.Billing
//             );
//             c.AddAddress(address);

//             var mouse = new Product("Mouse", "Rato", "image.png", 59.90M, 10);
//             var teclado = new Product("Teclado", "Teclado", "image.png", 159.90M, 11);
//             var impressora = new Product("Impressora", "Impressora", "image.png", 559.90M, 5);
//             var cadeira = new Product("Cadeira", "Cadeira", "image.png", 129.90M, 50);

//             var order = new Order(c);
//             // order.AddItem(new OrderItem(mouse, 5));
//             // order.AddItem(new OrderItem(teclado, 5));
//             // order.AddItem(new OrderItem(impressora, 5));
//             // order.AddItem(new OrderItem(cadeira, 5));

//             // Realizei o pedido
//             order.Place();

//             // Simular o pagamento
//             order.Pay();

//             // Simular envio
//             order.Ship();

//             // Simular o cancelamento
//             order.Cancel();
//         }
//     }
// }
