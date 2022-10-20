using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Core.Validation
{
    public class OperationError
    {
        public int Code { get; private set; }
        public string Description { get; private set; }

        public OperationError(Error error)
        {
            Code = (int)error;
            Description = error.ToString();
        }

        public OperationError(Error error, string description)
        {
            Code = (int)error;
            Description = description;
        }

        public enum Error
        {
            InvalidOperation,
            InvalidValue,
            ValueCannotBeNegative,
            InsufficientFunds
        }
    }
}
