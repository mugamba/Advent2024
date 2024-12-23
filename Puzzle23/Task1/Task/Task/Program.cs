
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Program
{
    public static Dictionary<string, List<string>> _keyValuePairs = new Dictionary<string, List<string>>();
    public static string _wifi;

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            var split = line.Split("-");
            if (_keyValuePairs.ContainsKey(split[0]))
                _keyValuePairs[split[0]].Add(split[1]);
            else
                _keyValuePairs.Add(split[0], new List<string>() { split[1], split[0] });

            if (_keyValuePairs.ContainsKey(split[1]))
                _keyValuePairs[split[1]].Add(split[0]);
            else
                _keyValuePairs.Add(split[1], new List<string>() { split[0], split[1] });
        }
        var totalMax = 0;
        foreach (var k in _keyValuePairs)
        {
            var list = new List<string>(); 
            foreach (var el in k.Value)
            {
               list.AddRange(_keyValuePairs[el]);
            }
            var grouped = list.GroupBy(o => o).Select(o=>new { Key = o.Key, Number = o.Count()}).ToList();
            var max = grouped.Max(o=>o.Number);
            while (max > 0)
            {
                var broj = grouped.Where(o => o.Number >= max).Count();
                if (broj == max && broj > totalMax)
                {
                    var setList = grouped.Where(o => o.Number >= max);
                    _wifi = String.Join(",", setList.Select(o=>o.Key).Order());
                    totalMax = broj;
                }
                max--;
            }
        }
        Console.WriteLine(_wifi);
        Console.ReadLine();
    }
}
