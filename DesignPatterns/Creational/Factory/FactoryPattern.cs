using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creational.Factory
{
    //why use factory
    //object creation logic becomes too convoluted
    //Constructor is not very descriptive
    //Cannot overload with same set of arguements with different names
    //can turn into "optional parameters hell"

    //Object creation (not piecewiese like builder) can be outsources to: 
    //A separate function(Factory Method)
    //That may exist in a separate class(Factory)
    //Can create hierarchy of factories with Abstract Factory

    //A factory is a component responsible solely for the wholesale (not piecewise) creation of objects.

    /// <summary>
    /// A Factory method is a static method that creates objects
    /// A Factory can take care of object creation
    /// A Factory can be external or reside inside the object as an inner class
    /// Hierarchies of factories can be used to create related objects
    /// </summary>
    public class Point
    {
        private double x, y;
        protected Point(double x, double y)
        {
            this.y = y; 
            this.x = x;
        }

        //factory methods
        #region Factory Methods
        public static Point NewCartesianPoint(double x, double y) { 
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta) {
            return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));
        }
        #endregion

        public override string ToString()
        {
            return $"{nameof(x)} : {x}, {nameof(y)} : {y}";
        }
    }
    internal class FactoryPattern
    {
        static void Main(string[] args)
        {
            var point = Point.NewCartesianPoint(0.0, 0.0);
            var polarPoint = Point.NewPolarPoint (1.0, Math.PI/2);
            Console.WriteLine(polarPoint);
        }
    }
}
