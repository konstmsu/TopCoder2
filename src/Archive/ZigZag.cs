using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class ZigZag 
{
    public int longestZigZag(int[] sequence) 
    {
        var endsUp = new int[sequence.Length];
        var endsDown = new int[sequence.Length];

        endsDown[0] = 1;
        endsUp[0] = 1;

        for (var i = 1; i < sequence.Length; i++)
        {
            for (var j = 0; j < i; j++)
            {
                endsUp[i] = Math.Max(endsUp[i], endsUp[j]);
                endsDown[i] = Math.Max(endsDown[i], endsDown[j]);

                if (sequence[j] < sequence[i])
                    endsUp[i] = Math.Max(endsUp[i], endsDown[j] + 1);
                else if (sequence[j] > sequence[i])
                    endsDown[i] = Math.Max(endsDown[i], endsUp[j] + 1);
            }
        }

        return Math.Max(endsDown[sequence.Length - 1], endsUp[sequence.Length - 1]);
    }
    
// BEGIN CUT HERE
    public static void Main(String[] args) 
    {
        try  
        {
            eq(0,(new ZigZag()).longestZigZag(new int[] { 1, 7, 4, 9, 2, 5 }),6);
            eq(1,(new ZigZag()).longestZigZag(new int[] { 1, 17, 5, 10, 13, 15, 10, 5, 16, 8 }),7);
            eq(2,(new ZigZag()).longestZigZag(new int[] { 44 }),1);
            eq(3,(new ZigZag()).longestZigZag(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }),2);
            eq(4,(new ZigZag()).longestZigZag(new int[] { 70, 55, 13, 2, 99, 2, 80, 80, 80, 80, 100, 19, 7, 5, 5, 5, 1000, 32, 32 }),8);
            eq(5,(new ZigZag()).longestZigZag(new int[] { 374, 40, 854, 203, 203, 156, 362, 279, 812, 955, 
               600, 947, 978, 46, 100, 953, 670, 862, 568, 188, 
               67, 669, 810, 704, 52, 861, 49, 640, 370, 908, 
               477, 245, 413, 109, 659, 401, 483, 308, 609, 120, 
               249, 22, 176, 279, 23, 22, 617, 462, 459, 244 }
               ),36);
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
