﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class CosineOperator : FunctionOperator
    {
        private readonly static string _fullname = "cosine";
        private readonly static string _acronym = "cos";
        private readonly static string _description = "Returns the cosine of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "cos(x)"
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

        public CosineOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        /// <summary>
        /// Evaluate the cosine of the given evaluable. Set <see cref="MathEvaluator.AngleAreInDegrees"/> to know how to process the operation.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The cosine of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            double angle = _left.Evaluate(variables);
            return Math.Cos(MathEvaluator.AngleAreInDegrees ? Funcs.DegreesToRadians(angle) : angle);
        }
    }
}
