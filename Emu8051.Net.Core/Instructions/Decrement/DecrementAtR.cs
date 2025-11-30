namespace Emu8051.Net.Core.Instructions.Decrement
{
    [BitVariant(1)]
    public class DecrementAtR : DecrementBase
    {
        private readonly byte _variantNumber;

        private const byte _baseOpCode = 0x16;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);

        public override string VariantMnemonic => $"{Mnemonic} @R{_variantNumber}";

        public DecrementAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            memory.RAM[targetRAMAddress]--;
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"DEC @R{_variantNumber} [{targetRAMAddress:X2}; {memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}