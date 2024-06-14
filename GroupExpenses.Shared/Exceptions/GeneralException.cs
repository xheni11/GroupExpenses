using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GroupExpenses.Shared.Exceptions
{
   internal class GeneralException:SystemException
   {
      public GeneralException() { }
      public GeneralException(string message,SystemException innerException) : base(message,innerException)
      {
      }

   }
}
