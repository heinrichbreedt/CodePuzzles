using System;
using System.Globalization;

namespace NumberToEnglish
{
   public static class Extensions
   {
      public static string Ones(this string digit)
      {
         var digt = Convert.ToInt32(digit);
         var name = "";
         switch (digt)
         {
            case 1:
               name = "One";
               break;
            case 2:
               name = "Two";
               break;
            case 3:
               name = "Three";
               break;
            case 4:
               name = "Four";
               break;
            case 5:
               name = "Five";
               break;
            case 6:
               name = "Six";
               break;
            case 7:
               name = "Seven";
               break;
            case 8:
               name = "Eight";
               break;
            case 9:
               name = "Nine";
               break;
         }
         return name;
      }

      public static string Tens(this string digit)
      {
         var digt = Convert.ToInt32(digit);
         string name = null;
         switch (digt)
         {
            case 10:
               name = "Ten";
               break;
            case 11:
               name = "Eleven";
               break;
            case 12:
               name = "Twelve";
               break;
            case 13:
               name = "Thirteen";
               break;
            case 14:
               name = "Fourteen";
               break;
            case 15:
               name = "Fifteen";
               break;
            case 16:
               name = "Sixteen";
               break;
            case 17:
               name = "Seventeen";
               break;
            case 18:
               name = "Eighteen";
               break;
            case 19:
               name = "Nineteen";
               break;
            case 20:
               name = "Twenty";
               break;
            case 30:
               name = "Thirty";
               break;
            case 40:
               name = "Fourty";
               break;
            case 50:
               name = "Fifty";
               break;
            case 60:
               name = "Sixty";
               break;
            case 70:
               name = "Seventy";
               break;
            case 80:
               name = "Eighty";
               break;
            case 90:
               name = "Ninety";
               break;
            default:
               if (digt > 0)
               {
                  name = Tens(digit.Substring(0, 1) + "0") + " " + Ones(digit.Substring(1));
               }
               break;
         }
         return name;
      }

      public static string TranslateCents(this string cents)
      {
         var cts = "";
         for (var i = 0; i < cents.Length; i++)
         {
            var digit = cents[i].ToString(CultureInfo.InvariantCulture);
            var engOne = digit.Equals("0") ? "Zero" : Ones(digit);
            cts += " " + engOne;
         }
         return cts;
      }

      public static string TranslateWholeNumber(this string number)
      {
         var word = "";
         var isDone = false;
         var dblAmt = (Convert.ToDouble(number));

         if (dblAmt > 0)
         {
            var beginsWithZero = number.StartsWith("0");
            var numDigits = number.Length;
            var digitGroup = 0; 
            var digitGroupName = ""; 
            switch (numDigits)
            {
               case 1: //ones' range
                  word = number.Ones();
                  isDone = true;
                  break;
               case 2: //tens' range
                  word = number.Tens();
                  isDone = true;
                  break;
               case 3: //hundreds' range
                  digitGroup = (numDigits % 3) + 1;
                  digitGroupName = " Hundred ";
                  break;
               case 4: //thousands' range
               case 5:
               case 6:
                  digitGroup = (numDigits % 4) + 1;
                  digitGroupName = " Thousand ";
                  break;
               case 7: //millions' range
               case 8:
               case 9:
                  digitGroup = (numDigits % 7) + 1;
                  digitGroupName = " Million ";
                  break;
               case 10: //Billions's range
                  digitGroup = (numDigits % 10) + 1;
                  digitGroupName = " Billion ";
                  break;
                  //add extra case options for anything above Billion...
               default:
                  isDone = true;
                  break;
            }
            if (!isDone)
            {
               word = TranslateWholeNumber(number.Substring(0, digitGroup)) + digitGroupName +
                      TranslateWholeNumber(number.Substring(digitGroup));

               if (beginsWithZero) word = " and " + word.Trim();
            }

            if (word.Trim().Equals(digitGroupName.Trim())) word = "";
         }
         return word.Trim();
      }
   }
}