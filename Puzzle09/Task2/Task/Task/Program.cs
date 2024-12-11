
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
 
    static Dictionary<int, Tuple<int, int, bool>> _nonEmptyPos = new Dictionary<int, Tuple<int, int, bool>>();
   
    static void Main(string[] args)
    {
        var array = File.ReadAllLines("input.txt").FirstOrDefault().ToCharArray();
        var indexCount = 0; 
        for (int i = 0; i < array.Length; i++)
        {
            if (i % 2 == 0)
            {
                var key = i / 2;
                var newlength = indexCount + int.Parse(array[i].ToString());
               _nonEmptyPos.Add(indexCount, Tuple.Create(key, int.Parse(array[i].ToString()), false));
                indexCount = newlength;

            }
            else
            {
                indexCount = indexCount + int.Parse(array[i].ToString());

            }
        }



        var anyToMove = true;
        while (anyToMove)
        {
            var toMove = _nonEmptyPos.Where(o => o.Value.Item3 == false).Select(o=>o.Key);
            var test = toMove.Count();
            anyToMove = toMove.Count() > 0;

            if (anyToMove)
            {

                var maxKey = toMove.Max(o => o);
                var element = _nonEmptyPos[maxKey];
                var dict = _nonEmptyPos.OrderBy(o => o.Key).Select(o=>o.Key).ToArray();
                var moved = false;
                for (int i = 0; i < dict.Length; i++)
                {
                    var gapsize = element.Item2;
                    var tuple1 = _nonEmptyPos[dict[i]];
                    if (maxKey <= dict[i])
                        continue;

                    var tuple2 = _nonEmptyPos[dict[i+1]];
                    if (gapsize <= dict[i + 1] - (dict[i] + tuple1.Item2))
                    {
                        var removed = _nonEmptyPos.Remove(maxKey);
                        _nonEmptyPos.Add(dict[i] + tuple1.Item2,
                            Tuple.Create(element.Item1, element.Item2, true));
                        moved = true;
                        break;
                    }
                }

                if (!moved)
                    _nonEmptyPos[maxKey] = Tuple.Create(element.Item1, element.Item2, true);
            }
        }
        

        long sum = 0;
        foreach (var dict in _nonEmptyPos)
        {
            for (var i = 0;i<dict.Value.Item2;i++)
                sum = sum + (dict.Key + i) * dict.Value.Item1;

        }


        Console.ReadKey();
    }
}
