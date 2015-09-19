using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class SuperRot 
{
    public string decoder(string message) 
    {
        var result = new StringBuilder();

        foreach(var c in message)
        {
            int r;

            if (c >= 'a' && c < 'n')
                r = c - 'a' + 'n';
            else if (c >= 'n' && c <= 'z')
                r = c - 'n' + 'a';

            else if (c >= 'A' && c < 'N')
                r = c - 'A' + 'N';
            else if (c >= 'N' && c <= 'Z')
                r = c - 'N' + 'A';

            else if (c >= '0' && c < '5')
                r = c - '0' + '5';
            else if (c >= '5' && c <= '9')
                r = c - '5' + '0';
            else
                r = c;

            result.Append((char)r);
        }

        return result.ToString();
    }
    
// BEGIN CUT HERE
    public static void Main(String[] args) 
    {
        try  
        {
            eq(0,(new SuperRot()).decoder("Uryyb 28"),"Hello 73");
            eq(1,(new SuperRot()).decoder("GbcPbqre"),"TopCoder");
            eq(2,(new SuperRot()).decoder(""),"");
            eq(3,(new SuperRot()).decoder("5678901234"),"0123456789");
            eq(4,(new SuperRot()).decoder("NnOoPpQqRr AaBbCcDdEe"),"AaBbCcDdEe NnOoPpQqRr");
            eq(5,(new SuperRot()).decoder("Gvzr vf 54 71 CZ ba Whyl 4gu bs gur lrne 7558 NQ"),"Time is 09 26 PM on July 9th of the year 2003 AD");
            eq(6,(new SuperRot()).decoder("Gur dhvpx oebja sbk whzcf bire n ynml qbt"),"The quick brown fox jumps over a lazy dog");
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
