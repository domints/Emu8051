namespace Emu8051.Net.Core.Instructions.Move
{
    public class MoveAccumulatorIRAM : MoveBase
    {
        public override byte OpCode => 0xE5;
        public override string VariantMnemonic => $"{Mnemonic} A,iram_addr";

        public override int Cycles => 2;

        public override void Execute(Memory memory)
        {
            memory.Accumulator = memory.DirectMemory[memory.RomOffset(1)];
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}