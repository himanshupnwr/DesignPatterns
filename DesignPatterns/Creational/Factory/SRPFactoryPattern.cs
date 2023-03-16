using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creational.Factory
{
    //single responsibility pattern for factory where we create a separate class for factories
    //a factory is just a separate component which knows how to initialize types in a particular way
    public static class PointFactory
    {

        //factory methods
        public static PointF NewCartesianPointF(double x, double y)
        {
            return new PointF(x, y);
        }

        public static PointF NewPolarPointF(double rho, double theta)
        {
            return new PointF(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    
    public class PointF
    {
        private double x, y;
        public PointF(double x, double y)
        {
            this.y = y;
            this.x = x;
        }

        public override string ToString()
        {
            return $"{nameof(x)} : {x}, {nameof(y)} : {y}";
        }
    }
    internal class SRPFactoryPattern
    {
        static void Main(string[] args)
        {
            var pointF = PointFactory.NewCartesianPointF(0.0, 0.0);
            var polarPointF = PointFactory.NewPolarPointF(1.0, Math.PI / 2);
            Console.WriteLine(polarPointF);
        }
    }
}
