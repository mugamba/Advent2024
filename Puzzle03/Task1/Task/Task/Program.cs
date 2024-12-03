
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var text = File.ReadAllText("input.txt");
string pat = @"(mul\(\d+,\d+\))";
RegexOptions options = RegexOptions.Multiline;
var total = 0;
foreach (Match m in Regex.Matches(text, pat, options))
{
    var newString =m.Value.Replace("mul(", "");
    newString = newString.Replace(")", "");
    var splits = newString.Split(",");
    total+= int.Parse(splits[0]) * int.Parse(splits[1]);

}



/*Suma svih elemenata liste*/
Console.WriteLine(total);
Console.ReadKey();
