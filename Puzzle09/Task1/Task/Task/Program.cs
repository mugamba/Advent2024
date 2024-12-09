
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    static Queue<int> _gaps = new Queue<int>();
    static Dictionary<int, int> _nonEmptyPos = new Dictionary<int, int>();
   
    static void Main(string[] args)
    {
        var array = File.ReadAllText("input.txt").ToCharArray();
        var indexCount = 0; 
        for (int i = 0; i < array.Length; i++)
        {
            if (i % 2 == 0)
            {
                var key = i / 2;

                var newlength = indexCount + int.Parse(array[i].ToString());
                for (int k = indexCount; k < newlength; k++)
                    _nonEmptyPos.Add(k, key);

                indexCount = newlength;

            }
            else
            { 
                indexCount = indexCount + int.Parse(array[i].ToString());

            }
        }

        while (_nonEmptyPos.Max(o => o.Key) > _nonEmptyPos.Count() - 1)
        {
            var maxKey = _nonEmptyPos.Max(o => o.Key);

            for (int i = 0; i < maxKey; i++)
            {
                if (!_nonEmptyPos.ContainsKey(i))
                {
                    var value = _nonEmptyPos[maxKey];
                    _nonEmptyPos.Remove(maxKey); ;
                    _nonEmptyPos.Add(i, value);
                    break;
                }
            }
          
        }

        long sum = 0;
        foreach (var dict in _nonEmptyPos)
        {
            sum = sum + dict.Key * dict.Value;
            
        }

        
        Console.ReadKey();
    }
}
