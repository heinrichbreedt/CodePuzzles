using System;
using System.Globalization;

namespace NumberToEnglish
{
   public class ChequeWriter
   {
      public string ChangeNumericToWords(double numb, bool useChequeTerminology = false)
      {
         var num = numb.ToString(CultureInfo.InvariantCulture);
         return ChangeToWords(num, useChequeTerminology);
      }

      static string ChangeToWords(string numb, bool useChequeTerminology)
      {
         var wholeNo = numb;
         string andStr = "", pointStr = "";
         var endStr = (useChequeTerminology) ? ("Only") : ("");
         var decimalPlace = numb.IndexOf(".", StringComparison.Ordinal);
         if (decimalPlace > 0)
         {
            wholeNo = numb.Substring(0, decimalPlace);
            var points = numb.Substring(decimalPlace + 1);
            if (Convert.ToInt32(points) > 0)
            {
               andStr = (useChequeTerminology) ? ("and") : ("point");
               endStr = (useChequeTerminology) ? ("Cents " + endStr) : ("");
               pointStr = points.TranslateCents();
            }
         }
         var val = string.Format("{0} {1}{2} {3}", wholeNo.TranslateWholeNumber().Trim(), andStr, pointStr, endStr);
         return val;
      }
   }
}