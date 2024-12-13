
using System;
using System.Collections.Generic;
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
                claw.ButtonAXOffset = int.Parse(xy[0].Trim().Replace("X+", "").Trim());
                claw.ButtonAYOffset = int.Parse(xy[1].Trim().Replace("Y+", "").Trim());
            }
            if (line.Contains("Button B:"))
            {
                var trimed = line.Replace("Button B:", "").Trim();
                var xy = trimed.Split(", ");
                claw.ButtonBXOffset = int.Parse(xy[0].Trim().Replace("X+", "").Trim());
                claw.ButtonBYOffset = int.Parse(xy[1].Trim().Replace("Y+", "").Trim());
            }
            if (line.Contains("Prize:"))
            {
                var trimed = line.Replace("Prize:", "").Trim();
                var xy = trimed.Split(", ");
                claw.PrizePositionX = int.Parse(xy[0].Trim().Replace("X=", "").Trim());
                claw.PrizePositionY = int.Parse(xy[1].Trim().Replace("Y=", "").Trim());

            }



        }

        _claws.Add(claw);

        foreach (var c in _claws)
        {
            var isMatch = false;
            for (var i = 1; i < 101; i++)
            {
                if (isMatch)
                    break;


                for (var j = 1; j < 101; j++)
                {

                    var prizex = c.ButtonAXOffset * i + c.ButtonBXOffset * j;
                    var prizeY = c.ButtonAYOffset * i + c.ButtonBYOffset * j;

                    if (c.PrizePositionX == prizex && c.PrizePositionY == prizeY)
                    {
                        _wins.Add(i * 3 + j);
                        isMatch = true; break;

                    }

                }
            }
        }

        Console.WriteLine (_wins.Sum());
        Console.ReadKey();
    }


    public class Claw
    {
        public int ButtonAXOffset;
        public int ButtonBXOffset;
        public int ButtonAYOffset;
        public int ButtonBYOffset;

        public int PrizePositionX;
        public int PrizePositionY;
    }

}
