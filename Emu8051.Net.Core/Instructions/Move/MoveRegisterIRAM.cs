namespace Emu8051.Net.Core.Instructions.Move
{
    [BitVariant(3)]
    public class MoveRegisterIRAM : MoveBase
    {
        private const byte _baseOpCode = 0xA8;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} R{_variantNumber},iram_addr";

        public override int Cycles => 2;

        public MoveRegisterIRAM(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var iramAddress = memory.RomOffset(1);
            memory.SetBankRegisterValue(_variantNumber, memory.DirectMemory[iramAddress]);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            var iramAddr = memory.RomOffset(1);
            return $"{Mnemonic} R{_variantNumber} [{registerValue:X2}], iram:{iramAddr:X2} [{memory.DirectMemory[iramAddr]:X2}]";
        }
    }
}