using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creational.Builder
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime dateOfBirth { get; set; }

        public class Builder : PersonBirthDateBuilder<Builder>
        {
            internal Builder() { }
        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Title)}: {Title}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<T>:PersonBuilder where T:PersonInfoBuilder<T>
    {
        public T Called(string name)
        {
            person.Name = name;
            return (T) this;
        }
    }

    public class PersonJobBuilder<T>:PersonInfoBuilder<PersonJobBuilder<T>> where T:PersonJobBuilder<T>
    {
        public T WorksAs(string position)
        {
            person.Title= position;
            return (T) this;
        }
    }

    public class PersonBirthDateBuilder<T>:PersonJobBuilder<PersonBirthDateBuilder<T>> where T:PersonBirthDateBuilder<T>
    {
        public T Born(DateTime dateOfBirth)
        {
            person.dateOfBirth = dateOfBirth;
            return (T) this;
        }
    }
    internal class BuilderInheritence
    {
        class SomeBuilder : PersonBirthDateBuilder<SomeBuilder> { }

        public static void Main(string[] args)
        {
            var me = Person.New.Called("Dimitri").WorksAs("Engineer").Born(DateTime.Now).Build();
            Console.WriteLine(me);
        }
    }
}
