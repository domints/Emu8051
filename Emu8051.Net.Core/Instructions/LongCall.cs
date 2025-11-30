namespace Emu8051.Net.Core.Instructions
{
    public class LongCall : IInstruction
    {
        public byte OpCode => 0x12;

        public string Mnemonic => "LCALL";

        public string VariantMnemonic => "LCALL code_addr";

        public int Cycles => 2;

        public void Execute(Memory memory)
        {
            var originalPC = memory.ProgramCounter;
            memory.ProgramCounter += 3;
            memory.PushToStack((byte)(memory.ProgramCounter & 0xFF));
            memory.PushToStack((byte)((memory.ProgramCounter >> 8) & 0xFF));
            memory.ProgramCounter = (ushort)(memory.RomOffset(-2) << 8 | memory.RomOffset(-1));
            //memory.Core.ProcessCall(originalPC, memory.ProgramCounter);
        }

        public string ValueString(Memory memory)
        {
            return $"{Mnemonic} {(ushort)(memory.RomOffset(1) << 8 | memory.RomOffset(2)):X4}";
        }
    }
}