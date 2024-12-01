using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


var lines = File.ReadAllLines("input.txt");
var firstList = new List<int>();
var secondList = new List<int>();
var simmList = new List<long>();

foreach (var line in lines)
{
    var splits = line.Split("   ");
    firstList.Add(int.Parse(splits[0].Trim()));
    secondList.Add(int.Parse(splits[1].Trim()));
}

for (int i = 0; i < firstList.Count; i++)
{
    /*trenutni element*/
    var selectedNumber = firstList[i];
    /*filtriramo drugu listu sa odabranim elementom, 
     * dobijemo broj ponavljanja u drugoj listi i množimo sa odabranim elem.*/
    simmList.Add(secondList.Where(o=>o == firstList[i]).Count() * selectedNumber);
}

/*Suma dvih iz similarity liste*/
Console.WriteLine(simmList.Sum());
Console.ReadKey();
