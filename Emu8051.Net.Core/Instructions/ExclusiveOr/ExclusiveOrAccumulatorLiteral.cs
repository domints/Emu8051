namespace Emu8051.Net.Core.Instructions.ExclusiveOr
{
    public class ExclusiveOrAccumulatorLiteral : ExclusiveOrBase
    {
        public override byte OpCode => 0x64;

        public override string VariantMnemonic => $"{Mnemonic} A,#value";

        public override int Cycles => 1;

        public override void Execute(Memory memory)
        {
            var value = memory.RomOffset(1);

            memory.Accumulator ^= value;

            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], #{memory.RomOffset(1):X2}";
        }
    }
}