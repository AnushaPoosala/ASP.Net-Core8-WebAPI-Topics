using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        public static int FindGCD(int n1, int n2, int max, int min)
        {
            //if (min == max) return min;
            //while(max%min!=0)
            //{
            //    int temp = min;
            //    min = max % min;
            //    max = temp;
            //}
            //return min;
            while (n1 > 0 && n2 > 0)
            {
                if (n1 > n2)
                {
                    n1 %= n2;
                }
                else
                {
                    n2 %= n1;
                }
            }
            return (n1 == 0) ? n2 : n1;
        }

        static void Main(string[] args)
        {
            Console.Write("enter an integer a: ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("enter an integer b: ");
            int b = int.Parse(Console.ReadLine());

            int max = (a > b) ? a: b;
            int min = (a < b) ? a : b;
                       
            int c=Program.FindGCD(a, b, min, max);
            Console.WriteLine("GCD Createst Common Divisor of {0},{1} is:{2}", a, b,c);
            Console.ReadLine();


        }
    }
}
