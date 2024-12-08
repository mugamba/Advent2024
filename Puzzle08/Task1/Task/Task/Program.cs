
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    static char[,] _resultArray;
    static int _x;
    static int _y;
    static Dictionary<char, List<Point>> _antenas = new Dictionary<char, List<Point>>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.Length;
        _y = lines[0].Length;

        _resultArray = new char[_x, _y];

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                var sign = lines[i].ToCharArray()[j];
                if (sign != '.')
                {
                    if (!_antenas.ContainsKey(sign))
                        _antenas.Add(sign, new List<Point>() { new Point(i, j) });
                    else
                        _antenas[sign].Add(new Point(i, j));
                }
                _resultArray[i, j] = '.';
            }

        MarkAllPoints();
        var counter = 0;
        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                var sign = _resultArray[i, j];
                if (sign == 'X')
                    counter++;

            }

        Console.WriteLine(counter);
        Console.ReadKey();
    }


    public static void MarkAllPoints()
    {
        
        foreach (var list in _antenas.Values)
        {

            foreach (var element in list)
            { 
                var index = list.IndexOf(element);
                for (int i=0; i<list.Count; i++) 
                {
                    if (i != index)
                        MarkPoint(list[index], list[i]);
                }
            }
                
        }
    
    }

    private static void MarkPoint(Point point1, Point point2)
    {
        var ofssetXPoint1  = point1.X - point2.X;
        var ofssetYPoint1 = point1.Y - point2.Y;

        var x1Mark = point1.X + ofssetXPoint1;
        var y1Mark = point1.Y + ofssetYPoint1;

        if (x1Mark >= 0 && x1Mark < _x && y1Mark >= 0 && y1Mark < _y)
            _resultArray[x1Mark, y1Mark] = 'X';


    }

    public static void printmap(int x, int y)
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Console.Write(_resultArray[i, j]);
            }
            Console.WriteLine();
        }
    }

}
