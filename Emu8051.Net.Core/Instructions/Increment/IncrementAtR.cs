namespace Emu8051.Net.Core.Instructions.Increment
{
    [BitVariant(1)]
    public class IncrementAtR : IncrementBase
    {
        private readonly byte _variantNumber;

        private const byte _baseOpCode = 0x06;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);

        public override string VariantMnemonic => $"{Mnemonic} @R{_variantNumber}";

        public IncrementAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            memory.RAM[targetRAMAddress]++;
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}