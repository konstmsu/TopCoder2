using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Marketing
{
    public long howMany(string[] compete)
    {
        var n = compete.Length;
        var colors = new int?[n];
        var adj = new bool[n, n];
        for (var i = 0; i < compete.Length; i++)
        {
            if (compete[i] == "")
                continue;

            var c = compete[i].Split(' ').Select(int.Parse).ToList();
            foreach (var e in c)
            {
                adj[i, e] = true;
                adj[e, i] = true;
            }
        }

        var groupCount = 0;
        for (var i = 0; i < n; i++)
        {
            if (colors[i] != null)
                continue;

            groupCount++;
            var toPaint = new Queue<int>();
            toPaint.Enqueue(i);
            colors[i] = 1;
            while (toPaint.Any())
            {
                var j = toPaint.Dequeue();
                var neiColor = colors[j] % 2 + 1;

                for (var k = 0; k < n; k++)
                {
                    if (adj[j, k])
                    {
                        if (colors[k] == null)
                            toPaint.Enqueue(k);
                        else if (colors[k] != neiColor)
                            return -1;

                        colors[k] = neiColor;
                    }
                }
            }
        }
        return (int)Math.Pow(2, groupCount);
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(0, (new Marketing()).howMany(new string[] { "1 4", "2", "3", "0", "" }), 2L);
            eq(1, (new Marketing()).howMany(new string[] { "1", "2", "0" }), -1L);
            eq(2, (new Marketing()).howMany(new string[] { "1", "2", "3", "0", "0 5", "1" }), 2L);
            eq(3, (new Marketing()).howMany(new string[] {"","","","","","","","","","",
                "","","","","","","","","","",
                "","","","","","","","","",""}), 1073741824L);
            eq(4, (new Marketing()).howMany(new string[] { "1", "2", "3", "0", "5", "6", "4" }), -1L);
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
