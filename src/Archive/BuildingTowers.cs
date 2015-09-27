using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class BuildingTowers
{
    public long maxHeight(int N, int K, int[] x1, int[] t1)
    {
        var tallestX = new List<int> { N - 1 };
        var tallestH = new List<long> { (N - 1) * (long)K };

        for (var i = 0; i < x1.Length; i++)
            x1[i]--;

        var x = new[] { 0 }.Concat(x1).ToArray();
        var t = new[] { 0 }.Concat(t1).ToArray();

        var interestingPoints = new List<long> { N - 1 };

        for (var i = 0; i < x.Length; i++)
            for (var j = i + 1; j < x.Length; j++)
            {
                var xl = (x[i] + x[j]) / 2.0 + (t[j] - t[i]) / 2.0 / K;
                var xr = (x[i] + x[j]) / 2.0 + (t[i] - t[j]) / 2.0 / K;

                if (xl >= x[i] && xl <= x[j])
                {
                    interestingPoints.Add((int)Math.Floor(xl));
                    interestingPoints.Add((int)Math.Ceiling(xl));
                }
                if (xr >= x[i] && xr <= x[j])
                {
                    interestingPoints.Add((int)Math.Floor(xr));
                    interestingPoints.Add((int)Math.Ceiling(xr));
                }
            }

        interestingPoints = interestingPoints.Distinct().ToList();

        var interestingHeights = interestingPoints.Select(p => p * K).ToList();
        for (var i = 0; i < x.Length; i++)
        {
            for (var j = 0; j < interestingPoints.Count; j++)
            {
                var limitedHeight = t[i] + Math.Abs(x[i] - interestingPoints[j]) * K;
                interestingHeights[j] = Math.Min(interestingHeights[j], limitedHeight);
            }
        }

        return interestingHeights.Max();
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(-1, (new BuildingTowers()).maxHeight(3, 1000, new int[] { 3 }, new int[] { 1 }), 1000L);
            eq(0, (new BuildingTowers()).maxHeight(10, 1, new int[] { 3, 8 }, new int[] { 1, 1 }), 3L);
            eq(1, (new BuildingTowers()).maxHeight(1000000000, 1000000000, new int[] { }, new int[] { }), 999999999000000000L);
            eq(2, (new BuildingTowers()).maxHeight(20, 3, new int[] { 4, 7, 13, 15, 18 }, new int[] { 8, 22, 1, 55, 42 }), 22L);
            eq(3, (new BuildingTowers()).maxHeight(780, 547990606, new int[] { 34, 35, 48, 86, 110, 170, 275, 288, 313, 321, 344, 373, 390, 410, 412, 441, 499, 525, 538, 568, 585, 627, 630, 671, 692, 699, 719, 752, 755, 764, 772 }, new int[] { 89, 81, 88, 42, 55, 92, 19, 91, 71, 42, 72, 18, 86, 89, 88, 75, 29, 98, 63, 74, 45, 11, 68, 34, 94, 20, 69, 33, 50, 69, 54 }), 28495511604L);
            eq(4, (new BuildingTowers()).maxHeight(7824078, 2374, new int[] {134668,488112,558756,590300,787884,868112,1550116,1681439,1790994,
               1796091,1906637,2005485,2152813,2171716,2255697,2332732,2516853,
               2749024,2922558,2930163,3568190,3882735,4264888,5080550,5167938,
               5249191,5574341,5866912,5936121,6142348,6164177,6176113,6434368,
               6552905,6588059,6628843,6744163,6760794,6982172,7080464,7175493,
               7249044}, new int[] {8,9,171315129,52304509,1090062,476157338,245,6,253638067,37,500,
               29060,106246500,129,22402,939993108,7375,2365707,40098,10200444,
               3193547,55597,24920935,905027,1374,12396141,525886,41,33,3692,
               11502,180,3186,5560,778988,42449532,269666,10982579,48,3994,1,9}), 1365130725L);
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
