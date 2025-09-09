using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    
    class Program
    {
        static string ReverseString(string original)
        {
            return new string(original.ToCharArray().Reverse().ToArray());
        }
        static void Main()
        {
            Console.WriteLine($"12345 -> {ReverseString("12345")}");
            Console.WriteLine($"abcdefgh -> {ReverseString("abcdefgh")}");
            Console.WriteLine($"hafanana -> {ReverseString("hafanana")}");
        }
    }
}
