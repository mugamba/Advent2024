
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

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            var split = line.Split("-");
            if (_keyValuePairs.ContainsKey(split[0]))
                _keyValuePairs[split[0]].Add(split[1]);
            else
                _keyValuePairs.Add(split[0], new List<string>() { split[1] });

            if (_keyValuePairs.ContainsKey(split[1]))
                _keyValuePairs[split[1]].Add(split[0]);
            else
                _keyValuePairs.Add(split[1], new List<string>() { split[0] });


        }

        foreach (var k in _keyValuePairs)
        {
            foreach (var el in k.Value)
            {
                 var intersect = k.Value.Intersect(_keyValuePairs[el]).ToList();

                if (intersect.Any())
                    foreach (var v in intersect)
                    { 
                            var list = new List<string>();
                        list.Add(k.Key);
                        list.Add(v);
                        list.Add(el);

                        var toList = list.Order().ToArray();

                        var tuple = Tuple.Create(toList[0], toList[1], toList[2]);
                        
                        if(!_threeConnected.Contains(tuple))
                            _threeConnected.Add(tuple);
                    
                    }

                       

            }

        }


        Console.WriteLine(_threeConnected.Where(o => o.Item1.StartsWith("t") || o.Item2.StartsWith("t") || o.Item3.StartsWith("t")).Count());
        Console.ReadLine();
        
    }



   

}
