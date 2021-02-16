using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
   public static class MathUtils
    {
        public const double pre = 1.0e-5;
        public static bool IsEqual(this double _d1, double _d2)
        {
            return Math.Abs(_d1 - _d2) < pre;
        }
    }
}
