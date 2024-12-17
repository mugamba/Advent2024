
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;


internal class Program
{
    public static int RegisterA;
    public static int RegisterB;
    public static int RegisterC;
    public static List<int> _instructions = new List<int>();
    public static List<int> _output = new List<int>();


    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        
        foreach (var line in lines) {

            if (string.IsNullOrEmpty(line)) continue;

            if (line.Contains("Register A:"))
                RegisterA = int.Parse(line.Replace("Register A:", "").Trim());
            if (line.Contains("Register B:"))
                RegisterB = int.Parse(line.Replace("Register B:", "").Trim());
            if (line.Contains("Register C:"))
                RegisterC = int.Parse(line.Replace("Register C:", "").Trim());

            if (line.Contains("Program:"))
            {
               var trimedLine = line.Replace("Program:", "").Trim();
               var splits = trimedLine.Split(",");

                foreach(var split in  splits)
                {
                    _instructions.Add(int.Parse(split));
                }

                
            }
        }

        var inPoint = 0;

        while (inPoint < _instructions.Count)
        {
            var instruction = _instructions[inPoint];
            inPoint++;
            var operand = _instructions[inPoint];
            inPoint++;
            if (instruction == 3)
                if (RegisterA != 0)
                    inPoint = operand;
                else
                {
                    continue;
                }
            else
            {
                GetOperation(instruction, operand);
            }


        }



        Console.WriteLine (String.Join(",", _output));
        Console.ReadKey();
    }


    public static Int32 ComboOperand(int i)
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

    public static void GetOperation(int instruction, int operand)
    {
        switch (instruction)
        {
            case 0:
                {
                    var number = ComboOperand(operand);
                    RegisterA = RegisterA /(int)(Math.Pow(2, number)); 
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
                    RegisterB = RegisterA / (int)(Math.Pow(2, number));
                    break;
                }
            case 7:
                {
                    var number = ComboOperand(operand);
                    RegisterC = RegisterA / (int)(Math.Pow(2, number));
                    break;
                }

        }

     

    }

}
