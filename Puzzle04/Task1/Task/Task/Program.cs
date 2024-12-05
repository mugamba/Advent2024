
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

    private static Boolean CharacterOnPosition(int i, int j, char c)
    {
        if (i < 0 || j < 0)
            return false;
        if (i > _x - 1 || j > _y - 1)
            return false;
        if (_matrix[i, j] != c)
            return false;
        
        return true;
    }

    private static Boolean CheckLeft(int i, int j)
    {
        return CharacterOnPosition(i, j, 'X') &&
            CharacterOnPosition(i, j+1, 'M') &&
            CharacterOnPosition(i, j+2, 'A') &&
            CharacterOnPosition(i, j+3, 'S');
    }

    private static Boolean CheckRight(int i, int j)
    {
        return CharacterOnPosition(i, j, 'X') &&
            CharacterOnPosition(i, j - 1, 'M') &&
            CharacterOnPosition(i, j - 2, 'A') &&
            CharacterOnPosition(i, j - 3, 'S');
    }

    private static Boolean CheckUp(int i, int j)
    {
        return CharacterOnPosition(i, j, 'X') &&
            CharacterOnPosition(i-1, j, 'M') &&
            CharacterOnPosition(i-2, j, 'A') &&
            CharacterOnPosition(i-3, j, 'S');

    }

    private static Boolean CheckDown(int i, int j)
    {
        return CharacterOnPosition(i, j, 'X') &&
            CharacterOnPosition(i + 1, j, 'M') &&
            CharacterOnPosition(i + 2, j, 'A') &&
            CharacterOnPosition(i + 3, j, 'S');

    }

    private static Boolean CheckUpRight(int i, int j)
    {
        return CharacterOnPosition(i, j, 'X') &&
            CharacterOnPosition(i - 1, j -1, 'M') &&
            CharacterOnPosition(i - 2, j -2, 'A') &&
            CharacterOnPosition(i - 3, j-3, 'S');

    }

    private static Boolean CheckUpLeft(int i, int j)
    {
        return CharacterOnPosition(i, j, 'X') &&
           CharacterOnPosition(i - 1, j + 1, 'M') &&
           CharacterOnPosition(i - 2, j + 2, 'A') &&
           CharacterOnPosition(i - 3, j + 3, 'S');

    }

    private static Boolean CheckDownLeft(int i, int j)
    {
        return CharacterOnPosition(i, j, 'X') &&
          CharacterOnPosition(i + 1, j + 1, 'M') &&
          CharacterOnPosition(i + 2, j + 2, 'A') &&
          CharacterOnPosition(i + 3, j + 3, 'S');

    }

    private static Boolean CheckDownRight(int i, int j)
    {
        return CharacterOnPosition(i, j, 'X') &&
          CharacterOnPosition(i +1, j - 1, 'M') &&
          CharacterOnPosition(i + 2, j - 2, 'A') &&
          CharacterOnPosition(i + 3, j - 3, 'S');

    }





}
