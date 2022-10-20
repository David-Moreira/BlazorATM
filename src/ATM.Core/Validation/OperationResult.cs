using System;
using System.Collections.Generic;
using System.Linq;

namespace ATM.Core.Validation
{
    public class OperationResult
    {
        private List<OperationError> _errors { get; set; }

        //
        // Summary:
        //     Failure constructor that takes error messages
        //
        // Parameters:
        //   errors:
        public OperationResult(params OperationError[] errors)
        {
            Succeeded = false;
            _errors = errors.ToList();
        }

        //
        // Summary:
        //     Failure constructor that takes error messages
        //
        // Parameters:
        //   errors:
        public OperationResult(IEnumerable<OperationError> errors)
        {
            Succeeded = false;
            _errors = errors.ToList();
        }

        //
        // Summary:
        //     Constructor that takes whether the result is successful
        //
        // Parameters:
        //   success:
        public OperationResult(bool success)
        {
            Succeeded = success;
        }

        //
        // Summary:
        //     List of errors
        public IEnumerable<OperationError> Errors => _errors;

        //
        // Summary:
        //     True if the operation was successful
        public bool Succeeded { get; private set; }

        /// <summary>
        /// Adds errors to a current instance and marks as unsuccessful operation if not empty.
        /// </summary>
        /// <param name="errors"></param>
        public void AddErrors(params OperationError[] errors)
        {
            if (errors.Count() > 0)
                Succeeded = false;

            if (Errors == null)
                _errors = errors.ToList();
            else
                _errors.AddRange(errors);
        }

        //
        // Summary:
        //     Static success result
        public static OperationResult Success() { return new OperationResult(true); }

        //
        // Summary:
        //     Failed helper method
        //
        // Parameters:
        //   errors:
        public static OperationResult Failed(params OperationError[] errors)
        {
            return new OperationResult(errors);
        }

        public override string ToString()
        {
            return Succeeded ? "Success" : String.Join(",", Errors.Select(x => x.Description));
        }
    }
}