using System;

namespace RustyUltimateLib
{
    public struct IntRange
    {
        public int Start;
        public int End;

        public IntRange(int start = int.MinValue, int end = int.MaxValue)
        {
            Start = start;
            End = end;
        }
    }
    public static class Lib
    {
        public static int ValidatedInput(string reading, IntRange range = new IntRange(), Func<string, string> onFail = null)
        {            
            if (int.TryParse(reading, out int n) && n >= range.Start && n <= range.End)
                return n;
            onFail($"Неверный ввод. Допустимые значения от {range.Start} до {range.End}");
            return 0;
        }
    }
}
