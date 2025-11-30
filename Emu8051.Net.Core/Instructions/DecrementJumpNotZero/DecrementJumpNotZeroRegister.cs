namespace Emu8051.Net.Core.Instructions.DecrementJumpNotZero
{
    [BitVariant(3)]
    public class DecrementJumpNotZeroRegister : IInstruction
    {
        private int _variantNumber;

        private const byte _baseOpCode = 0xD8;

        public byte OpCode => (byte)(_baseOpCode + _variantNumber);

        public string Mnemonic => "DJNZ";

        public string VariantMnemonic => $"{Mnemonic} R{_variantNumber},rel_addr";

        public int Cycles => 2;

        public DecrementJumpNotZeroRegister(int variantNumber)
        {
            _variantNumber = variantNumber;
        }

        public void Execute(Memory memory)
        {
            var value = memory.GetBankRegisterValue(_variantNumber);
            value--;
            memory.SetBankRegisterValue(_variantNumber, value);
            if (value != 0)
            {
                memory.ProgramCounter = (ushort)(memory.ProgramCounter + 2 + (sbyte)memory.RomOffset(1));
            }
            else
            {
                memory.ProgramCounter += 2;
            }
        }

        public string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} R{_variantNumber} [{registerValue:X2}], {(sbyte)memory.RomOffset(1)}";
        }
    }
}