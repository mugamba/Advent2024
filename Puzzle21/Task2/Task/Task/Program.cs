
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;


internal class Program
{
    public static Dictionary<Point, char> _keypad = new Dictionary<Point, char>();
    public static Dictionary<Point, char> _numPad = new Dictionary<Point, char>();
    public static Dictionary<string, List<string>> _keypadMemo = new Dictionary<string, List<string>>();
    public static Dictionary<Tuple<string, int>, long> _distanceMemo = new Dictionary<Tuple<string, int>, long>();

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

        _keypadMemo.Add("A<", new List<string> { "v<<A", "<v<A" });
        _keypadMemo.Add("<A", new List<string> { ">>^A", ">^>A" });
        _keypadMemo.Add("A>", new List<string> { "vA" });
        _keypadMemo.Add(">A", new List<string> { "^A" });
        _keypadMemo.Add("Av", new List<string> { "v<A", "<vA" });
        _keypadMemo.Add("vA", new List<string> { "^>A", ">^A" });
        _keypadMemo.Add("^A", new List<string> { ">A" });
        _keypadMemo.Add("A^", new List<string> { "<A" });
        _keypadMemo.Add(">v", new List<string> { "<A" });
        _keypadMemo.Add("v>", new List<string> { ">A" });
        _keypadMemo.Add("v^", new List<string> { "^A" });
        _keypadMemo.Add("^v", new List<string> { "vA" });
        _keypadMemo.Add("v<", new List<string> { "<A" });
        _keypadMemo.Add("<v", new List<string> { ">A" });
        _keypadMemo.Add("<^", new List<string> { ">^A" });
        _keypadMemo.Add("^<", new List<string> { "v<A" });
        _keypadMemo.Add(">^", new List<string> { "^<A", "<^A" });
        _keypadMemo.Add("^>", new List<string> { "v>A", ">vA" });
        _keypadMemo.Add("<>", new List<string> { ">>A" });
        _keypadMemo.Add("><", new List<string> { "<<A" });
        _keypadMemo.Add("AA", new List<string> { "A" });
        _keypadMemo.Add(">>", new List<string> { "A" });
        _keypadMemo.Add("<<", new List<string> { "A" });
        _keypadMemo.Add("^^", new List<string> { "A" });
        _keypadMemo.Add("vv", new List<string> { "A" });

        var sum = 0L;
        foreach (var input in lines)
        {
            var result = DoTypingNumpad( input);
            var t = DoTypingKeypad(result, 0);
            sum = sum + t * long.Parse(input.Replace("A", ""));
        }

      
        Console.WriteLine(sum);
        Console.ReadKey();
    }


    public static long DoTypingKeypad(List<string> input, int depth)
    {
        if (depth == 25)
            return input.Select(o=>o.Length).Min();

        var tokenMinLength = long.MaxValue;
        foreach (var token in input)
        {
            var test = token;
                test = "A" + token;

            var minOfCuurentToken = 0L; var result = 0L;
            for (int i = 1; i < test.Length; i++)
            {
                var previous = string.Concat(test[i - 1].ToString() + test[i].ToString());
            
                if (_distanceMemo.ContainsKey(Tuple.Create(previous, depth)))
                    result = _distanceMemo[Tuple.Create(previous, depth)];
                else
                {
                   result = DoTypingKeypad(_keypadMemo[previous], depth + 1);
                    _distanceMemo.Add(Tuple.Create(previous, depth), result);
                }
                minOfCuurentToken = minOfCuurentToken + result;
            }
            if (minOfCuurentToken < tokenMinLength)
                tokenMinLength = minOfCuurentToken;
        }

        return tokenMinLength;
    }
       

    private static List<String> DoTypingNumpad(string input)
    {
        var sb = new StringBuilder();
        var list = new List<String>();
        for (int i = 0; i < input.Length; i++)
        {
            char previous = 'A';
            if (i > 0)
                previous = input[i - 1];

            char current = input[i];
            var templist = Step(_numPad, previous, current);

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
