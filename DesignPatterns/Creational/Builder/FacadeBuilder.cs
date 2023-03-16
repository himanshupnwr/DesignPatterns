using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creational.Builder
{
    public class EmployeePerson
    {
        //address
        public string StreetAddress, PostCode, City;

        //employment
        public string CompanyName, Position;

        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(PostCode)}: {PostCode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";

        }
    }

    //Facade
    public class EmployeePersonBuilder
    {
        //the object we are going to build
        protected EmployeePerson person = new EmployeePerson();

        public EmployeePresonAddressBuilder Lives => new EmployeePresonAddressBuilder(person);
        public EmployeePersonJobBuilder Works => new EmployeePersonJobBuilder(person);

        public static implicit operator EmployeePerson(EmployeePersonBuilder epb)
        {
            return epb.person;
        }
    }

    public class EmployeePersonJobBuilder : EmployeePersonBuilder
    {
        public EmployeePersonJobBuilder(EmployeePerson person)
        {
            this.person = person;
        }

        public EmployeePersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public EmployeePersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public EmployeePersonJobBuilder Earning(int annualIncome)
        {
            person.AnnualIncome = annualIncome;
            return this;
        }
    }

    public class EmployeePresonAddressBuilder : EmployeePersonBuilder
    {
        // might not work with a value type!
        public EmployeePresonAddressBuilder(EmployeePerson person)
        {
            this.person = person;
        }

        public EmployeePresonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public EmployeePresonAddressBuilder WithPostcode(string postcode)
        {
            person.PostCode = postcode;
            return this;
        }

        public EmployeePresonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }

    }
    internal class FacadeBuilder
    {
        static void Main(string[] args)
        {
            var epb = new EmployeePersonBuilder();
            EmployeePerson person = epb
              .Lives
                .At("123 London Road")
                .In("London")
                .WithPostcode("SW12BC")
              .Works
                .At("Fabrikam")
                .AsA("Engineer")
                .Earning(123000);

            Console.WriteLine(person);
        }
    }
}
