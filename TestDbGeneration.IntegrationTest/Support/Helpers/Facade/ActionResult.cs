using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDbGeneration.IntegrationTest.Support.Helpers.Facade
{
    public class ActionResult<T>
    {
        public readonly T Value = default!;

        public readonly List<string> Errors = null;

        public ActionResult(T value) => this.Value = value;

        public bool HasValue => Errors?.Count > 0;

        public ActionResult()
        {
            Errors = new List<string>();
        }
        
        public ActionResult<T> AddErrors(List<string> errors)
        {
            Errors.AddRange(errors);
            return this;
        }

        public ActionResult<T> AddError(string error)
        {
            Errors.Add(error);
            return this;
        }
    }
}
