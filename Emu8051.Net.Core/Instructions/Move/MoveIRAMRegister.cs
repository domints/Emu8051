namespace Emu8051.Net.Core.Instructions.Move
{
    [BitVariant(3)]
    public class MoveIRAMRegister : MoveBase
    {
        private const byte _baseOpCode = 0x88;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} iram_addr,R{_variantNumber}";

        public override int Cycles => 2;

        public MoveIRAMRegister(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var iramAddress = memory.RomOffset(1);
            memory.DirectMemory[iramAddress] = memory.GetBankRegisterValue(_variantNumber);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} iram {memory.RomOffset(1)} [{memory.DirectMemory[memory.RomOffset(1)]:X2}], R{_variantNumber} [{registerValue:X2}]";
        }
    }
}