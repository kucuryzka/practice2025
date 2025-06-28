using System;

namespace task04;

public interface ISpaceship
{
    void MoveForward();
    void Rotate(int angle);
    void Fire();
    int Speed { get; }
    int FirePower { get; }

}


public class Cruiser : ISpaceship
{
    public int Speed { get; }
    public int FirePower { get; }
    public double x { get; private set; }
    public double y { get; private set; }
    public int angle { get; private set; }

    public int DIST { get; } = 10;

    public Cruiser()
    {
        this.Speed = 50;
        this.FirePower = 10;
        this.x = 0;
        this.y = 0;
        this.angle = 0;
    }

    public void MoveForward()
    {
        double rads = (Math.PI / 180) * angle;
        x = Math.Round(DIST * Math.Cos(rads));
        y = Math.Round(DIST * Math.Sin(rads));
         System.Console.WriteLine($"The cruiser has moved forward by {DIST} meters, current coordinates are ({x},{y})");
    }

    public void Rotate(int angle)
    {
        this.angle = (this.angle + angle) % 360;
        System.Console.WriteLine($"The cruiser has rotated by {angle} degrees, current angle equals {this.angle} degrees");
    }

    public void Fire()
    {
        System.Console.WriteLine("CRUISER GOES KA-BOOM");
    }
}


public class Fighter : ISpaceship
{
    public int Speed { get; }
    public int FirePower { get; }
    public double x { get; private set; }
    public double y { get; private set; }
    public int angle { get; private set; }

    public int DIST { get; } = 10;

    public Fighter()
    {
        this.Speed = 100;
        this.FirePower = 5;
        this.x = 0;
        this.y = 0;
        this.angle = 0;
    }

    public void MoveForward()
    {
        double rads = (Math.PI / 180) * angle;
        x = Math.Round(DIST * Math.Cos(rads));
        y = Math.Round(DIST * Math.Sin(rads));
        System.Console.WriteLine($"The fighter has moved forward by {DIST} meters, current coordinates are ({x},{y})");
    }

    public void Rotate(int angle)
    {
        this.angle = (this.angle + angle) % 360;
        System.Console.WriteLine($"The fighter has rotated by {angle} degrees, current angle equals {this.angle} degrees");
    }

    public void Fire()
    {
        System.Console.WriteLine("FIGHTER GOES BTOOM");
    }
}