
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    static Point _startPosition;
    static Point _currentPosition;
    static List<Point> _path = new List<Point>();
    static Queue<char> _moves = new Queue<char>();
  
    static void Main(string[] args)
    {
        var linesField = File.ReadAllLines("input.txt").Where(o=>o.StartsWith("#")).ToList();
        var linesMoves = File.ReadAllLines("input.txt").Where(o => !o.StartsWith("#")).ToList();
        _x = linesField.Count;
        _y = linesField[0].Length;

        _matrix = new char[_x, _y];

        var builder = new StringBuilder();


        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                var sign = linesField[i].ToCharArray()[j];
                _matrix[i, j] = sign;
                if (sign == '@')
                { 
                    _startPosition = new Point(i, j);
                    _matrix[i, j] = '.';
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

        
        while ( _moves.Count > 0 ) 
        {
            DoMove(_currentPosition);
            //printmap(_x, _y);
            //Console.WriteLine();
        }

        Console.WriteLine(sumstones(_x, _y));
        Console.ReadKey();
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

    private static void MoveUp(Point p)
    {
        var stonesToMove = new List<Point>();
        var canMove = false;
        var counter = 1;
        while (!canMove)
        {
            var newX = p.X - counter;
            var newY = p.Y;
            if (_matrix[newX, newY] == '#')
                break;

            if (_matrix[newX, newY] == '.')
            {
                canMove = true;
                break;
            }
            if (_matrix[newX, newY] == 'O')
            {
                stonesToMove.Add(new Point(newX, newY));
                counter++;
            }
        }

        if (canMove)
        {
            _currentPosition = new Point(p.X - 1, p.Y);
            _matrix[_currentPosition.X, _currentPosition.Y] = '.';
            foreach (var st in stonesToMove)
                _matrix[st.X - 1, st.Y] = 'O';
        }
    }

    private static void MoveDown(Point p)
    {
        var stonesToMove = new List<Point>();
        var canMove = false;
        var counter = 1;
        while (!canMove)
        {
            var newX = p.X + counter;
            var newY = p.Y;
            if (_matrix[newX, newY] == '#')
                break;

            if (_matrix[newX, newY] == '.')
            {
                canMove = true;
                break;
            }
            if (_matrix[newX, newY] == 'O')
            {
                stonesToMove.Add(new Point(newX, newY));
                counter++;
            }
        }

        if (canMove)
        {
            _currentPosition = new Point(p.X + 1, p.Y);
            _matrix[_currentPosition.X, _currentPosition.Y] = '.';
            foreach (var st in stonesToMove)
                _matrix[st.X + 1, st.Y] = 'O';
        }
    }

    private static void MoveLeft(Point p)
    {
        var stonesToMove = new List<Point>();
        var canMove = false;
        var counter = 1;
        while (!canMove)
        {
            var newX = p.X;
            var newY = p.Y - counter;
            if (_matrix[newX, newY] == '#')
                break;

            if (_matrix[newX, newY] == '.')
            {
                canMove = true;
                break;
            }
            if (_matrix[newX, newY] == 'O')
            {
                stonesToMove.Add(new Point(newX, newY));
                counter++;
            }
        }

        if (canMove)
        {
            _currentPosition = new Point(p.X, p.Y-1);
            _matrix[_currentPosition.X, _currentPosition.Y] = '.';
            foreach (var st in stonesToMove)
                _matrix[st.X, st.Y-1] = 'O';
        }
    }

    private static void MoveRight(Point p)
    {
        var stonesToMove = new List<Point>();
        var canMove = false;
        var counter = 1;
        while (!canMove)
        {
            var newX = p.X;
            var newY = p.Y + counter;
            if (_matrix[newX, newY] == '#')
                break;

            if (_matrix[newX, newY] == '.')
            {
                canMove = true;
                break;
            }
            if (_matrix[newX, newY] == 'O')
            {
                stonesToMove.Add(new Point(newX, newY));
                counter++;
            }
        }

        if (canMove)
        {
            _currentPosition = new Point(p.X, p.Y + 1);
            _matrix[_currentPosition.X, _currentPosition.Y] = '.';
            foreach (var st in stonesToMove)
                _matrix[st.X, st.Y + 1] = 'O';
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
