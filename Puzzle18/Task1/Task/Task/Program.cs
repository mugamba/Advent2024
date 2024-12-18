
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
    static Point _start;
    static Point _end;
    static void Main(string[] args)
    {
        var linesField = File.ReadAllLines("input.txt").Take(1024);
        
        var builder = new StringBuilder();

        foreach (var line in linesField)
        {
            var splits = line.Split(",");
            _blockades.Add(new Point(int.Parse(splits[0]), int.Parse(splits[1])), 0);
        }
        _x = 71;
        _y = 71;
        _matrix = new char[_x, _y];
        
        for (int i = 0; i < _y; i++)
            for (int j = 0; j < _x; j++)
            {
                if (_blockades.ContainsKey(new Point(j, i)))
                    _matrix[j, i] = '#';
                else
                    _matrix[j, i] = '.';


                var point = new Point(j, i);
                if (!_blockades.ContainsKey(point))
                    _distances.Add(point, Tuple.Create<Point?, int>(null, int.MaxValue));
            }

        _start = new Point(0, 0);
        _end = new Point(70, 70);

        _toVisit.Add(_start, 0);

        var endNode = false;
        _distances[_start] = Tuple.Create<Point?, int>(null, 0);

        printmap(_x, _y);

        while (!endNode)
        {
            var toVisit = _distances.Where(o=>!_visited.ContainsKey(o.Key)).OrderBy(o => o.Value.Item2).Take(1).FirstOrDefault();
            VisitNode(toVisit.Key);
            endNode = _visited.Where(o => o.Key == _end).Any();
        }
        //var result = _distances.Where(o => o.Key.Item1 == _end).OrderBy(o => o.Value).FirstOrDefault();

        Console.WriteLine(_distances[_end].Item2);
        Console.ReadKey();
    }


    public static void VisitNode(Point pos)
    {
        var currrentdistance = _distances[pos].Item2;

        var newpoint = new Point(pos.X, pos.Y - 1);
        if (!_blockades.ContainsKey(newpoint) && newpoint.Y >= 0 && newpoint.Y < _y)
        {
            if (_distances[newpoint].Item2 >= currrentdistance + 1)
            {
                _distances[newpoint] = Tuple.Create<Point?, int>(pos, currrentdistance + 1);
               
            }

        }

        var newpoint1 = new Point(pos.X, pos.Y + 1);
        if (!_blockades.ContainsKey(newpoint1) && newpoint1.Y >= 0 && newpoint1.Y < _y)
        {

            if (_distances[newpoint1].Item2 >= currrentdistance + 1)
            {
                _distances[newpoint1] = Tuple.Create<Point?, int>(pos, currrentdistance + 1);
               
            }

        }
        var newpoint2 = new Point(pos.X-1, pos.Y);
        if (!_blockades.ContainsKey(newpoint2) && newpoint2.X >= 0 && newpoint2.X < _x)
        {

            if (_distances[newpoint2].Item2 >= currrentdistance + 1)
            {
                _distances[newpoint2] = Tuple.Create<Point?, int>(pos, currrentdistance + 1);
               
            }

        }
        var newpoint3 = new Point(pos.X+1, pos.Y);
        if (!_blockades.ContainsKey(newpoint3) && newpoint3.X >= 0 && newpoint3.X < _x)
        {

            if (_distances[newpoint3].Item2 >= currrentdistance + 1)
            {
                _distances[newpoint3] = Tuple.Create<Point?, int>(pos, currrentdistance + 1);

            }

        }
       
        _visited.Add(pos, 0);
        
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
