using WiredBrainCoffee.DataProcessor.Model;
using WiredBrainCoffee.DataProcessor.Processing;

namespace WiredBrainCoffee.DataProcessorTests.Processing
{
    public class MachineDataProcessorTests
    {
        private FakeCoffeeCountStore coffeeCountStore;
        private MachineDataProcessor machineDataProcessor;

        public MachineDataProcessorTests()
        {
            this.coffeeCountStore = new FakeCoffeeCountStore();
            this.machineDataProcessor = new MachineDataProcessor(coffeeCountStore);
        }


        [Fact]
        public void ShouldSaveCountPerCoffeeType()
        {
            //Arrange
           
            var items = new[]
            {
                new MachineDataItem("Cappuccino", new DateTime(2022, 10, 27, 8, 0, 0)),
                new MachineDataItem("Cappuccino", new DateTime(2022, 10, 27, 9, 0, 0)),
                new MachineDataItem("Espresso", new DateTime(2022, 10, 27, 10, 0, 0)),
            };

            //Act
            machineDataProcessor.ProcessItems(items);

            //Assert
            Assert.Equal(2, coffeeCountStore.SavedItems.Count);

            var item = coffeeCountStore.SavedItems[0];
            Assert.Equal("Cappuccino", item.CoffeeType);
            Assert.Equal(2, item.Count);

            item = coffeeCountStore.SavedItems[1];
            Assert.Equal("Espresso", item.CoffeeType);
            Assert.Equal(1, item.Count); 

        }

        [Fact]
        public void ShouldClearPreviousCoffeeCount()
        {
            //Arrange
            var coffeeCountStore = new FakeCoffeeCountStore();
            var machineDataProcessor = new MachineDataProcessor(coffeeCountStore);
            var items = new[]
            {
                new MachineDataItem("Cappuccino", new DateTime(2022, 10, 27, 8, 0, 0)),
            };

            //Act
            machineDataProcessor.ProcessItems(items);
            machineDataProcessor.ProcessItems(items);

            //Assert
            Assert.Equal(2, coffeeCountStore.SavedItems.Count);

            foreach (var item in coffeeCountStore.SavedItems)
            {
                Assert.Equal("Cappuccino", item.CoffeeType);
                Assert.Equal(1, item.Count);
            }
        }

        [Fact]
        public void ShouldIgnoreItemsThatAreNotNewer()
        {
            //Arrange
            var items = new[]
            {
                new MachineDataItem("Cappuccino", new DateTime(2022, 10, 27, 8, 0, 0)),
                new MachineDataItem("Cappuccino", new DateTime(2022, 10, 27, 7, 0, 0)),
                new MachineDataItem("Cappuccino", new DateTime(2022, 10, 27, 7, 10, 0)),
                new MachineDataItem("Cappuccino", new DateTime(2022, 10, 27, 9, 0, 0)),
                new MachineDataItem("Espresso", new DateTime(2022, 10, 27, 10, 0, 0)),
                new MachineDataItem("Espresso", new DateTime(2022, 10, 28, 10, 0, 0)),

            };

            //Act
            machineDataProcessor.ProcessItems(items);

            //Assert
            Assert.Equal(2, coffeeCountStore.SavedItems.Count);

        }



    }
}
