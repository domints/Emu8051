namespace Emu8051.Net.Core.Instructions.ExclusiveOr
{
    public class ExclusiveOrAccumulatorIRAM : ExclusiveOrBase
    {
        public override byte OpCode => 0x65;

        public override string VariantMnemonic => $"{Mnemonic} A,iram_addr";

        public override int Cycles => 1;

        public override void Execute(Memory memory)
        {
            var ramAddr = memory.RomOffset(1);

            memory.Accumulator ^= memory.DirectMemory[ramAddr];

            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}