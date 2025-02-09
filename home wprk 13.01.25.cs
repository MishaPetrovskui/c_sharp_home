using System;
using System.Collections.Generic;
using System.Text;
public interface IDrivable
{
    void StartEngine();
    void StopEngine();
    void Drive();
}
public class Car : IDrivable
{
    public void StartEngine()
    {
        Console.WriteLine("Двигун авто запущено");
    }

    public void StopEngine()
    {
        Console.WriteLine("Двигун авто вимкнено");
    }

    public void Drive()
    {
        Console.WriteLine("Автомобіль їде");
    }
}
public class Motorcycle : IDrivable
{
    public void StartEngine()
    {
        Console.WriteLine("Двигун мотоциклу запущено");
    }

    public void StopEngine()
    {
        Console.WriteLine("Двигун мотоциклу вимкнено");
    }

    public void Drive()
    {
        Console.WriteLine("Мотоцикл їде");
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.InputEncoding = UTF8Encoding.UTF8;
        IDrivable car = new Car();
        IDrivable motorcycle = new Motorcycle();

        car.StartEngine();
        car.Drive();
        car.StopEngine();

        Console.WriteLine();

        motorcycle.StartEngine();
        motorcycle.Drive();
        motorcycle.StopEngine();
    }
}
