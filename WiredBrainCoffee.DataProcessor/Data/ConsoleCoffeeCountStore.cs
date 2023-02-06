using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data
{
    public class ConsoleCoffeeCountStore : ICoffeeCountStore
    {
        private readonly TextWriter textWriter; 

        public ConsoleCoffeeCountStore(): this(Console.Out) { }
        public ConsoleCoffeeCountStore(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        public void Save(CoffeeCountItem item)
        {
            var line = $"{item.CoffeeType}:{item.Count}";
            Console.WriteLine(line);
        }
            
    }
}
