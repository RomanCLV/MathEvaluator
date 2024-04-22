﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework.Operators.Functions
{
    internal class IntegralOperator : FunctionNOperator
    {
        private readonly static string _fullname = "integral";
        private readonly static string _acronym = "int";
        private readonly static string _description = "Returns the integral of the given evaluable. Must give the name of the variable and the boundaries. By default the step is 0.1.";
        private readonly static string[] _usages = new string[3]
        {
            "int(evaluable, var_name, from, to)",
            "int(evaluable, var_name, from, to, step)",
            "int(x^2, x, 1, 10)",
        };
        private readonly static uint _minArg = 4;
        private readonly static uint _maxArg = 5;
        private readonly static FunctionOperatorDetails _details = new FunctionOperatorDetails(_fullname, _acronym, _description, _minArg, _maxArg, _usages);

        public new static string FullName => _fullname;
        public new static string Acronym => _acronym;
        public new static string Description => _description;
        public new static IReadOnlyList<string> Usages => _usages;
        public new static uint MinArg => _minArg;
        public new static uint MaxArg => _maxArg;
        public new static FunctionOperatorDetails Details => _details;

        private readonly string _variableName;
        private readonly IEvaluable _from;
        private readonly IEvaluable _to;
        private readonly IEvaluable _step;
        private readonly IEvaluable[] _dependingEvaluable;

        public IntegralOperator(IEvaluable evaluable, string variableName, IEvaluable from, IEvaluable to) : this(evaluable, variableName, from, to, new ValueOperator(0.1))
        {
        }

        public IntegralOperator(IEvaluable evaluable, string variableName, IEvaluable from, IEvaluable to, IEvaluable step) : base(evaluable)
        {
            _variableName = variableName;
            _from = from;
            _to = to;
            _step = step;
            _dependingEvaluable = new IEvaluable[3]
            {
                _from,
                _to,
                _step
            };
        }

        protected override IEvaluable[] GetDependingEvaluables()
        {
            return _dependingEvaluable;
        }

        public override bool DependsOnVariables(out List<string> variables)
        {
            bool result = false;
            bool fromToStepDependOnVariables = base.DependsOnVariables(out List<string> fromToStepVariables);
            bool expressionDependsOnVariables = _left.DependsOnVariables(out List<string> expressionVariables);
            variables = new List<string>(fromToStepVariables.Count + expressionVariables.Count);

            if (fromToStepDependOnVariables)
            {
                variables.AddRange(fromToStepVariables);
                result = true;
            }
            if (expressionDependsOnVariables)
            {
                expressionVariables.Remove(_variableName);
                variables.AddRange(expressionVariables);
                result |= expressionVariables.Count > 0;
            }
            return result;
        }

        public new static IntegralOperator Create(string[] args)
        {
            if (args.Length < _minArg)
            {
                throw new ArgumentException("Too few arguments in " + _acronym + "()");
            }
            else if (args.Length > _maxArg)
            {
                throw new ArgumentException("Too many arguments in " + _acronym + "()");
            }

            return args.Length == 4 ?
               new IntegralOperator(new Expression().Set(args[0]), args[1], new Expression().Set(args[2]), new Expression().Set(args[3])) :
               new IntegralOperator(new Expression().Set(args[0]), args[1], new Expression().Set(args[2]), new Expression().Set(args[3]), new Expression().Set(args[4]));
        }

        public override double Evaluate(params Variable[] variables)
        {
            double from = _from.Evaluate(variables);
            double to = _to.Evaluate(variables);
            double step = _step.Evaluate(variables);
            if (step < 0.000001)
            {
                throw new InvalidOperationException("The step of sum must be greater than 0.000001. Step was: " + step);
            }
            Variable[] intVariables = new Variable[variables.Length + 1];
            Variable xVar = new Variable("x", 0.0);
            intVariables[0] = xVar;
            for (int i = 0; i < variables.Length; i++)
            {
                intVariables[i + 1] = variables[i];
            }

            double result = 0.0;
            xVar.Value = from;
            double f_x = _left.Evaluate(intVariables);
            double f_x1;
            double lastX = Math.Round(to - step, 6);
            while (xVar.Value <= lastX)
            {
                xVar.Value = Math.Round((double)xVar.Value + step, 6);
                f_x1 = _left.Evaluate(intVariables);

                result += step * (f_x + ((f_x1 - f_x) / 2.0));
                f_x = f_x1;
            }

            return result;
        }

        public override string ToString()
        {
            return _acronym + '(' + _left.ToString() + ", " + _variableName + ", " + string.Join<IEvaluable>(", ", _dependingEvaluable) + ')';
        }
    }
}
