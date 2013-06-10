using System;

namespace NumberToEnglish
{
   internal class Program
   {
      static void Main(string[] args)
      {
         var translator = new ChequeWriter();

         Console.WriteLine(translator.ChangeNumericToWords(23.46));
         Console.WriteLine(translator.ChangeNumericToWords(23.46, true));
         Console.WriteLine(translator.ChangeNumericToWords(456798.01));
         Console.WriteLine(translator.ChangeNumericToWords(456798.01, true));
         Console.ReadLine();
      }
   }
}