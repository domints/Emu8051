namespace Emu8051.Net.Core.Instructions.Add
{
    [BitVariant(1)]
    public class AddAtR : AddBase
    {
        private const byte _baseOpCode = 0x26;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} A,@R{_variantNumber}";

        public AddAtR(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            var value = memory.RAM[targetRAMAddress];
            Add(value, false, memory);
            memory.ProgramCounter++;
        }

        public override string ValueString(Memory memory)
        {
            var targetRAMAddress = memory.GetBankRegisterValue(_variantNumber);
            return $"ADD A [0x{memory.Accumulator:X2}], @R{_variantNumber} [0x{targetRAMAddress:X2}; 0x{memory.RAM[targetRAMAddress]:X2}]";
        }
    }
}