using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_11
{
    public class Pair
    {
        public int A;
        public int B;
        
        public Pair()
        {
            A = 0; B = 0;
        }
        
        public Pair(int a, int b)
        {
            A = a; B = b;
        }

        public static Pair operator *(Pair a, Pair b) => new Pair(a.A * b.A, a.B * b.B);

        public static Pair operator *(Pair a, int b) => new Pair(a.A * b, a.B * b);

        public static Pair operator *(int a, Pair b) => b * a;

    }
}
