using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class $CLASSNAME$ 
{
    public $RC$ $METHODNAME$($METHODPARMS$) 
    {
    }
    
$BEGINCUT$
    public static void Main(String[] args) 
    {
        try  
        {
$MAINBODY$
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

    static void print<T>(T[,] a)
    {
        if (a == null)
            Console.WriteLine("<NULL>");

        var str = new StringBuilder();
        for (var i = 0; i < a.GetLength(1); i++)
        {
            for (var j = 0; j < a.GetLength(0); j++)
                str.Append(a[i, j] + "\t");

            str.AppendLine();
        }
        Console.WriteLine(str);
    }
$ENDCUT$
}