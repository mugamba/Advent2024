
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var text = File.ReadAllText("input.txt");
string pat = @"(mul\(\d+,\d+\))";
RegexOptions options = RegexOptions.Multiline;
var dict = new Dictionary<int, int>();
foreach (Match m in Regex.Matches(text, pat, options))
{
    var newString =m.Value.Replace("mul(", "");
    newString = newString.Replace(")", "");
    var splits = newString.Split(",");
    dict.Add(m.Index, int.Parse(splits[0]) * int.Parse(splits[1]));
}

pat = @"(don't\(\).*do\(\))";
var matches = Regex.Matches(text, pat, options);
var dontRanges = new List<Tuple<int, int>>();
foreach (Match m in matches)
{
    dontRanges.Add(new Tuple<int, int>(m.Index, m.Index + m.Length));
}

var filteredDict = dict.Where(x => !dontRanges.Any(o => o.Item1 < x.Key && o.Item2> x.Key)); 
Console.WriteLine(filteredDict.Select(o=>o.Value).Sum());
Console.ReadKey();
