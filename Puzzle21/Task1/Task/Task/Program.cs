
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;


internal class Program
{
    public static Dictionary<Point, char> _keypad = new Dictionary<Point, char>();
    public static Dictionary<Point, char> _numPad = new Dictionary<Point, char>();
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var builder = new StringBuilder();

        _numPad.Add(new Point(0, 0), '7');
        _numPad.Add(new Point(0, 1), '8');
        _numPad.Add(new Point(0, 2), '9');
        _numPad.Add(new Point(1, 0), '4');
        _numPad.Add(new Point(1, 1), '5');
        _numPad.Add(new Point(1, 2), '6');
        _numPad.Add(new Point(2, 0), '1');
        _numPad.Add(new Point(2, 1), '2');
        _numPad.Add(new Point(2, 2), '3');
        _numPad.Add(new Point(3, 1), '0');
        _numPad.Add(new Point(3, 2), 'A');


        _keypad.Add(new Point(0, 2), 'A');
        _keypad.Add(new Point(0, 1), '^');
        _keypad.Add(new Point(1, 1), 'v');
        _keypad.Add(new Point(1, 0), '<');
        _keypad.Add(new Point(1, 2), '>');

        var sum = 0;
        foreach (var input in lines)
        {
            var result = DoTyping(_numPad, input);
            int i = 0;
            while (i < 2)
            {
                var temp = new List<string>();
                foreach (var r in result)
                {
                    var t = DoTyping(_keypad, r);
                    temp.AddRange(t);
                }
                result = temp;
                i++;
                var tss = result.Select(o => o.Length).Min();
            }
            sum = sum + result.Select(o => o.Length).Min() * int.Parse(input.Replace("A", ""));
        }
        //var result = KeypadForInput(_keypad, "<A^A>^^AvvvA");
        Console.WriteLine(sum);
        Console.ReadKey();
    }

    private static List<String> DoTyping(Dictionary<Point, char> inputDict, string input)
    {
        var sb = new StringBuilder();
        var list = new List<String>();
        for (int i = 0; i < input.Length; i++)
        {
            char previous = 'A';
            if (i > 0)
                previous = input[i - 1];

            char current = input[i];
            var templist = Step(inputDict, previous, current);

            if (list.Count == 0)
                list.AddRange(templist);
            else
            {
                var tt = new List<String>();
                foreach (var l in list)
                    foreach (var t in templist)
                    {
                        tt.Add(l + t);
                    }
                list = tt; 
            }
        }
        return list;
    }

    private static List<String> Step(Dictionary<Point, char> input,  char start, char destination)
    {
        var point = input.Where(o => o.Value == start).Select(o=>o.Key).FirstOrDefault();
        var point1 = input.Where(o => o.Value == destination).Select(o => o.Key).FirstOrDefault();
        var distance = Math.Abs(point.X - point1.X) + Math.Abs(point.Y - point1.Y);

        return GetAllPosiblePaths(input, distance, "", point, point1);

        
    }


    public static List<String> GetAllPosiblePaths(Dictionary<Point, char> input,  int distance, string currentString, Point step, Point destination)
    {
     
        var list = new List<String>();

        var newDistance = Math.Abs(step.X - destination.X) + Math.Abs(step.Y - destination.Y);

        if (newDistance > distance)
            return list;

        if (distance < currentString.Length)
            return list;

        if (distance == currentString.Length)
        {
            if (step == destination)
            {
                list.Add(currentString+"A");
                return list;
            }
            else
                return list;
        }
        
        var newPoint = new Point(step.X - 1, step.Y);
        if (input.ContainsKey(newPoint))
        {

            list.AddRange(GetAllPosiblePaths(input, distance, currentString + "^", newPoint, destination));
        }

        newPoint = new Point(step.X + 1, step.Y);
        if (input.ContainsKey(newPoint))
        {

            list.AddRange(GetAllPosiblePaths(input, distance, currentString + "v", newPoint, destination));
        }


        newPoint = new Point(step.X, step.Y - 1);
        if (input.ContainsKey(newPoint))
        {

            list.AddRange(GetAllPosiblePaths(input, distance, currentString + "<", newPoint, destination));
        }


        newPoint = new Point(step.X, step.Y + 1);
        if (input.ContainsKey(newPoint))
        {
            list.AddRange(GetAllPosiblePaths(input, distance, currentString + ">", newPoint, destination));
        }

        return list;

    }




}
