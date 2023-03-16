using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.ProtoType
{
    //Complicated objects aren't designed from scratch - they reiterate existing designs
    //An existing (partially or fully constrcuted) design is a Prototype
    //We make a copy (Clone) the prototype and customize it  - Requires 'deep copy support'
    //We make the cloning convenient(e.g, via a factory)
    public interface IDeepCopyable<T> where T: new()
    {
        void CopyTo(T target);
        public T DeepCopy()
        {
            T t  = new T();
            CopyTo(t);
            return t;
        }

    }

    public static class DeepCopyExtensions
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> item)
          where T : new()
        {
            return item.DeepCopy();
        }

        public static T DeepCopy<T>(this T person)
          where T : ProtoPerson, new()
        {
            return ((IDeepCopyable<T>)person).DeepCopy();
        }
    }

    public class HouseAddress : IDeepCopyable<HouseAddress>
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public HouseAddress()
        {

        }
        public HouseAddress(string street, int houseNumber) { 
            Street = street;
            HouseNumber = houseNumber;
        }
        public override string ToString()
        {
            return $"{nameof(Street)}: {Street}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public void CopyTo(HouseAddress target)
        {
            target.Street = Street;
            target.HouseNumber = HouseNumber;
        }
    }

    public class ProtoPerson : IDeepCopyable<ProtoPerson>
    {
        public string[] Names;
        public HouseAddress Address;

        public ProtoPerson()
        {

        }

        public ProtoPerson(string[] names, HouseAddress address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(",", Names)}, {nameof(Address)}: {Address}";
        }

        public virtual void CopyTo(ProtoPerson target)
        {
            target.Names = (string[])Names.Clone();
            target.Address = Address.DeepCopy();
        }
    }

    public class ProtoEmployee : ProtoPerson, IDeepCopyable<ProtoEmployee>
    {
        public int Salary;

        public void CopyTo(ProtoEmployee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
        }
    }
    internal class PrototypeInheritence
    {
        static void Main()
        {
            var john = new ProtoEmployee();
            john.Names = new[] { "John", "Doe" };
            john.Address = new HouseAddress { HouseNumber = 123, Street = "London Road" };
            john.Salary = 321000;
            var copy = john.DeepCopy();

            copy.Names[1] = "Smith";
            copy.Address.HouseNumber++;
            copy.Salary = 123000;

            Console.WriteLine(john);
            Console.WriteLine(copy);
        }
    }
}
