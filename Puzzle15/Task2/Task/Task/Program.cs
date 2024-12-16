
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using static Program;


internal class Program
{
    static char[,] _matrix;
    static int _x;
    static int _y;
    static Point _startPosition;
    static Point _currentPosition;
    static List<Stone> _stones = new List<Stone>();
    static List<Point> _blockades = new List<Point>();

    static List<Point> _path = new List<Point>();
    static Queue<char> _moves = new Queue<char>();

    static void Main(string[] args)
    {
        var linesField = File.ReadAllLines("input.txt").Where(o => o.StartsWith("#")).ToList();
        var linesMoves = File.ReadAllLines("input.txt").Where(o => !o.StartsWith("#")).ToList();
        _x = linesField.Count;
        _y = linesField[0].Length * 2;

        _matrix = new char[_x, _y];

        var builder = new StringBuilder();


        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {

                var sign = linesField[i].ToCharArray()[j / 2];
                _matrix[i, j] = sign;
                if (sign == '#')
                {
                    _blockades.Add(new Point(i, j));
                }

                if (sign == '@')
                {
                    if (j % 2 == 0)
                    {
                        _matrix[i, j] = '@';
                        _startPosition = new Point(i, j);
                    }
                    else
                        _matrix[i, j] = '.';
                }
                if (sign == 'O')
                {
                    if (j % 2 == 0)
                    {
                    }
                    else
                        _stones.Add(new Stone() { One = new Point(i, j - 1), Two = new Point(i, j) });
                }


            }

        _currentPosition = _startPosition;

        foreach (var line in linesMoves)
        {
            if (String.IsNullOrEmpty(line))
                continue;

            builder.Append(line);

        }
        foreach (var c in builder.ToString().ToCharArray())
            _moves.Enqueue(c);





      
        while (_moves.Count > 0)
        {
           
            DoMove(_currentPosition);
            //printmap(_x, _y);
            //Console.WriteLine();
        }

        Console.WriteLine(_stones.Select(o=>o.One).Select(o=>o.X * 100 + o.Y).Sum());
        Console.ReadKey();
    }

    public class Stone
    {
        public Point One { get; set; }
        public Point Two { get; set; }
    }

    private static void DoMove(Point p)
    {

        if (_moves.TryDequeue(out var move))
        {
            if (move == '^')
            {
                MoveUp(p);
            }
            if (move == 'v')
            {
                MoveDown(p);
            }
            if (move == '<')
            {
                MoveLeft(p);
            }
            if (move == '>')
            {
                MoveRight(p);
            }
        }
    }

    public static void MoveUp(Point currentPos)
    {
        var up = new Point(currentPos.X-1, currentPos.Y);

        if (_blockades.Contains(up))
            return;

        var oldStones = new List<Stone>();
        var counter = 0;
        var toMakeMove = false;
        var topPoints = new List<Point>() { up};
        while (!toMakeMove)
        {
            
            var listOfTopStone = _stones.Where(o => topPoints.Contains(o.One) || topPoints.Contains(o.Two)).ToList();
            topPoints = listOfTopStone.Select(o => o.One).ToList();
            topPoints = topPoints.Union(listOfTopStone.Select(o => o.Two)).ToList();
            topPoints= topPoints.Select(o => new Point(o.X - 1, o.Y)).ToList();

            if (topPoints.Any(o=>_blockades.Contains(o)))
            {
                return;
            }
          
            if (listOfTopStone.Count == 0)
            {
                toMakeMove = true;
            }
            oldStones = oldStones.Union(listOfTopStone).ToList();
            counter++;
        }

        if (toMakeMove)
        {
            foreach (var item in oldStones)
            {
                item.One = new Point(item.One.X-1, item.One.Y);
                item.Two = new Point(item.Two.X-1, item.Two.Y);
            }
            _currentPosition = up;
        }

    }

    public static void MoveDown(Point currentPos)
    {
        var down = new Point(currentPos.X + 1, currentPos.Y);

        if (_blockades.Contains(down))
            return;

        var oldStones = new List<Stone>();
        var counter = 0;
        var toMakeMove = false;
        var topPoints = new List<Point>() { down };
        while (!toMakeMove)
        {
            var listOfTopStone = _stones.Where(o => topPoints.Contains(o.One) || topPoints.Contains(o.Two)).ToList();
            topPoints = listOfTopStone.Select(o => o.One).ToList();
            topPoints = topPoints.Union(listOfTopStone.Select(o => o.Two)).ToList();
            topPoints = topPoints.Select(o => new Point(o.X + 1, o.Y)).ToList();

            if (topPoints.Any(o => _blockades.Contains(o)))
            {
                return;
            }

            if (listOfTopStone.Count == 0)
            {
                toMakeMove = true;
            }
            oldStones = oldStones.Union(listOfTopStone).ToList();
            counter++;
        }

        if (toMakeMove)
        {
            foreach (var item in oldStones)
            {
                item.One = new Point(item.One.X + 1, item.One.Y);
                item.Two = new Point(item.Two.X + 1, item.Two.Y);
            }
            _currentPosition = down;
        }

    }



    public static void MoveLeft(Point currentPos)
    {
        var left = new Point(currentPos.X, currentPos.Y - 1);

        if (_blockades.Contains(left))
            return;

        var list = _stones.Where(o => o.Two == left).ToList();

        if (list.Count == 0)
        {
            _currentPosition = left;
            return;
        }

        var counter = 0;
        var toMakeMove = false;
        while (!toMakeMove)
        {
            counter = counter + 2;

            if (_blockades.Contains(new Point(left.X, left.Y - counter)))
            {
                return;
            }

            var list1 = _stones.Where(o => o.Two == new Point(left.X, left.Y - counter)).ToList();
            if (list1.Count == 0)
            {
                toMakeMove = true;

               
            }
            list = list.Union(list1).ToList();
        }

        if (toMakeMove)
        { 
            foreach(var item in list) 
            {
                item.One = new Point(item.One.X, item.One.Y - 1);
                item.Two = new Point(item.Two.X, item.Two.Y - 1);
            }
            _currentPosition = left;
        }

    }

    public static void MoveRight(Point currentPos)
    {
        var right = new Point(currentPos.X, currentPos.Y + 1);

        if (_blockades.Contains(right))
            return;

        var list = _stones.Where(o => o.One == right).ToList();

        if (list.Count == 0)
        {
            _currentPosition = right;
            return;
        }

        var counter = 0;
        var toMakeMove = false;
        while (!toMakeMove)
        {
            counter = counter + 2;

            if (_blockades.Contains(new Point(right.X, right.Y + counter)))
            {
                return;
            }

            var list1 = _stones.Where(o => o.One == new Point(right.X, right.Y + counter)).ToList();
            if (list1.Count == 0)
            {
                toMakeMove = true;
            }
            list = list.Union(list1).ToList();
        }

        if (toMakeMove)
        {
            foreach (var item in list)
            {
                item.One = new Point(item.One.X, item.One.Y + 1);
                item.Two = new Point(item.Two.X, item.Two.Y + 1);
            }
            _currentPosition = right;
        }

    }






    public static void printmap(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {


                if (_blockades.Contains(new Point(i, j)))
                    Console.Write("#");
                else
                {
                    if (_stones.Any(o => o.One == new Point(i, j)))
                    {
                        Console.Write("[");
                    }
                    else
                    {
                        if (_stones.Any(o => o.Two == new Point(i, j)))
                            Console.Write("]");
                        else
                              if (_currentPosition == new Point(i, j))
                            Console.Write("@");
                        else
                            Console.Write(".");
                    }

                }



            }
            Console.WriteLine();
        }



    }

   



}
