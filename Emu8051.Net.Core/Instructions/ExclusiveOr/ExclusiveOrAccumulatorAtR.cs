namespace Emu8051.Net.Core.Instructions.ExclusiveOr
{
    [BitVariant(1)]
    public class ExclusiveOrAccumulatorAtR : ExclusiveOrBase
    {
        private readonly byte _variantNumber;

        private const byte _baseOpCode = 0x66;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);

        public override string VariantMnemonic => $"{Mnemonic} A,@R{_variantNumber}";

        public override int Cycles => 1;

        public ExclusiveOrAccumulatorAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            memory.Accumulator ^= memory.RAM[targetRAMAddress];

            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}