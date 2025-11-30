namespace Emu8051.Net.Core.Instructions.Increment
{
    [BitVariant(3)]
    public class IncrementRegister : IncrementBase
    {
        private readonly byte _variantNumber;

        private const byte _baseOpCode = 0x08;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);

        public override string VariantMnemonic => $"{Mnemonic} R{_variantNumber}";

        public IncrementRegister(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var value = memory.GetBankRegisterValue(_variantNumber);
            value++;
            memory.SetBankRegisterValue(_variantNumber, value);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} R{_variantNumber} [{registerValue:X2}]";
        }
    }
}