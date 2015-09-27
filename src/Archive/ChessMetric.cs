using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class ChessMetric
{
    public long howMany(int size, int[] start, int[] end, int numMoves)
    {
        var board = new long[size, size, numMoves+1];
        board[start[0], start[1], 0] = 1;

        for (var i = 1; i <=numMoves; i++)
        {
            for (var r = 0; r < size; r++)
                for (var c = 0; c < size; c++)
                {
                    foreach(var m in GetMoves(new[] { r, c }, size))
                    {
                        unchecked
                        {
                            board[m[0], m[1], i] += board[r, c, i - 1];
                        }
                    }
                }
        }

        return board[end[0], end[1], numMoves];
    }

    IEnumerable<int[]> GetMoves(int[] start, int size)
    {
        var y = start[0];
        var x = start[1];

        return new[]
        {
            new[] {y-1,x-1},
            new[] {y-1,x},
            new[] {y-1,x+1},

            new[] {y,x-1},
            new[] {y,x+1},

            new[] {y+1,x-1},
            new[] {y+1,x},
            new[] {y+1,x+1},

            new[] {y-2,x-1},
            new[] {y-1,x-2},

            new[] {y+2,x-1},
            new[] {y+1,x-2},

            new[] {y-2,x+1},
            new[] {y-1,x+2},

            new[] {y+2,x+1},
            new[] {y+1,x+2},
        }.Where(a => a[0] >= 0 && a[0] < size && a[1] >= 0 && a[1] < size);
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(-1, (new ChessMetric()).howMany(10, new[] { 5, 5 }, new[] { 9, 9 }, 4), 133L);
            eq(0, (new ChessMetric()).howMany(3, new int[] { 0, 0 }, new int[] { 1, 0 }, 1), 1L);
            eq(1, (new ChessMetric()).howMany(3, new int[] { 0, 0 }, new int[] { 1, 2 }, 1), 1L);
            eq(2, (new ChessMetric()).howMany(3, new int[] { 0, 0 }, new int[] { 2, 2 }, 1), 0L);
            eq(3, (new ChessMetric()).howMany(3, new int[] { 0, 0 }, new int[] { 0, 0 }, 2), 5L);
            eq(4, (new ChessMetric()).howMany(100, new int[] { 0, 0 }, new int[] { 0, 99 }, 50), 243097320072600L);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }

    static void eq(int n, object have, object need)
    {
        if (eq(have, need))
            Console.WriteLine("Case " + n + " passed.");
        else
        {
            Console.Write("Case " + n + " failed: expected ");
            print(need);
            Console.Write(", received ");
            print(have);
            Console.WriteLine();
        }
    }

    static void eq(int n, Array have, Array need)
    {
        if (have == null || have.Length != need.Length)
        {
            Console.WriteLine("Case " + n + " failed: returned " + have.Length + " elements; expected " + need.Length + " elements.");
            print(have);
            print(need);
            return;
        }

        for (int i = 0; i < have.Length; i++)
            if (!eq(have.GetValue(i), need.GetValue(i)))
            {
                Console.WriteLine("Case " + n + " failed. Expected and returned array differ in position " + i);
                print(have);
                print(need);
                return;
            }

        Console.WriteLine("Case " + n + " passed.");
    }

    static bool eq(object a, object b)
    {
        if (a is double && b is double)
            return Math.Abs((double)a - (double)b) < 1E-9;
        else
            return a != null && b != null && a.Equals(b);
    }

    static void print(object a)
    {
        if (a is string)
            Console.Write("\"{0}\"", a);
        else if (a is long)
            Console.Write("{0}L", a);
        else
            Console.Write(a);
    }

    static void print(Array a)
    {
        if (a == null)
            Console.WriteLine("<NULL>");

        Console.Write('{');

        for (int i = 0; i < a.Length; i++)
        {
            print(a.GetValue(i));

            if (i != a.Length - 1)
                Console.Write(", ");
        }

        Console.WriteLine('}');
    }
    // END CUT HERE
}
