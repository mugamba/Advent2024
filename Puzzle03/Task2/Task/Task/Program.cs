
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var text = File.ReadAllText("input.txt");
string pat = @"(mul\(\d+,\d+\))";
RegexOptions options = RegexOptions.Multiline;
var dict = new Dictionary<int, long>();
foreach (Match m in Regex.Matches(text, pat, options))
{
    var newString =m.Value.Replace("mul(", "");
    newString = newString.Replace(")", "");
    var splits = newString.Split(",");
    dict.Add(m.Index, long.Parse(splits[0]) * long.Parse(splits[1]));
}

pat = @"(don't\(\))|(do\(\))";
var matches = Regex.Matches(text, pat, options);
var dontRanges = new List<Tuple<int, int>>();
Boolean disabled = false;
int disabledIndex = 0;
int enabledIndex = 0;

foreach (Match m in matches)
{
    if (disabled == false && m.Value == "don't()")
    {
        disabledIndex = m.Index;
        disabled = true;
    }
    if (disabled == true && m.Value == "do()")
    {
        enabledIndex = m.Index;
        disabled = false;

        dontRanges.Add(new Tuple<int, int>(disabledIndex, enabledIndex));
    }
}

/*If it stayed disabled till the end of file*/
if (disabled == true)
    dontRanges.Add(new Tuple<int, int>(disabledIndex, int.MaxValue));

/*Uzmi iz dictionarya sve one koji nisu disejblani*/
var filteredDict = dict.Where(x =>
{
    var test = dontRanges.Any(o => o.Item1 <= x.Key && o.Item2 >= x.Key);
    return !test;
}).ToDictionary();

Console.WriteLine(filteredDict.Select(o=>o.Value).Sum());
Console.ReadKey();
