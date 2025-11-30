namespace Emu8051.Net.Core.Instructions.Move
{
    [BitVariant(3)]
    public class MoveRegisterLiteral : MoveBase
    {
        private const byte _baseOpCode = 0x78;
        private readonly byte _variantNumber;

        public override byte OpCode => (byte)(_baseOpCode + _variantNumber);
        public override string VariantMnemonic => $"{Mnemonic} R{_variantNumber},#value";

        public override int Cycles => 1;

        public MoveRegisterLiteral(int variantNumber)
        {
            _variantNumber = (byte)variantNumber;
        }

        public override void Execute(Memory memory)
        {
            memory.SetBankRegisterValue(_variantNumber, memory.RomOffset(1));
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            var registerValue = memory.GetBankRegisterValue(_variantNumber);
            return $"{Mnemonic} R{_variantNumber} [{registerValue:X2}], #{memory.RomOffset(1):X2}";
        }
    }
}