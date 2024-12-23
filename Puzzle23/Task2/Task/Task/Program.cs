
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;


internal class Program
{
    public static Dictionary<string, List<string>> _keyValuePairs = new Dictionary<string, List<string>>();
    public static List<Tuple<string, string, string>> _threeConnected = new List<Tuple<string, string, string>>();
    public static List<string> _maxLan = new List<string>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            var split = line.Split("-");
            if (_keyValuePairs.ContainsKey(split[0]))
                _keyValuePairs[split[0]].Add(split[1]);
            else
                _keyValuePairs.Add(split[0], new List<string>() { split[0], split[1] });

            if (_keyValuePairs.ContainsKey(split[1]))
                _keyValuePairs[split[1]].Add(split[0]);
            else
                _keyValuePairs.Add(split[1], new List<string>() { split[1], split[0] });
        }

        Console.WriteLine(String.Join(", ", _keyValuePairs["co"]));
        Console.WriteLine(String.Join(", ", _keyValuePairs["de"]));
        Console.WriteLine(String.Join(", ", _keyValuePairs["ka"]));
        Console.WriteLine(String.Join(", ", _keyValuePairs["ta"]));
        Console.WriteLine(String.Join(", ", _keyValuePairs["vc"]));
        Console.WriteLine(String.Join(", ", _keyValuePairs["wq"]));

        var max = 0;
        foreach (var k in _keyValuePairs)
        {
          
        }


        Console.WriteLine();
        Console.ReadLine();
        
    }



   

}
