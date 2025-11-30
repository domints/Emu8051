namespace Emu8051.Net.Core.Instructions.Add
{
    public class AddCarryLiteral : AddCarryBase
    {
        public override byte OpCode => 0x34;

        public override string VariantMnemonic => $"{Mnemonic} A,#value";

        public override void Execute(Memory memory)
        {
            var value = memory.RomOffset(1);
            Add(value, true, memory);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], #{memory.RomOffset(1):X2}";
        }
    }
}