
using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessorTests.Data
{
    public class ConsoleCoffeeCountStoreTests
    {
        [Fact]
        public void ShouldWriteOutputToConsole()
        {
            //Arrange
            var coffeeCountItem = new CoffeeCountItem("Cappuccino", 5);
            var stringWriter = new StringWriter(); 
            var consoleCoffeeCountStore = new ConsoleCoffeeCountStore(stringWriter);

            //Act
            consoleCoffeeCountStore.Save(coffeeCountItem);
            var result = stringWriter.ToString();

            //Assert
            Assert.Equal($"{coffeeCountItem.CoffeeType} : {coffeeCountItem.Count}{Environment.NewLine}\n", result);
        }

    }
}
