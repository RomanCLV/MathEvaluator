using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEvaluatorNetFramework
{
    internal interface IEvaluable
    {
        double Evaluate(params Variable[] variables);

        bool DependsOnVariables(out List<string> variables);
    }
}
