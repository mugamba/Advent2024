
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
    static Point _startPosition;
    static List<Point> _path = new List<Point>();
  
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
                if (sign == '^')
                { 
                    _startPosition = new Point(i, j);
                }

            }

        AddPoints(_startPosition.X, _startPosition.Y);
        /*Suma svih elemenata liste*/
        var tt =  _path.Distinct().Count();
        Console.ReadKey();
    }

    private static void AddPoints(int i, int j)
    {
        if (i < 0 || j < 0)
            return;
        if (i > _x - 1 || j > _y - 1)
            return;

        while (!(i < 0 || j < 0) && !(i > _x - 1 || j > _y - 1))
        {
            _path.Add(new Point(i, j));

            if (_matrix[i, j] == '^')
            {
                if (!IsObsticle(i - 1, j))
                {
                    _matrix[i, j] = '.';
                     i = i - 1;
                    if (i >= 0 && i < _x)
                        _matrix[i, j] = '^';
                    continue;
                }
                else
                {
                    Rotate90(i, j);
                    continue;
                }
            }
            if (_matrix[i, j] == '>')
            {
                if (!IsObsticle(i, j + 1))
                {
                    _matrix[i, j] = '.';
                    j = j + 1;
                    if (j >= 0 && j < _y)
                        _matrix[i, j] = '>';
                    continue;
                }
                else
                {
                    Rotate90(i, j);
                    continue;
                }
            }
            if (_matrix[i, j] == 'v')
            {
                if (!IsObsticle(i + 1, j))
                {
                    _matrix[i, j] = '.';
                    i = i + 1;
                    if (i>=0 && i<_x)
                        _matrix[i, j] = 'v';
                    continue;
                }
                else
                {
                    Rotate90(i, j);
                    continue;
                }
            }
            if (_matrix[i, j] == '<')
            {
                if (!IsObsticle(i, j - 1))
                {
                    _matrix[i, j] = '.';
                    j = j - 1;
                    if (j >= 0 && j < _y)
                        _matrix[i, j] = '<';
          
                    continue;
                }
                else
                {
                    Rotate90(i, j);
                    continue;
                }
            }
        }

    }

    private static Boolean IsObsticle(int i, int j)
    {
        if (i < 0 || j < 0)
            return false;
        if (i > _x - 1 || j > _y - 1)
            return false;

        return _matrix[i, j] == '#';
    }

    private static void Rotate90(int i, int j)
    {
        if (_matrix[i, j] == '^')
        {
            _matrix[i, j] = '>';
            return;
        }
        if (_matrix[i, j] == '>')
        {
            _matrix[i, j] = 'v';
            return;
        }
        if (_matrix[i, j] == 'v')
        {
            _matrix[i, j] = '<';
            return;
        }
        if (_matrix[i, j] == '<')
        {
            _matrix[i, j] = '^';
            return;
        }

        
    }
    





}
