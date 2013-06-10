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
            Console.WriteLine(Fizzbuzzed(i));
         }
         Console.ReadLine();
      }

      static string Fizzbuzzed(int number)
      {
         var result = "";

         if (number % 3 == 0) result = "Fizz";
         if (number % 5 == 0) result += "Buzz";
         if (result == "") result = number.ToString(CultureInfo.InvariantCulture);
         return result;
      }
   }
}