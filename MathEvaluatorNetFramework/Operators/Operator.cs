﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathEvaluatorNetFramework.Exceptions;

namespace MathEvaluatorNetFramework.Operators
{
    internal abstract class Operator : IEvaluable
    {
        protected readonly IEvaluable _left;

        public Operator(IEvaluable left)
        {
            _left = left;
        }

        /// <summary>
        /// Evaluate one or more evaluable entities together. Can raise exceptions depending on <see cref="MathEvaluator.Parameters.RaiseDivideByZeroException"/> and <see cref="MathEvaluator.Parameters.RaiseDomainException"/>.
        /// </summary>
        /// <param name="variables"></param>
        /// <returns>The result provided by the operations between the evalubale entities. Can also return <see cref="double.NaN"/>, <see cref="double.PositiveInfinity"/> and <see cref="double.NegativeInfinity"/> depending on <see cref="MathEvaluator.Parameters.RaiseDivideByZeroException"/> and <see cref="MathEvaluator.Parameters.RaiseDomainException"/>.</returns>
        /// <exception cref="DomainException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        public abstract double Evaluate(params Variable[] variables);

        public virtual bool DependsOnVariables(out List<string> variables)
        {
            return _left.DependsOnVariables(out variables);
        }

        public abstract override string ToString();
    }
}
