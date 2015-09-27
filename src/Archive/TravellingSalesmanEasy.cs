using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class TravellingSalesmanEasy 
{
    public int getMaxProfit(int M, int[] profit, int[] city, int[] visit) 
    {
        var total = 0;
        var sold = new HashSet<int>();
        foreach (var v in visit)
        {
            var sellable = new List<int>();
            for (var i = 0; i < profit.Length; i++)
            {
                if (sold.Contains(i))
                    continue;

                if (city[i] == v)
                    sellable.Add(i);
            }

            if (!sellable.Any())
                continue;

            var bestSellableIndex = sellable.OrderByDescending(i => profit[i]).First();

            sold.Add(bestSellableIndex);
            total += profit[bestSellableIndex];
        }

        return total;
    }
    
// BEGIN CUT HERE
    public static void Main(String[] args) 
    {
        try  
        {
            eq(0,(new TravellingSalesmanEasy()).getMaxProfit(2, new int[] {3,10}, new int[] {1,1}, new int[] {2,1}),10);
            eq(1,(new TravellingSalesmanEasy()).getMaxProfit(1, new int[] {3,5,2,6,4}, new int[] {1,1,1,1,1}, new int[] {1,1,1}),15);
            eq(2,(new TravellingSalesmanEasy()).getMaxProfit(6, new int[] {77,33,10,68,71,50,89}, new int[] {4,1,5,6,2,2,1}, new int[] {6,5,5,6,4}),155);
            eq(3,(new TravellingSalesmanEasy()).getMaxProfit(7, new int[] {22,91,53,7,80,100,36,65,92,93,19,92,95,3,60,50,39,36,2,30,63,84,2}, new int[] {5,3,4,3,6,5,6,6,5,2,7,6,3,2,6,1,7,4,6,3,7,2,5}, new int[] {5,7,1,3,6,2,5,7,3,6,3,2,7,3,1,3,1,7,2,3,1,1,3,1,6,1}),1003);
            eq(4,(new TravellingSalesmanEasy()).getMaxProfit(85, new int[] {94,21,99,27,91,1,64,96,32,39,84,71,97,53,73,20,7,13,33,45,5,85,7,87,
               94,37,48,30,5,85,47,62,91,18,71,37,7,25,75,17,40,19,89,85,86,87,45,
               12,61,71,32,73,63,89,25,51,60,76,32,2,69,78,28,32,74,44,47,11,82,5,
               2,28,54,35,67,44,35,6,70,66,77,7,60,67,33,66,21,91,76,75,16,79,20,24,
               91,31,2,50,11,19,93,49,4,7,55,9,95,39,54,12,48,38,73,100,57,97,44,2,
               2,51,40,4,51,3,95,93,56,88,60,98,67,7,99,46,71,75,24,82,87,29,92,92,
               81,87,34,83,58,46,79,53,38,32,97,41,65,10,54,81,42,37,76,28,11,50,
               13,29,15,99,73,72,2,81,39,75,1,54}, new int[] {72,69,19,25,3,65,10,42,37,76,29,34,41,14,46,46,37,55,30,32,84,57,74,
               16,10,48,67,31,44,84,11,59,67,63,5,31,28,71,3,21,42,21,61,50,5,79,79,
               27,69,33,47,70,76,70,17,73,28,64,77,84,9,6,63,71,17,71,40,9,8,16,76,
               76,6,53,47,10,45,31,78,55,13,35,50,43,32,78,78,44,20,56,24,43,80,62,
               72,16,5,72,67,29,11,51,64,27,7,44,59,1,40,71,64,63,67,81,72,22,73,59,
               21,44,3,18,9,75,72,43,13,44,79,42,58,49,81,44,42,41,35,81,63,74,42,79,
               42,39,45,49,18,73,53,36,80,34,75,57,10,79,79,33,48,18,81,3,69,36,37,
               49,54,29,17,81,83,13,8,69,5,84}, new int[] {39,29,15,5,3,65,29,64,60,21,13,10,73,75,44,84,15,61,26,49,31,27,83,24,
               16,55,60,74,71,53,68,15,75,15,36,4,47,9,77,45,63,32,77,84,8,68,11,5,18,
               80,36,52,42,59,79,83,81,29,43,70,29,19,68,5,83,60,71,66,62,81,85,39,42,
               40,69,60,34,12,2,4,31,36,81,33,71,32,68,5,30,59,61,10,71,49,63,30,62,
               83,35,56,82,2,14,59,68,74,32,31,3,28,38,54,38}),4369);
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
