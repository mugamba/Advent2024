
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

        MoveLeft(new Point(_currentPosition.X, _currentPosition.Y));

        // printmap(_x, _y);

        //while (_moves.Count > 0)
        //{
        //  //  DoMove(_currentPosition);
        //    //printmap(_x, _y);
        //    //Console.WriteLine();
        //}

        //Console.WriteLine(sumstones(_x, _y));
        Console.ReadKey();
    }

    public class Stone
    {
        public Point One { get; set; }
        public Point Two { get; set; }
    }


    private static Boolean IsObsticle(int i, int j)
    {
        if (_matrix[i, j] == '.')
            return false;
        else
            return true;
    }

    private static Boolean IsStone(int i, int j)
    {
        if (_matrix[i, j] == 'O')
            return true;
        else
            return false;
    }

    //private static void DoMove(Point p)
    //{

    //    if (_moves.TryDequeue(out var move))
    //    {
    //        if (move == '^')
    //        {
    //            MoveUp(p);
    //        }
    //        if (move == 'v')
    //        {
    //            MoveDown(p);
    //        }
    //        if (move == '<')
    //        {
    //            MoveLeft(p);
    //        }
    //        if (move == '>')
    //        {
    //            MoveRight(p);
    //        }
    //    }
    //}

    public static void MoveLeft(Point currentPoint)
    {
       var toMove = LeftStonesShouldMove(new List<Stone>(), new List<Point> { new Point(currentPoint.X, currentPoint.Y-1) });

    }

    public static List<Stone> LeftStonesShouldMove(List<Stone> stones, List<Point> shouldMove)
    {
        /*Canot move ran into block*/
        if (shouldMove.Any(o => _blockades.Contains(o)))
            return null;

        var list = _stones.Where(o => shouldMove.Contains(o.One) || shouldMove.Contains(o.Two)).ToList();
        

        if (list.Count == 0)
            return stones;

        stones.AddRange(list);
        var listNextMove = list.Select(o => new Point(o.One.X, o.One.Y - 1)).Union(list.Select(o => new Point(o.Two.X, o.Two.Y - 1))).ToList();

        return LeftStonesShouldMove(stones, listNextMove);

    }




    public static void printmap(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
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
