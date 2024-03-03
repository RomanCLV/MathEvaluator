using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class RadianOperator : FunctionOperator
    {
        private readonly static string _fullname = "radian";
        private readonly static string _acronym = "rad";
        private readonly static string _description = "Convert the value of the given evaluable (as degrees) into radians.";
        private readonly static string[] _usages = new string[1]
        {
            "rad(x)"
        };
        private readonly static uint _minArg = 1;
        private readonly static uint _maxArg = 1;
        private readonly static FunctionOperatorDetails _details = new FunctionOperatorDetails(_fullname, _acronym, _description, _minArg, _maxArg, _usages);

        public new static string FullName => _fullname;
        public new static string Acronym => _acronym;
        public new static string Description => _description;
        public new static IReadOnlyList<string> Usages => _usages;
        public new static uint MinArg => _minArg;
        public new static uint MaxArg => _maxArg;
        public new static FunctionOperatorDetails Details => _details;

        public RadianOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Convert the value of the given evaluable (as degrees) into radians.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The value of the given evaluable (as degrees) into radians.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.DegreesToRadians(_left.Evaluate(variables));
        }
    }
}
