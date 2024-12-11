
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    static Dictionary<long, long> _line = new Dictionary<long, long>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt").FirstOrDefault();


        var splits = lines.Split(" ");
        foreach (var line in splits) 
        {
            if (!_line.ContainsKey(long.Parse(line)))
                _line.Add(long.Parse(line), 1);
            else
                _line[long.Parse(line)]++;
        }

        for(var i = 0; i < 75; i++)
        {
            DoBlink();
        }

        Console.WriteLine (_line.Select(o => o.Value).Sum());
        Console.ReadKey();
    }


    public static void DoBlink()
    {

        var toSplit = _line.Where(o => o.Key.ToString().Length % 2 == 0).Select(o => o.Key);
        var newDictionary = new Dictionary<long, long>();

        var toMultiply = _line.Where(o => o.Key.ToString().Length % 2 != 0 && o.Key != 0).Select(o => o.Key);

        if (_line.ContainsKey(0))
            newDictionary.Add(1, _line[0]);

        foreach (var key in toSplit)
        {

            var valueSplited = _line[key];
            var stringKy = key.ToString();

            var firsthalf = stringKy.Substring(0, stringKy.Length / 2);
            var secondhalf = stringKy.Substring(stringKy.Length / 2, stringKy.Length / 2);

            long first = long.Parse(firsthalf);
            long second = long.Parse(secondhalf);

            if (!newDictionary.ContainsKey(first))
                newDictionary.Add(first, valueSplited);
            else
                newDictionary[first] = newDictionary[first] + valueSplited;

            if (!newDictionary.ContainsKey(second))
                newDictionary.Add(second, valueSplited);
            else
                newDictionary[second] = newDictionary[second] + valueSplited;

        }

        foreach (var key in toMultiply)
        {

            var valueMultipled = _line[key];

            var newKey = key * 2024;
            if (!newDictionary.ContainsKey(newKey))
                newDictionary.Add(newKey, valueMultipled);
            else
                newDictionary[newKey] = newDictionary[newKey] + valueMultipled;
        }

        _line = newDictionary;
    }

}
