namespace Emu8051.Net.Core.Instructions.Move
{
    [BitVariant(3)]
    public class MoveRegisterAccumulator : MoveBase
    {
        private const byte _baseOpCode = 0xF8;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} R{_variantNumber},A";

        public override int Cycles => 1;

        public MoveRegisterAccumulator(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            memory.SetBankRegisterValue(_variantNumber, memory.Accumulator);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} R{_variantNumber} [{registerValue:X2}], A [{memory.Accumulator:X2}]";
        }
    }
}