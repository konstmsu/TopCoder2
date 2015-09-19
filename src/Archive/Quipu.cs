// BEGIN CUT HERE
// END CUT HERE

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Quipu
{
    public int readKnots(string knots)
    {
        var number = 0;

        var currentDigit = 0;

        for (var i = 1; i < knots.Length; i++)
        {
            var c = knots[i];

            if (c == '-')
            {
                number = number * 10 + currentDigit;
                currentDigit = 0;
            }
            else
            {
                currentDigit++;
            }
        }

        return number;
    }

}

// BEGIN CUT HERE
public class Tests
{
    public void TestAll()
    {
        eq(0, (new Quipu()).readKnots("-XX-XXXX-XXX-"), 243);
        eq(1, (new Quipu()).readKnots("-XX--XXXX---XXX-"), 204003);
        eq(2, (new Quipu()).readKnots("-X-"), 1);
        eq(3, (new Quipu()).readKnots("-X-------"), 1000000);
        eq(4, (new Quipu()).readKnots("-XXXXXXXXX--XXXXXXXXX-XXXXXXXXX-XXXXXXX-XXXXXXXXX-"), 909979);
    }

    public static void Main(String[] args)
    {
        try
        {
            new Tests().TestAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static void eq(int n, object have, object need)
    {
        if (eq(have, need))
        {
            Console.WriteLine("Case " + n + " passed.");
        }
        else
        {
            Console.Write("Case " + n + " failed: expected ");
            print(need);
            Console.Write(", received ");
            print(have);
            Console.WriteLine();
        }
    }
    private static void eq(int n, Array have, Array need)
    {
        if (have == null || have.Length != need.Length)
        {
            Console.WriteLine("Case " + n + " failed: returned " + have.Length + " elements; expected " + need.Length + " elements.");
            print(have);
            print(need);
            return;
        }
        for (int i = 0; i < have.Length; i++)
        {
            if (!eq(have.GetValue(i), need.GetValue(i)))
            {
                Console.WriteLine("Case " + n + " failed. Expected and returned array differ in position " + i);
                print(have);
                print(need);
                return;
            }
        }
        Console.WriteLine("Case " + n + " passed.");
    }
    private static bool eq(object a, object b)
    {
        if (a is double && b is double)
        {
            return Math.Abs((double)a - (double)b) < 1E-9;
        }
        else
        {
            return a != null && b != null && a.Equals(b);
        }
    }
    private static void print(object a)
    {
        if (a is string)
        {
            Console.Write("\"{0}\"", a);
        }
        else if (a is long)
        {
            Console.Write("{0}L", a);
        }
        else
        {
            Console.Write(a);
        }
    }
    private static void print(Array a)
    {
        if (a == null)
        {
            Console.WriteLine("<NULL>");
        }
        Console.Write('{');
        for (int i = 0; i < a.Length; i++)
        {
            print(a.GetValue(i));
            if (i != a.Length - 1)
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine('}');
    }
}
// END CUT HERE
