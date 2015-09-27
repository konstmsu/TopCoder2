using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class AvoidRoads
{
    public long numWays(int width, int height, string[] bad)
    {
        var b = new long[width + 2, height + 2];
        b[0, 0] = 1;

        for (var c = 0; c <= width; c++)
            for (var r = 0; r <= height; r++)
            {
                if (c == 0 && r == 0)
                    continue;

                if (c > 0 &&
                    !bad.Contains(string.Format("{0} {1} {2} {3}", c, r, c - 1, r)) &&
                    !bad.Contains(string.Format("{0} {1} {2} {3}", c - 1, r, c, r)))
                    b[c, r] += b[c - 1, r];

                if (r > 0 &&
                    !bad.Contains(string.Format("{0} {1} {2} {3}", c, r, c, r - 1)) &&
                    !bad.Contains(string.Format("{0} {1} {2} {3}", c, r - 1, c, r)))
                    b[c, r] += b[c, r - 1];
            }

        return b[width, height];
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(0, (new AvoidRoads()).numWays(6, 6, new string[] { "0 0 0 1", "6 6 5 6" }), 252L);
            eq(1, (new AvoidRoads()).numWays(1, 1, new string[] { }), 2L);
            eq(2, (new AvoidRoads()).numWays(35, 31, new string[] { }), 6406484391866534976L);
            eq(3, (new AvoidRoads()).numWays(2, 2, new string[] { "0 0 1 0", "1 2 2 2", "1 1 2 1" }), 0L);
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
