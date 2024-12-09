
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

        var anyMoved = true;
        while (_nonEmptyPos.Where(o => o.Value.Item3 == false).Count() > 0 && anyMoved)
        {
            var maxKey = _nonEmptyPos.Where(o => o.Value.Item3 == false).Max(o => o.Key);
            var element = _nonEmptyPos[maxKey];

            anyMoved = false;
            
                var dict = _nonEmptyPos.OrderBy(o => o.Key).ToDictionary();
                for (int i=0;i<dict.Count-1;i++)
                {
                    var gapsize = element.Item2;
                    var tuple1 = dict.ElementAt(i);
                    if (maxKey <= tuple1.Key)
                        continue;
                    
                    var tuple2 = dict.ElementAt(i+1);
                    if (gapsize <= tuple2.Key - (tuple1.Key + tuple1.Value.Item2))
                    {
                       var removed =  _nonEmptyPos.Remove(maxKey);
                        _nonEmptyPos.Add(tuple1.Key + tuple1.Value.Item2,
                            Tuple.Create(element.Item1, element.Item2, true));
                        anyMoved = true;
                        break;
                    }
                }

                element = Tuple.Create(element.Item1, element.Item2, true);
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
