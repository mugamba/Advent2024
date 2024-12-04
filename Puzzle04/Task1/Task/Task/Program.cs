
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


internal class Program
{
    static char[,] _matrix;
    static int _x;
    static int _y;

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var lines = File.ReadAllLines("input.txt");
        _x = lines.Length;
        _y = lines[0].Length;

        _matrix = new char[_x, _y];

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
                _matrix[i, j] = lines[i].ToCharArray()[j];


        var counter = 0;
        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                if (CheckLeft(i, j))
                    counter++;
                if (CheckRight(i, j))
                    counter++;
                if (CheckUp(i, j))
                    counter++;
                if (CheckDown(i, j))
                    counter++;
                if (CheckUpRight(i, j))
                    counter++;
                if (CheckUpLeft(i, j))
                    counter++;
                if (CheckDownRight(i, j))
                    counter++;
                if (CheckDownLeft(i, j))
                    counter++;
            }
        /*Suma svih elemenata liste*/
        Console.WriteLine(counter);
        Console.ReadKey();
    }

    private static Boolean IsInsideMatrix(int i, int j)
    {
        if (i < 0 || j < 0)
            return false;
        if (i > _x - 1 || j > _y - 1)
            return false;
        return true;
    }

    private static Boolean CheckLeft(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'X')
            return false;

        nextIndexX = i; nextIndexY = j + 1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return false;

        nextIndexX = i;  nextIndexY = j + 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return false;

        nextIndexX = i;nextIndexY = j + 3;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'S')
            return false;

        return true;
    }

    private static Boolean CheckRight(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'X')
            return false;

        nextIndexX = i; nextIndexY = j-1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return false;

        nextIndexX = i; nextIndexY = j-2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return false;

        nextIndexX = i; nextIndexY = j-3;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'S')
            return false;

        return true;

    }

    private static Boolean CheckUp(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'X')
            return false;

        nextIndexX = i-1; nextIndexY = j;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return false;

        nextIndexX = i-2; nextIndexY = j;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return false;

        nextIndexX = i-3; nextIndexY = j;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'S')
            return false;

        return true;

    }

    private static Boolean CheckDown(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'X')
            return false;

        nextIndexX = i + 1; nextIndexY = j;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return false;

        nextIndexX = i + 2; nextIndexY = j;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return false;

        nextIndexX = i + 3; nextIndexY = j;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'S')
            return false;

        return true;

    }

    private static Boolean CheckUpRight(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'X')
            return false;

        nextIndexX = i - 1; nextIndexY = j+1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return false;

        nextIndexX = i - 2; nextIndexY = j + 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return false;

        nextIndexX = i - 3; nextIndexY = j + 3;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'S')
            return false;

        return true;

    }

    private static Boolean CheckUpLeft(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'X')
            return false;

        nextIndexX = i - 1; nextIndexY = j - 1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return false;

        nextIndexX = i - 2; nextIndexY = j - 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return false;

        nextIndexX = i - 3; nextIndexY = j - 3;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'S')
            return false;

        return true;

    }

    private static Boolean CheckDownLeft(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'X')
            return false;

        nextIndexX = i + 1; nextIndexY = j - 1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return false;

        nextIndexX = i + 2; nextIndexY = j - 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return false;

        nextIndexX = i + 3; nextIndexY = j - 3;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'S')
            return false;

        return true;

    }

    private static Boolean CheckDownRight(int i, int j)
    {
        var nextIndexX = 0; var nextIndexY = 0;
        if (_matrix[i, j] != 'X')
            return false;

        nextIndexX = i + 1; nextIndexY = j + 1;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'M')
            return false;

        nextIndexX = i + 2; nextIndexY = j + 2;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'A')
            return false;

        nextIndexX = i + 3; nextIndexY = j + 3;
        if (!IsInsideMatrix(nextIndexX, nextIndexY))
            return false;
        if (_matrix[nextIndexX, nextIndexY] != 'S')
            return false;

        return true;

    }





}
