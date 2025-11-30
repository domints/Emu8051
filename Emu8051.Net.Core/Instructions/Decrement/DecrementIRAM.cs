namespace Emu8051.Net.Core.Instructions.Decrement
{
    public class DecrementIRAM : DecrementBase
    {
        public override byte OpCode => 0x15;

        public override string VariantMnemonic => $"{Mnemonic} iram_addr";

        public override void Execute(Memory memory)
        {
            var addr = memory.RomOffset(1);
            memory.DirectMemory[addr]--;
            memory.ProgramCounter += 2;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}