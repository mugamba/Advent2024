
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    static int[,] _matrix;
    static int _x;
    static int _y;
    static Dictionary<Point, List<Point>> _trailHeads = new Dictionary<Point, List<Point>>();
    

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.Length;
        _y = lines[0].Length;

        _matrix = new int[_x, _y];

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                var sign = lines[i].ToCharArray()[j];
                _matrix[i, j] = int.Parse(sign.ToString());

            }

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                if (_matrix[i, j] == 0)
                    TraverseTrail(new Point(i, j), 0, i, j);

            }

        var sum = 0;
        foreach (var value in _trailHeads.Values)
        {
            sum = sum + value.Distinct().Count();
        
        }
        Console.WriteLine(sum);
        Console.ReadKey();
    }


    public static void TraverseTrail(Point startPoint,  int n, int i, int j)
    {
        if (n == 9)
        {
            if (!_trailHeads.ContainsKey(startPoint))
                _trailHeads.Add(startPoint, new List<Point>() { new Point(i, j) });
            else
                _trailHeads[startPoint].Add(new Point(i, j));

            return;
        }

        if (i - 1 >= 0 && _matrix[i - 1, j] == n + 1)
            TraverseTrail(startPoint, _matrix[i - 1, j], i - 1, j);

        if (i + 1 < _x && _matrix[i + 1, j] == n + 1)
            TraverseTrail(startPoint, _matrix[i + 1, j], i + 1, j);

        if (j - 1 >= 0 && _matrix[i, j-1] == n + 1)
            TraverseTrail(startPoint, _matrix[i, j-1], i, j-1);

        if (j + 1 < _y && _matrix[i, j + 1] == n + 1)
            TraverseTrail(startPoint, _matrix[i, j + 1], i, j + 1);

    }

}
