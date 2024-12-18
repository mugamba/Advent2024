
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
    static Dictionary<Tuple<Point, char>, Node> _distances = new Dictionary<Tuple<Point, char>, Node>();
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
                            _distances.Add(Tuple.Create(new Point(i, j), '>'), new Node());
                        }
                        if (k == 1)
                        {
                            _distances.Add(Tuple.Create(new Point(i, j), '^'), new Node());
                        }
                        if (k == 2)
                        {
                            _distances.Add(Tuple.Create(new Point(i, j), 'v'), new Node());
                        }
                        if (k == 3)
                        {
                            _distances.Add(Tuple.Create(new Point(i, j), '<'), new Node());
                        }

                    }
                }
            }

        var startNode = new Node();
        startNode._distance = 0;
        _distances[_start] = startNode;
        var endNode = false;

        while (!endNode)
        {

            var toVisit = _distances.Where(o => !_visited.ContainsKey(o.Key)).OrderBy(o => o.Value._distance).Take(1).FirstOrDefault();
            VisitNode(toVisit.Key);
            endNode = _distances.Where(o => o.Key.Item1 == _end).Any(o => o.Value._distance != int.MaxValue);

        }

        var result = _distances.Where(o => o.Key.Item1 == _end).OrderBy(o => o.Value._distance).FirstOrDefault();
        var next = result.Key;
        _shortestPath.Add(next, 0);

        var list = _distances[next]._list;

        while (list.Count > 0)
        {
            var newList = new List<Tuple<Point, char>>();
            foreach (var item in list)
            {
                if (!_shortestPath.ContainsKey(item))
                    _shortestPath.Add(item, 0);
                newList.AddRange(_distances[item]._list);
            }
            list = newList;
        }

        Console.WriteLine(_shortestPath.Select(o => o.Key.Item1).Distinct().Count());
        Console.ReadKey();
    }


    public static void VisitNode(Tuple<Point, char> pos)
    {
        var currrentdistance = _distances[pos]._distance;

        if (pos.Item2 == '>')
        {

            if (_matrix[pos.Item1.X, pos.Item1.Y + 1] == '.')
            {
                var node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y + 1), '>')];
                if (node._distance > currrentdistance + 1)
                {
                    node._list.Clear();
                    node._list.Add(pos);
                    node._distance = currrentdistance + 1;
                }
                if (node._distance == currrentdistance + 1)
                {
                    if (!node._list.Contains(pos))
                        node._list.Add(pos);
                }
            }

        }
        if (pos.Item2 == '<')
        {
            if (_matrix[pos.Item1.X, pos.Item1.Y - 1] == '.')
            {
                var node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y - 1), '<')];
                if (node._distance > currrentdistance + 1)
                {
                    node._list.Clear();
                    node._list.Add(pos);
                    node._distance = currrentdistance + 1;
                }
                if (node._distance == currrentdistance + 1)
                {
                    if (!node._list.Contains(pos))
                        node._list.Add(pos);
                }
            }

        }
        if (pos.Item2 == '^')
        {
            if (_matrix[pos.Item1.X - 1, pos.Item1.Y] == '.')
            {

                var node = _distances[Tuple.Create(new Point(pos.Item1.X - 1, pos.Item1.Y), '^')];
                if (node._distance > currrentdistance + 1)
                {
                    node._list.Clear();
                    node._list.Add(pos);
                    node._distance = currrentdistance + 1;
                }
                if (node._distance == currrentdistance + 1)
                {
                    if (!node._list.Contains(pos))
                        node._list.Add(pos);
                }

            }

        }
        if (pos.Item2 == 'v')
        {
            if (_matrix[pos.Item1.X + 1, pos.Item1.Y] == '.')
            {
                var node = _distances[Tuple.Create(new Point(pos.Item1.X + 1, pos.Item1.Y), 'v')];
                if (node._distance > currrentdistance + 1)
                {
                    node._list.Clear();
                    node._list.Add(pos);
                    node._distance = currrentdistance + 1;
                }
                if (node._distance == currrentdistance + 1)
                {
                    if (!node._list.Contains(pos))
                        node._list.Add(pos);
                }
            }
        }

        if (pos.Item2 == '>')
        {
            var node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')];
            if (node._distance > currrentdistance + 1000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 1000;
            }
            if (node._distance == currrentdistance + 1000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }

            node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')];
            if (node._distance > currrentdistance + 1000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 1000;
            }
            if (node._distance == currrentdistance + 1000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }

            node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')];
            if (node._distance > currrentdistance + 2000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 2000;
            }
            if (node._distance == currrentdistance + 2000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }
        }

        if (pos.Item2 == '<')
        {
            var node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')];
            if (node._distance > currrentdistance + 1000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 1000;
            }
            if (node._distance == currrentdistance + 1000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }


            node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')];
            if (node._distance > currrentdistance + 1000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 1000;
            }
            if (node._distance == currrentdistance + 1000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }

            node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')];
            if (node._distance > currrentdistance + 2000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 2000;
            }
            if (node._distance == currrentdistance + 2000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }
        }


        if (pos.Item2 == '^')
        {
            var node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')];
            if (node._distance > currrentdistance + 1000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 1000;
            }
            if (node._distance == currrentdistance + 1000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }


            node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')];
            if (node._distance > currrentdistance + 1000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 1000;
            }
            if (node._distance == currrentdistance + 1000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }

            node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), 'v')];
            if (node._distance > currrentdistance + 2000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 2000;
            }
            if (node._distance == currrentdistance + 2000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }
        }
        if (pos.Item2 == 'v')
        {
            var node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '<')];
            if (node._distance > currrentdistance + 1000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 1000;
            }
            if (node._distance == currrentdistance + 1000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }


            node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '>')];
            if (node._distance > currrentdistance + 1000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 1000;
            }
            if (node._distance == currrentdistance + 1000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }

            node = _distances[Tuple.Create(new Point(pos.Item1.X, pos.Item1.Y), '^')];
            if (node._distance > currrentdistance + 2000)
            {
                node._list.Clear();
                node._list.Add(pos);
                node._distance = currrentdistance + 2000;
            }
            if (node._distance == currrentdistance + 2000)
            {
                if (!node._list.Contains(pos))
                    node._list.Add(pos);
            }
        }

        _visited.Add(pos, true);
    }
    public class Node
    {
        public List<Tuple<Point, char>> _list = new List<Tuple<Point, char>>();
        public int _distance = int.MaxValue;
    }





}
