using System;

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

        public static Pair operator *(Pair a, Pair b) => new(a.A * b.A, a.B * b.B);

        public static Pair operator *(Pair a, int b) => new(a.A * b, a.B * b);

        public static Pair operator *(int a, Pair b) => b * a;

        public static bool operator ==(Pair a, Pair b) => a.A == b.A && a.B == b.B;

        public static bool operator !=(Pair a, Pair b) => !(a == b);

    }

    public static class PairExt
    {
        public static Pair Increment(this Pair a) => a * 2;
    }

    public class Line2D
    {
        public Pair A;
        public Pair B;

        public Line2D()
        {
            A = new();
            B = new();
        }

        public Line2D(Pair _a, Pair _b)
        {
            A = _a;
            B = _b;
        }

        public int GetLength() => Math.Abs(A.A - B.A + A.B - B.B);
    }

    public class RightAngled
    {
        private Line2D _a = new();
        private Line2D _b = new();

        public Line2D CathetA
        {
            get => _a;
            set
            {
                _a = value;

                if (_a.A != _b.A)
                    return;

                Hypotenuse = new(_a.B, _b.B);

                CalcSquare();
            }
        }
        public Line2D CathetB
        {
            get => _b;
            set
            {
                _b = value;

                if (_a.A != _b.A)
                    return;

                Hypotenuse = new(_a.B, _b.B);

                CalcSquare();
            }
        }
        public Line2D Hypotenuse { get; private set; }
        public double Square;

        private void CalcSquare()
        {
            int x = _a.GetLength();
            int y = _b.GetLength();
            int z = Hypotenuse.GetLength();

            int hPerim = (_a.GetLength() + _b.GetLength() + Hypotenuse.GetLength()) / 2;
            int a = hPerim - _a.GetLength();
            int b = hPerim - _b.GetLength();
            int c = hPerim - Hypotenuse.GetLength();
            Square = Math.Sqrt(hPerim * a * b  * c);
        }
    }

}
