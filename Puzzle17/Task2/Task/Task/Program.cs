
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;


internal class Program
{
    public static long RegisterA;
    public static long RegisterB;
    public static long RegisterC;
    public static List<long> _instructions = new List<long>();
    public static List<long> _output = new List<long>();
    public static List<long> _digitsReversed = new List<long>();
    public static List<long> _outputed = new List<long>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var lineWithoutRepatingAndDivA = "2,4,1,1,7,5,1,5,4,0,5,5";
        var originalLine = "2,4,1,1,7,5,1,5,4,0,5,5,0,3,3,0";
        var splitor = originalLine.Split(",");
      
        foreach (var line in splitor)
        {
            _digitsReversed.Add(int.Parse(line));

        }
        _digitsReversed.Reverse();

        var trimedLine = lineWithoutRepatingAndDivA;
        var splits = trimedLine.Split(",");

        foreach (var split in splits)
        {
            _instructions.Add(int.Parse(split));
        }

        DoRecursion(0, 0);
        Console.WriteLine();
        Console.ReadKey();
    }


    public static void DoRecursion(int digitPos, long previousA)
    {
        if (digitPos == 16)
        {
            Console.WriteLine(previousA);
            return;
        }
        
        for (long i = previousA * 8; i < previousA * 8 + 8; i++)
        {
            RegisterA = i;
            var inPoint = 0;
            _output.Clear();
            while (inPoint < _instructions.Count)
            {
                var instruction = _instructions[inPoint];
                inPoint++;
                var operand = _instructions[inPoint];
                inPoint++;
                if (instruction == 3)
                    if (RegisterA != 0)
                        inPoint = (int)operand;
                    else
                    {
                        continue;
                    }
                else
                {
                    GetOperation(instruction, operand);
                }
            }

            if (_output.First() == _digitsReversed[digitPos])
            {
                DoRecursion(digitPos + 1, i);
            }
        }
    }

    public static long ComboOperand(long i)
    {
        switch (i)
        { 
            case 0: return 0;
            case 1: return 1;
            case 2: return 2;
            case 3: return 3;
            case 4: return RegisterA;
            case 5: return RegisterB;
            case 6: return RegisterC;
        }

       throw new  Exception("Unknown operand :" + i.ToString());
    
    }

    public static void GetOperation(long instruction, long operand)
    {
        switch (instruction)
        {
            case 0:
                {
                    var number = ComboOperand(operand);
                    RegisterA = RegisterA /(long)(Math.Pow(2, number)); 
                    break;
                }
            case 1: {
                    RegisterB = RegisterB ^ operand;
                    break;
                };
            case 2:
                {
                    var number = ComboOperand(operand);
                    RegisterB = number % 8;
                    break;
                }
            case 4:
                {
                    RegisterB = RegisterB ^ RegisterC;
                    break;
                };
            case 5:
                {
                    var number = ComboOperand(operand);
                    _output.Add(number % 8);
                    break;
                };
            case 6:
                {
                    var number = ComboOperand(operand);
                    RegisterB = RegisterA / (long)(Math.Pow(2, number));
                    break;
                }
            case 7:
                {
                    var number = ComboOperand(operand);
                    RegisterC = RegisterA / (long)(Math.Pow(2, number));
                    break;
                }

        }

     

    }

}
