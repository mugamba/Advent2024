
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;


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
        foreach (var region in _regions)
        {
            sum += region.Value.Count() * NumberOfSides(region.Value);
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


        if (i - 1 >= 0 && _matrix[i - 1, j] == key.Item1)
            CrawlRegion(key, i - 1, j);

        if (i + 1 < _x && _matrix[i + 1, j] == key.Item1)
            CrawlRegion(key, i + 1, j);

        if (j - 1 >= 0 && _matrix[i, j - 1] == key.Item1)
            CrawlRegion(key, i, j - 1);

        if (j + 1 < _y && _matrix[i, j + 1] == key.Item1)
            CrawlRegion(key, i, j + 1);

    }


    public static int NumberOfSides(List<Point> regions)
    {
        var minX = regions.Select(o => o.X).Min();
        var maxX = regions.Select(o => o.X).Max();
        var minY = regions.Select(o => o.Y).Min();
        var maxY = regions.Select(o => o.Y).Max();

        var verticalComingIn = new List<Point>();
        var verticalComingOut = new List<Point>();
        var horizontalcomingIn = new List<Point>();
        var horizontalgoingout = new List<Point>();

        for (var i = minX - 1; i <= maxX + 1; i++)
        {
            var isInside = false;
            for (var j = minY - 1; j <= maxY + 1; j++)
            {
                if (regions.Contains(new Point(i, j)) && !isInside)
                {
                    isInside = true;
                    horizontalcomingIn.Add(new Point(i, j));
                }

                if (!regions.Contains(new Point(i, j)) && isInside)
                {
                    isInside = false;
                    horizontalgoingout.Add(new Point(i, j));
                }
            }
        }



        for (var j = minY - 1; j <= maxY + 1; j++)
        {
            var isInside = false;
            for (var i = minX - 1; i <= maxX + 1; i++)
            {
                if (regions.Contains(new Point(i, j)) && !isInside)
                {
                    isInside = true;
                    verticalComingIn.Add(new Point(i, j));
                }

                if (!regions.Contains(new Point(i, j)) && isInside)
                {
                    isInside = false;
                    verticalComingOut.Add(new Point(i, j));
                }
            }

        }

        var distinctYin = horizontalcomingIn.Select(o => o.Y).Distinct();
        var distinctYout = horizontalgoingout.Select(o => o.Y).Distinct();

        int counter1 = 0;
        foreach (var y in distinctYin)
        {
            var list = horizontalcomingIn.Where(o => o.Y == y).Select(o => o.X);
            var previous = list.First();
            counter1++;
            foreach (var x in list)
            {
                if (x == previous)
                    continue;
                
                if (previous + 1 != x)
                    counter1++;

                previous = x;
            }

        }

        foreach (var y in distinctYout)
        {
            var list = horizontalgoingout.Where(o => o.Y == y).Select(o => o.X);
            var previous = list.First();
            counter1++;
            foreach (var x in list)
            {
                if (x == previous)
                    continue;

                if (previous + 1 != x)
                    counter1++;

                previous = x;
            }

        }

        var distinctXin = verticalComingIn.Select(o => o.X).Distinct();
        var distinctXout = verticalComingOut.Select(o => o.X).Distinct();

        foreach (var x in distinctXin)
        {
            var list = verticalComingIn.Where(o => o.X == x).Select(o => o.Y);
            var previous = list.First();
            counter1++;
            foreach (var y in list)
            {
                if (y == previous)
                    continue;

                if (previous + 1 != y)
                    counter1++;

                previous = y;
            }

        }

        foreach (var x in distinctXout)
        {
            var list = verticalComingOut.Where(o => o.X == x).Select(o => o.Y);
            var previous = list.First();
            counter1++;
            foreach (var y in list)
            {
                if (y == previous)
                    continue;

                if (previous + 1 != y)
                    counter1++;

                previous = y;
            }

        }

        return counter1;
        
    }

}
