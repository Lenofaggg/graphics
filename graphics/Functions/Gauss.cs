using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphics.Functions
{
    internal class Gauss : IExcFunction
    {
        public double Derivative(double o)
        {
            return (-2) * (o)*Math.Exp(Math.Pow((-o), 2));
        }

        public double Execute(double input)
        {
            return Math.Exp(Math.Pow((-input), 2));
        }
    }
}
