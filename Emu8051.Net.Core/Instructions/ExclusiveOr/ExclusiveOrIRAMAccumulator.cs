namespace Emu8051.Net.Core.Instructions.ExclusiveOr
{
    public class ExclusiveOrIRAMAccumulator : ExclusiveOrBase
    {
        public override byte OpCode => 0x62;

        public override string VariantMnemonic => $"{Mnemonic} iram_addr,A";

        public override int Cycles => 1;

        public override void Execute(Memory memory)
        {
            var ramAddr = memory.RomOffset(1);
            var value = memory.Accumulator;

            memory.DirectMemory[ramAddr] ^= value;

            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}], A [{memory.Accumulator:X2}]";
        }
    }
}