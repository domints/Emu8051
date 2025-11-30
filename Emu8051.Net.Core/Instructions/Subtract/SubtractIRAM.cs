namespace Emu8051.Net.Core.Instructions.Subtract
{
    public class SubtractIRAM : SubtractBase
    {
        public override byte OpCode => 0x95;

        public override string VariantMnemonic => $"{Mnemonic} A,iram_addr";

        public override void Execute(Memory memory)
        {
            var value = memory.DirectMemory[memory.RomOffset(1)];
            SubtractAndSetFlags(value, memory);
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} A [{memory.Accumulator:X2}], IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}