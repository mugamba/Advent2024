
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

        var sum = 0;
        foreach (var input in lines)
        {
            var result = DoTypingNumpad( input);
            int i = 0;
            while (i < 25)
            {
                var temp = new List<string>();
                foreach (var r in result)
                {
                    
                    var t = DoTypingKeypad("A"+r);
                    temp.AddRange(t);
                }
                result = temp;

               i++;
            }
            sum = sum + result.Select(o => o.Length).Min() * int.Parse(input.Replace("A", ""));
        }

      
        Console.WriteLine(sum);
        Console.ReadKey();
    }


    public static List<string> DoTypingKeypad(string input)
    {
        if (_keypadMemo.ContainsKey(input))
             return _keypadMemo[input];

        var result1 = DoTypingKeypad()


        var temp = new List<string>();  
        var builder = new StringBuilder();
        var array = input.ToCharArray();
      
        for (int i=1;i<input.Length; i++) 
        {
            var previous = string.Concat(input[i-1].ToString() + input[i].ToString());
            builder.Append(previous);
            if (temp.Count == 0)
            {
                temp.AddRange(DoTypingKeypad(previous));
            }
            else
            {
                var r = temp.SelectMany(x => DoTypingKeypad(previous), (x, y) => string.Concat(x, y)).ToList();
                var min = r.Min(o => o.Length);
                temp = r.Where(o => o.Length == min).ToList();
                var current = builder.ToString();
                if (!_keypadMemo.ContainsKey(current))
                    _keypadMemo.Add(current, temp);
            }
        }
        return temp; 
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

    //public static void Splits(String input)
    //{
    //    var strings = new List<string>();   
    //    for (int i = 0; i < input.Length; i++)
    //    {
    //        if (i == 0)
    //            strings.Add("A" + input[i]);
    //        else
    //            strings.Add(input[i-1].ToString() + input[i]);

    //    }
    
    //    var dict = strings.GroupBy(o => o)
    //                  .ToDictionary(g => g.Key, g => (long)g.Count());


    //    foreach (var pair in _tokens)
    //    { 
    //        if (dict.ContainsKey(pair.Value))
    //            File.AppendAllText("text.txt", " " + String.Format(" T:{0} C:{1}", pair.Value, dict[pair.Value]));
    //        else
    //            File.AppendAllText("text.txt", " " + String.Format(" T:{0} C:{1}", pair.Value, 0));
    //    }


    //}

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
