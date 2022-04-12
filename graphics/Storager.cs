using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace graphics
{
    internal class Storager
    {
        public string[] Read(StreamReader sr)
        {
            var s = sr.ReadLine().Split('/');
            return s;
        }

        public void Write(StreamWriter sw, double d)
        {
            sw.Write($"{d.ToString()} \n ");
        }

        public string[] GenerateInputs(double jSet)
        {
            string[] ret= new string[2];
            
            ret[0] = jSet.ToString();
            ret[1] = $"{jSet * jSet} {jSet * jSet} {jSet * jSet}";

            return ret;
        }
    }
}
