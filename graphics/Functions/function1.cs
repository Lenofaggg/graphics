using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphics.Functions
{
    internal class function1 : IExcFunction
    {
        public double Derivative(double o)
        {
            throw new NotImplementedException();
        }

        public double Execute(double input)
        {
             //return Math.Sqrt(input);
            return Math.Pow(input,2);
            //return input;
        }
    }
}
