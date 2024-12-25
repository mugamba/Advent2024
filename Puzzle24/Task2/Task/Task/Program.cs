
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
    public static Dictionary<string, string> _keyValues = new Dictionary<string, string>();
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
               // _keyValues.Add(splits[0].Trim(), (splits[1].Trim() == "1" ? true : false));
            }

            if (line.Contains("->"))
            {
                var splits = line.Split("->");

                _keyValues.Add(splits[0].Trim(), splits[1].Trim());
                commands.Add(line);
            }
        }

        var midTransfer1 = ""; var midTransfer2 = ""; var transfer = ""; var midznamenka = ""; var znamenka = "";
        for (var i = 0; i < 39; i++)
        {
            var ipad = (i).ToString().PadLeft(2, '0');
            var xFilter = "x" + ipad;
            var zFilter = "z" + ipad;
            var filteredList = _keyValues.Where(o => o.Key.Contains(xFilter));
            var newFilter = filteredList.Select(o => o.Value).ToList();
            var x02 =  _keyValues.Where(o => newFilter.Any(g=>o.Key.Contains(g)));
            var zLine = _keyValues.Where(o => o.Value.Contains(zFilter)).FirstOrDefault();

            if (i != 0)
            {
                filteredList = filteredList.Union(x02);
            }
            Console.WriteLine();
            var newTransfer = ""; midznamenka = "";
            foreach (var t in filteredList)
            {
                if (t.Key.Contains("x00"))
                {
                    if (t.Key.Contains("AND"))
                    {
                        newTransfer = t.Value;
                        Console.WriteLine("z00 transfer ->{0}", _keyValues.Where(o => o.Value == t.Value).Select(o => o.Key).FirstOrDefault());
                    }
                    if (t.Key.Contains("XOR"))
                    {
                        znamenka = t.Value;
                        Console.WriteLine("z00 znamenka ->{0}", _keyValues.Where(o => o.Value == t.Value).Select(o => o.Key).FirstOrDefault());
                    }
                }
                else
                {
                    if (t.Key.Contains("x" + ipad) && t.Key.Contains("XOR"))
                    {
                        midznamenka = t.Value;
                        Console.WriteLine("z" +ipad + " midznamenka ->{0}   {1}",t.Value, _keyValues.Where(o => o.Value == t.Value).Select(o => o.Key).FirstOrDefault());
                    }

                    if (zLine.Key.Contains("XOR") && midznamenka != "")
                    {
                        if (!(zLine.Key.Contains(midznamenka) && zLine.Key.Contains(transfer)))
                        {
                            Console.WriteLine("------------");
                            Console.WriteLine("Erorr -> {0}  {1}", zLine.Key, zLine.Value);
                            Console.WriteLine("------------");
                        }

                    }
                    if (!zLine.Key.Contains("XOR"))
                    {

                        Console.WriteLine("------------");
                        Console.WriteLine("Erorr -> {0}  {1}", zLine.Key, zLine.Value);
                        Console.WriteLine("------------");
                    }
                   

                    if (t.Key.Contains("x" + ipad) && t.Key.Contains("AND"))
                    {
                        midTransfer1 = t.Value;
                        Console.WriteLine("z" + ipad + " midTransfer ->{0} {1}",t.Value,  _keyValues.Where(o => o.Value == t.Value).Select(o => o.Key).FirstOrDefault());
                    }

                    if (!t.Key.Contains("x" + ipad) && t.Key.Contains("XOR"))
                    {
                        znamenka = t.Key;
                        Console.WriteLine("z" + ipad + " znamenka ->{0}  {1}",t.Key, OperandMaper(t.Key));
                    }
                    if (!t.Key.Contains("x" + ipad) && t.Key.Contains("AND"))
                    {
                        midTransfer2 = t.Value;
                        Console.WriteLine("z" + ipad + " midtransfer2 ->{0} {1}",t.Key, OperandMaper(t.Key));
                    }
                    if (!t.Key.Contains("x" + ipad) && t.Key.Contains("OR") && !t.Key.Contains("XOR"))
                    {
                        newTransfer = t.Value;
                        Console.WriteLine("z" + ipad + " transfer ->{0} {1}",t.Key,  OperandMaper(t.Key));
                    }
                    if (t.Value.Contains("z"))
                        Console.WriteLine("z" + ipad + " znamenka ->{0} {1}",t.Key, OperandMaper(t.Key));

                }
            }
            transfer = newTransfer;
        }

        var list = new List<string>();
        list.AddRange(new []{ "z06", "jmq", "z13", "gmh", "rqf", "cbd", "z38", "qrh"  });


       
        Console.WriteLine( String.Join(",", list.Order()));
    

        Console.ReadKey();
    }


    public static String OperandMaper(string command)
    {
        if (command.Contains("x"))
            return command;

        var splits = command.Split(" ");
        var first = _keyValues.Where(o => o.Value == splits[0].Trim()).FirstOrDefault().Key;
        if (!first.Contains("x"))
            first = OperandMaper(first);

        var second =  _keyValues.Where(o => o.Value == splits[2].Trim()).FirstOrDefault().Key;
        if (!second.Contains("x"))
            second = OperandMaper(second);
        
        var operation = splits[1].Trim();

        return String.Format("({0}) {1} ({2})", first, operation, second);

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
