using NUnit.Framework;
using Recipies;
using System;
using System.Globalization;

namespace LibraryTests
{
    [TestFixture]
    [SetCulture("en-US")]
    public class RecipeTests
    {
        private static string productDescription = "Test Product";
        private static double unitCost = 9.99;
        private static string productJson = $@"{{""Description"":""{productDescription}"",""UnitCost"":{unitCost}}}";
        private static string equipmentDescription = "Test Equipment";
        private static double hourlyCost = 99.99;
        private static string equipmentJson = $@"{{""Description"":""{equipmentDescription}"",""HourlyCost"":{hourlyCost}}}";
        private static double quantity = 999.99;
        private static int time = 10;
        private static string stepJson = $@"{{""Input"":{productJson},""Quantity"":{quantity},""Time"":{time},""Equipment"":{equipmentJson}}}";
        private static string recipeJson = $@"{{""FinalProduct"":{productJson},""Steps"":[{stepJson}]}}";

        [Test]
        public void SerializeStepTest()
        {
            Product product = new Product(productDescription, unitCost);
            Equipment equipment = new Equipment(equipmentDescription, hourlyCost);
            Step step = new Step(product, quantity, equipment, time);
            Recipe recipe = new Recipe();
            recipe.FinalProduct = product;
            recipe.AddStep(step);

            string actual = recipe.ConvertToJson();

            Assert.AreEqual(recipeJson, actual);
        }

        [Test]
        public void DeserializeStepTest()
        {
            Step step = new Step(stepJson);

            Assert.AreEqual(step.Input.Description, productDescription);
            Assert.AreEqual(step.Quantity, quantity);
            Assert.AreEqual(step.Equipment.Description, equipmentDescription);
            Assert.AreEqual(step.Time, time);
        }
    }
}