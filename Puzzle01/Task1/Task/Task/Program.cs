
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var firstList = new List<int>();
var secondList = new List<int>();
var diffList = new List<long>();

foreach (var line in lines)
{
    var splits = line.Split("   ");
    firstList.Add(int.Parse(splits[0].Trim()));
    secondList.Add(int.Parse(splits[1].Trim()));
}

firstList.Sort();
secondList.Sort();

for (int i = 0; i<firstList.Count; i++)
{
    /*Razlika se dodaje u apsolutnoj vrijednosti jer može bit i + i -, 
     * ali distance je uvijek pozitivan*/
    diffList.Add(Math.Abs(secondList[i] - firstList[i]));
}

/*Suma svih elemenata liste*/
Console.WriteLine(diffList.Sum());
Console.ReadKey();
