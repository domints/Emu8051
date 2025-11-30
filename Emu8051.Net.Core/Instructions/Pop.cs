namespace Emu8051.Net.Core.Instructions
{
    public class Pop : IInstruction
    {
        public byte OpCode => 0xD0;

        public string Mnemonic => "POP";

        public string VariantMnemonic => "POP iram_addr";

        public int Cycles => 1;

        public void Execute(Memory memory)
        {
            memory.DirectMemory[memory.RomOffset(1)] = memory.PopFromStack();

            memory.ProgramCounter += 2;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} IRAM [0x{memory.RomOffset(1):X2}; 0x{memory.DirectMemory[memory.RomOffset(1)]:X2}] ST:0x{memory.PeekStack():X2}";
        }
    }
}