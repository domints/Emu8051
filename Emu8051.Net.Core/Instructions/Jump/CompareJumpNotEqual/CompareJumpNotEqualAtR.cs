namespace Emu8051.Net.Core.Instructions.Jump.CompareJumpNotEqual
{
    [BitVariant(1)]
    public class CompareJumpNotEqualAtR : CompareJumpNotEqualBase
    {
        private const byte _baseOpCode = 0xB6;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);

        public override string VariantMnemonic => $"{Mnemonic} @R{_variantNumber},#value,rel_addr";

        public CompareJumpNotEqualAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var value = memory.RomOffset(1);
            var addressOffset = (sbyte)memory.RomOffset(2);
            memory.ProgramCounter += 3;
            var iramAddress = memory.GetBankRegisterValue(_variantNumber);
            if (memory.RAM[iramAddress] != value)
                memory.ProgramCounter = (ushort)(memory.ProgramCounter + addressOffset);

            memory.Carry = memory.RAM[iramAddress] < value;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}], #{memory.RomOffset(1):X2}, {(sbyte)memory.RomOffset(2)}";
        }
    }
}