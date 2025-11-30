namespace Emu8051.Net.Core
{
    public class DirectMemoryProxy
    {
        private byte[] _iram;
        private SpecialFunctionRegisters _sfrs;

        public DirectMemoryProxy(byte[] iram, SpecialFunctionRegisters sfrs)
        {
            _iram = iram;
            _sfrs = sfrs;
        }

        public byte this[int index] { get => index < 0x80 ? _iram[index] : _sfrs[index]; set => DirectStore((byte)index, value); }

        private void DirectStore(byte address, byte value)
        {
            if (address < 0x80)
            {
                _iram[address] = value;
            }
            else
            {
                _sfrs[address] = value;
            }
        }

        public int Count => 0xFF;
    }
}