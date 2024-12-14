
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    public static List<Robot> _robots = new List<Robot>();
    public static int _X;
    public static int _Y;


    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines) 
        {

            var robot = new Robot();
            var splits = line.Split(" ");

            var startPoints = splits[0].Trim().Replace("p=", "").Trim().Split(",");
            var speed = splits[1].Trim().Replace("v=", "").Trim().Split(",");

            robot.StartPoint = new Point(int.Parse(startPoints[0]), int.Parse(startPoints[1]));
            robot.SpeedVector = new Point(int.Parse(speed[0]), int.Parse(speed[1]));
            _robots.Add(robot);
        }

        //_robots.Add(new Robot()
        //{
        //    StartPoint = new Point(2, 4),
        //    SpeedVector = new Point(2, -3)
        //});
        _X = 101;
        _Y = 103;

        foreach (var robot in _robots)
        {
            for (int i = 0; i < 100; i++)
            {
                robot.Move();
            }
        }

      

        var firstQuadrant = _robots.Where(o => o.CurrentPoint.X < _X / 2 && o.CurrentPoint.Y < _Y / 2).Count();
        var secondQuadrant = _robots.Where(o => o.CurrentPoint.X > _X / 2 && o.CurrentPoint.Y < _Y / 2).Count();
        var thirdQuadrant = _robots.Where(o => o.CurrentPoint.X < _X / 2 && o.CurrentPoint.Y > _Y / 2).Count();
        var fourthQuadrant = _robots.Where(o => o.CurrentPoint.X > _X / 2 && o.CurrentPoint.Y > _Y / 2).Count();

        Console.WriteLine (firstQuadrant * secondQuadrant * thirdQuadrant * fourthQuadrant );
        Console.ReadKey();
    }


    public class Robot
    {
        public Point StartPoint { get; set; }
        public Point CurrentPoint { get; set; }
        public Point SpeedVector { get; set; }
        public int Moves { get; set; }

        public void Move()
        {
            if (Moves != 0 && StartPoint == CurrentPoint)
            { 
                
            }


            Point moveFrom;
            if (Moves == 0)
                moveFrom = StartPoint;
            else
                moveFrom = CurrentPoint;


            var x = moveFrom.X + SpeedVector.X;
            if (x < 0)
                x = _X + x;
            if (x >= _X)
                x = x % _X;
            var y = moveFrom.Y + SpeedVector.Y;
            if (y < 0)
                y = _Y + y;
            if (y >= _Y)
                y = y % _Y;

            CurrentPoint = new Point(x, y);
            Moves++;
        }
    }

}
