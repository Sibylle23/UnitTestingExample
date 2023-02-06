using System;
using WiredBrainCoffee.DataProcessor.Parsing;

namespace WiredBrainCoffee.DataProcessorTests.Parsing
{
    public class CsvLineParserTests
    {
        [Fact]
        public void ShouldParseValidLine()
        {
            // Arrange
            string[] csvLines = new[] { "Cappuccino; 10/27/2025 8:06:04 AM" };
            
            // Act
            var machineDataItems = CsvLineParser.Parse(csvLines);

            // Assert
            Assert.NotNull(machineDataItems);
            Assert.Single(machineDataItems);
            Assert.Equal("Cappuccino", machineDataItems[0].CoffeeType);
            Assert.Equal(new DateTime(2025, 10, 27, 8, 6, 4), machineDataItems[0].CreatedAt);
        }

        [Fact]
        public void ShouldSkipEmptyLines()
        {
            // Arrange
            string[] csvLines = new[] { "Cappuccino; 10/27/2025 8:06:04 AM", "", " ", "Mocca; 10/27/2022 8:06:04 AM" };

            // Act
            var machineDataItems = CsvLineParser.Parse(csvLines);

            // Assert
            Assert.True(machineDataItems.Length == 2);
        }

        [InlineData("Cappuccino", "Invalid csv line")]
        [InlineData("Cappuccino; InvalidDateTime", "Invalid datetime in csv line")]

        [Theory]
        public void ShouldThrowExceptionForInvalidLine(string csvLine, string expectedErrorMessage)
        {
            //Arrange
            var csvLines = new[] {csvLine};

            //Act
            var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(csvLines));

            //Assert
            Assert.Equal($"{expectedErrorMessage} : {csvLine}", exception.Message); 
        } 
    }
}
