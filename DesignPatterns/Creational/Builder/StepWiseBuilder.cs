using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;

namespace DesignPatterns.Creational.Builder
{
    public enum CarType
    {
        Sedan,
        Crossover
    };
    public class Car
    {
        public CarType CarType;
        public int WheelSize;
    }
    public interface ISpecifyCarType
    {
        public ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        public IBuildCar WithWheels(int wheelSize);
    }
    public interface IBuildCar
    {
        public Car Build();
    }
    public class CarBuilder
    {
        public static ISpecifyCarType Create()
        {
            return new Implementation();
        }
    }
    public class Implementation : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
    {
        private Car car = new Car();
        public Car Build()
        {
            return car;
        }

        public ISpecifyWheelSize OfType(CarType type)
        {
            car.CarType= type;
            return this;
        }

        public IBuildCar WithWheels(int wheelSize)
        {
            switch (car.CarType)
            {
                case CarType.Crossover when wheelSize < 17 || wheelSize > 20:
                case CarType.Sedan when wheelSize < 15 || wheelSize > 17:
                    throw new ArgumentException($"Wrong size of wheel for {car.CarType}");

            }
            car.WheelSize= wheelSize;
            return this;

        }
    }
    internal class StepWiseBuilder
    {
        static void Main(string[] args)
        {
            var car = CarBuilder.Create().OfType(CarType.Crossover).WithWheels(18).Build();
            Console.WriteLine(car);
        }
    }
}
