
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


internal class Program
{
    public static List<Tuple<long, List<long>>> _input = new List<Tuple<long, List<long>>>();


    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        
        foreach (var line in lines)
        {
            var splits = line.Split(':');
            var key = long.Parse(splits[0].Trim());
            var list = new List<long>();

            var values = splits[1].Trim().Split(' ');
            list.AddRange(values.Select(o => long.Parse(o)));
            _input.Add(Tuple.Create(key, list));

        }

        long sum = 0;
        foreach (var line in _input)
        {
            if (TestLine(line.Item1, line.Item2, 0, line.Item2[0]))
                sum = sum + line.Item1;
        }

        Console.WriteLine(sum);      
        Console.ReadKey();
    }

    private static Boolean TestLine(long key, List<long> elements, int index, long currentelement)
    {
        if (elements.Count - 1 == index)
            return key == currentelement;

        return TestLine(key, elements, index + 1, currentelement * elements[index + 1])
            || TestLine(key, elements, index + 1, currentelement + elements[index + 1]);
            

    }
}
