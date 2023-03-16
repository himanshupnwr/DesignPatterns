using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creational.Builder
{
    public class Employee
    {
        public string Name, Position;
    }
    public sealed class EmployeeBuilder
    {
        public readonly List<Action<Employee>> Actions = new List<Action<Employee>>();

        public EmployeeBuilder Called(string Name)
        {
            Actions.Add(p => { p.Name = Name; });
            return this;
        }

        public Employee Build()
        {
            var emp = new Employee();
            Actions.ForEach(a => a(emp));
            return emp;
        }
    }
    public static class EmployeeBuilderExtenstions
    {
        public static EmployeeBuilder WorksAsA(this EmployeeBuilder builder, string position)
        {
            builder.Actions.Add(p => { p.Name = position;});
            return builder;
        }
    }
    internal class FunctionalBuilder
    {
        public static void Main(string[] args)
        {
            var empbuilder = new EmployeeBuilder();
            var employee = empbuilder.Called("Himanshu").WorksAsA("Programmer").Build();
        }
    }
}
