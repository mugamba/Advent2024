
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    static char[,] _matrix;
    static int _x;
    static int _y;
    static Dictionary<Tuple<char, Point>, List<Point>> _regions = new Dictionary<Tuple<char, Point>, List<Point>>();
    

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.Length;
        _y = lines[0].Length;

        _matrix = new char[_x, _y];

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                var sign = lines[i].ToCharArray()[j];
                _matrix[i, j] = sign;

            }

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                if (_regions.Where(o => o.Key.Item1 == _matrix[i, j]).Count() != 0
                    && _regions.Where(o => o.Key.Item1 == _matrix[i, j]).Any(o => o.Value.Any(o => o == new Point(i, j))))
                    continue;
                else
                    CrawlRegion(Tuple.Create(_matrix[i, j], new Point(i, j)), i, j);

            }

        var sum = 0;
        foreach(var region in _regions) 
        {

           sum += region.Value.Count() * region.Value.Select(o => 4 - Neighbours(o, region.Value)).Sum();
        
        }


        Console.WriteLine(sum);
        Console.ReadKey();
    }


    public static void CrawlRegion(Tuple<char, Point> key, int i, int j)
    {
        if (key.Item1 != _matrix[i, j])
            return;

        if (!_regions.ContainsKey(key))
            _regions.Add(key, new List<Point> { new Point(i, j) });
        else
            if (!_regions[key].Contains(new Point(i, j)))
            _regions[key].Add(new Point(i, j));
        else
            return;
                    

        if (i - 1 >= 0 && _matrix[i - 1, j] == key.Item1 )
            CrawlRegion(key, i - 1, j);

        if (i + 1 < _x && _matrix[i + 1, j] == key.Item1)
            CrawlRegion(key, i + 1, j);

        if (j - 1 >= 0 && _matrix[i, j-1] == key.Item1)
            CrawlRegion(key, i, j-1);

        if (j + 1 < _y && _matrix[i, j + 1] == key.Item1)
            CrawlRegion(key, i, j + 1);

    }


    public static int Neighbours(Point p, List<Point> regions)
    {
        var count = 0;
        if (regions.Contains(new Point(p.X + 1, p.Y)))
            count++;
        if (regions.Contains(new Point(p.X - 1, p.Y)))
                count++;
        if (regions.Contains(new Point(p.X, p.Y+1)))
            count++;
        if (regions.Contains(new Point(p.X, p.Y-1)))
            count++;

        return count;

    }

}
