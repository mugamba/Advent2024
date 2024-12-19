
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
    public static List<string> _valid = new List<string>();
    public static Dictionary<string, Boolean> _memo = new Dictionary<string, Boolean>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt").Take(1).FirstOrDefault();
        var splits =  lines.Split(", ");
        var linesToCheck = File.ReadAllLines("input.txt").Skip(2).ToList();

        foreach (var sp in splits)
            _towels.Add(sp, 0);

        _samplesToCheck.AddRange(linesToCheck);

        foreach (var line in _samplesToCheck)
        {
            if (DoRecursion(line))
            {
                _valid.Add(line);
            }

            
        }
        

        Console.WriteLine (_valid.Distinct().Count());
        Console.ReadKey();
    }


    public static Boolean DoRecursion(string inputreduced)
    {
        if (inputreduced == "")
        {
            return true ;
        }

        if (_memo.ContainsKey(inputreduced))
        {
            return _memo[inputreduced]; 
        }


        var toTest = _towels.Where(o =>o.Key.Length <= inputreduced.Length &&  inputreduced.StartsWith(o.Key)).ToList();

        if (toTest.Count == 0)
            _memo.Add(inputreduced, false);


        Boolean result = false;
        foreach (var test in toTest)
        {
            var newString = inputreduced.Substring(test.Key.Length, inputreduced.Length - test.Key.Length);
            result = DoRecursion(newString);
            if (!_memo.ContainsKey(newString))
                _memo.Add(newString, result);
          
            if (result)
                return true;

        }

        return false;
    }

}
