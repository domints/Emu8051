namespace Emu8051.Net.Core.Instructions.Subtract
{
    [BitVariant(3)]
    public class SubtractRegister : SubtractBase
    {
        private const byte _baseOpCode = 0x98;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} A,R{_variantNumber}";

        public SubtractRegister(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var value = memory.GetBankRegisterValue(_variantNumber);
            SubtractAndSetFlags(value, memory);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], R{_variantNumber} [{registerValue:X2}]";
        }
    }
}