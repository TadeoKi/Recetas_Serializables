using NUnit.Framework;
using Recipies;
using System.Globalization;

namespace LibraryTests
{
    [TestFixture]
    [SetCulture("en-US")]
    public class StepTests
    {
        private static string productDescription = "Test Product";
        private static double unitCost = 9.99;
        private static string productJson = $@"{{""Description"":""{productDescription}"",""UnitCost"":{unitCost}}}";
        private static string equipmentDescription = "Test Equipment";
        private static double hourlyCost = 99.99;
        private static string equipmentJson = $@"{{""Description"":""{equipmentDescription}"",""HourlyCost"":{hourlyCost}}}";
        private static double quantity = 999.99;
        private static int time = 10;
        string expected = $@"{{""Input"":{productJson},""Quantity"":{quantity},""Time"":{time},""Equipment"":{equipmentJson}}}";

        private static string json = $@"{{""Input"":{productJson},""Quantity"":{quantity},""Time"":{time},""Equipment"":{equipmentJson}}}";

        [Test]
        public void SerializeStepTest()
        {
            Product product = new Product(productDescription, unitCost);
            Equipment equipment = new Equipment(equipmentDescription, hourlyCost);
            IJsonConvertible step = new Step(product, quantity, equipment, time);

            string actual = step.ConvertToJson();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializeStepTest()
        {
            string json = $@"{{""Input"":{productJson},""Quantity"":{quantity},""Time"":{time},""Equipment"":{equipmentJson}}}";

            Step step = new Step(json);

            Assert.AreEqual(step.Input.Description, productDescription);
            Assert.AreEqual(step.Quantity, quantity);
            Assert.AreEqual(step.Equipment.Description, equipmentDescription);
            Assert.AreEqual(step.Time, time);
        }
    }
}