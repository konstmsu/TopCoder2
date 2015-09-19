using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class ContestScore
{
    public string[] sortResults(string[] data)
    {
        var groups = new List<string>();
        var scores = new List<decimal[]>();

        foreach (var d in data)
        {
            var parts = d.Split(' ');
            groups.Add(parts[0]);
            scores.Add(parts.Skip(1).Select(p => decimal.Parse(p)).ToArray());
        }

        var groupCount = groups.Count;

        if (groupCount == 0)
            return new string[0];

        var judgeCount = scores[0].Length;

        var totalRanking = new decimal[groupCount];

        for (var judge = 0; judge < judgeCount; judge++)
        {
            var indicies = Enumerable.Range(0, groups.Count).OrderByDescending(g => scores[g][judge]).ToList();

            var ranks = new int[groupCount];
            for (var j = 0; j < groupCount; j++)
            {
                int r;

                if (j > 0 && scores[indicies[j]][judge] == scores[indicies[j - 1]][judge])
                    r = ranks[indicies[j - 1]];
                else
                    r = j + 1;

                ranks[indicies[j]] = r;
            }

            for (var i = 0; i < groupCount; i++)
                totalRanking[i] += ranks[i];
        }

        var totalScore = scores.Select(s => s.Sum()).ToList();

        var sortedIndicies = Enumerable.Range(0, groupCount).OrderBy(g => totalRanking[g]).ThenByDescending(g => totalScore[g]).ThenBy(g => groups[g]);

        return sortedIndicies.Select(i => string.Format("{0} {1} {2:0.0}", groups[i], totalRanking[i], totalScore[i])).ToArray();
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(0, (new ContestScore()).sortResults(new string[] {"A 90.7 92.9 87.4",
                "B 90.5 96.6 88.0",
                "C 92.2 91.0 95.3"}), new string[] { "C 5 278.5", "B 6 275.1", "A 7 271.0" });
            eq(1, (new ContestScore()).sortResults(new string[] {"STANFORD 85.3 90.1 82.6 84.6 96.6 94.5 87.3 90.3",
                "MIT 95.5 83.9 80.4 85.5 98.7 98.3 96.7 82.7",
                "PRINCETON 99.2 79.1 87.6 85.1 93.6 96.4 86.0 90.6",
                "HARVARD 83.6 92.0 85.5 94.3 97.5 91.5 92.5 83.0",
                "YALE 99.5 92.6 86.2 82.0 96.4 92.6 84.5 78.6",
                "COLUMBIA 97.2 87.6 81.7 93.7 88.0 86.3 95.9 89.6",
                "BROWN 92.2 95.8 92.1 81.5 89.5 87.0 95.5 86.4",
                "PENN 96.3 80.7 81.2 91.6 85.8 92.2 83.9 87.8",
                "CORNELL 88.0 83.7 85.0 83.8 99.8 92.1 91.0 88.9"}), new string[] { "PRINCETON 34 717.6", "MIT 36 721.7", "HARVARD 38 719.9", "COLUMBIA 39 720.0", "STANFORD 39 711.3", "YALE 40 712.4", "BROWN 41 720.0", "CORNELL 42 712.3", "PENN 51 699.5" });
            eq(2, (new ContestScore()).sortResults(new string[] { }), new string[] { });
            eq(3, (new ContestScore()).sortResults(new string[] {"AA 90.0 80.0 70.0 60.0 50.0 40.0",
                "BBB 80.0 70.0 60.0 50.0 40.0 90.0",
                "EEE 70.0 60.0 50.0 40.0 90.0 80.0",
                "AAA 60.0 50.0 40.0 90.0 80.0 70.0",
                "DDD 50.0 40.0 90.0 80.0 70.0 60.0",
                "CCC 40.0 90.0 80.0 70.0 60.0 50.0"}), new string[] { "AA 21 390.0", "AAA 21 390.0", "BBB 21 390.0", "CCC 21 390.0", "DDD 21 390.0", "EEE 21 390.0" });
            eq(4, (new ContestScore()).sortResults(new string[] { "A 00.1", "B 05.2", "C 29.0", "D 00.0" }), new string[] { "C 1 29.0", "B 2 5.2", "A 3 0.1", "D 4 0.0" });

            eq(-1, (new ContestScore()).sortResults(new string[] { "YGAAMUIPEO 80.0 77.0 88.0 81.0 79.0 75.0 86.0 99.0", "KKOGNBMDSA 77.0 82.0 81.0 80.0 76.0 95.0 79.0 82.0", "RJIXYEZDLS 77.0 90.0 91.0 77.0 88.0 80.0 87.0 96.0", "MORHOTFLTM 90.0 79.0 83.0 92.0 83.0 75.0 78.0 98.0", "TBJFTUTOPU 78.0 91.0 97.0 92.0 93.0 90.0 86.0 83.0", "WCIROTOTYU 94.0 89.0 88.0 89.0 81.0 87.0 90.0 89.0", "DCLUYMLIZR 93.0 91.0 84.0 76.0 77.0 88.0 79.0 87.0", "UPHULPWNRA 75.0 78.0 75.0 86.0 81.0 88.0 77.0 88.0", "WLOXFZTVJR 75.0 88.0 88.0 82.0 99.0 95.0 78.0 75.0", "GMDZMBCJFH 85.0 76.0 75.0 87.0 84.0 91.0 95.0 90.0", "AGSJZQVHJY 77.0 86.0 93.0 95.0 96.0 82.0 98.0 94.0", "SHGAOZGCQX 84.0 86.0 96.0 85.0 79.0 75.0 93.0 97.0", "ELTQJSPXWA 85.0 99.0 90.0 81.0 80.0 82.0 78.0 98.0", "GOCWOFAKKN 79.0 80.0 86.0 83.0 94.0 91.0 93.0 76.0", "MLRILHNKQX 83.0 98.0 78.0 94.0 98.0 93.0 94.0 82.0", "JYNWLNUKTG 96.0 93.0 78.0 96.0 86.0 82.0 96.0 97.0", "DQBTOTUPSL 97.0 91.0 90.0 91.0 95.0 77.0 97.0 86.0", "QXNLDTBGLF 95.0 99.0 94.0 90.0 94.0 93.0 77.0 94.0", "SXKNIEFWJV 88.0 85.0 91.0 83.0 93.0 79.0 79.0 77.0", "EGVPNZEJGL 75.0 76.0 81.0 96.0 86.0 80.0 92.0 81.0" }),  
                new string[] { "QXNLDTBGLF 49 736.0", "JYNWLNUKTG 52 724.0", "AGSJZQVHJY 55 721.0", "DQBTOTUPSL 56 724.0", "MLRILHNKQX 60 720.0", "TBJFTUTOPU 64 710.0", "WCIROTOTYU 74 707.0", "ELTQJSPXWA 77 693.0", "SHGAOZGCQX 80 695.0", "RJIXYEZDLS 86 686.0", "GMDZMBCJFH 86 683.0", "GOCWOFAKKN 88 682.0", "WLOXFZTVJR 90 680.0", "MORHOTFLTM 90 678.0", "SXKNIEFWJV 92 675.0", "DCLUYMLIZR 95 675.0", "EGVPNZEJGL 102 667.0", "YGAAMUIPEO 102 665.0", "KKOGNBMDSA 111 652.0", "UPHULPWNRA 117 648.0" });
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
