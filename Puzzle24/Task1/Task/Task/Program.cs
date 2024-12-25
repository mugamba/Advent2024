
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


internal class Program
{
    public static Dictionary<string, Boolean> _keyValues = new Dictionary<string, Boolean>();
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var builder = new StringBuilder();
        var commands = new List<string>();

        foreach (var line in lines)
        { 
            if (String.IsNullOrEmpty(line)) continue;

            if (line.Contains(":"))
            { 
              var splits =   line.Split(':');
                _keyValues.Add(splits[0].Trim(), (splits[1].Trim() == "1" ? true : false));
            }

            if (line.Contains("->"))
            {
                commands.Add(line);
            }
        }

        var allCalculated = false;
        while (!allCalculated)
        {
            var count = 0;
            foreach (var command in commands)
            {
                var splits = command.Split("->");
                splits[0].Trim();
                var next = splits[1].Trim();
                var operands = splits[0].Split(" ");
                var first = operands[0].Trim();
                var second = operands[2].Trim();
                var operation = operands[1].Trim();

                if (_keyValues.ContainsKey(first) && _keyValues.ContainsKey(second))
                {
                    if (!_keyValues.ContainsKey(next))
                        _keyValues.Add(next, OperandMaper(_keyValues[first], _keyValues[second], operation));

                    count++;                
                }
            }

            if (commands.Count == count)
            {
                allCalculated = true;
            }
        }

        var t = new BitArray(_keyValues.Where(o => o.Key.StartsWith("z")).OrderBy(o => o.Key).Select(o => o.Value).ToArray());
        var number = BitArrayToU64(t);
//var result = KeypadForInput(_keypad, "<A^A>^^AvvvA");
        //Console.WriteLine(sum);
        Console.ReadKey();
    }


    public static Boolean OperandMaper(bool first, bool second, string comm)
    {
        switch (comm)
        {
            case "XOR":
                return first ^ second;
            case "OR":
                // code block
                return first  | second; 
            case "AND":
                return first & second;
                
        }

        return false;
    }

    public static ulong BitArrayToU64(BitArray ba)
    {
        var len = Math.Min(64, ba.Count);
        ulong n = 0;
        for (int i = 0; i < len; i++)
        {
            if (ba.Get(i))
                n |= 1UL << i;
        }
        return n;
    }



}
