namespace Emu8051.Net.Core.Instructions.Move
{
    [BitVariant(1)]
    public class MoveIRAMAtR : MoveBase
    {
        private const byte _baseOpCode = 0x86;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} iram_addr,@R{_variantNumber}";

        public override int Cycles => 2;

        public MoveIRAMAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            memory.DirectMemory[memory.RomOffset(1)] = memory.RAM[targetRAMAddress];
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} iram {memory.RomOffset(1):X2} [{memory.DirectMemory[memory.RomOffset(1)]:X2}], @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}