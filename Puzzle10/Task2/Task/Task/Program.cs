
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
    public static Dictionary<Tuple<int, int, char>, int> _visitedList = new Dictionary<Tuple<int, int, char>, int>();
    public static List<Tuple<int, int>> _listOfPoints = new List<Tuple<int, int>>();
    public static Point _start;
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.Length;
        _y = lines[0].Length;

        _matrix = new char[_x, _y];

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {  
                _matrix[i, j] = lines[i].ToCharArray()[j];
                if (_matrix[i, j] == '^')
                    _start = new Point(i, j);
            }



        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {

                if (_matrix[i, j] == '#')
                    continue;

                _matrix[i, j] = '#';

                _matrix[_start.X, _start.Y] = '^';

                var result = TravereseMatrix(_start.X, _start.Y, Tuple.Create(i, j));
                if (result)
                    _listOfPoints.Add(Tuple.Create(i, j));

                _matrix[i, j] = '.';


            }

       


        var test = _listOfPoints.Distinct().Count();
    }

    private static Boolean TravereseMatrix(int i, int j, Tuple<int, int> testPoint)
    {
        _visitedList.Clear();
        while (!(i < 0 || j < 0) && !(i > _x - 1 || j > _y - 1))
        {

            var tuple = Tuple.Create(i, j, _matrix[i, j]);

            if (_visitedList.ContainsKey(tuple))
            {
                return true;
            }
            else
                _visitedList.Add(tuple, 0);
            
            if (_matrix[i, j] == '^')
            {
                if (!IsBlocked(i - 1, j))
                {
                    _matrix[i, j] = '.';
                    i = i - 1;
                    if (i >= 0)
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
                if (!IsBlocked(i, j+1))
                {
                    _matrix[i, j] = '.';
                    j = j + 1;
                    if (j <=(_y -1))
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
                if (!IsBlocked(i+1, j))
                {
                    _matrix[i, j] = '.';
                    i = i + 1;
                    if (i <= (_x - 1))
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
                if (!IsBlocked(i, j-1))
                {
                    _matrix[i, j] = '.';
                    j = j - 1;
                    if (j >= 0)
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
        return false;
    }


    

    private static Boolean IsBlocked(int i, int j)
    {
        if (i < 0 || j < 0)
            return false;
        if (i > _x - 1 || j > _y-1)
            return false;

        return _matrix[i, j]=='#';

    }

    public static void Rotate90(int i, int j)
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

    public static void printmap(int x, int y)
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Console.Write(_matrix[i, j]);
            }
            Console.WriteLine();
        }



    }






}
