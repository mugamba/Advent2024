
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

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.Length;
        _y = lines[0].Length;

        _matrix = new char[_x, _y];

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
                _matrix[i, j] = lines[i].ToCharArray()[j];


        List<Point> pointsOfA = new List<Point>();
        /*Skupljamo sve točke gdje se nalazi A u dijagonalnim ispisima "SAM" */
        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                var pointTemp = CheckUpRight(i, j);
                if (pointTemp != null)
                    pointsOfA.Add(pointTemp.Value);

                pointTemp = CheckUpLeft(i, j);
                if (pointTemp != null)
                    pointsOfA.Add(pointTemp.Value);

                pointTemp = CheckDownRight(i, j);
                if (pointTemp != null)
                    pointsOfA.Add(pointTemp.Value);


                pointTemp = CheckDownLeft(i, j);
                if (pointTemp != null)
                    pointsOfA.Add(pointTemp.Value);

            }

        /*Gdje imamo da je ista A točka više od jednom to je X*/
        var groupedByA = pointsOfA.GroupBy(o => o)
            .Select(g => new { PointOfA = g.Key, Counter = g.Count() }).ToList();
        var result = groupedByA.Where(g => g.Counter > 1).Count();
        Console.WriteLine(result);
        Console.ReadKey();
    }

    private static Boolean IsInsideMatrix(int i, int j)
    {
        if (i < 0 || j < 0)
            return false;
        if (i > _x - 1 || j > _y - 1)
            return false;
        return true;
    }

  
   
    private static Point? CheckUpRight(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'S')
            return null;

        nextIndexX = i - 1; nextIndexY = j+1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return null;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return null;

        nextIndexX = i - 2; nextIndexY = j + 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return null;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return null;

        return new Point(i - 1, j + 1);
    
    }

    private static Point? CheckUpLeft(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'S')
            return null;

        nextIndexX = i - 1; nextIndexY = j - 1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return null;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return null;

        nextIndexX = i - 2; nextIndexY = j - 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return null;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return null;

        return new Point(i-1, j-1);
    }

    private static Point? CheckDownLeft(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'S')
            return null;

        nextIndexX = i + 1; nextIndexY = j - 1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return null;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return null;

        nextIndexX = i + 2; nextIndexY = j - 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return null;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return null;

        return new Point(i+1, j-1);

    }

    private static Point? CheckDownRight(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'S')
            return null;

        nextIndexX = i + 1; nextIndexY = j + 1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return null;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return null;

        nextIndexX = i + 2; nextIndexY = j + 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return null;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return null;

        return new Point(i+1, j+1);

    }





}
