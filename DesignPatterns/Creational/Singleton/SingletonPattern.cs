using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Singleton
{
    //Motivation is that for some components it only makes sense to have one object in the system
    //example - database repository, object factory
    //constructor call is expnesive we lets do it only once and provide everyone with same instance
    //wants to prevent anyone creating additional copies
    //Need to take care of lazy instantiation and therad safety

    //Singleton is a component which is instantiated only once

    //Making a 'safe' singleton is easy. construct a static Lazy<T> and return its value
    //Singletons are difficult to test
    //Instead of directly using a singleton, considering depending on an abstraction(eg an interface)
    //Consider defining singleton lifetime in DI Container
    internal class SingletonPattern
    {
    }
}
