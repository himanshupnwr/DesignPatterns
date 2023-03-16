using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.ProtoType
{
    //To implement a prototype partially construct an object and store it somewhere
    //clone the prototype - implement your own deep copy functionality or
    //serialize and deserialize
    //customize the resulting instance
    public class Point
    {
        public int X, Y;
    }

    public class ProtoTypeExercise
    {
        public Point Start, End;

        public ProtoTypeExercise DeepCopy()
        {
            var newStart = new Point { X = Start.X, Y = Start.Y };
            var newEnd = new Point { X = End.X, Y = End.Y };
            return new ProtoTypeExercise { Start = newStart, End = newEnd };
        }
    }
}

/*namespace Coding.Exercise.UnitTests
  {
    [TestFixture]
    public class FirstTestSuite
    {
      [Test]
      public void Test()
      {
        var line1 = new Line
        {
          Start = new Point { X = 3, Y = 3},
          End = new Point { X = 10, Y = 10}
        };

        var line2 = line1.DeepCopy();
        line1.Start.X = line1.End.X = line1.Start.Y = line1.End.Y = 0;

        Assert.That(line2.Start.X, Is.EqualTo(3));
        Assert.That(line2.Start.Y, Is.EqualTo(3));
        Assert.That(line2.End.X, Is.EqualTo(10));
        Assert.That(line2.End.Y, Is.EqualTo(10));
      }
    }
  }*/
