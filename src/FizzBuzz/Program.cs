using System;
using System.Globalization;

namespace FizzBuzz
{
   class Program
   {
      static void Main(string[] args)
      {
         for (var i = 0; i <= 100; i++)
         {
            Console.WriteLine(fizzbuzzed(i));
         }
         Console.ReadLine();
      }

      static string fizzbuzzed(int number)
      {
         if (number%15 == 0)
            return "FizzBuzz";
         if (number%3 == 0)
            return "Fizz";
         if (number%5 == 0)
            return "Buzz";
         return number.ToString(CultureInfo.InvariantCulture);
      }
   }
}