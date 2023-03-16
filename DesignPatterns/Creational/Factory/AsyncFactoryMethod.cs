using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Factory
{

    //want to use async and await at time of object creation or in a constructor call
    public class Foo
    {
        private Foo() { } //make constructor private

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync() {
            var result = new Foo();
            return result.InitAsync();
        }
    }
    internal class AsyncFactoryMethod
    {
        public static async Task Main(string[] args)
        {
            await Foo.CreateAsync();
        }
    }
    //this is how we can quickly get at the result with asynchronous initialization just by using a factory
}
