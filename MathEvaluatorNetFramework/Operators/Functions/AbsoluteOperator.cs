﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class AbsoluteOperator : FunctionOperator
    {
        private readonly static string _fullname = "absolute";
        private readonly static string _acronym = "abs";
        private readonly static string _description = "Returns the absolute value of the given evaluable.";
        private readonly static string[] _usages = new string[1]
        {
            "abs(x)"
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

        public AbsoluteOperator(IEvaluable evaluable) : base(evaluable)
        {
        }

        public new static AbsoluteOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }
            return new AbsoluteOperator(new Expression().Set(args[0]));
        }

        /// <summary>
        /// Evaluate the absolute value of the given evaluable.
        /// </summary>
        /// <param name="variables">The used variables in the evaluable entities.</param>
        /// <returns>The absolute value of the given evaluable.</returns>
        public override double Evaluate(params Variable[] variables)
        {
            return Math.Abs(_left.Evaluate(variables));
        }

        public override string ToString()
        {
            return _acronym + '(' + _left.ToString() + ')';
        }
    }
}
