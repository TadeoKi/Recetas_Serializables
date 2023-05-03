using NUnit.Framework;
using Recipies;
using System.Globalization;

namespace LibraryTests
{
    [TestFixture]
    [SetCulture("en-US")]
    public class EquipmentTests
    {
        private static string description = "Test Equipment";
        private static double hourlyCost = 9.99;
        private static string expected = $@"{{""Description"":""{description}"",""HourlyCost"":{hourlyCost}}}";
        private static string json = $@"{{""Description"":""{description}"",""HourlyCost"":{hourlyCost}}}";

        [Test]
        public void SerializeEquipmentTest()
        {
            IJsonConvertible equipment = new Equipment(description, hourlyCost);
            string actual = equipment.ConvertToJson();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeserializeEquipmentTest()
        {
            Equipment equipment = new Equipment(json);

            Assert.AreEqual(description, equipment.Description);
            Assert.AreEqual(hourlyCost, equipment.HourlyCost);
        }
    }
}