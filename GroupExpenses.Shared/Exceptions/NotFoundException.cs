using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GroupExpenses.Shared.Exceptions
{
   public class NotFoundException:SystemException
   {
      public NotFoundException(string message) : base(message)
      {
      }

      public NotFoundException(string message,SystemException innerException) : base(message,innerException)
      {
      }

      public NotFoundException(string name,object key)
          : base($"Entity '{name}' (Id: {key}) was not found.")
      {
      }
   }
}
