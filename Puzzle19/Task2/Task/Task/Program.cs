
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    public static Dictionary<string, int> _towels = new Dictionary<string, int>();
    public static List<string> _samplesToCheck = new List<string>();
    public static Dictionary<string, long> _valid = new Dictionary<string, long>();
    public static Dictionary<string, long> _memo = new Dictionary<string, long>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt").Take(1).FirstOrDefault();
        var splits =  lines.Split(", ");
        var linesToCheck = File.ReadAllLines("input.txt").Skip(2).ToList();

        foreach (var sp in splits)
            _towels.Add(sp, 0);

        _samplesToCheck.AddRange(linesToCheck);

        var result = 0L;
        foreach (var line in _samplesToCheck)
        {
            _valid.Add(line, DoRecursion(line));
        }

        Console.WriteLine (_valid.Select(o=>o.Value).Sum());
        Console.ReadKey();
    }


    public static long DoRecursion(string inputreduced)
    {
        if (inputreduced == "")
        {
            return 1;
        }
        if (_memo.ContainsKey(inputreduced))
        {
            return _memo[inputreduced]; 
        }

        var toTest = _towels.Where(o =>o.Key.Length <= inputreduced.Length &&  inputreduced.StartsWith(o.Key)).ToList();
        if (toTest.Count == 0)
            _memo.Add(inputreduced, 0);

        long sum = 0;
        foreach (var test in toTest)
        {
            var newString = inputreduced.Substring(test.Key.Length, inputreduced.Length - test.Key.Length);
            var result = DoRecursion(newString);
            if (!_memo.ContainsKey(newString))
                _memo.Add(newString, result);

            sum = sum + result;
        }
        return sum;
    }
}
