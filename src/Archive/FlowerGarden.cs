using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class FlowerGarden
{
    class Comparer : IComparer<int>
    {
        int[] _height;
        int[] _bloom;
        int[] _wilt;

        public Comparer(int[] height, int[] bloom, int[] wilt)
        {
            _height = height;
            _bloom = bloom;
            _wilt = wilt;
        }

        public int Compare(int x, int y)
        {
            if (x == y)
                return 0;

            if (_bloom[x] <= _wilt[y] && _bloom[y] <= _wilt[x])
                if (_height[x] > _height[y])
                    return 1;
                else
                    return -1;

            return 0;
        }
    }

    public int[] getOrdering(int[] height, int[] bloom, int[] wilt)
    {
        var n = height.Length;

        var before = Enumerable.Range(0, n).Select(_ => new HashSet<int>()).ToList();

        for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                if (bloom[i] <= wilt[j] && bloom[j] <= wilt[i])
                    if (height[i] > height[j])
                        before[i].Add(j);

        var result = new List<int>();

        for (;;)
        {
            var candidates = new HashSet<int>();

            for (var i = 0; i < n; i++)
            {
                if (!result.Contains(i) && !before[i].Any())
                    candidates.Add(i);
            }

            if (!candidates.Any())
                break;

            var best = candidates.OrderByDescending(c => height[c]).First();

            before.ForEach(s => s.Remove(best));

            result.Add(best);
        }

        return result.Select(c => height[c]).ToArray();
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(0, (new FlowerGarden()).getOrdering(new int[] { 5, 4, 3, 2, 1 }, new int[] { 1, 1, 1, 1, 1 }, new int[] { 365, 365, 365, 365, 365 }), new int[] { 1, 2, 3, 4, 5 });
            eq(1, (new FlowerGarden()).getOrdering(new int[] { 5, 4, 3, 2, 1 }, new int[] { 1, 5, 10, 15, 20 }, new int[] { 4, 9, 14, 19, 24 }), new int[] { 5, 4, 3, 2, 1 });
            eq(2, (new FlowerGarden()).getOrdering(new int[] { 5, 4, 3, 2, 1 }, new int[] { 1, 5, 10, 15, 20 }, new int[] { 5, 10, 15, 20, 25 }), new int[] { 1, 2, 3, 4, 5 });
            eq(3, (new FlowerGarden()).getOrdering(new int[] { 5, 4, 3, 2, 1 }, new int[] { 1, 5, 10, 15, 20 }, new int[] { 5, 10, 14, 20, 25 }), new int[] { 3, 4, 5, 1, 2 });
            eq(4, (new FlowerGarden()).getOrdering(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 3, 1, 3, 1, 3 }, new int[] { 2, 4, 2, 4, 2, 4 }), new int[] { 2, 4, 6, 1, 3, 5 });
            eq(5, (new FlowerGarden()).getOrdering(new int[] { 3, 2, 5, 4 }, new int[] { 1, 2, 11, 10 }, new int[] { 4, 3, 12, 13 }), new int[] { 4, 5, 2, 3 });
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
