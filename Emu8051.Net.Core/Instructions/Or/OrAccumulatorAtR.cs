namespace Emu8051.Net.Core.Instructions.Or
{
    [BitVariant(1)]
    public class OrAccumulatorAtR : OrBase
    {
        private const byte _baseOpCode = 0x46;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} A,@R{_variantNumber}";

        public override int Cycles => 1;

        public OrAccumulatorAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            memory.Accumulator = (byte)(memory.Accumulator | memory.RAM[targetRAMAddress]);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}