using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DesignPatterns.Creational.Builder
{
    //You are asked to implement the Builder design pattern for rendering simple chunks of code.

    //Sample use of the builder you are asked to create:

    //var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
    //Console.WriteLine(cb);
    //The expected output of the above code is:

    //public class Person
    //{
    //   public string Name;
    //   public int Age;
    //}
    //Please observe the same placement of curly braces and use two-space indentation.

    //Solution
    class Field
    {
        public string Type, Name;

        public override string ToString()
        {
            return $"public {Type} {Name}";
        }
    }

    class TypeClass
    {
        public string Name;
        public List<Field> Fields = new List<Field>();

        public TypeClass() { }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {Name}").AppendLine("{");
            foreach (var f in Fields)
                sb.AppendLine($"  {f};");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class BuilderExercise
    {
        private TypeClass typeClass = new TypeClass();
        public BuilderExercise(string rootName)
        {
            typeClass.Name = rootName;
        }

        public BuilderExercise AddField(string name, string type)
        {
            typeClass.Fields.Add(new Field { Name = name, Type = type });
            return this;
        }

        public override string ToString()
        {
            return typeClass.ToString();
        }
    }
}

    //var cb = new BuilderExercise("Person").AddField("Name", "string").AddField("Age", "int");
    /*namespace Coding.Exercise.UnitTests
    {
        [TestFixture]
        public class FirstTestSuite
        {
            private static string Preprocess(string s)
            {
                return s.Trim().Replace("\r\n", "\n");
            }

            [Test]
            public void EmptyTest()
            {
                var cb = new CodeBuilder("Foo");
                Assert.That(Preprocess(cb.ToString()), Is.EqualTo("public class Foo\n{\n}"));
            }

            [Test]
            public void PersonTest()
            {
                var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
                Assert.That(Preprocess(cb.ToString()),
                  Is.EqualTo(
                    @"public class Person
                    {
                        public string Name;
                        public int Age;
                    }"
                  ));
            }
        }
    }*/
