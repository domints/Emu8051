namespace Emu8051.Net.Core.Instructions.Move
{
    public class MoveIRAMLiteral : MoveBase
    {
        public override byte OpCode => 0x75;
        public override string VariantMnemonic => $"{Mnemonic} iram_addr,#value";

        public override int Cycles => 2;

        public override void Execute(Memory memory)
        {
            var ramAddr = memory.RomOffset(1);
            var value = memory.RomOffset(2);
            memory.DirectMemory[ramAddr] = value;
            memory.ProgramCounter += 3;
        }

        public override string ValueString(Memory memory)
        {
            return $"{Mnemonic} IRAM [{memory.RomOffset(1):X2}; {memory.DirectMemory[memory.RomOffset(1)]:X2}], #{memory.RomOffset(2):X2}";
        }
    }
}