using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter a number n to check Amstrong or not");
            int n= int.Parse(Console.ReadLine());
            int n1 = n;
            int sum= 0; int powerValue=0; 
                         
            powerValue=(int)Math.Log(n,10)+1;
            Console.WriteLine("PowerValue is:{0} for Number:{1}", powerValue, n);

            while (n>0)
            {
                int m = n % 10;
                sum = sum + (int)Math.Pow(m, powerValue);
                Console.WriteLine("m is{0}, sum is {1}", m, sum);
                n = n / 10 ;
            }
            if (sum == n1)
                Console.WriteLine("number is Amstrong");
            else Console.WriteLine("number is NOT Amstrong");

        }
    }
}
