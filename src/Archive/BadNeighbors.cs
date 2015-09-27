using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class BadNeighbors 
{
    public int maxDonations(int[] donations) 
    {
        return Math.Max(maxDonations(donations, 0, donations.Length - 1), maxDonations(donations, 1, donations.Length));
    }

    int maxDonations(int[] donations, int startAt, int count)
    {
        var n = donations.Length;
        var max = new int[n];

        max[startAt] = donations[startAt];

        for (var i = startAt + 1; i < count; i++)
        {
            if (i >= 2)
                max[i] = Math.Max(max[i - 1], max[i - 2] + donations[i]);
            else
                max[i] = max[i - 1];
        }

        return max[count - 1];
    }

    // BEGIN CUT HERE
    public static void Main(String[] args) 
    {
        try  
        {
            eq(0,(new BadNeighbors()).maxDonations(new int[]  { 10, 3, 2, 5, 7, 8 }),19);
            eq(1,(new BadNeighbors()).maxDonations(new int[] { 11, 15 }),15);
            eq(2,(new BadNeighbors()).maxDonations(new int[] { 7, 7, 7, 7, 7, 7, 7 }),21);
            eq(3,(new BadNeighbors()).maxDonations(new int[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 }),16);
            eq(4,(new BadNeighbors()).maxDonations(new int[] { 94, 40, 49, 65, 21, 21, 106, 80, 92, 81, 679, 4, 61,  
                 6, 237, 12, 72, 74, 29, 95, 265, 35, 47, 1, 61, 397,
                 52, 72, 37, 51, 1, 81, 45, 435, 7, 36, 57, 86, 81, 72 }),2926);
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
