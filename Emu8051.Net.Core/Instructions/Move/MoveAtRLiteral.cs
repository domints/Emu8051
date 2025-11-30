namespace Emu8051.Net.Core.Instructions.Move
{
    [BitVariant(1)]
    public class MoveAtRLiteral : MoveBase
    {
        private const byte _baseOpCode = 0x76;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} @R{_variantNumber},#value";

        public override int Cycles => 1;

        public MoveAtRLiteral(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            memory.RAM[targetRAMAddress] = memory.RomOffset(1);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}], #{memory.RomOffset(1):X2}";
        }
    }
}