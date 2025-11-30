namespace Emu8051.Net.Core.Instructions.MoveExternal
{
    [BitVariant(1)]
    public class MoveExternalAccumulatorAtR : MoveExternalBase
    {
        private const byte _baseOpCode = 0xE2;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} A,@R{_variantNumber}";
        public override int Cycles => 2;

        public MoveExternalAccumulatorAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            StoreAccInExternalMemory(memory, targetRAMAddress);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}