﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;



internal class Program
{
    public static Dictionary<int, List<int>> _orders = new Dictionary<int, List<int>>();
    public static Dictionary<int, List<int>> _lines = new Dictionary<int, List<int>>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var counter = 0;
        foreach (var line in lines)
        {
            if (String.IsNullOrEmpty(line)) continue;

            if (line.Contains("|"))
            { 
                var splits = line.Split('|');
                var key = int.Parse(splits[0]);
                var value = int.Parse(splits[1]);

                if (_orders.ContainsKey(key))
                    _orders[key].Add(value);
                else
                    _orders.Add(key, new List<int> { value });
            }

            if (line.Contains(","))
            {
                var splits = line.Split(',');
                _lines.Add(counter, new List<int>());
                foreach (var split in splits)
                {
                    _lines[counter].Add(int.Parse(split));
                }
                counter++;
            }
        }

        var dict = _lines.Where(o => IsLineInOrder(o.Value)).ToDictionary();
        var sum = 0;
        foreach (var d in dict)
        {
            if (d.Value.Count % 2 == 1)
            {
                sum = sum + d.Value[d.Value.Count / 2];
            }
            else
                sum = sum + d.Value[(d.Value.Count / 2) - 1];

        }
        Console.WriteLine(sum);
        Console.ReadKey();
    }


    public static bool IsLineInOrder(List<int> line)
    {
        
        for (int i = 0;i<line.Count; i++)
        {
            var key = line[i];
            if (!_orders.ContainsKey(key))
                continue;

            if (!_orders[key].All(o => line.IndexOf(o) > i || line.IndexOf(o) == -1))
                return false;
        }
       
        return true;
    }

   



}
