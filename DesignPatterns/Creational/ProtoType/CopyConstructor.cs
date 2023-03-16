using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.ProtoType
{
    public class CopyAddress
    {
        public string StreetAddress, City, Country;

        public CopyAddress(string streetAddress, string city, string country)
        {
            StreetAddress = streetAddress ?? throw new ArgumentNullException(paramName: nameof(streetAddress));
            City = city ?? throw new ArgumentNullException(paramName: nameof(city));
            Country = country ?? throw new ArgumentNullException(paramName: nameof(country));
        }

        public CopyAddress(CopyAddress other)
        {
            StreetAddress = other.StreetAddress;
            City = other.City;
            Country = other.Country;
        }

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
        }
    }

    public class Employee
    {
        public string Name;
        public CopyAddress Address;

        public Employee(string name, CopyAddress address)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        public Employee(Employee other)
        {
            Name = other.Name;
            Address = new CopyAddress(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
    internal class CopyConstructor
    {
        static void Main(string[] args)
        {
            var john = new Employee("John", new CopyAddress("123 London Road", "London", "UK"));

            //var chris = john;
            var chris = new Employee(john);

            chris.Name = "Chris";
            Console.WriteLine(john); // oops, john is called chris
            Console.WriteLine(chris);

        }
    }
}
