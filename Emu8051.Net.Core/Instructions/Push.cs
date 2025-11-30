namespace Emu8051.Net.Core.Instructions
{
    public class Push : IInstruction
    {
        public byte OpCode => 0xC0;

        public string Mnemonic => "PUSH";

        public string VariantMnemonic => "PUSH iram_addr";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            var value = memory.DirectMemory[memory.RomOffset(1)];
            memory.PushToStack(value);

            memory.ProgramCounter += 2;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} IRAM [0x{memory.RomOffset(1):X2}; 0x{memory.DirectMemory[memory.RomOffset(1)]:X2}]";
        }
    }
}