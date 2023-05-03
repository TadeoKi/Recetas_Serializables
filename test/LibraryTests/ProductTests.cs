using NUnit.Framework;
using Recipies;
using System.Globalization;

namespace LibraryTests
{
    [TestFixture]
    [SetCulture("en-US")]
    public class ProductTests
    {
        private static string description = "Test Product";
        private static double unitCost = 9.99;
        private static string expected = $@"{{""Description"":""{description}"",""UnitCost"":{unitCost}}}";
        private static string json = $@"{{""Description"":""{description}"",""UnitCost"":{unitCost}}}";

        [Test]
        public void SerializeProductTest()
        {

            IJsonConvertible Product = new Product(description, unitCost);
            string actual = Product.ConvertToJson();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [SetCulture("en-US")]
        public void DeserializeProductTest()
        {
            Product Product = new Product(json);

            Assert.AreEqual(description, Product.Description);
            Assert.AreEqual(unitCost, Product.UnitCost);
        }
    }
}