
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
    public static Dictionary<int, long> _keyValuePairs = new Dictionary<int, long>();
  
    public static List<WindowPrice> _windoprices = new List<WindowPrice>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            _keyValuePairs.Add(int.Parse(line), long.Parse(line));
        }
        //_keyValuePairs.Add(123, 123L);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        // the code that you want to measure comes here
       
        foreach (var key in _keyValuePairs.Keys)
        {
            var t = new List<int>();
            var dict = new Dictionary<string, int>();
            int i = 0;
            while (i < 2000)
            {
                t.Add((int)(_keyValuePairs[key] % 10));
                var result = GetOperation(_keyValuePairs[key]);
                _keyValuePairs[key] = result;
                i++;
            }

            for (i=4;i< t.Count;i++) 
            {
                var sign = String.Format("{0},{1},{2},{3}",(t[i - 3]  - t[i-4]).ToString(),
                   (t[i - 2] - t[i - 3]).ToString(), 
                   (t[i - 1] - t[i - 2]).ToString(),
                   (t[i] - t[i-1]).ToString());

                if (!dict.ContainsKey(sign))
                    dict.Add(sign, t[i]);
            }

           
            _windoprices.AddRange(dict.Select(o => new WindowPrice() { _window = o.Key, _price = o.Value }));


        }


       var resulttt =   _windoprices.GroupBy(o => o._window).Select(g => new { Key = g.Key, Price = g.Sum(o => o._price) }).OrderByDescending(o => o.Price).FirstOrDefault();

        Console.WriteLine(_keyValuePairs.Sum(o=>o.Value));
        Console.ReadKey();
    }


    public static long GetOperation(long secretKey)
    {
        secretKey = ((secretKey * 64) ^ secretKey) % 16777216;
        secretKey = ((secretKey / 32) ^ secretKey) % 16777216;
        secretKey = ((secretKey * 2048) ^ secretKey) % 16777216;
        return secretKey;
    }

    public class WindowPrice
    {
        public String _window;
        public int _price;
    }

}
