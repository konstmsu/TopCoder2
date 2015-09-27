using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class PipePath
{
    public double capCost(string[] caps, string[] costs, int source, int sink)
    {
        var n = caps.Length;

        var ca = new int?[n, n];
        var co = new int?[n, n];

        for (var i = 0; i < n; i++)
        {
            if (caps[i] == "")
                continue;

            foreach (var cap in caps[i].Split(' '))
            {
                var parts = cap.Split(',').Select(int.Parse).ToList();
                ca[i, parts[0]] = parts[1];
            }

            foreach (var cost in costs[i].Split(' '))
            {
                var parts = cost.Split(',').Select(int.Parse).ToList();
                co[i, parts[0]] = parts[1];
            }
        }

        var mins = new int?[n];
        var sums = new int?[n];
        var bests = new double?[n];
        var from = new int?[n];

        var changed = true;

        while (changed)
        {
            changed = false;
            for (var s = 0; s < n; s++)
            {
                if (s == sink)
                    continue;

                for (var k = 0; k < n; k++)
                {
                    if (!co[s, k].HasValue)
                        continue;

                    var target = k;

                    if (target == source)
                        continue;

                    var min = Math.Min(ca[s, target].Value, mins[s] ?? int.MaxValue);
                    var sum = co[s, target] + (sums[s] ?? 0);
                    var coeff = min / (double)sum;
                    if (!bests[target].HasValue || bests[target] < coeff)
                    {
                        mins[target] = min;
                        sums[target] = sum;
                        bests[target] = coeff;
                        from[target] = s;
                        changed = true;
                    }
                }
            }
        }

        var seq = new List<int>();
        for (int? f = sink; f != null; f = from[f.Value])
        {
            seq.Insert(0, f.Value);
        }

        int sum2 = 0;
        int min2 = int.MaxValue;

        for (var j = 0; j < seq.Count - 1; j++)
        {
            sum2 += co[seq[j], seq[j + 1]].Value;
            min2 = Math.Min(min2, ca[seq[j], seq[j + 1]].Value);
        }
        var coeff2 = min2 / (double)sum2;

        return bests[sink] ?? 0;
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(-2, (new PipePath()).capCost(new[] {
                "1,68 9,87 7,62 9,70 7,22 8,78 8,43 3,63 5,16 2,24",
                "7,70 7,39 9,79 4,78 9,59 0,64 5,35 2,20 7,40 9,31",
                "6,35 9,18 9,78 8,83 9,67 6,13 8,48 5,22 3,87 3,82",
                "5,74 9,64 5,87 5,1 7,20 4,24 4,95 0,64 9,56 9,82",
                "7,81 7,10 5,7 6,36 9,34 5,48 8,17 3,75 8,88 8,35",
                "3,66 9,53 7,52 3,13 9,88 4,94 3,69 2,87 1,35 6,21",
                "1,33 8,96 1,11 3,44 8,25 7,32 8,46 0,20 3,10 7,49",
                "3,10 4,10 0,23 8,25 3,33 0,43 4,23 2,87 0,55 5,9",
                "5,60 1,34 7,40 2,29 0,21 6,30 4,66 3,63 1,22 7,30",
                "7,44 7,69 2,40 4,78 3,8 0,48 7,20 7,24 0,22 2,1" },
                new[]{
                    "1,69 9,70 7,51 9,30 7,88 8,10 8,44 3,27 5,53 2,75",
                    "7,87 7,58 9,38 4,99 9,93 0,54 5,82 2,42 7,73 9,70",
                    "6,9 9,33 9,5 8,6 9,23 6,58 8,12 5,32 3,40 3,61",
                    "5,11 9,6 5,23 5,8 7,20 4,49 4,25 0,90 9,23 9,95",
                    "7,79 7,62 5,74 6,84 9,73 5,50 8,92 3,97 8,84 8,89",
                    "3,12 9,82 7,93 3,37 9,27 4,70 3,78 2,32 1,44 6,84",
                    "1,22 8,5 1,27 3,13 8,27 7,60 8,41 0,34 3,13 7,28",
                    "3,1 4,89 0,90 8,64 3,49 0,88 4,39 2,22 0,48 5,25",
                    "5,93 1,79 7,78 2,53 0,71 6,98 4,49 3,86 1,13 7,74",
                    "7,12 7,72 2,25 4,19 3,46 0,19 7,11 7,51 0,1 2,78" }, 9, 3), 1.0434782608695652);

            eq(-1, (new PipePath()).capCost(
                new[] { "1,10", "2,11", "3,12", "4,13", "5,14", "6,15", "7,16", "8,17", "9,18", "10,19", "11,110", "12,111", "13,112", "14,113", "15,114", "16,115", "17,116", "18,117", "19,118", "20,119", "" },
                new[] { "1,10", "2,11", "3,12", "4,13", "5,14", "6,15", "7,16", "8,17", "9,18", "10,19", "11,110", "12,111", "13,112", "14,113", "15,114", "16,115", "17,116", "18,117", "19,118", "20,119", "" }, 20, 0), 0.0);

            eq(0, (new PipePath()).capCost(new string[] { "1,10 2,9", "", "1,100" }, new string[] { "1,100 2,50", "", "1,50" }, 0, 1), 0.1);
            eq(1, (new PipePath()).capCost(new string[] {"1,3 3,5","3,2 2,6","3,2","1,1 2,4 4,6",
                "0,3 2,7"}, new string[] {"1,3 3,5","3,2 2,6","3,2","1,1 2,4 4,6",
                "0,3 2,7"}, 0, 4), 0.45454545454545453);
            eq(2, (new PipePath()).capCost(new string[] { "1,15 1,20", "2,10", "" }, new string[] { "1,10 1,10", "2,15", "" }, 0, 2), 0.4);
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
