using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphics.Functions
{
    class Sigmoid : IExcFunction
    {
        public double Derivative(double o)
        {
            return  o * (1 - o);
        }

        public double Execute(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }
    }
}
