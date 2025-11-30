namespace Emu8051.Net.Core.Instructions.Jump.CompareJumpNotEqual
{
    [BitVariant(3)]
    public class CompareJumpNotEqualRegister : CompareJumpNotEqualBase
    {
        private const byte _baseOpCode = 0xB8;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} R{_variantNumber},#value,rel_addr";

        public CompareJumpNotEqualRegister(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var value = memory.RomOffset(1);
            var addressOffset = (sbyte)memory.RomOffset(2);
            memory.ProgramCounter += 3;
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            if (registerValue != value)
                memory.ProgramCounter = (ushort)(memory.ProgramCounter + addressOffset);

            memory.Carry = registerValue < value;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} R{_variantNumber} [{registerValue:X2}], #{memory.RomOffset(1):X2}, {(sbyte)memory.RomOffset(2)}";
        }
    }
}