﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class HyperbolicTangentOperator : FunctionOperator
    {
        private readonly static string _fullname = "hyperbolic tangent";
        private readonly static string _acronym = "tanh";
        private readonly static string _description = "Returns the hyperbolic tangent (defined as tanh(x) = sinh(x) / cosh(x)) of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "tanh(x)"
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

        public HyperbolicTangentOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the hyperbolic tangent (defined as <c>tanh(x) = sinh(x) / cosh(x)</c>) of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The hyperbolic tangent of the given evaluable using <see cref="Funcs.Tanh(double)"/>.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Funcs.Tanh(_left.Evaluate(variables));
        }
    }
}
