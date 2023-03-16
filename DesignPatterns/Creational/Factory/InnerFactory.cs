using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Factory
{
    
    public class PointInner
    {
        private double x, y;
        private PointInner(double x, double y)
        {
            this.y = y;
            this.x = x;
        }

        public override string ToString()
        {
            return $"{nameof(x)} : {x}, {nameof(y)} : {y}";
        }

        public static PointInner orgin = new PointInner(0,0);

        public static class FactoryInner
        {

            //factory methods
            public static PointInner NewCartesianPointF(double x, double y)
            {
                return new PointInner(x, y);
            }

            public static PointInner NewPolarPointF(double rho, double theta)
            {
                return new PointInner(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }
    internal class InnerFactoryPattern
    {
        static void Main(string[] args)
        {
            var pointF = PointInner.FactoryInner.NewCartesianPointF(0.0, 0.0);
            var polarPointF = PointInner.FactoryInner.NewPolarPointF(1.0, Math.PI / 2);
            Console.WriteLine(polarPointF);
        }
    }
}
