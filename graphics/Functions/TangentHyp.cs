using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphics.Functions
{
    internal class TangentHyp : IExcFunction
    {
        public double Derivative(double o)
        {
            return 1 - Math.Pow(o, 2);
        }

        public double Execute(double input)
        {
            return (Math.Exp(2 * input) - 1) / (Math.Exp(2 * input) + 1);
        }
    }
}
