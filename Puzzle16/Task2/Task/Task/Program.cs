
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;


internal class Program
{
    static char[,] _matrix;
    static int _x;
    static int _y;
    static Dictionary<Tuple<Point, char>, Tuple<Tuple<Point, char>, int>> _distances = new Dictionary<Tuple<Point, char>, Tuple<Tuple<Point, char>, int>>();
    static Dictionary<Tuple<Point, char>, Tuple<Tuple<Point, char>, int>> _alternatePath = new Dictionary<Tuple<Point, char>, Tuple<Tuple<Point, char>, int>>();
    static Dictionary<Tuple<Point, char>, Boolean> _visited = new Dictionary<Tuple<Point, char>, Boolean>();
    static Tuple<Point, char> _start;
    static Point _end;
    static Dictionary<Tuple<Point, char>, int> _shortestPath = new Dictionary<Tuple<Point, char>, int>();
    static int _maxPath;


    static void Main(string[] args)
    {
        var linesField = File.ReadAllLines("input.txt");
        _x = linesField.Length;
        _y = linesField[0].Length;

        _matrix = new char[_x, _y];
        var builder = new StringBuilder();



        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                var sign = linesField[i].ToCharArray()[j];
                _matrix[i, j] = sign;



                if (sign == 'S')
                {
                    _matrix[i, j] = '.';
                    _start = Tuple.Create(new Point(i, j), '>');
                }

                if (sign == 'E')
                {
                    _matrix[i, j] = '.';
                    _end = new Point(i, j);
                }
                if (sign != '#')
                {

                    for (int k = 0; k < 4; k++)
                    {
                        if (k == 0)
                        {
                            _distances.Add(Tuple.Create(new Point(i, j), '>'), Tuple.Create<Tuple<Point, char>, int>(null, int.MaxValue));
                        }
                        if (k == 1)
                        {
                            _distances.Add(Tuple.Create(new Point(i, j), '^'), Tuple.Create<Tuple<Point, char>, int>(null, int.MaxValue));
                        }
                        if (k == 2)
                        {
                            _distances.Add(Tuple.Create(new Point(i, j), 'v'), Tuple.Create<Tuple<Point, char>, int>(null, int.MaxValue));
                        }
                        if (k == 3)
                        {
                            _distances.Add(Tuple.Create(new Point(i, j), '<'), Tuple.Create<Tuple<Point, char>, int>(null, int.MaxValue));
                        }

                    }
                }

            }

        _distances[_start] = Tuple.Create<Tuple<Point, char>, int>(null, 0);

        var endNode = false;

        while (!endNode)
        {

            var toVisit = _distances.Where(o => !_visited.ContainsKey(o.Key)).OrderBy(o => o.Value.Item2).Take(1).FirstOrDefault();
            VisitNode(toVisit.Key);
            endNode = _distances.Where(o => o.Key.Item1 == _end).Any(o => o.Value.Item2 != int.MaxValue);

        }


        var result = _distances.Where(o => o.Key.Item1 == _end).OrderBy(o => o.Value.Item2).FirstOrDefault();

        var next = result.Key;
        _shortestPath.Add(next, _distances[next].Item2);
        /*retrace steps*/
        while (next != null)
        {
            if (_distances[next].Item2 == 0)
            {
                break;
            }

            var previous = _distances[next].Item1;
            _shortestPath.Add(previous, _distances[previous].Item2);
            next = previous;
        }

        var sameKeys = _shortestPath.Keys.Intersect(_alternatePath.Keys).ToList();
       

        foreach (var k in sameKeys)
        {
            var previous = _alternatePath[k].Item1;
            if (!_shortestPath.ContainsKey(previous))
            {
                var next11 = previous;
                /*retrace steps*/
                while (next11 != null)
                {
                    if (_shortestPath.ContainsKey(next11))
                    {
                        break;
                    }

                    previous = _distances[next11].Item1;
                    _shortestPath.Add(previous, _distances[previous].Item2);
                    next11 = previous;
                }

            }

        }


        var shortestPathPoints = _shortestPath.Select(o => o.Key.Item1).Distinct().ToList();
        Console.WriteLine(result.Value);
        Console.ReadKey();
    }


    public static void VisitNode(Tuple<Point, char> pos)
    {
        var currrentdistance = _distances[pos].Item2;

        if (pos.Item2 == '>')
        {

            if (_matrix[pos.Item1.X, pos.Item1.Y + 1] == '.')
            {
                if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y + 1), '>')].Item2 > currrentdistance + 1)
                    _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y + 1), '>')] = Tuple.Create(pos, currrentdistance + 1);
                if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y + 1), '>')].Item2 == currrentdistance + 1)
                    _alternatePath.Add(Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y + 1), '>'), Tuple.Create(pos, currrentdistance + 1));
            }
                
        }
        if (pos.Item2 == '<')
        {
            if (_matrix[pos.Item1.X, pos.Item1.Y - 1] == '.')
            {
                if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y - 1), '<')].Item2 > currrentdistance + 1)
                    _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y - 1), '<')] = Tuple.Create(pos, currrentdistance + 1);
                if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y -1), '<')].Item2 == currrentdistance + 1)
                    _alternatePath.Add(Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y - 1), '<'), Tuple.Create(pos, currrentdistance + 1));
            }

        }
        if (pos.Item2 == '^')
        {
            if (_matrix[pos.Item1.X - 1, pos.Item1.Y] == '.')
            {
                if (_distances[Tuple.Create(new Point(pos.Item1.X - 1, pos.Item1.Y), '^')].Item2 > currrentdistance + 1)
                    _distances[Tuple.Create(new Point(pos.Item1.X - 1, pos.Item1.Y), '^')] = Tuple.Create(pos, currrentdistance + 1);
                if (_distances[Tuple.Create(new Point(pos.Item1.X - 1, pos.Item1.Y), '^')].Item2 == currrentdistance + 1)
                    _alternatePath[Tuple.Create(new Point(pos.Item1.X - 1, pos.Item1.Y), '^')] = Tuple.Create(pos, currrentdistance + 1);
            }

        }
        if (pos.Item2 == 'v')
        {
            if (_matrix[pos.Item1.X + 1, pos.Item1.Y] == '.')
            {
                if (_distances[Tuple.Create(new Point(pos.Item1.X + 1, pos.Item1.Y), 'v')].Item2 > currrentdistance + 1)
                    _distances[Tuple.Create(new Point(pos.Item1.X + 1, pos.Item1.Y), 'v')] = Tuple.Create(pos, currrentdistance + 1);
                if (_distances[Tuple.Create(new Point(pos.Item1.X + 1, pos.Item1.Y), 'v')].Item2 == currrentdistance + 1)
                    _alternatePath[Tuple.Create(new Point(pos.Item1.X + 1, pos.Item1.Y), 'v')] = Tuple.Create(pos, currrentdistance + 1);

            }
        }

        if (pos.Item2 == '>')
        {
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')].Item2 > currrentdistance + 1000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')] = Tuple.Create(pos, currrentdistance + 1000);
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')].Item2 > currrentdistance + 1000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')] = Tuple.Create(pos, currrentdistance + 1000);
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')].Item2 > currrentdistance + 2000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')] = Tuple.Create(pos, currrentdistance + 2000);
        }

        if (pos.Item2 == '<')
        {
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')].Item2 > currrentdistance + 1000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')] = Tuple.Create(pos, currrentdistance + 1000);
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')].Item2 > currrentdistance + 1000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')] = Tuple.Create(pos, currrentdistance + 1000);
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')].Item2 > currrentdistance + 2000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')] = Tuple.Create(pos, currrentdistance + 2000);
        }


        if (pos.Item2 == '^')
        {
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')].Item2 > currrentdistance + 1000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')] = Tuple.Create(pos, currrentdistance + 1000);
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')].Item2 > currrentdistance + 1000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')] = Tuple.Create(pos, currrentdistance + 1000);
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')].Item2 > currrentdistance + 2000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')] = Tuple.Create(pos, currrentdistance + 2000);
        }
        if (pos.Item2 == 'v')
        {
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')].Item2 > currrentdistance + 1000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')] = Tuple.Create(pos, currrentdistance + 1000);
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')].Item2 > currrentdistance + 1000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')] = Tuple.Create(pos, currrentdistance + 1000);
            if (_distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')].Item2 > currrentdistance + 2000)
                _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')] = Tuple.Create(pos, currrentdistance + 2000);
        }

        _visited.Add(pos, true);

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

    public static long sumstones(int x, int y)
    {
        var sum = 0L;
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (_matrix[i, j] == 'O')
                    sum = sum + 100 * i + j;
            }
           
        }

        return sum;

    }





}
