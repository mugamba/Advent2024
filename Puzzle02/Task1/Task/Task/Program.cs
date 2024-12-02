
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
for(int j=0; j<input.Count; j++)
{
    diffs.Add(new List<int>());

    for (var i = 1; i < input[j].Count; i++)
    {
        diffs[j].Add(input[j][i] - input[j][i - 1]);
    } 
    
}

var result = diffs.Where(o => (o.All(g => g > 0) || o.All(g => g < 0)) && o.All(g => Math.Abs(g) <= 3))
    .ToList().Count();

/*Suma svih elemenata liste*/
Console.WriteLine(result);
Console.ReadKey();
