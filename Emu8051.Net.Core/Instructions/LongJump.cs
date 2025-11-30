namespace Emu8051.Net.Core.Instructions
{
    public class LongJump : IInstruction
    {
        public byte OpCode => 0x02;

        public string Mnemonic => "LJMP";

        public int Cycles => 2;

        public string VariantMnemonic => "LJMP addr16";

        public void Execute(Memory memory)
        {
            var newPC = (memory.RomOffset(1) << 8) | memory.RomOffset(2);
            memory.ProgramCounter = (ushort)newPC;
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} {(ushort)(memory.RomOffset(1) << 8 | memory.RomOffset(2)):X4}";
        }
    }
}