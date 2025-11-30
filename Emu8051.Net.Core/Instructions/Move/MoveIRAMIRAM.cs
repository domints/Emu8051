namespace Emu8051.Net.Core.Instructions.Move
{
    public class MoveIRAMIRAM : MoveBase
    {
        public override byte OpCode => 0x85;
        public override string VariantMnemonic => $"{Mnemonic} iram_addr,iram_addr";

        public override int Cycles => 2;

        public override void Execute(Memory memory)
        {
            memory.DirectMemory[memory.RomOffset(1)] = memory.RAM[memory.RomOffset(2)];
            memory.ProgramCounter += 3;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}], IRAM [{memory.RomOffset(2):X2}; {memory.DirectMemory[memory.RomOffset(2)]:X2}]";
        }
    }
}