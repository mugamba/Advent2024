
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        _keypadMemo.Add("A<", new List<string> { "v<<A", "<v<A" });
        _keypadMemo.Add("<A", new List<string> { ">>^A", ">^>A" });
        _keypadMemo.Add("A>", new List<string> { "vA" });
        _keypadMemo.Add(">A", new List<string> { "^A" });
        _keypadMemo.Add("Av", new List<string> { "v<A", "<vA" });
        _keypadMemo.Add("vA", new List<string> { "^>A", ">^A" });
        _keypadMemo.Add("^A", new List<string> { ">A" });
        _keypadMemo.Add("A^", new List<string> { "<A" });
        _keypadMemo.Add("^A", new List<string> { ">A" });
        _keypadMemo.Add("A^", new List<string> { "<A" });


        _keypad


        _keypadMemo.Add("AA", new List<string> { "A" });
        _keypadMemo.Add(">>", new List<string> { "A" });
        _keypadMemo.Add("<<", new List<string> { "A" });
        _keypadMemo.Add("^^", new List<string> { "A" });
        _keypadMemo.Add("vv", new List<string> { "A" });



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
                        _dic1.Add(r, t);
                   

                    temp.AddRange(t);
                }
                result = temp;

               i++;
            }
            //sum = sum + result.Select(o => o.Length).Min() * int.Parse(input.Replace("A", ""));
        }

        var start = DoTyping(_numPad, "029A").ToList();


        foreach (var t in start)
        {
           var ti =  DoTyping(_keypad, t.Substring(0, 6));
          var td = DoTyping(_keypad, t.Substring(6, t.Length - 6));

        }

        //foreach (var s in start)
        //{
        //    File.AppendAllText("text.txt", String.Format("line {0} --- length ({1})", s, s.Length));
        //    File.AppendAllText("text.txt", Environment.NewLine);
        //    Splits(s);

        //    File.AppendAllText("text.txt", Environment.NewLine);

        //}

        var keys = _dic1.Where(o => o.Value.Any(o => o.Length == 68)).ToDictionary();

        foreach (var key in keys)
        {
            
            File.AppendAllText("text.txt", String.Format("line {0} --- length ({1})", key.Key, key.Key.Length));
            File.AppendAllText("text.txt", Environment.NewLine);
            Splits(key.Key);  

            File.AppendAllText("text.txt", Environment.NewLine);

            foreach (var t in _dic1[key.Key])
            {
                //File.AppendAllText("text.txt", "      " + String.Format("line {0} --- length ({1})", t, t.Length));
                //File.AppendAllText("text.txt", Environment.NewLine);
                ////Splits(t);


                //File.AppendAllText("text.txt", Environment.NewLine);
            }


        }


        //var result = KeypadForInput(_keypad, "<A^A>^^AvvvA");
        Console.WriteLine(sum);
        Console.ReadKey();
    }

    private static List<String> DoTypingKeypad(string input)
    {
        var sb = new StringBuilder();
        var list = new List<String>();
        for (int i = 0; i < input.Length; i++)
        {
            char previous = 'A';
            if (i > 0)
                previous = input[i - 1];

            char current = input[i];
            var templist = Step(_keypad, previous, current);

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

    public static void Splits(String input)
    {
        var strings = new List<string>();   
        for (int i = 0; i < input.Length; i++)
        {
            if (i == 0)
                strings.Add("A" + input[i]);
            else
                strings.Add(input[i-1].ToString() + input[i]);

        }
    
        var dict = strings.GroupBy(o => o)
                      .ToDictionary(g => g.Key, g => (long)g.Count());


        foreach (var pair in _tokens)
        { 
            if (dict.ContainsKey(pair.Value))
                File.AppendAllText("text.txt", " " + String.Format(" T:{0} C:{1}", pair.Value, dict[pair.Value]));
            else
                File.AppendAllText("text.txt", " " + String.Format(" T:{0} C:{1}", pair.Value, 0));
        }


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
