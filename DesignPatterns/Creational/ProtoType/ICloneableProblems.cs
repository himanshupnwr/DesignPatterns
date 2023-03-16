using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.ProtoType
{
    //why ICloneable is not a good idea because of the shallow copy
    public class Address : ICloneable
    {
        public readonly string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}:{StreetName}, {nameof(HouseNumber)}:{HouseNumber}";
        }

        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }
    }

    public class Person : ICloneable
    {
        public readonly string[] Names;
        public readonly Address address;

        public Person(string[] names, Address Address)
        {
            Names = names;
            address = Address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}:{string.Join(",", Names)}, {nameof(address)}:{address}";
        }

        public object Clone()
        {
            return new Person(Names, address);
        }
    }
    internal class ICloneableProblems
    {
        static void Main()
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("Lonfon Road", 123));
            var jane = (Person)john.Clone();
            jane.address.HouseNumber = 321; //issue is that john is now at 321

            //clone is shallow copy
            jane.Names[0] = "Names";
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
