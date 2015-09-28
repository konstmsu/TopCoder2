using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Circuits
{
    public int howLong(string[] connects, string[] costs)
    {
        const int MAX = 50;
        var cc = new int?[MAX, MAX];

        for (var i = 0; i < connects.Length; i++)
        {
            if (string.IsNullOrEmpty(connects[i]))
                continue;

            var n = connects[i].Split(' ').Select(int.Parse).ToList();
            var s = costs[i].Split(' ').Select(int.Parse).ToList();

            for (var j = 0; j < n.Count; j++)
                cc[i, n[j]] = s[j];
        }

        var dist = new int[MAX, MAX];
        for (var i = 0; i < MAX; i++)
        {
            dist[i, i] = 0;
            for (var j = 0; j < MAX; j++)
                dist[i, j] = cc[i, j] ?? 0;
        }

        var max = 0;
        for (var k = 0; k < MAX; k++)
            for (var i = 0; i < MAX; i++)
                for (var j = 0; j < MAX; j++)
                {
                    if (dist[i, k] > 0 && dist[k, j] > 0)
                    {
                        dist[i, j] = Math.Max(dist[i, j], dist[i, k] + dist[k, j]);
                        max = Math.Max(max, dist[i, j]);
                    }
                }

        return max;
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(1, (new Circuits()).howLong(new string[] { "1 2 3 4 5", "2 3 4 5", "3 4 5", "4 5", "5", "" }, new string[] { "2 2 2 2 2", "2 2 2 2", "2 2 2", "2 2", "2", "" }), 10);
            eq(0, (new Circuits()).howLong(new string[] {"1 2",
                "2",
                ""}, new string[] {"5 3",
                "7",
                ""}), 12);
            eq(2, (new Circuits()).howLong(new string[] { "1", "2", "3", "", "5", "6", "7", "" }, new string[] { "2", "2", "2", "", "3", "3", "3", "" }), 9);
            eq(3, (new Circuits()).howLong(new string[] {"","2 3 5","4 5","5 6","7","7 8","8 9","10",
                "10 11 12","11","12","12",""}, new string[] {"","3 2 9","2 4","6 9","3","1 2","1 2","5",
                "5 6 9","2","5","3",""}), 22);
            eq(4, (new Circuits()).howLong(new string[] { "", "2 3", "3 4 5", "4 6", "5 6", "7", "5 7", "" }, new string[] { "", "30 50", "19 6 40", "12 10", "35 23", "8", "11 20", "" }), 105);
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
            Console.WriteLine(FormatObj(need));
            Console.Write(", received ");
            Console.WriteLine(FormatObj(have));
            Console.WriteLine();
        }
    }

    static void eq<T>(int n, T[] have, T[] need)
    {
        if (have == null || have.Length != need.Length)
        {
            Console.WriteLine("Case " + n + " failed: returned " + have.Length + " elements; expected " + need.Length + " elements.");
            Console.WriteLine("  Actual:" + Format(have));
            Console.WriteLine("Expected:" + Format(need));
            return;
        }

        for (int i = 0; i < have.Length; i++)
            if (!eq(have.GetValue(i), need.GetValue(i)))
            {
                Console.WriteLine("Case " + n + " failed. Expected and returned array differ in position " + i);
                Console.WriteLine("  Actual:" + Format(have));
                Console.WriteLine("Expected:" + Format(need));
                return;
            }

        Console.WriteLine("Case " + n + " passed.");
    }

    static bool eq<T>(T a, T b)
    {
        return EqualityComparer<T>.Default.Equals(a, b);
    }

    static bool eq(double a, double b)
    {
        return Math.Abs(a - b) < 1E-9;
    }

    static string FormatObj<T>(T a)
    {
        if (a == null)
            return "<NULL>";

        if (a is string)
            return string.Format("\"{0}\"", a);

        if (a is long)
            return string.Format("{0}L", a);

        return a.ToString();
    }

    static string Format<T>(T[] a)
    {
        if (a == null)
            Console.WriteLine("<NULL>");

        return "{" + string.Join(", ", a.Select(v => FormatObj(v))) + "}";
    }

    static string Format<T>(T[,] a)
    {
        if (a == null)
            Console.WriteLine("<NULL>");

        var str = new StringBuilder();
        for (var i = 0; i < a.GetLength(1); i++)
        {
            for (var j = 0; j < a.GetLength(0); j++)
                str.Append(a[i, j] + "\t");

            str.AppendLine();
        }
        return str.ToString();
    }
    // END CUT HERE
}
