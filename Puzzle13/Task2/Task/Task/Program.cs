
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
    public static List<long> _wins = new List<long>();


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
                claw.PrizePositionX = 10000000000000L + long.Parse(xy[0].Trim().Replace("X=", "").Trim());
                claw.PrizePositionY = 10000000000000L + long.Parse(xy[1].Trim().Replace("Y=", "").Trim());

            }

        }

        _claws.Add(claw);   
        foreach (var c in _claws)
        {
            TestLine(c);
        }
        Console.WriteLine(_wins.Sum());
        Console.ReadKey();
    }


    public static void TestLine(Claw ct)
    {

        var a1 = -ct.ButtonAYOffset * ct.ButtonBXOffset;
        var r1 = -ct.ButtonAYOffset * ct.PrizePositionX;

        var a2 = ct.ButtonAXOffset * ct.ButtonBYOffset;
        var r2 = ct.ButtonAXOffset * ct.PrizePositionY;

        var a = a1 + a2;
        var r = r1 + r2;

        if (r == 0 || a == 0)
            return;

        if (r % a != 0)
            return;

        if (r / a < 0) 
            return;

            var resultB = r / a;
            var test = (ct.PrizePositionX - resultB * ct.ButtonBXOffset) % ct.ButtonAXOffset;

        if (test != 0)
            return;

        var resultA = (ct.PrizePositionX - resultB * ct.ButtonBXOffset) / ct.ButtonAXOffset;
        _wins.Add(resultA * 3 + resultB);

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
