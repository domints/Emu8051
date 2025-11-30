namespace Emu8051.Net.Core.Instructions.Exchange
{
    [BitVariant(3)]
    public class ExchangeRegister : ExchangeBase
    {
        private const byte _baseOpCode = 0xC8;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} A,R{_variantNumber}";

        public ExchangeRegister(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var accumulatorValue = memory.Accumulator;
            memory.Accumulator = memory.GetBankRegisterValue(_variantNumber);
            memory.SetBankRegisterValue(_variantNumber, accumulatorValue);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], R{_variantNumber} [{registerValue:X2}]";
        }
    }
}