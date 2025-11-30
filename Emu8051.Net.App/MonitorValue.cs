using Emu8051.Net.Core;

namespace Emu8051.Net.App
{
    public class MonitorValue
    {
        private IFormattable? lastValue;
        public string Name { get; set; } = string.Empty;
        public Func<Memory, IFormattable>? Getter { get; set; }
        public string? Format { get; set; } = "X2";
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public bool ValueUnder { get; set; }
        public (bool changed, IFormattable currentValue) GetLatestValue(Memory memory)
        {
            if (Getter == null)
                return (false, 0);

            var latest = Getter(memory);

            if (latest != lastValue)
            {
                lastValue = latest;
                return (true, lastValue);
            }

            return (false, lastValue);
        }
    }
}