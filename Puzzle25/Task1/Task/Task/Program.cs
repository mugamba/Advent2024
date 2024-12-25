
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
   
    public static List<Tile> _tiles = new List<Tile>();
    

    static void Main(string[] args)
    {
        var linesField = File.ReadAllLines("input.txt");
        var x = linesField.Length;
        var y = linesField[0].Length;
        var builder = new StringBuilder();

        int k = 0;
        while (k < (x / 8) +1)
        {
            var tile = new Tile() { _id = k };
            for (int i = k * 8; i < k * 8 + 7; i++)
            {
                Console.WriteLine(linesField[i]);
                for (int j = 0; j < y; j++)
                {
                    var sign = linesField[i].ToCharArray()[j];
                    if (sign == '#')
                    {
                        tile._points.Add(new Point(i%8, j));
                        tile._x = 7;
                        tile._y = 5;

                        

                    }

                }
            }
            _tiles.Add(tile);
            Console.WriteLine();
            k++;
        }
        var count = 0;
        for(int i = 0; i < _tiles.Count; i++) 
        {
            for (int j = i + 1; j< _tiles.Count; j++)
            {
                if (_tiles[i].IsMatch(_tiles[j]))
                {
                    Console.WriteLine("{0} - {1}", _tiles[i]._id, _tiles[j]._id);
                    count++;
                }

            }
        
        }
        
        Console.WriteLine(count);
        Console.ReadKey();
    }


    public class Tile 
    {
        public int _id;
        public int _x;
        public int _y;
        public List<Point> _points = new List<Point>();

        public Boolean IsMatch(Tile other)
        {
            for (int i = 0; i < _y; i++)
            {
                if ((_points.Where(o => o.Y == i).Intersect(other._points.Where(o => o.Y == i))).Count() > 0)
                {
                    return false;
                }
            
            }

            return true;
        
        }

    } 




}
