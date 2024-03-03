using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class FunctionOperatorDetails
    {
        public string FullName { get; }

        public string Acronym { get; }

        public string Description { get; }

        public uint MinArg { get; }

        public uint MaxArg { get; }

        private readonly string[] _usages;

        public IReadOnlyList<string> Usages => _usages;

        public FunctionOperatorDetails(string fullname, string acronym, string description, uint minArg, uint maxArg, string[] usages)
        {
            FullName = fullname;
            Acronym = acronym;
            Description = description;
            MinArg = minArg;
            MaxArg = maxArg;
            _usages = usages;
        }
    }
}
