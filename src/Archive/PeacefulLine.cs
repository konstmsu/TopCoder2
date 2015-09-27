using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class PeacefulLine 
{
    public string makeLine(int[] x) 
    {
        if (!x.Any())
            return "possible";

        var counts = new int[26];

        foreach (var i in x)
            counts[i]++;

        var largestGroup = counts.Max();

        if (largestGroup > (x.Length + 1) / 2)
            return "impossible";

        return "possible";
    }
    
// BEGIN CUT HERE
    public static void Main(String[] args) 
    {
        try  
        {
            eq(0,(new PeacefulLine()).makeLine(new int[] {1,2,3,4}),"possible");
            eq(1,(new PeacefulLine()).makeLine(new int[] {1,1,1,2}),"impossible");
            eq(2,(new PeacefulLine()).makeLine(new int[] {1,1,2,2,3,3,4,4}),"possible");
            eq(3,(new PeacefulLine()).makeLine(new int[] {3,3,3,3,13,13,13,13}),"possible");
            eq(4,(new PeacefulLine()).makeLine(new int[] {3,7,7,7,3,7,7,7,3}),"impossible");
            eq(5,(new PeacefulLine()).makeLine(new int[] {25,12,3,25,25,12,12,12,12,3,25}),"possible");
            eq(6,(new PeacefulLine()).makeLine(new int[] {3,3,3,3,13,13,13,13,3}),"possible");
        } 
        catch( Exception ex)  
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
