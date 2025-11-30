namespace Emu8051.Net.Core.Instructions
{
    [BitVariant(1)]
    public class ExchangeDigitAccumulatorAtR : IInstruction
    {
        private const byte _baseOpCode = 0xD6;
        private readonly byte _variantNumber;
        public string Mnemonic => "XCHD";

        public byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public string VariantMnemonic => $"{Mnemonic} A,@R{_variantNumber}";

        public int Cycles => 1;


        public ExchangeDigitAccumulatorAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            var accNibble = memory.Accumulator & 0x0f;
            var regNibble = memory.RAM[targetRAMAddress] & 0x0f;
            var accVal = (memory.Accumulator & 0xf0) | regNibble;
            var regVal = (memory.RAM[targetRAMAddress] & 0xf0) | accNibble;
            memory.Accumulator = (byte)accVal;
            memory.RAM[targetRAMAddress] = (byte)regVal;
            memory.ProgramCounter++;
        }

        public string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}