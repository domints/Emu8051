namespace Emu8051.Net.Core.Instructions.Move
{
    [BitVariant(1)]
    public class MoveAtRIRAM : MoveBase
    {
        private const byte _baseOpCode = 0xA6;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} @R{_variantNumber},iram_addr";

        public override int Cycles => 2;

        public MoveAtRIRAM(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            memory.RAM[targetRAMAddress] = memory.DirectMemory[memory.RomOffset(1)];
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}], iram {memory.RomOffset(1):X2} [{memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}