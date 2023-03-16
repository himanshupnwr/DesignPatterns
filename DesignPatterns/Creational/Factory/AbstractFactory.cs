using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Factory
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea:IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nuce but i'd prefer it with milk");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is good");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in tea bag, boil water, pour {amount} ml, add lemon, enjoy");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        private List<Tuple<string, IHotDrinkFactory>> _factories = new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach(var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if(typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    _factories.Add(Tuple.Create(t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t)));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available Drink");
            for(var index = 0; index < _factories.Count; index++)
            {
                var tuple = _factories[index];
                Console.WriteLine($"{index}:{tuple.Item1}");
            }

            while(true)
            {
                string s;
                if((s = Console.ReadLine()) != null && int.TryParse(s, out int it) && it > 0 && it<_factories.Count) {
                    Console.WriteLine("Specify amount");
                    if (s != null && int.TryParse(s, out int amount) && amount > 0)
                    {
                        return _factories[it].Item2.Prepare(amount);
                    }
                }
                Console.WriteLine("Incorrect input, try again");
            }
        }
    }

    internal class AbstractFactory
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            IHotDrink drink = machine.MakeDrink();
            drink.Consume();
        }
    }
}
