
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    public static List<Claw> _claws = new List<Claw>();
    public static List<int> _wins = new List<int>();


    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var claw = new Claw();
        foreach (var line in lines)
        {
            if (String.IsNullOrEmpty(line))
            {
                _claws.Add(claw);
                claw = new Claw();
                continue;
            }
            if (line.Contains("Button A:"))
            {
                var trimed = line.Replace("Button A:", "").Trim();
                var xy = trimed.Split(", ");
                claw.ButtonAXOffset = long.Parse(xy[0].Trim().Replace("X+", "").Trim());
                claw.ButtonAYOffset = long.Parse(xy[1].Trim().Replace("Y+", "").Trim());
            }
            if (line.Contains("Button B:"))
            {
                var trimed = line.Replace("Button B:", "").Trim();
                var xy = trimed.Split(", ");
                claw.ButtonBXOffset = long.Parse(xy[0].Trim().Replace("X+", "").Trim());
                claw.ButtonBYOffset = long.Parse(xy[1].Trim().Replace("Y+", "").Trim());
            }
            if (line.Contains("Prize:"))
            {
                var trimed = line.Replace("Prize:", "").Trim();
                var xy = trimed.Split(", ");
                claw.PrizePositionX = long.Parse(xy[0].Trim().Replace("X=", "").Trim());
                claw.PrizePositionY = long.Parse(xy[1].Trim().Replace("Y=", "").Trim());

            }



        }

        _claws.Add(claw);

        long intx = 1; long inty = 1;
        var isTrue = false;

        foreach (var c in _claws)
        {

            var test = c.PrizePositionX % c.ButtonAXOffset;
            var test1 = c.PrizePositionX % c.ButtonBXOffset;

            var test2 = c.PrizePositionY % c.ButtonAYOffset;
            var test3 = c.PrizePositionY % c.ButtonBYOffset;


            TestLine(c);
        }

        Console.WriteLine(_wins.Sum());
        Console.ReadKey();
    }


    public static Int64 TestLine(Claw ct)
    {
        var matchesX =new List<int>();
        var matchesY = new List<int>();

        for (int i = 0; i < 10000; i++)
        {
            if ((ct.PrizePositionX - i * ct.ButtonAXOffset) % ct.ButtonBXOffset == 0)
            {
                //   var inty = (ct.PrizePositionX - i * ct.ButtonAXOffset) / ct.ButtonBXOffset;
                matchesX.Add(i);
                if (matchesX.Count > 50 && matchesY.Count > 50)
                    break;

            }
            if ((ct.PrizePositionY - i * ct.ButtonAYOffset) % ct.ButtonBYOffset == 0)
            {
                matchesY.Add(i);
                if (matchesX.Count > 50 && matchesY.Count > 50)
                    break;
            }
        }

        foreach (var t in matchesX)
        {
            var test = t % 7;

        }

        return 0;
    }


    public class Claw
    {
        public long ButtonAXOffset;
        public long ButtonBXOffset;
        public long ButtonAYOffset;
        public long ButtonBYOffset;
        public long PrizePositionX;
        public long PrizePositionY;
    }

}
