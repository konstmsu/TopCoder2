using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class BenfordsLaw
{
    public int questionableDigit(int[] transactions, int threshold)
    {
        var digitCounts = new double[10];
        var probabilities = new double[10];

        for (var d = 1; d < 10; d++)
            probabilities[d] = Math.Log10(1 + 1.0 / d);

        for (var i = 0; i < transactions.Length; i++)
        {
            var t = transactions[i];
            var d = t.ToString()[0] - '0';
            digitCounts[d] += 1;
        }

        for (var i = 1; i < digitCounts.Length; i++)
        {
            var prob = digitCounts[i] / transactions.Length;
            if (prob > probabilities[i] * threshold || prob < probabilities[i] / threshold)
                return i;
        }

        return -1;
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(0, (new BenfordsLaw()).questionableDigit(new int[] { 5236,7290,200,1907,3336,9182,17,4209,8746,7932,
                 6375,909,2189,3977,2389,2500,1239,3448,6380,4812 }, 2), 1);
            eq(1, (new BenfordsLaw()).questionableDigit(new int[] { 1, 10, 100, 2, 20, 200, 2000, 3, 30, 300 }, 2), 2);
            eq(2, (new BenfordsLaw()).questionableDigit(new int[] { 9, 87, 765, 6543, 54321, 43219, 321987, 21987, 1987, 345, 234, 123 }, 2), -1);
            eq(3, (new BenfordsLaw()).questionableDigit(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 7, 6, 5, 4, 3, 2, 1 }, 8), 9);
            eq(4, (new BenfordsLaw()).questionableDigit(new int[] { 987,234,1234,234873487,876,234562,17,
                 7575734,5555,4210,678234,3999,8123 }, 3), 8);
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
