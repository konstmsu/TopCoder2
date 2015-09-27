using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Hawaiian
{
    public string[] getWords(string sentence)
    {
        var allowed = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'h', 'k', 'l', 'm', 'n', 'p', 'w', 'A', 'E', 'I', 'O', 'U', 'H', 'K', 'L', 'M', 'N', 'P', 'W', };
        return sentence.Split(' ').Where(w => w.All(c => allowed.Contains(c))).ToArray();
    }

    // BEGIN CUT HERE
    public static void Main(String[] args)
    {
        try
        {
            eq(0, (new Hawaiian()).getWords("Hawaii is an island"), new string[] { "Hawaii", "an" });
            eq(1, (new Hawaiian()).getWords("Mauna Kea and Mauna Koa are two mountains"), new string[] { "Mauna", "Kea", "Mauna", "Koa" });
            eq(2, (new Hawaiian()).getWords("Pineapple grows in Hawaii"), new string[] { "Pineapple", "in", "Hawaii" });
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
