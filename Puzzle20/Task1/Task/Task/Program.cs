
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    static Dictionary<Point, Tuple<Point?, int>> _distances = new Dictionary<Point, Tuple<Point?, int>>();
    static Dictionary<Point, int> _blockades = new Dictionary<Point, int>();
    static Dictionary<Point, int> _visited = new Dictionary<Point, int>();
    static Dictionary<Point, int> _toVisit = new Dictionary<Point, int>();
    static Dictionary<Point, int> _sparedPicoSeconds = new Dictionary<Point, int>();

    static Point _start;
    static Point _end;
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        var builder = new StringBuilder();


        _x = lines.Length;
        _y = lines[0].Length;
        _matrix = new char[_x, _y];

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                _matrix[i, j] = lines[i].ToCharArray()[j];

            }


        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                var sign = _matrix[i, j];

                if (sign == '#')
                    _blockades.Add(new Point(i, j), 0);
                else
                    _distances.Add(new Point(i, j), Tuple.Create<Point?, int>(null, int.MaxValue));


                if (sign == 'S')
                    _start = new Point(i, j);

                if (sign == 'E')
                    _end = new Point(i, j);

            }

        DoDijkstra();
        var minStartingTime = _distances[_end].Item2;

        for (int i = 1; i < _x - 1; i++)
        {
            Console.WriteLine(i);
            for (int j = 1; j < _y - 1; j++)
            {
                if (_blockades.ContainsKey(new Point(i, j)))
                {
                    _blockades.Remove(new Point(i, j));
                    _distances.Add(new Point(i, j), Tuple.Create<Point?, int>(null, int.MaxValue));
                    _sparedPicoSeconds.Add(new Point(i, j), minStartingTime - DoDijkstra());
                    _blockades.Add(new Point(i, j), 0);
                    _distances.Remove(new Point(i, j));
                }
            }
        }

        Console.WriteLine(_sparedPicoSeconds.Where(o=>o.Value >= 100).Count());
        Console.ReadKey();
    }

    private static int DoDijkstra()
    {
        var endNode = false;
        foreach (var node in _distances)
        {
            _distances[node.Key] = Tuple.Create<Point?, int>(null, int.MaxValue);
        }
        _toVisit.Clear();
        _visited.Clear();

        _distances[_start] = Tuple.Create<Point?, int>(null, 0);
        _toVisit.Add(_start, 0);

        while (!endNode)
        {
            var toVisit = _toVisit.OrderBy(o => o.Value).Take(1).FirstOrDefault();
            VisitNode(toVisit.Key);
            endNode = _visited.Where(o => o.Key == _end).Any();
        }
        return _distances[_end].Item2;
    }

    public static void VisitNode(Point pos)
    {
        var currrentdistance = _distances[pos].Item2;

        var newpoint = new Point(pos.X, pos.Y - 1);
        if (!_blockades.ContainsKey(newpoint) && !_visited.ContainsKey(newpoint))
        {
            if (_distances[newpoint].Item2 > currrentdistance + 1)
            {
                _distances[newpoint] = Tuple.Create<Point?, int>(pos, currrentdistance + 1);
                if (!_toVisit.ContainsKey(newpoint))
                    _toVisit.Add(newpoint, currrentdistance + 1);
            }
            

        }

        var newpoint1 = new Point(pos.X, pos.Y + 1);
        if (!_blockades.ContainsKey(newpoint1) && !_visited.ContainsKey(newpoint1))
        {

            if (_distances[newpoint1].Item2 > currrentdistance + 1)
            {
                _distances[newpoint1] = Tuple.Create<Point?, int>(pos, currrentdistance + 1);
                if (!_toVisit.ContainsKey(newpoint1))
                    _toVisit.Add(newpoint1, currrentdistance + 1);

            }

        }
        var newpoint2 = new Point(pos.X-1, pos.Y);
        if (!_blockades.ContainsKey(newpoint2) && !_visited.ContainsKey(newpoint2))
        {

            if (_distances[newpoint2].Item2 > currrentdistance + 1)
            {
                _distances[newpoint2] = Tuple.Create<Point?, int>(pos, currrentdistance + 1);
                if (!_toVisit.ContainsKey(newpoint2))
                    _toVisit.Add(newpoint2, currrentdistance + 1);

            }

        }
        var newpoint3 = new Point(pos.X+1, pos.Y);
        if (!_blockades.ContainsKey(newpoint3) && !_visited.ContainsKey(newpoint3))
        {

            if (_distances[newpoint3].Item2 > currrentdistance + 1)
            {
                _distances[newpoint3] = Tuple.Create<Point?, int>(pos, currrentdistance + 1);
                if (!_toVisit.ContainsKey(newpoint3))
                    _toVisit.Add(newpoint3, currrentdistance + 1);

            }

        }
        
        _visited.Add(pos, 0);
        _toVisit.Remove(pos);

    }


    public static void printmap(int x, int y)
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Console.Write(_matrix[j, i]);
            }
            Console.WriteLine();
        }
    }





}
