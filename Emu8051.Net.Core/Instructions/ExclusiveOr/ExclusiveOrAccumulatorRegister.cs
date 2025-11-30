namespace Emu8051.Net.Core.Instructions.ExclusiveOr
{
    [BitVariant(3)]
    public class ExclusiveOrAccumulatorRegister : ExclusiveOrBase
    {
        private readonly byte _variantNumber;

        private const byte _baseOpCode = 0x68;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);

        public override string VariantMnemonic => $"{Mnemonic} A,R{_variantNumber}";

        public override int Cycles => 1;

        public ExclusiveOrAccumulatorRegister(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            memory.Accumulator ^= memory.GetBankRegisterValue(_variantNumber);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} A [{memory.Accumulator:X2}], R{_variantNumber} [{registerValue:X2}]";
        }
    }
}