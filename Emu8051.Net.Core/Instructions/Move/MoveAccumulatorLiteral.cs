namespace Emu8051.Net.Core.Instructions.Move
{
    public class MoveAccumulatorLiteral : MoveBase
    {
        public override byte OpCode => 0x74;
        public override string VariantMnemonic => $"{Mnemonic} A,#value";

        public override int Cycles => 1;

        public override void Execute(Memory memory)
        {
            memory.Accumulator = memory.RomOffset(1);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], #{memory.RomOffset(1):X2}";
        }
    }
}