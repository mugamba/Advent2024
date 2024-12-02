
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
    Boolean? rising = null; int breakingIndex = -1;  
    for (var i = 1; i < list.Count; i++)
    {
        var diff = list[i] - list[i - 1];
        if (diff == 0)
        {
            breakingIndex = i;
            break;
        }
        if (rising.HasValue && rising.Value == true && diff < 0)
        {
            breakingIndex = i;
            break;
        }
        if (rising.HasValue && rising.Value == false && diff > 0)
        {
            breakingIndex = i;
            break;
        }
        if (Math.Abs(diff) > 3)
        {
            breakingIndex = i;
            break;
        }

        if (rising == null)
        {
            if (diff < 0)
                rising = false;

            if (diff > 0)
                rising = true;

        }

    }

    if (breakingIndex == -1)
    { 
        counter++;
        continue;
    }

    var count = list.Count;
    for (var j = 0; j < count; j++)
    {
        var listCopy = list.ToList();
        listCopy.RemoveAt(j);
        rising = null; breakingIndex = -1;
        for (var i = 1; i < listCopy.Count; i++)
        {
            var diff = listCopy[i] - listCopy[i - 1];
            if (diff == 0)
            {
                breakingIndex = i;
                break;
            }
            if (rising.HasValue && rising.Value == true && diff < 0)
            {
                breakingIndex = i;
                break;
            }
            if (rising.HasValue && rising.Value == false && diff > 0)
            {
                breakingIndex = i;
                break;
            }
            if (Math.Abs(diff) > 3)
            {
                breakingIndex = i;
                break;
            }

            if (rising == null)
            {
                if (diff < 0)
                    rising = false;

                if (diff > 0)
                    rising = true;

            }

        }

        if (breakingIndex == -1)
        {
            counter++;
            break;
            
        }

    }

}


/*Suma svih elemenata liste*/
Console.WriteLine(counter);
Console.ReadKey();
