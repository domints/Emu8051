namespace Emu8051.Net.Core.Instructions.Exchange
{
    [BitVariant(1)]
    public class ExchangeAtR : ExchangeBase
    {
        private const byte _baseOpCode = 0xC6;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} A,@R{_variantNumber}";

        public ExchangeAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            var accumulatorValue = memory.Accumulator;
            memory.Accumulator = memory.RAM[targetRAMAddress];
            memory.RAM[targetRAMAddress] = accumulatorValue;
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"XCH A [{memory.Accumulator:X2}], @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}