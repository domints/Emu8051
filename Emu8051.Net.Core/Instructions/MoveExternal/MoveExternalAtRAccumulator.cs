namespace Emu8051.Net.Core.Instructions.MoveExternal
{
    [BitVariant(1)]
    public class MoveExternalAtRAccumulator : MoveExternalBase
    {
        private const byte _baseOpCode = 0xF2;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} @R{_variantNumber},A";
        public override int Cycles => 2;

        public MoveExternalAtRAccumulator(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            ReadAccFromExternalMemory(memory, targetRAMAddress);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}], A [{memory.Accumulator:X2}]";
        }
    }
}