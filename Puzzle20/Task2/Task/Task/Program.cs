
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
    static Dictionary<Point, int> _distances = new Dictionary<Point, int>();
    static Dictionary<Point, int> _originalDistances = new Dictionary<Point, int>();
    static Dictionary<Point, int> _distancesEmpty = new Dictionary<Point, int>();


    static Dictionary<Point, int> _blockades = new Dictionary<Point, int>();
    static Dictionary<Point, int> _visited = new Dictionary<Point, int>();
    static Dictionary<Point, int> _toVisit = new Dictionary<Point, int>();
    static Dictionary<Tuple<Point, Point>, int> _ustede = new Dictionary<Tuple<Point, Point>, int>();


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

                _distances.Add(new Point(i, j), int.MaxValue);
                _distancesEmpty.Add(new Point(i, j), int.MaxValue);

                if (sign == 'S')
                    _start = new Point(i, j);

                if (sign == 'E')
                    _end = new Point(i, j);

            }


        DoDijkstra();
        var minStartingTime = _distances[_end];
        foreach (var d in _distances)
            _originalDistances.Add(d.Key, d.Value);

       

       foreach (var d in _originalDistances.Where(o => o.Value != int.MaxValue).OrderBy(o => o.Value))
        {
          
            var points = _distancesEmpty.Where(o => Math.Abs(d.Key.X - o.Key.X) + Math.Abs(d.Key.Y - o.Key.Y) <= 20).Select(g => g.Key)
                .ToList().Intersect(_originalDistances.Where(o => o.Value != int.MaxValue).Select(o=>o.Key).ToList());
            
            foreach (var p in points)
            {
                var distance = Math.Abs(d.Key.X - p.X) + Math.Abs(d.Key.Y - p.Y);

                //Console.WriteLine(distance);
                //Console.WriteLine(d.Value + "  " + _originalDistances[p]);
                if (_originalDistances[p] > d.Value)
                {
                    if ((_originalDistances[p] - d.Value) > distance )
                    {
                        //Console.WriteLine("Point({0}-{1}) -> Point({2}-{3})   -- ušteda {4}", d.Key.X, d.Key.Y,  p.X, p.Y, _originalDistances[p] - d.Value - distance );
                        _ustede.Add(Tuple.Create(d.Key, p), _originalDistances[p] - d.Value - distance);
                    }
                }
            }
        }
        Console.WriteLine(_ustede.Where(o=>o.Value >= 100).Count());
        Console.ReadKey();
    }

    private static void DoDijkstra()
    {
        var endNode = false;
        foreach (var node in _distances)
        {
            _distances[node.Key] = int.MaxValue;
        }
        _toVisit.Clear();
        _visited.Clear();
        _distances[_start] = 0; 
        _toVisit.Add(_start, 0);

        while (!endNode)
        {
            var toVisit = _toVisit.OrderBy(o => o.Value).Take(1).FirstOrDefault();
            VisitNode(toVisit.Key);
            endNode = _visited.Where(o => o.Key == _end).Any();
        }
        
    }

    public static void VisitNode(Point pos)
    {
        var currrentdistance = _distances[pos];
        var newpoint = new Point(pos.X, pos.Y - 1);
        if (InBounds(newpoint))
            if ((!_blockades.ContainsKey(newpoint)) && !_visited.ContainsKey(newpoint))
            {
                if (_distances[newpoint] > currrentdistance + 1)
                {
                    _distances[newpoint] =  currrentdistance + 1;
                    if (!_toVisit.ContainsKey(newpoint))
                        _toVisit.Add(newpoint, currrentdistance + 1);
                }
            }

        var newpoint1 = new Point(pos.X, pos.Y + 1);
        if (InBounds(newpoint1))
            if (!_blockades.ContainsKey(newpoint1) && !_visited.ContainsKey(newpoint1))
            {

                if (_distances[newpoint1] > currrentdistance + 1)
                {
                    _distances[newpoint1] =  currrentdistance + 1;
                    if (!_toVisit.ContainsKey(newpoint1))
                        _toVisit.Add(newpoint1, currrentdistance + 1);

                }

            }
        var newpoint2 = new Point(pos.X - 1, pos.Y);
        if (InBounds(newpoint2))
            if ((!_blockades.ContainsKey(newpoint2)) && !_visited.ContainsKey(newpoint2))
            {

                if (_distances[newpoint2] > currrentdistance + 1)
                {
                    _distances[newpoint2] =  currrentdistance + 1;
                    if (!_toVisit.ContainsKey(newpoint2))
                        _toVisit.Add(newpoint2, currrentdistance + 1);

                }

            }
        var newpoint3 = new Point(pos.X + 1, pos.Y);
        if (InBounds(newpoint3))
            if ((!_blockades.ContainsKey(newpoint3)) && !_visited.ContainsKey(newpoint3))
            {

                if (_distances[newpoint3] > currrentdistance + 1)
                {
                    _distances[newpoint3] =  currrentdistance + 1;
                    if (!_toVisit.ContainsKey(newpoint3))
                        _toVisit.Add(newpoint3, currrentdistance + 1);

                }

            }

        _visited.Add(pos, 0);
        _toVisit.Remove(pos);
    }

    private static bool InBounds(Point newpoint)
    {
        return (newpoint.X >= 0 && newpoint.Y >= 0 && newpoint.X < _x  && newpoint.Y < _y);
    }






}
