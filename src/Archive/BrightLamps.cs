using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class BrightLamps
{
    public int maxBrightness(String init, int[] a, int K)
    {
        var even = new bool[K];
        var min = new int[K];

        for (var i = 0; i < min.Length; i++)
            min[i] = a[i];

        var total = 0;

        for(var i = 0; i < a.Length; i++)
        {
            if (init[i] == '0')
                even[i % K] ^= true;

            min[i % K] = Math.Min(min[i % K], a[i]);
            total += a[i];
        }

        var totalMinOdd = 0;
        var totalMinEven = 0;
        for (var i = 0; i < min.Length; i++)
        {
            if (even[i])
                totalMinEven += min[i];
            else
                totalMinOdd += min[i];
        }

        return total - Math.Min(totalMinEven, totalMinOdd);
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(0, (new BrightLamps()).maxBrightness("011", new int[] { 3, 2, 4 }, 2), 7);
            eq(1, (new BrightLamps()).maxBrightness("0010100101", new int[] { 1, 1, 1, 3, 4, 3, 4, 5, 1, 5 }, 1), 28);
            eq(2, (new BrightLamps()).maxBrightness("1111111111", new int[] { 5, 5, 3, 3, 4, 3, 5, 1, 1, 3 }, 7), 33);
            eq(3, (new BrightLamps()).maxBrightness("0010000001", new int[] { 8, 3, 10, 8, 3, 7, 4, 6, 3, 10 }, 4), 55);
            eq(4, (new BrightLamps()).maxBrightness("00001001010101100001100000010000011001000000001011", new int[] {99, 29, 22, 50, 17, 49, 50, 54, 43, 29, 30, 33, 38, 53, 71, 48, 82, 25, 62, 93, 90, 64, 43, 95, 68,
               35, 79, 11, 13, 47, 51, 44, 35, 55, 4, 34, 7, 10, 25, 38, 29, 58, 36, 34, 77, 90, 37, 58, 20, 20}, 17), 2068);
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
