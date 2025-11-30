namespace Emu8051.Net.Core.Instructions
{
    public class ShortJump : IInstruction
    {
        public byte OpCode => 0x80;

        public string Mnemonic => "SJMP";

        public string VariantMnemonic => "SJMP rel_addr";

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            var offset = (sbyte)memory.RomOffset(1);
            var newPc = memory.ProgramCounter + 2 + offset;
            memory.ProgramCounter = (ushort)newPc;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} [0x{(sbyte)memory.RomOffset(1):X2}]";
        }
    }
}