namespace Emu8051.Net.Core.Instructions.Or
{
    public class OrIRAMAccumulator : OrBase
    {
        public override byte OpCode => 0x42;

        public override string VariantMnemonic => $"{Mnemonic} iram_addr,A";

        public override int Cycles => 1;

        public override void Execute(Memory memory)
        {
            memory.DirectMemory[memory.RomOffset(1)] = (byte)(memory.DirectMemory[memory.RomOffset(1)] | memory.Accumulator);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}], A [{memory.Accumulator:X2}]";
        }
    }
}