
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
  
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            _keyValuePairs.Add(int.Parse(line), long.Parse(line));
        }
        int i = 0;
        //_keyValuePairs.Add(123, 123L);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        // the code that you want to measure comes here
        while (i < 2000)
        {
            foreach (var key in _keyValuePairs.Keys) {
                var result = GetOperation(_keyValuePairs[key]);
                _keyValuePairs[key] = result;
            }
            i++;
        }
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine(elapsedMs);
        Console.WriteLine(_keyValuePairs.Sum(o=>o.Value));
        Console.ReadKey();
    }



    public static long GetOperationBitwise(long secretKey)
    {
        secretKey = ((secretKey << 6) ^ secretKey) & 16777215;
        secretKey = ((secretKey >> 5) ^ secretKey) & 16777215;
        secretKey = ((secretKey << 11) ^ secretKey) & 16777215;
        return secretKey;
    }

    public static long GetOperation(long secretKey)
    {
        secretKey = ((secretKey * 64) ^ secretKey) % 16777216;
        secretKey = ((secretKey / 32) ^ secretKey) % 16777216;
        secretKey = ((secretKey * 2048) ^ secretKey) % 16777216;
        return secretKey;
    }

}
