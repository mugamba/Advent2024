
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var input = new List<List<int>>();
var diffs = new List<List<int>>();

foreach (var line in lines)
{
    var splits = line.Split(" ");
    var tempList = new List<int>();
    foreach (var split in splits)
    { 
        tempList.Add(int.Parse(split));
    }
    input.Add(tempList);
}
var counter = 0;
foreach (var list in input)
{
    Boolean? rising = null; Boolean isSafe = false;  
    for (var i = 1; i < list.Count; i++)
    {
        var diff = list[i] - list[i - 1];
        if (rising == null)
        {
            if (diff < 0)
                rising = false;
            else
                rising = true;
        }

        if (rising)
    } 
    

    


}

var result = diffs.Where(o => (o.All(g => g > 0) || o.All(g => g < 0)) && o.All(g => Math.Abs(g) <= 3))
    .ToList().Count();

/*Suma svih elemenata liste*/
Console.WriteLine(result);
Console.ReadKey();
